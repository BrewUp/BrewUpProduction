using BrewUpProduction.Modules.Produzione.Domain.Sagas;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUpProduction.Modules.Produzione.Domain.Consumers;

public class StartProductionSagaConsumer : CommandConsumerBase<StartProductionSaga>
{
	protected override ICommandHandlerAsync<StartProductionSaga> HandlerAsync { get; }

	public StartProductionSagaConsumer(ISagaRepository sagaRepository, IServiceBus serviceBus,
		AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
		ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
	{
		HandlerAsync = new ProductionSaga(serviceBus, sagaRepository);
	}
}