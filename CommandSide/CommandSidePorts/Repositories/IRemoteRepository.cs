using System;
using CommandSide.Domain.RemoteDomain;
using Common;
using Shared.Remote.Events;

namespace CommandSide.CommandSidePorts.Repositories
{
    public interface IRemoteRepository : IRepository<Remote, RemoteCreated>
    {
        Result BorrowBy(RemoteName remoteName, Func<Remote, Result<Remote>> transformer);
    }
}