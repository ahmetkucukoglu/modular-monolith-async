using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Order.Core.Events;

public record OrderCreated(Aggregates.Order Order) : IDomainEvent;