using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Dtos;
using BrewUpProduction.ReadModel.Abstracts;
using BrewUpProduction.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Concretes;

public sealed class ProductionOrchestrator : IProductionOrchestrator
{
    private readonly IServiceBus _serviceBus;
    private readonly IPersister _persister;
    private readonly ILogger _logger;

    public ProductionOrchestrator(IServiceBus serviceBus,
        IPersister persister,
        ILoggerFactory loggerFactory)
    {
        _serviceBus = serviceBus;
        _persister = persister;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task StartProductionAsync(PostProductionBeer postBrewBeer)
    {
        try
        {
            var command = new StartBeerProduction(
                new BatchId(Guid.NewGuid()),
                new BatchNumber(postBrewBeer.BatchNumber),
                new BeerId(postBrewBeer.BeerId),
                new BeerType(postBrewBeer.BeerType),
                new Quantity(postBrewBeer.Quantity),
                new ProductionStartTime(DateTime.UtcNow)
            );

            await _serviceBus.SendAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task CompleteProductionAsync(PostProductionBeer postBrewBeer)
    {
        try
        {
            var command = new CompleteBeerProduction(new BatchId(postBrewBeer.BatchId),
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