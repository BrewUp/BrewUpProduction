using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUpProduction.Modules.Produzione.Shared.Commands;

public sealed class AskAvailabilityToStore : Command
{
	public readonly BatchId BatchId;
	public readonly CorrelationId CorrelationId;

	public readonly BeerId BeerId;
	public readonly BeerType BeerType;

	public readonly Quantity Quantity;

	public AskAvailabilityToStore(BatchId aggregateId, CorrelationId correlationId, BeerId beerId, BeerType beerType,
		Quantity quantity) : base(aggregateId)
	{
		BatchId = aggregateId;
		CorrelationId = correlationId;

		BeerId = beerId;
		BeerType = beerType;
		Quantity = quantity;
	}
}