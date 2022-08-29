using Muflone.Core;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public class ProductionExceptionHappened : DomainEvent
{
    public readonly string Message;

    public ProductionExceptionHappened(IDomainId aggregateId, string message) : base(aggregateId)
    {
        Message = message;
    }
}