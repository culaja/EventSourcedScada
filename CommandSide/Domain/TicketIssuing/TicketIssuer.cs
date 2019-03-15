using System;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Shared.TicketIssuer;
using static CommandSide.Domain.TicketIssuing.Commands.TicketNumber;
using static CommandSide.Domain.TicketIssuing.OpenTimes;
using static Common.Result;

namespace CommandSide.Domain.TicketIssuing
{
    public sealed class TicketIssuer : AggregateRoot
    {
        private OpenTimes _currentOpenTimes = NoOpenTimes;
        private TicketNumber _expectedTicketNumberToBeIssued = FirstTicketNumber;

        public TicketIssuer(Guid id) : base(id)
        {
        }

        public static TicketIssuer NewTicketIssuerFrom(Guid id)
        {
            var ticketIssuer = new TicketIssuer(id);
            ticketIssuer.ApplyChange(new TicketIssuerCreated(ticketIssuer.Id));
            return ticketIssuer;
        }

        private TicketIssuer Apply(TicketIssuerCreated _) => this;

        public Result<TicketIssuer> SetOpenTimes(OpenTimes openTimes)
        {
            openTimes.IsolateOpenTimesToRemove(_currentOpenTimes).Map(openTime =>
                ApplyChange(new OpenTimeRemoved(Id, openTime.Day, openTime.BeginTimeOfDay, openTime.EndTimeOfDay)));
            
            openTimes.IsolateOpenTimesToAdd(_currentOpenTimes).Map(openTime =>
                ApplyChange(new OpenTimeAdded(Id, openTime.Day, openTime.BeginTimeOfDay, openTime.EndTimeOfDay)));

            return Ok(this);
        }

        private TicketIssuer Apply(OpenTimeAdded e)
        {
            _currentOpenTimes = _currentOpenTimes.Add(e.ToOpenTime());
            return this;
        }

        private TicketIssuer Apply(OpenTimeRemoved e)
        {
            _currentOpenTimes = _currentOpenTimes.Remove(e.ToOpenTime());
            return this;
        }

        public Result<TicketIssuer> IssueATicketWith(
            TicketId ticketId,
            TicketNumber ticketNumber,
            Func<DateTime> currentTimeProvider) => Ok()
                .Ensure(() => IsTimeInOpenTimesRange(currentTimeProvider()), $"Can't issue a ticket outside of configured open times.")
                .Ensure(() => IsExpectedTicketNumber(ticketNumber), $"Can't issue a ticket with number {ticketNumber} since expected number is '{_expectedTicketNumberToBeIssued}'.")
                .OnSuccess(() => ApplyChange(new TicketIssued(Id, ticketId, ticketNumber)))
                .ToTypedResult(this);

        private bool IsTimeInOpenTimesRange(DateTime currentTime) => _currentOpenTimes.IsInRange(currentTime);
        
        private bool IsExpectedTicketNumber(TicketNumber ticketNumber) => ticketNumber.Equals(_expectedTicketNumberToBeIssued);

        private TicketIssuer Apply(TicketIssued e)
        {
            _expectedTicketNumberToBeIssued = _expectedTicketNumberToBeIssued.Next;
            return this;
        }
    }
}