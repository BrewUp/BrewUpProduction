using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public class BeerTypesSetEvent : DomainEvent
{
    public BeerType BeerType { get; }

    public BeerTypesSetEvent(BeerId aggregateId, BeerType beerType) :
        base(aggregateId)
    {
        BeerType = beerType;
    }
}