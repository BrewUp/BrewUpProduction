using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public sealed class BeerProductionAdded : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BeerType BeerType;

    public readonly BatchId BatchId;
    public readonly BatchNumber BatchNumber;

    public readonly Quantity Quantity;
    public readonly ProductionStartTime ProductionStartTime;

    public BeerProductionAdded(BeerId aggregateId, BeerType beerType, BatchId batchId, BatchNumber batchNumber,
        Quantity quantity, ProductionStartTime productionStartTime) : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerType = beerType;

        BatchId = batchId;
        BatchNumber = batchNumber;

        Quantity = quantity;
        ProductionStartTime = productionStartTime;
    }
}