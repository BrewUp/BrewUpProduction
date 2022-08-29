using BrewUpProduction.Modules.Produzione.Domain.Abstracts;
using BrewUpProduction.Modules.Produzione.Domain.Entities;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;

public sealed class AddBeerProductionCommandHandler : CommandHandlerAsync<AddBeerProduction>
{
    public AddBeerProductionCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(AddBeerProduction command, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var beer = await Repository.GetByIdAsync<Beer>(command.BeerId.Value);
            beer.AddProductionOrder(command.BeerId, command.BatchId, command.BatchNumber, command.Quantity,
                command.ProductionStartTime);

            await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(command.BeerId, ex);
        }
    }
}