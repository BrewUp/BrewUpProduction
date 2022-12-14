using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Abstracts;

public abstract class ProductionDomainEventHandler<T> : IDomainEventHandlerAsync<T> where T : class, IDomainEvent
{
    protected ILogger Logger;

    protected ProductionDomainEventHandler(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }

    public abstract Task HandleAsync(T @event, CancellationToken cancellationToken = new());

    #region Dispose
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ProductionDomainEventHandler()
    {
        Dispose(false);
    }
    #endregion
}