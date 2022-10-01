using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public sealed class BeerBottled : DomainEvent
{
    public readonly BatchId BatchId;
    public readonly BottleHalfLitre BottleHalfLitre;
    public readonly Quantity Quantity;

    public BeerBottled(BatchId aggregateId, BottleHalfLitre bottleHalfLitre, Quantity quantity) : base(aggregateId)
    {
        BatchId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
        Quantity = quantity;
    }
}