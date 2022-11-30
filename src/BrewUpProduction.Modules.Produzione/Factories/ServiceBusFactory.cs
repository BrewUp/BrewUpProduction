using Microsoft.Extensions.DependencyInjection;
using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Factories;

public class ServiceBusFactory
{
	private readonly IServiceProvider _serviceProvider;

	public ServiceBusFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public IServiceBus CreateServiceBus() =>
		_serviceProvider.GetRequiredService<IServiceBus>();
}