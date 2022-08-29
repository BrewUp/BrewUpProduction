using BrewUpProduction.Modules.Produzione.Shared.Validators;
using BrewUpProduction.Shared.Concretes;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpProduction.Modules.Produzione.Shared;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
        services.AddScoped<ValidationHandler>();
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<ProductionBeerValidator>());

        return services;
    }
}