using System;
using Common;
using Domain;
using Shared.CustomerQueue;

namespace Ports.Repositories
{
    public interface ICustomerQueueRepository : IRepository<CustomerQueue, CustomerQueueCreated>
    {
        Result<CustomerQueue> BorrowSingle(Func<CustomerQueue, Result<CustomerQueue>> transformer);
    }
}