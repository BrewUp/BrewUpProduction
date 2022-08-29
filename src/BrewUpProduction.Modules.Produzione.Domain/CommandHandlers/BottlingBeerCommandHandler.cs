using BrewUpProduction.Modules.Produzione.Domain.Abstracts;
using BrewUpProduction.Modules.Produzione.Domain.Entities;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;

public sealed class BottlingBeerCommandHandler : CommandHandlerAsync<BottlingBeer>
{
    public BottlingBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(BottlingBeer command, CancellationToken cancellationToken = new())
    {
        try
        {
            var beer = await Repository.GetByIdAsync<Beer>(command.BeerId.Value);
            beer.BottlingBeer(command.BeerId, command.BottleHalfLitre);

            await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(command.BeerId, ex);
        }
    }
}