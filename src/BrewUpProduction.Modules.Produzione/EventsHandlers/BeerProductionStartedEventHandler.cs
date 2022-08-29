using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.Logging;

namespace BrewUpProduction.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedEventHandler : ProductionDomainEventHandler<BeerProductionStarted>
{
    private readonly IBeerService _beerService;
    private IProductionBroadcastService _productionBroadcastService;

    public BeerProductionStartedEventHandler(ILoggerFactory loggerFactory,
        IBeerService beerService,
        IProductionBroadcastService productionBroadcastService) : base(loggerFactory)
    {
        _beerService = beerService;
        _productionBroadcastService = productionBroadcastService;
    }

    public override async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _beerService.CreateBeerAsync(@event.BeerId, @event.BeerType, @event.BatchId,
                @event.ProductionStartTime);

            await _productionBroadcastService.PublishProductionOrderUpdatedAsync(@event.BatchId);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}