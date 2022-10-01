using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public class ProductionExceptionHappened : DomainEvent
{
    public readonly string Message;

    public ProductionExceptionHappened(BatchId aggregateId, string message) : base(aggregateId)
    {
        Message = message;
    }
}