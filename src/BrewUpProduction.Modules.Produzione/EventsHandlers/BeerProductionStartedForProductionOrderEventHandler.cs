using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.Logging;

namespace BrewUpProduction.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedForProductionOrderEventHandler : ProductionDomainEventHandler<BeerProductionStarted>
{
    private readonly IProductionService _productionService;
    private readonly IProductionBroadcastService _productionBroadcastService;

    public BeerProductionStartedForProductionOrderEventHandler(ILoggerFactory loggerFactory,
        IProductionBroadcastService productionBroadcastService,
        IProductionService productionService) : base(loggerFactory)
    {
        _productionService = productionService;
        _productionBroadcastService = productionBroadcastService;
    }

    public override async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _productionService.CreateProductionOrderAsync(@event.BatchId, @event.BatchNumber, @event.BeerId,
                @event.BeerType, @event.Quantity, @event.ProductionStartTime);

            await _productionBroadcastService.PublishProductionOrderUpdatedAsync(@event.BatchId);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}