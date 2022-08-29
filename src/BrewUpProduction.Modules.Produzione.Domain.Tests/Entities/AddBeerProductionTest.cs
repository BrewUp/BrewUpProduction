using BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Domain.Tests.Entities;

public sealed class AddBeerProductionTest : CommandSpecification<AddBeerProduction>
{
    private readonly BatchId _firstBatchId = new(Guid.NewGuid());
    private readonly BatchNumber _firstBatchNumber = new("2022-125");

    private readonly BatchId _secondBatchId = new(Guid.NewGuid());
    private readonly BatchNumber _secondBatchNumber = new("2022-126");

    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly BeerType _beerType = new("IPA");

    private readonly Quantity _quantity = new(200);
    private readonly ProductionStartTime _productionStartTime = new(DateTime.UtcNow);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionStarted(_beerId, _beerType, _firstBatchId, _firstBatchNumber, _quantity,
            _productionStartTime);
    }

    protected override AddBeerProduction When() =>
        new(_beerId, _secondBatchId, _secondBatchNumber, _quantity, _productionStartTime);

    protected override ICommandHandlerAsync<AddBeerProduction> OnHandler() =>
        new AddBeerProductionCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerProductionAdded(_beerId, _beerType, _secondBatchId, _secondBatchNumber, _quantity,
            _productionStartTime);
    }
}