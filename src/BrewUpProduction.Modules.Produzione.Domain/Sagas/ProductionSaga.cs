using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace BrewUpProduction.Modules.Produzione.Domain.Sagas;

public class ProductionSaga : Saga<ProductionSagaState>,
	ICommandHandlerAsync<StartProductionSaga>,
	IDomainEventHandlerAsync<StoreAvailability>,
	IDomainEventHandlerAsync<BeerProductionStarted>
{
	private CorrelationId _correlationId;

	public ProductionSaga(IServiceBus serviceBus, ISagaRepository sagaRepository):base(serviceBus, sagaRepository)
	{
		
	}

	public async Task HandleAsync(StartProductionSaga command, CancellationToken cancellationToken = new ())
	{
		if (cancellationToken.IsCancellationRequested)
			cancellationToken.ThrowIfCancellationRequested();

		try
		{
			_correlationId = new CorrelationId(Guid.NewGuid());
			var askAvailabilityToStore = new AskAvailabilityToStore(command.BatchId, _correlationId, command.BeerId,
				command.BeerType, command.Quantity);

			await ServiceBus.SendAsync(askAvailabilityToStore, cancellationToken);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task HandleAsync(StoreAvailability @event, CancellationToken cancellationToken = new())
	{
		if (cancellationToken.IsCancellationRequested)
			cancellationToken.ThrowIfCancellationRequested();

		if (!@event.Headers.CorrelationId.Equals(_correlationId.Value))
			return;


	}

	public async Task HandleAsync(BeerProductionStarted @event, CancellationToken cancellationToken = new ())
	{
		throw new NotImplementedException();
	}
}