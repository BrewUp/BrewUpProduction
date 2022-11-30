using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Factories;

public sealed class DomainEventHandlerFactoryAsync : IDomainEventHandlerFactoryAsync
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventHandlerFactoryAsync(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDomainEventHandlerAsync<T> CreateDomainEventHandlerAsync<T>() where T : class, IDomainEvent
    {
        return _serviceProvider.GetRequiredService<IDomainEventHandlerAsync<T>>();
    }

    public IEnumerable<IDomainEventHandlerAsync<T>> CreateDomainEventHandlersAsync<T>() where T : class, IDomainEvent
    {
        return _serviceProvider.GetServices<IDomainEventHandlerAsync<T>>();
    }
}