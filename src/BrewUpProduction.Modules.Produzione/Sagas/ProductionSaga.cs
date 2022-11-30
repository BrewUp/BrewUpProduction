using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace BrewUpProduction.Modules.Produzione.Sagas;

public class ProductionSaga : Saga<ProductionSaga.ProductionSagaState>,
	ISagaStartedBy<AskAvailabilityToStore>,
	IDomainEventHandlerAsync<StoreAvailabilityVerified>,
	IDomainEventHandlerAsync<BeerProductionStarted>
{

	private CorrelationId _correlationId;
	public ProductionSaga(IServiceBus serviceBus, ISagaRepository sagaRepository) : base(serviceBus, sagaRepository)
	{
	}

	public async Task StartedBy(AskAvailabilityToStore command)
	{
		_correlationId = command.CorrelationId;

		await ServiceBus.SendAsync(command);
	}

	public async Task HandleAsync(StoreAvailabilityVerified @event, CancellationToken cancellationToken = new())
	{
		if (cancellationToken.IsCancellationRequested)
			cancellationToken.ThrowIfCancellationRequested();

		if (!@event.Headers.CorrelationId.Equals(_correlationId.Value))
			return;

		if (@event.QuantityAvailable.Value < @event.QuantityToProduce.Value)
		{
			var quantityToProduce = new Quantity(@event.QuantityToProduce.Value - @event.QuantityAvailable.Value);

			// Cercare BatchNumber e BeerType nel mio ReadModel

			var startBeerProduction = new StartBeerProduction(@event.BatchId, _correlationId, new BatchNumber("123"),
				@event.BeerId, new BeerType(""), quantityToProduce, new ProductionStartTime(DateTime.UtcNow));

			await ServiceBus.SendAsync(startBeerProduction, cancellationToken);
		}
		else
		{
			// Dipende dalla scelta
		}
	}

	public async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new())
	{
		throw new NotImplementedException();
	}

	public class ProductionSagaState
	{
		public CorrelationId CorrelationId { get; private set; }

		public void SaveCorrelationId(CorrelationId correlationId)
		{
			CorrelationId = correlationId;
		}
	}
}
