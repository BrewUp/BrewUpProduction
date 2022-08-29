using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Core;

namespace BrewUpProduction.Modules.Produzione.Domain.Entities;

public class ProductionOrder : Entity
{
    internal BatchNumber BatchNumber { get; } = new(string.Empty);

    private Quantity _quantityToBeProduced = new(0);
    private Quantity _quantityProduced = new(0);

    private ProductionStartTime _productionStartTime;
    private ProductionCompleteTime _productionCompleteTime;

    protected ProductionOrder()
    {}

    internal static ProductionOrder StartProduction(BatchId batchId, BatchNumber batchNumber, Quantity quantity,
        ProductionStartTime productionStartTime) => new(batchId, batchNumber, quantity, productionStartTime);

    private ProductionOrder(BatchId batchId, BatchNumber batchNumber, Quantity quantity,
        ProductionStartTime productionStartTime) : base(batchId)
    {
        BatchNumber = batchNumber;

        _quantityToBeProduced = quantity;
        _productionStartTime = productionStartTime;

        _productionCompleteTime = new ProductionCompleteTime(DateTime.MinValue);
    }

    internal void CompleteProduction(Quantity quantity, ProductionCompleteTime productionCompleteTime)
    {
        _quantityProduced = quantity;
        _productionCompleteTime = productionCompleteTime;
    }
}