using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Sagas;

public class SagaSerializer : ISerializer
{
	public Task<T> DeserializeAsync<T>(string serializedData, CancellationToken cancellationToken = new ()) where T : class
	{
		throw new NotImplementedException();
	}

	public Task<string> SerializeAsync<T>(T data, CancellationToken cancellationToken = new CancellationToken()) where T : class
	{
		throw new NotImplementedException();
	}
}