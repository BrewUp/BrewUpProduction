﻿using BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;
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
    
    private readonly BatchNumber _batchNumber = new("1234");
    
    private readonly BottleHalfLitre _bottleHalfLitre = new(50);
    private readonly Quantity _residualQuantity = new(5);
    private readonly Quantity _finalQuantity = new(30);
    private readonly BeerLabel _beerLabel = new("Label");

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerProductionCompleted(_beerId, _batchNumber, _finalQuantity,
            new ProductionCompleteTime(DateTime.UtcNow));
    }

    protected override BottlingBeer When()
    {
        return new BottlingBeer(_beerId, _bottleHalfLitre);
    }

    protected override ICommandHandlerAsync<BottlingBeer> OnHandler() =>
        new BottlingBeerCommandHandler(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerBottledV2(_beerId, _bottleHalfLitre, _residualQuantity, _beerLabel);
    }
}