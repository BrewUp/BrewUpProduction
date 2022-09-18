using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.Logging;

namespace BrewUpProduction.Modules.Produzione.EventsHandlers;

public sealed class ProductionExceptionHappenedEventHandler : ProductionDomainEventHandler<ProductionExceptionHappened>
{
    public ProductionExceptionHappenedEventHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    public override Task HandleAsync(ProductionExceptionHappened @event, CancellationToken cancellationToken = new ())
    {
        return Task.CompletedTask;
    }
}