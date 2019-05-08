using System.Collections.Generic;
using Common;
using static System.Globalization.CultureInfo;

namespace CommandSide.Domain
{
    public sealed class AnalogValue : ValueObject<AnalogValue>
    {
        private readonly decimal _value;

        private AnalogValue(decimal value)
        {
            _value = value;
        }

        public static AnalogValue AnalogValueFrom(decimal value) =>
            new AnalogValue(value);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }

        public override string ToString() => _value.ToString(InvariantCulture);

        public static implicit operator decimal(AnalogValue analogValue) => analogValue._value;
    }
}