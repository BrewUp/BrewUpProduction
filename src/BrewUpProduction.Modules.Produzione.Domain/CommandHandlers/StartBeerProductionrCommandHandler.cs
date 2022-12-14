using BrewUpProduction.Modules.Produzione.Domain.Abstracts;
using BrewUpProduction.Modules.Produzione.Domain.Entities;
using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
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

        /* Mi arriva il comando
         * Sono quindi alla porta di ingresso del domino
         * - devo validarlo (posso rifiutarlo)
         * - devo andare dall'aggregato Birra_i3d_Autunno e dirgli che ? partita la produzione
         * - 
         */
        try
        {
            var beer = Beer.StartBeerProduction(new BeerId(command.AggregateId.Value), command.BeerType,
                command.BatchId, command.BatchNumber, command.Quantity, command.ProductionStartTime);

            await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            CoreException.CreateAggregateException(new BeerId(command.AggregateId.Value), ex);
        }
    }
}