using System;
using System.Collections.Generic;
using CommandSide.CommandSidePorts;
using CommandSide.CommandSidePorts.System;
using CommandSide.Domain;
using CommandSide.Domain.TicketIssuing;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.Tests.Specifications.TicketIssuerSpecifications.Stubs;
using Common.Time;
using Shared.TicketIssuer;
using Shared.TicketIssuer.Events;
using static System.DayOfWeek;
using static CommandSide.Domain.CounterId;
using static CommandSide.Domain.TicketId;
using static CommandSide.Domain.TicketIssuing.Commands.TicketNumber;
using static Common.Time.TimeOfDay;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications
{
    public static class TicketIssuerTestValues
    {
        public static readonly TimeOfDay Hour9 = TimeOfDayFromHour(9);
        public static readonly TimeOfDay Hour10 = TimeOfDayFromHour(10);
        public static readonly TimeOfDay Hour11 = TimeOfDayFromHour(11);
        public static readonly TimeOfDay Hour12 = TimeOfDayFromHour(12);
        public static readonly TimeOfDay Hour14 = TimeOfDayFromHour(14);
        public static readonly TimeOfDay Hour16 = TimeOfDayFromHour(16);
        
        public static readonly OpenTime Monday9To12 = new OpenTime(Monday, Hour9, Hour12);
        public static readonly OpenTime Monday10To11 = new OpenTime(Monday, Hour10, Hour11);
        public static readonly OpenTime Monday14To16 = new OpenTime(Monday, Hour14, Hour16);
        
        public static readonly OpenTime Tuesday9To12 = new OpenTime(Tuesday, Hour9, Hour12);
        
        public static readonly OpenTimes MondayOpenTimes = new OpenTimes(new []
        {
            Monday9To12,
            Monday14To16
        });
        
        public static readonly OpenTimes TuesdayOpenTimes = new OpenTimes(new []
        {
            Tuesday9To12
        });
        
        public static readonly OpenTimes AllOpenTimes = new OpenTimes(new []
        {
            Monday9To12,
            Monday14To16,
            Tuesday9To12
        });

        public static readonly TicketId Ticket1Id = NewTicketId();
        public static readonly TicketNumber TicketNumber1 = TicketNumberFrom(1);
        public static readonly TicketId Ticket2Id = NewTicketId();
        public static readonly TicketNumber TicketNumber2 = TicketNumberFrom(2);
        
        public static readonly CounterId Counter1Id = NewCounterIdFrom(1);
        
        public static readonly Guid SingleTicketIssuerId = Guid.NewGuid();
        
        public static readonly TicketIssuerCreated SingleTicketIssuerCreated = new TicketIssuerCreated(SingleTicketIssuerId);
        
        public static readonly OpenTimeAdded Monday9To12Added = new OpenTimeAdded(SingleTicketIssuerId, Monday9To12.Day, Monday9To12.BeginTimeOfDay, Monday9To12.EndTimeOfDay);
        public static readonly OpenTimeAdded Monday14To16Added = new OpenTimeAdded(SingleTicketIssuerId, Monday14To16.Day, Monday14To16.BeginTimeOfDay, Monday14To16.EndTimeOfDay);
        public static readonly OpenTimeAdded Tuesday9To12Added = new OpenTimeAdded(SingleTicketIssuerId, Tuesday9To12.Day, Tuesday9To12.BeginTimeOfDay, Tuesday9To12.EndTimeOfDay);
        public static readonly IReadOnlyList<OpenTimeAdded> AllOpenTimesAdded = new[] {Monday9To12Added, Monday14To16Added, Tuesday9To12Added};
        public static readonly IReadOnlyList<OpenTimeAdded> MondayOpenTimesAdded = new[] {Monday9To12Added, Monday14To16Added};
        
        public static readonly OpenTimeRemoved Monday9To12Removed = new OpenTimeRemoved(SingleTicketIssuerId, Monday9To12.Day, Monday9To12.BeginTimeOfDay, Monday9To12.EndTimeOfDay);
        public static readonly OpenTimeRemoved Monday14To16Removed = new OpenTimeRemoved(SingleTicketIssuerId, Monday14To16.Day, Monday14To16.BeginTimeOfDay, Monday14To16.EndTimeOfDay);
        public static readonly OpenTimeRemoved Tuesday9To12Removed = new OpenTimeRemoved(SingleTicketIssuerId, Tuesday9To12.Day, Tuesday9To12.BeginTimeOfDay, Tuesday9To12.EndTimeOfDay);
        public static readonly IReadOnlyList<OpenTimeRemoved> AllOpenTimesRemoved = new[] {Monday9To12Removed, Monday14To16Removed, Tuesday9To12Removed};
        
        public static readonly TicketIssued Ticket1Issued = new TicketIssued(SingleTicketIssuerId, Ticket1Id, TicketNumber1);
        public static readonly TicketIssued Ticket2Issued = new TicketIssued(SingleTicketIssuerId, Ticket2Id, TicketNumber2);

        public static readonly TicketId Ticket10kId = NewTicketId();
        public static readonly TicketNumber Ticket10kNumber = TicketNumberFrom(10000);
        public static readonly TicketId Ticket10kOneId = NewTicketId();
        public static readonly TicketNumber Ticket10kOneNumber = TicketNumberFrom(10001);
        
        public static readonly OutOfLineTicketIssued OutOfLineTicket10kIssuedForCounter1 = new OutOfLineTicketIssued(SingleTicketIssuerId, Ticket10kId, Ticket10kNumber, Counter1Id);
        public static readonly OutOfLineTicketIssued OutOfLineTicket10kOne1IssuedForCounter1 = new OutOfLineTicketIssued(SingleTicketIssuerId, Ticket10kOneId, Ticket10kOneNumber, Counter1Id);
        
        public static readonly ILocalTimeProvider AlwaysMonday10LocalTimeProviderStub = new LocalTimeProviderStub(new DateTime(2019, 3, 11, 10, 0, 0));
        
        public static readonly ITicketIdGenerator Ticket1IdGenerator = new TicketIdGeneratorStub(Ticket1Id);
        public static readonly ITicketIdGenerator Ticket2IdGenerator = new TicketIdGeneratorStub(Ticket2Id);
        public static readonly ITicketIdGenerator Ticket10kIdGenerator = new TicketIdGeneratorStub(Ticket10kId);
        public static readonly ITicketIdGenerator Ticket10kOneIdGenerator = new TicketIdGeneratorStub(Ticket10kOneId);
    }
}