using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;

namespace BrewUpProduction.Modules.Produzione.Abstracts;

public interface IProductionBroadcastService
{
    Task PublishProductionOrderUpdatedAsync(BatchId batchId);
}