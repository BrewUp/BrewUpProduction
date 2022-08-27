using BrewUpProduction.ReadModel.MongoDb;
using BrewUpProduction.Shared.Configuration;

namespace BrewUpProduction.Modules;

public class ReadModelModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var mongoDbSettings = new MongoDbSettings();
        builder.Configuration.GetSection("BrewUp:MongoDbSettings").Bind(mongoDbSettings);
        builder.Services.AddMongoDb(mongoDbSettings);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}