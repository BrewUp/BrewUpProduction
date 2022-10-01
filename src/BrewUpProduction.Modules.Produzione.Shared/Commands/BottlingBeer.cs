using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUpProduction.Modules.Produzione.Shared.Commands;

public sealed class BottlingBeer : Command
{
    public readonly BatchId BatchId;
    public readonly BottleHalfLitre BottleHalfLitre;

    public BottlingBeer(BatchId aggregateId, BottleHalfLitre bottleHalfLitre) : base(aggregateId)
    {
        BatchId = aggregateId;
        BottleHalfLitre = bottleHalfLitre;
    }
}