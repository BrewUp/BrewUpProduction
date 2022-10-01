using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUpProduction.Modules.Produzione.Shared.Commands;

public class CompleteBeerProduction : Command
{
    public readonly BatchId BatchId;
    public readonly BatchNumber BatchNumber;

    public readonly Quantity Quantity;
    public readonly ProductionCompleteTime ProductionCompleteTime;

    public CompleteBeerProduction(BatchId aggregateId, BatchNumber batchNumber, Quantity quantity,
        ProductionCompleteTime productionCompleteTime) : base(aggregateId)
    {
        BatchId = aggregateId;

        BatchNumber = batchNumber;

        ProductionCompleteTime = productionCompleteTime;
        Quantity = quantity;
    }
}