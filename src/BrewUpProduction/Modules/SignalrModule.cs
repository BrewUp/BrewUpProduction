using BrewUpProduction.Modules.Produzione.Endpoints;
using BrewUpProduction.Modules.Produzione.Hubs;

namespace BrewUpProduction.Modules;

public class SignalrModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR(o => o.EnableDetailedErrors = true)
            .AddJsonProtocol(options => {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("v1/signalr/", ProductionEndpoints.HandleGetSignalR)
            .WithTags("SignalR");

        endpoints.MapHub<ProductionHub>("/hubs/production");

        return endpoints;
    }
}