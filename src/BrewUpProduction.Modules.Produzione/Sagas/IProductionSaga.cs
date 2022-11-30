using BrewUpProduction.Modules.Produzione.Shared.Commands;

namespace BrewUpProduction.Modules.Produzione.Sagas;

public interface IProductionSaga
{
	Task StartedBy(StartProductionSaga command, CancellationToken cancellationToken = new());
}