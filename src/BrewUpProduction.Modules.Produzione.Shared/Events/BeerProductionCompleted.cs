using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public class BeerProductionCompleted : DomainEvent
{
    public readonly BatchId BatchId;
    public readonly BatchNumber BatchNumber;

    public readonly BeerId BeerId;

    public readonly Quantity Quantity;
    public readonly ProductionCompleteTime ProductionCompleteTime;

    public BeerProductionCompleted(BatchId aggregateId, BatchNumber batchNumber, BeerId beerId, Quantity quantity,
        ProductionCompleteTime productionCompleteTime) : base(aggregateId)
    {
        BatchId = aggregateId;
        BatchNumber = batchNumber;

        BeerId = beerId;

        ProductionCompleteTime = productionCompleteTime;
        Quantity = quantity;
    }
}