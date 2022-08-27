using Muflone.Core;

namespace BrewUpProduction.Modules.Produzione.Shared.CustomTypes;

public sealed class BatchId : DomainId
{
    public BatchId(Guid value) : base(value)
    {
    }
}