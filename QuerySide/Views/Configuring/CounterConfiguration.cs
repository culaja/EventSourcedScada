using System.Collections.Generic;
using Common;

namespace QuerySide.Views.Configuring
{
    public sealed class CounterConfiguration : ValueObject<CounterConfiguration>
    {
        public int Id { get; }
        public string Name { get; set; }

        public CounterConfiguration(
            int id,
            string name)
        {
            Id = id;
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }
}