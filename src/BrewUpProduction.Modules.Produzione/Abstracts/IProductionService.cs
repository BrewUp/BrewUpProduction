using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Dtos;

namespace BrewUpProduction.Modules.Produzione.Abstracts;

public interface IProductionService
{
    Task CreateProductionOrderAsync(BatchId batchId, BatchNumber batchNumber, BeerId beerId, BeerType beerType,
        Quantity quantity, ProductionStartTime productionStartTime);
    Task CompleteProductionOrderAsync(BatchNumber batchNumber, Quantity quantity,
        ProductionCompleteTime productionCompleteTime);

    Task<IEnumerable<ProductionOrderJson>> GetProductionOrdersAsync();
}