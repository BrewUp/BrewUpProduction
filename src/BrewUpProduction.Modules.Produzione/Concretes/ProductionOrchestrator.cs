using BrewUpProduction.Modules.Produzione.Abstracts;
using BrewUpProduction.Modules.Produzione.Sagas;
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
    private readonly ProductionSaga _productionSaga;

    public ProductionOrchestrator(IServiceBus serviceBus,
        ILoggerFactory loggerFactory, ProductionSaga productionSaga)
    {
        _serviceBus = serviceBus;
        _productionSaga = productionSaga;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task StartProductionAsync(PostProductionBeer postBrewBeer)
    {
        try
        {
            var command = new AskAvailabilityToStore(
                new BatchId(Guid.NewGuid()),
                new CorrelationId(Guid.NewGuid()),
                new BeerId(postBrewBeer.BeerId),
                new BeerType(postBrewBeer.BeerType),
                new Quantity(postBrewBeer.Quantity)
            );

            await _productionSaga.StartedBy(command);
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