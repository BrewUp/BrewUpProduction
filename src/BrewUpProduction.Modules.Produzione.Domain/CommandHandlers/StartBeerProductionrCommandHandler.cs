using BrewUpProduction.Modules.Produzione.Domain.Abstracts;
using BrewUpProduction.Modules.Produzione.Domain.Entities;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Domain.CommandHandlers;

public class StartBeerProductionCommandHandler : CommandHandlerAsync<StartBeerProduction>
{
    public StartBeerProductionCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(StartBeerProduction command, CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();
        try
        {
            var order = Order.StartBeerProduction(command.BatchId, command.BatchNumber, command.BeerId,
                command.BeerType, command.Quantity, command.ProductionStartTime);

            await Repository.SaveAsync(order, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(command.BatchId, ex);
        }
    }
}