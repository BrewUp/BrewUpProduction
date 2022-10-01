using BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Domain.Tests.Entities;

public class BottlingBeerCommandWithExceptionTest : CommandSpecification<BottlingBeer>
{
    private readonly BatchId _batchId = new(Guid.NewGuid());
    private readonly BatchNumber _batchNumber = new("1234");

    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly BeerType _beerType = new ("Pilsner");

    private readonly Quantity _initialQuantity = new(30);
    private readonly Quantity _residualQuantity = new(5);

    private readonly BottleHalfLitre _bottleHalfLitre = new(50);

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionStarted(_batchId, _batchNumber, _beerId, _beerType, _initialQuantity,
            new ProductionStartTime(DateTime.UtcNow));
        yield return new BeerProductionCompleted(_batchId, _batchNumber, _initialQuantity,
            new ProductionCompleteTime(DateTime.UtcNow));
        yield return new BeerBottled(_batchId, _bottleHalfLitre, _residualQuantity);
    }

    protected override BottlingBeer When()
    {
        return new BottlingBeer(_batchId, _bottleHalfLitre);
    }

    protected override ICommandHandlerAsync<BottlingBeer> OnHandler() =>
        new BottlingBeerCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new ProductionExceptionHappened(_batchId, "Non hai abbastanza birra!!!!");
    }
}