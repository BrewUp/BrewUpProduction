using System.Collections.Concurrent;
using Muflone.Persistence;
using Muflone.Saga.Persistence;

namespace BrewUpProduction.Modules.Produzione.Sagas;

public class InMemorySagaRepository : ISagaRepository, IDisposable
{
	private readonly ISerializer _serializer;
	internal static readonly ConcurrentDictionary<Guid, string> Data = new();

	public InMemorySagaRepository(ISerializer serializer)
	{
		this._serializer = serializer;
	}

	public async Task<TSagaState> GetById<TSagaState>(Guid correlationId) where TSagaState : class, new()
	{
		if (!Data.TryGetValue(correlationId, out var stateSerialized))
			return default;

		return await _serializer.DeserializeAsync<TSagaState>(stateSerialized).ConfigureAwait(false);
	}

	public async Task Save<TSagaState>(Guid id, TSagaState sagaState) where TSagaState : class, new()
	{
		var serializedData = await _serializer.SerializeAsync(sagaState);

		Data[id] = serializedData;
	}

	public Task Complete(Guid correlationId)
	{
		Data.TryRemove(correlationId, out _);
		return Task.CompletedTask;
	}

	#region Dispose
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			Data.Clear();
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~InMemorySagaRepository()
	{
		Dispose(false);
	}
	#endregion
}