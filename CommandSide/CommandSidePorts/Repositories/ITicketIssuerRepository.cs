using System;
using CommandSide.Domain.TicketIssuing;
using Common;
using Shared.TicketIssuer;

namespace CommandSide.CommandSidePorts.Repositories
{
    public interface ITicketIssuerRepository : IRepository<TicketIssuer, TicketIssuerCreated>
    {
        Result<TicketIssuer> BorrowSingle(Func<TicketIssuer, Result<TicketIssuer>> transformer);
    }
}