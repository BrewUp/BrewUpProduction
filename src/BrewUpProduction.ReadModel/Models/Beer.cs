using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Dtos;
using BrewUpProduction.ReadModel.Abstracts;

namespace BrewUpProduction.ReadModel.Models;

public class Beer : ModelBase
{
    public string BeerType { get; private set; } = string.Empty;
    public double Quantity { get; private set; } = 0;

    protected Beer()
    {}

    public static Beer CreateBeer(BeerId beerId, BeerType beerType, BatchId batchId,
        ProductionStartTime productionStartTime) =>
        new(beerId.Value, beerType.Value);

    public void UpdateQuantity(Quantity quantity) => Quantity = quantity.Value;

    private Beer(Guid beerId, string beerType)
    {
        Id = beerId.ToString();
        BeerType = beerType;
    }

    public BeerJson ToJson() => new()
    {
        BeerId = Id,
        BeerType = BeerType,
        Quantity = Quantity
    };
}