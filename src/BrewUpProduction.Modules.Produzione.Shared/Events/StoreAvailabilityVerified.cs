using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public sealed class StoreAvailabilityVerified : DomainEvent
{
	public readonly BatchId BatchId;
	public readonly CorrelationId CorrelationId;

	public readonly BeerId BeerId;

	public readonly Quantity QuantityToProduce;
	public readonly Quantity QuantityAvailable;

	public StoreAvailabilityVerified(BatchId aggregateId, Guid correlationId, BeerId beerId, Quantity quantityToProduce,
		Quantity quantityAvailable) : base(aggregateId, correlationId)
	{
		BatchId = aggregateId;
		CorrelationId = new CorrelationId(correlationId);

		BeerId = beerId;
		QuantityToProduce = quantityToProduce;
		QuantityAvailable = quantityAvailable;
	}
}