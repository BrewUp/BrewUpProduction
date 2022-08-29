using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUpProduction.Modules.Produzione.Shared.Commands;

public sealed class BottlingBeer : Command
{
    public readonly BeerId BeerId;
    public readonly BottleHalfLitre BottleHalfLitre;

    public BottlingBeer(BeerId aggregateId, BottleHalfLitre bottleHalfLitre) : base(aggregateId)
    {
        BeerId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
    }
}