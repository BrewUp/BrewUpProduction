using BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUpProduction.Modules.Produzione.Domain.Consumers;

public sealed class AddBeerProductionConsumer : CommandConsumerBase<AddBeerProduction>
{
    protected override ICommandHandlerAsync<AddBeerProduction> HandlerAsync { get; }

    public AddBeerProductionConsumer(IRepository repository,
        AzureServiceBusConfiguration azureServiceBusConfiguration,
        ILoggerFactory loggerFactory,
        ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        HandlerAsync = new AddBeerProductionCommandHandler(repository, loggerFactory);
    }
}