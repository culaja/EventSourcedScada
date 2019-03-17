using System.Collections.Generic;
using Common;

namespace QuerySide.Views.AssigningCustomer
{
    public sealed class AssignedCustomer : ValueObject<AssignedCustomer>
    {
        public int TicketNumber { get; }
        public int WaitingCustomerCount { get; }
        public int ExpectedWaitingTimeInSeconds { get; }

        public AssignedCustomer(
            int ticketNumber,
            int waitingCustomerCount,
            int expectedWaitingTimeInSeconds)
        {
            TicketNumber = ticketNumber;
            WaitingCustomerCount = waitingCustomerCount;
            ExpectedWaitingTimeInSeconds = expectedWaitingTimeInSeconds;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TicketNumber;
            yield return WaitingCustomerCount;
            yield return ExpectedWaitingTimeInSeconds;
        }
    }
}