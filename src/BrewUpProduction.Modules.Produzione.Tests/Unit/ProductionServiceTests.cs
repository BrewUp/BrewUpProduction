using BrewUpProduction.Modules.Produzione.Shared.Commands;
using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Muflone.Persistence;

namespace BrewUpProduction.Modules.Produzione.Tests.Unit;

public class ProductionServiceTests
{
    [Fact]
    public void Can_Retrive_EventName()
    {
        var eventName = nameof(ProductionStarted).ToLower();

        Assert.Equal("productionstarted", eventName);
    }

    [Fact]
    public async Task Can_Serialize_And_Deserialize_A_Command()
    {
        var messageSerilizer = new Serializer();

        var command = new StartBeerProduction(
            new BeerId(Guid.NewGuid()),
            new BatchId(Guid.NewGuid()),
            new BatchNumber("1234"),
            new BeerType("IPA"),
            new Quantity(100),
            new ProductionStartTime(DateTime.UtcNow)
        );

        var commandSerialized = await messageSerilizer.SerializeAsync(command);
        var commandDeserialized = await messageSerilizer.DeserializeAsync<StartBeerProduction>(commandSerialized);

        Assert.Equal(command.BatchId, commandDeserialized.BatchId);
    }
}