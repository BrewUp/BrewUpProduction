using BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Domain.Tests.Entities;

public class BottlingBeerCommandTest : CommandSpecification<BottlingBeer>
{
    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly BeerType _beerType = new("IPA");

    private readonly BatchId _batchId = new(Guid.NewGuid());
    private readonly BatchNumber _batchNumber = new("1234");
    
    private readonly BottleHalfLitre _bottleHalfLitre = new(50);

    private readonly Quantity _quantity = new(200);
    private readonly Quantity _residualQuantity = new(5);
    private readonly Quantity _finalQuantity = new(30);
    private readonly BeerLabel _beerLabel = new("Label");

    private readonly ProductionStartTime _productionStartTime = new(DateTime.UtcNow);
    private readonly ProductionCompleteTime _productionCompleteTime = new(DateTime.UtcNow);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionStarted(_batchId, _batchNumber, _beerId, _beerType, _quantity, _productionStartTime);
        yield return new BeerProductionCompleted(_batchId, _batchNumber, _beerId, _finalQuantity, _productionCompleteTime);
    }

    protected override BottlingBeer When() => new (_batchId, _bottleHalfLitre);

    protected override ICommandHandlerAsync<BottlingBeer> OnHandler() =>
        new BottlingBeerCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerBottledV2(_batchId, _bottleHalfLitre, _residualQuantity, _beerLabel);
    }
}