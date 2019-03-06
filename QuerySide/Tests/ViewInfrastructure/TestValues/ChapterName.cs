using System.Collections.Generic;
using Common;

namespace Tests.ViewInfrastructure.TestValues
{
    public sealed class ChapterName : Id
    {
        private readonly string _name;

        public ChapterName(string name)
        {
            _name = name;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }

        public override string ToString() => _name;

        public static implicit operator string(ChapterName name) => name._name;
    }
}