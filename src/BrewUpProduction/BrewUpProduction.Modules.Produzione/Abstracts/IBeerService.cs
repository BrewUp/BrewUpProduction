using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Dtos;

namespace BrewUpProduction.Modules.Produzione.Abstracts;

public interface IBeerService
{
    Task CreateBeerAsync(BeerId beerId, BeerType beerType, BatchId batchId, ProductionStartTime productionStartTime);
    Task UpdateBeerQuantityAsync(BeerId beerId, Quantity quantity);

    Task<IEnumerable<BeerJson>> GetBeersAsync();
}