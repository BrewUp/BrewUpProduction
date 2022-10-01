using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using BrewUpProduction.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpProduction.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionCompletedEventHandler : ProductionDomainEventHandler<BeerProductionCompleted>
{
    private readonly IProductionService _productionService;
    private readonly IProductionBroadcastService _productionBroadcastService;

    public BeerProductionCompletedEventHandler(ILoggerFactory loggerFactory,
        IProductionService productionService,
        IProductionBroadcastService productionBroadcastService) : base(loggerFactory)
    {
        _productionService = productionService;
        _productionBroadcastService = productionBroadcastService;
    }

    public override async Task HandleAsync(BeerProductionCompleted @event, CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await _productionService.CompleteProductionOrderAsync(@event.BatchNumber, @event.Quantity,
                @event.ProductionCompleteTime);

            await _productionBroadcastService.PublishProductionOrderUpdatedAsync(new BatchId(@event.AggregateId.Value));
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}