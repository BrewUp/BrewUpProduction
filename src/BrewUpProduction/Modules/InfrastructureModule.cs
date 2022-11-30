using BrewUpProduction.Modules.Produzione;
using BrewUpProduction.Modules.Produzione.Consumers.DomainEvents;
using BrewUpProduction.Modules.Produzione.Domain.Consumers;
using BrewUpProduction.Modules.Produzione.Sagas;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using BrewUpProduction.ReadModel.MongoDb;
using BrewUpProduction.Shared;
using BrewUpProduction.Shared.Configuration;
using Muflone.Factories;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.Azure;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;

namespace BrewUpProduction.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 98;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProductionModule();

        builder.Services.AddEventStore(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>());
        builder.Services.AddScoped<ISagaRepository, InMemorySagaRepository>();
        builder.Services.AddScoped<ISerializer, SagaSerializer>();

        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var domainEventHandlerFactoryAsync = serviceProvider.GetRequiredService<IDomainEventHandlerFactoryAsync>();
        var repository = serviceProvider.GetRequiredService<IRepository>();

        var clientId = builder.Configuration["BrewUp:ClientId"];
        var serviceBusConnectionString = builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"];
        var azureBusConfiguration =
            new AzureServiceBusConfiguration(serviceBusConnectionString, nameof(StartBeerProduction), clientId);

        var consumers = new List<IConsumer>
        {
            new StartBeerProductionConsumer(repository, azureBusConfiguration with { TopicName = nameof(StartBeerProduction) }, loggerFactory),
            new BeerProductionStartedConsumer(domainEventHandlerFactoryAsync, azureBusConfiguration with { TopicName = nameof(BeerProductionStarted) }, loggerFactory),

            new CompleteBeerProductionConsumer(repository, azureBusConfiguration with { TopicName = nameof(CompleteBeerProduction) }, loggerFactory),
            new BeerProductionCompletedConsumer(domainEventHandlerFactoryAsync, azureBusConfiguration with { TopicName = nameof(BeerProductionCompleted) }, loggerFactory),

            new ProductionExceptionHappenedConsumer(domainEventHandlerFactoryAsync, azureBusConfiguration with { TopicName = nameof(ProductionExceptionHappened) }, loggerFactory)
        };
        builder.Services.AddMufloneTransportAzure(
            new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], "",
                clientId), consumers);

        builder.Services.AddScoped<ProductionSaga>();

        var mongoDbSettings = new MongoDbSettings();
        builder.Configuration.GetSection("BrewUp:MongoDbSettings").Bind(mongoDbSettings);
        builder.Services.AddEventstoreMongoDb(mongoDbSettings);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}