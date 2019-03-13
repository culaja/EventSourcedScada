using System;
using CommandSide.Domain.TicketIssuing.Configuring;
using Common;
using Shared.TicketIssuer;
using static CommandSide.Domain.TicketIssuing.Configuring.OpenTimes;
using static Common.Result;

namespace CommandSide.Domain.TicketIssuing
{
    public sealed class TicketIssuer : AggregateRoot
    {
        private OpenTimes _currentOpenTimes = NoOpenTimes;

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
    }
}