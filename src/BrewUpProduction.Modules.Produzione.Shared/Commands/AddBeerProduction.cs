using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUpProduction.Modules.Produzione.Shared.Commands;

public sealed class AddBeerProduction : Command
{
    public readonly BeerId BeerId;

    public readonly BatchId BatchId;
    public readonly BatchNumber BatchNumber;

    public readonly Quantity Quantity;
    public readonly ProductionStartTime ProductionStartTime;

    public AddBeerProduction(BeerId aggregateId, BatchId batchId, BatchNumber batchNumber,
        Quantity quantity, ProductionStartTime productionStartTime) : base(aggregateId)
    {
        BeerId = aggregateId;

        BatchId = batchId;
        BatchNumber = batchNumber;
        
        Quantity = quantity;
        ProductionStartTime = productionStartTime;
    }
}