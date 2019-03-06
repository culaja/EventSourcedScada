using System;
using Common.Messaging;

namespace Tests.ViewInfrastructure.TestValues
{
    public sealed class SomeOtherEvent : DomainEvent
    {
        public SomeOtherEvent() : base(Guid.NewGuid(), "SomeOtherTopic")
        {
        }
        
        public static SomeOtherEvent NewSomeOtherEvent => new SomeOtherEvent();
    }
}