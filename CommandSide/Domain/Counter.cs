using System;
using Common;

namespace Domain
{
    public sealed class Counter : Entity
    {
        public string Name { get; }

        public Counter(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}