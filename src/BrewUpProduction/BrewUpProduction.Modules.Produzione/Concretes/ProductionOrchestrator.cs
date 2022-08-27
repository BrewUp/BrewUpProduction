using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Dtos;
using BrewUpProduction.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Concretes;

public sealed class ProductionOrchestrator : IProductionOrchestrator
{
    private readonly IServiceBus _serviceBus;
    private readonly ILogger _logger;

    public ProductionOrchestrator(IServiceBus serviceBus,
        ILoggerFactory loggerFactory)
    {
        _serviceBus = serviceBus;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task StartProductionAsync(PostProductionBeer postBrewBeer)
    {
        var command = new StartBeerProduction(
            new BeerId(postBrewBeer.BeerId),
            new BatchId(Guid.NewGuid()),
            new BatchNumber(postBrewBeer.BatchNumber),
            new BeerType(postBrewBeer.BeerType),
            new Quantity(postBrewBeer.Quantity),
            new ProductionStartTime(DateTime.UtcNow)
        );

        await _serviceBus.SendAsync(command);
    }

    public async Task CompleteProductionAsync(PostProductionBeer postBrewBeer)
    {
        try
        {
            var command = new CompleteBeerProduction(new BeerId(postBrewBeer.BeerId),
                new BatchNumber(postBrewBeer.BatchNumber),
                new Quantity(postBrewBeer.Quantity),
                new ProductionCompleteTime(postBrewBeer.ProductionTime));

            await _serviceBus.SendAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}