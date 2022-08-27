using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using BrewUpProduction.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpProduction.Modules.Produzione.EventsHandlers;

public sealed class BeerProductionCompletedEventHandler : ProductionDomainEventHandler<BeerProductionCompleted>
{
    private readonly IBeerService _beerService;

    public BeerProductionCompletedEventHandler(ILoggerFactory loggerFactory,
        IBeerService beerService) : base(loggerFactory)
    {
        _beerService = beerService;
    }

    public override async Task HandleAsync(BeerProductionCompleted @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _beerService.UpdateBeerQuantityAsync(@event.BeerId, @event.Quantity);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}