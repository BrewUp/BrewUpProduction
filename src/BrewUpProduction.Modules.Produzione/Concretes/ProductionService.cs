using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Dtos;
using BrewUpProduction.ReadModel.Abstracts;
using BrewUpProduction.ReadModel.Models;
using BrewUpProduction.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpProduction.Modules.Produzione.Concretes;

public sealed class ProductionService : ProductionBaseService, IProductionService
{
    public ProductionService(ILoggerFactory loggerFactory, IPersister persister)
        : base(persister, loggerFactory)
    {
    }

    public async Task CreateProductionOrderAsync(BatchId batchId, BatchNumber batchNumber, BeerId beerId,
        BeerType beerType, Quantity quantity, ProductionStartTime productionStartTime)
    {
        try
        {
            var productionOrder =
                ProductionOrder.CreateProductionOrder(batchId, batchNumber, beerId, beerType, quantity,
                    productionStartTime);
            await Persister.InsertAsync(productionOrder);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task CompleteProductionOrderAsync(BatchNumber batchNumber, Quantity quantity,
        ProductionCompleteTime productionCompleteTime)
    {
        try
        {
            var productionOrder = await Persister.GetByIdAsync<ProductionOrder>(batchNumber.Value);

            if (string.IsNullOrEmpty(productionOrder.Id))
                return;

            productionOrder.CompleteProduction(quantity, productionCompleteTime);
            var propertiesToUpdate = new Dictionary<string, object>
            {
                { "ProductionCompleteTime", productionOrder.ProductionCompleteTime },
                { "QuantityProduced", productionOrder.QuantityProduced },
                { "Status", productionOrder.Status }
            };

            await Persister.UpdateOneAsync<ProductionOrder>(productionOrder.Id, propertiesToUpdate);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IEnumerable<ProductionOrderJson>> GetProductionOrdersAsync()
    {
        try
        {
            var productionOrders = await Persister.FindAsync<ProductionOrder>();
            var ordersArray = productionOrders as ProductionOrder[] ??
                              productionOrders.OrderByDescending(p => p.Id).ToArray();

            return ordersArray.Any()
                ? ordersArray.Select(p => p.ToJson())
                : Enumerable.Empty<ProductionOrderJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}