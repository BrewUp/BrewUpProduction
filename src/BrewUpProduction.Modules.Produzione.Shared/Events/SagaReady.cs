using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public class SagaReady : DomainEvent
{
	public readonly BatchId BatchId;
	public readonly CorrelationId CorrelationId;

	public readonly BeerId BeerId;

	public readonly Quantity Quantity;

	public SagaReady(BatchId aggregateId, CorrelationId correlationId, BeerId beerId, 
		Quantity quantity) : base(aggregateId)
	{
		BatchId = aggregateId;
		CorrelationId = correlationId;

		BeerId = beerId;
		Quantity = quantity;
	}
}