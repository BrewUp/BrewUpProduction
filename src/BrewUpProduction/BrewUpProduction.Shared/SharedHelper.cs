using BrewUpProduction.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;

namespace BrewUpProduction.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, EventStoreSettings eventStoreSettings)
    {
        services.AddMufloneEventStore(eventStoreSettings.ConnectionString);

        return services;
    }
}