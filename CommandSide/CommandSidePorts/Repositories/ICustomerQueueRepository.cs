using System;
using CommandSide.Domain;
using Common;
using Shared.CustomerQueue;

namespace CommandSide.CommandSidePorts.Repositories
{
    public interface ICustomerQueueRepository : IRepository<CustomerQueue, CustomerQueueCreated>
    {
        Result<CustomerQueue> BorrowSingle(Func<CustomerQueue, Result<CustomerQueue>> transformer);
    }
}