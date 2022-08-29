using System.Text.Json.Serialization;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpProduction.Modules.Produzione.Shared.Events;

public sealed class ProductionStarted : IntegrationEvent
{
    [JsonPropertyName("beerId")]
    public readonly BeerId BeerId;
    [JsonPropertyName("productionStartTime")]
    public readonly ProductionStartTime ProductionStartTime;
    [JsonPropertyName("quantity")]
    public readonly Quantity Quantity;
    [JsonPropertyName("batchId")]
    public readonly BatchId BatchId;

    public ProductionStarted(BeerId aggregateId, ProductionStartTime productionStartTime,
        Quantity quantity, BatchId batchId) : base(aggregateId)
    {
        BeerId = aggregateId;
        ProductionStartTime = productionStartTime;
        Quantity = quantity;
        BatchId = batchId;
    }
}