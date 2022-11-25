using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Hubs;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BrewUpProduction.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionStartedForProductionOrderEventHandler : ProductionDomainEventHandler<BeerProductionStarted>
{
    private readonly IProductionService _productionService;
    private readonly IProductionBroadcastService _productionBroadcastService;
    private readonly IHubContext<ProductionHub> _hubContext;

    public BeerProductionStartedForProductionOrderEventHandler(ILoggerFactory loggerFactory,
        IProductionService productionService,
        IProductionBroadcastService productionBroadcastService,
        IHubContext<ProductionHub> hubContext) : base(loggerFactory)
    {
        _productionService = productionService;
        _productionBroadcastService = productionBroadcastService;
        _hubContext = hubContext;
    }

    public override async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await _productionService.CreateProductionOrderAsync(@event.BatchId, @event.BatchNumber, @event.BeerId,
                @event.BeerType, @event.Quantity, @event.ProductionStartTime);

            await _hubContext.Clients.All.SendAsync("beerProductionStarted", @event.BatchId, cancellationToken: cancellationToken);
            //await _productionBroadcastService.PublishProductionOrderUpdatedAsync(@event.BatchId);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred processing event {@event.MessageId}. Message: {ex.Message}");
            throw;
        }
    }
}