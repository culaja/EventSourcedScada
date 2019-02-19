using System;
using static System.Guid;

namespace Tests.Specifications.CustomerQueueSpecifications
{
    public static class CustomerQueueTestValues
    {
        public static readonly Guid CounterA_Id = NewGuid();
        public static readonly string CounterA_Name = "CounterA";

        public static readonly Guid CounterB_Id = NewGuid();
        public static readonly string CounterB_Name = "CounterB";
    }
}