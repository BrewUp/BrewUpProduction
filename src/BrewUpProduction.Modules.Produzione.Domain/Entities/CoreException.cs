﻿using BrewUpProduction.Modules.Produzione.Shared.CustomTypes;
using BrewUpProduction.Modules.Produzione.Shared.Events;
using Muflone.Core;

namespace BrewUpProduction.Modules.Produzione.Domain.Entities;

public class CoreException : AggregateRoot
{
    private string _message = string.Empty;

    protected CoreException()
    {
    }

    internal static CoreException CreateAggregateException(BeerId aggregateId, Exception ex)
    {
        return new CoreException(aggregateId, ex);
    }

    private CoreException(BeerId aggregateId, Exception ex)
    {
        RaiseEvent(new ProductionExceptionHappened(aggregateId,
            $"StackTrace: {ex.StackTrace} - Source: {ex.Source} - Message: {ex.Message}"));
    }

    private void Apply(ProductionExceptionHappened @event)
    {
        Id = @event.AggregateId;

        _message = @event.Message;
    }
}