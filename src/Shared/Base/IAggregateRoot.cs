// Shared/Base/IAggregateRoot.cs
using System.Collections.Generic;
using Shared.Events;

namespace Shared.Base
{
    public interface IAggregateRoot<TId> : IEntity<TId>
        where TId : notnull
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        long DomainVersion { get; }
        void ClearDomainEvents();
    }
}