namespace BrewUpProduction.Modules
{
    public sealed class CorsModule : IModule
    {
        public bool IsEnabled => true;
        public int Order => 0;

        public IServiceCollection RegisterModule(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("CorsPolicy", corsBuilder =>
                //    corsBuilder.WithOrigins("https://localhost:7283/")
                //        .AllowAnyMethod()
                //        .AllowAnyHeader()
                //        .AllowCredentials());

                options.AddPolicy("CorsPolicy", corsBuilder =>
                    corsBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return builder.Services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }
    }
}