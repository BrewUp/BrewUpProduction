using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public sealed class StoreAvailabilityVerifiedV2 : DomainEvent
{
	public readonly BatchId BatchId;
	public readonly BatchNumber BatchNumber;
	public readonly CorrelationId CorrelationId;

	public readonly BeerId BeerId;
	public readonly BeerType BeerType;

	public readonly Quantity QuantityToProduce;
	public readonly Quantity QuantityAvailable;

	public StoreAvailabilityVerifiedV2(BatchId aggregateId, BatchNumber batchNumber, Guid correlationId, BeerId beerId,
		BeerType beerType, Quantity quantityToProduce, Quantity quantityAvailable) : base(aggregateId, correlationId)
	{
		BatchId = aggregateId;
		BatchNumber = batchNumber;
		CorrelationId = new CorrelationId(correlationId);

		BeerId = beerId;
		BeerType = beerType;
		QuantityToProduce = quantityToProduce;
		QuantityAvailable = quantityAvailable;
	}
}