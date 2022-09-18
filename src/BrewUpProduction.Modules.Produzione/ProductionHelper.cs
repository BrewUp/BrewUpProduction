using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Concretes;
using BrewUpProduction.Modules.Produzione.EventsHandlers;
using BrewUpProduction.Modules.Produzione.Factories;
using BrewUpProduction.Modules.Produzione.Hubs;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione;

public static class ProductionHelper
{
    public static IServiceCollection AddProductionModule(this IServiceCollection services)
    {
        services.AddScoped<IProductionOrchestrator, ProductionOrchestrator>();
        services.AddSingleton<IProductionBroadcastService, ProductionBroadcastService>();

        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<IBeerService, BeerService>();

        services.AddSingleton<ProductionHub>();

        #region DomainEventHandler
        services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactoryAsync>();
        services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();

        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionStarted>, BeerProductionStartedEventHandler>();
        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionStarted>, BeerProductionStartedForProductionOrderEventHandler>();

        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionCompleted>, BeerProductionCompletedEventHandler>();
        services
            .AddScoped<IDomainEventHandlerAsync<BeerProductionCompleted>, BeerProductionCompletedForProductionOrderEventHandler>();

        services.AddScoped<IDomainEventHandlerAsync<BeerProductionAdded>, BeerProductionAddedEventHandler>();

        services.AddScoped<IDomainEventHandlerAsync<ProductionExceptionHappened>, ProductionExceptionHappenedEventHandler>();
        #endregion

        return services;
    }
}