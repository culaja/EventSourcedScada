using System.Collections.Generic;
using Common;

namespace QuerySide.Views.AssigningCustomer
{
    public sealed class CounterId : Id
    {
        private readonly int _counterId;

        public CounterId(int counterId)
        {
            _counterId = counterId;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _counterId;
        }
    }
}