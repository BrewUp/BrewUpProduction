using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Hubs;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;

namespace BrewUpProduction.Modules.Produzione.Concretes;

public sealed class ProductionBroadcastService : IProductionBroadcastService
{
    private readonly ProductionHub _productionHub;

    public ProductionBroadcastService(ProductionHub productionHub)
    {
        _productionHub = productionHub;
    }

    public async Task PublishProductionOrderUpdatedAsync(BatchId batchId)
    {
        await _productionHub.ProductionOrderStartedUpdatedAsync(batchId);
    }
}