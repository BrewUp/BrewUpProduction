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
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var order = await Repository.GetByIdAsync<Order>(command.BatchId.Value);
            order.BottlingBeer(command.BottleHalfLitre);

            await Repository.SaveAsync(order, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(command.BatchId, ex);
        }
    }
}