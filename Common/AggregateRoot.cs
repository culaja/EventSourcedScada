using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Messaging;
using static System.DateTime;
using static Common.Nothing;

namespace Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }
        public ulong Version { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        protected AggregateRoot(Guid id)
        {
            Id = id;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public Nothing ApplyFrom(IDomainEvent e)
        {
            ApplyChange(e, false);
            return NotAtAll;
        }

        protected Nothing ApplyChange(IDomainEvent e)
        {
            ApplyChange(e, true);
            return NotAtAll;
        }

        private void ApplyChange(IDomainEvent e, bool isNew)
        {
            var applyMethodInfo = GetType().GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {e.GetType()}, null);

            if (applyMethodInfo == null)
            {
                throw new InvalidOperationException($"Aggregate '{GetType().Name}' can't apply '{e.GetType().Name}' event type.");
            }

            applyMethodInfo.Invoke(this, new object[] {e});

            IncrementedVersion();
            if (isNew)
            {
                e.SetVersion(Version);
                e.SetTimestamp(UtcNow);
                _domainEvents.Add(e);
            }
        }

        private void IncrementedVersion() => ++Version;
    }
}