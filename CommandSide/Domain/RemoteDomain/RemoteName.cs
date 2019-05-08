using System.Collections.Generic;
using Common;

namespace CommandSide.Domain.RemoteDomain
{
    public sealed class RemoteName : ValueObject<RemoteName>
    {
        private readonly string _name;

        private RemoteName(string name)
        {
            _name = name;
        }

        public static RemoteName RemoteNameFrom(Maybe<string> remoteName)
        {
            return new RemoteName(remoteName.Value);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }

        public override string ToString() => _name;

        public static implicit operator string(RemoteName remoteName) => remoteName._name;
    }
}