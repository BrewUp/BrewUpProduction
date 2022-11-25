using BrewUpProduction.Modules.Produzione.Shared.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpProduction.Modules.Produzione.Shared;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
	    services.AddFluentValidationAutoValidation();
	    services.AddValidatorsFromAssemblyContaining<ProductionBeerValidator>();

        return services;
    }
}