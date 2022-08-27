using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public sealed class BeerBottledV2 : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BottleHalfLitre BottleHalfLitre;
    public readonly Quantity Quantity;
    public readonly BeerLabel BeerLabel;

    public BeerBottledV2(BeerId aggregateId, BottleHalfLitre bottleHalfLitre,
        Quantity quantity, BeerLabel beerLabel) : base(aggregateId)
    {
        BeerId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
        Quantity = quantity;
        BeerLabel = beerLabel;
    }
}