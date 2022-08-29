using BrewUpProduction.Modules.Produzione.Shared.Dtos;

namespace BrewUpProduction.Modules.Produzione.Abstracts;

public interface IProductionOrchestrator
{
    Task StartProductionAsync(PostProductionBeer postBrewBeer);
    Task CompleteProductionAsync(PostProductionBeer postBrewBeer);
}