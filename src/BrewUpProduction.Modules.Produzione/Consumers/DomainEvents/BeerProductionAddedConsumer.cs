using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.Logging;
using Muflone.Factories;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUpProduction.Modules.Produzione.Consumers.DomainEvents;

public sealed class BeerProductionAddedConsumer : DomainEventConsumerBase<BeerProductionAdded>
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerProductionAdded>> HandlersAsync { get; }
    public BeerProductionAddedConsumer(IDomainEventHandlerFactoryAsync domainEventHandlerFactoryAsync,
        AzureServiceBusConfiguration azureServiceBusConfiguration,
        ILoggerFactory loggerFactory,
        ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        HandlersAsync = domainEventHandlerFactoryAsync.CreateDomainEventHandlersAsync<BeerProductionAdded>();
    }
}