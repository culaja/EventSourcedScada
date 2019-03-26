using System;
using Common;
using QuerySide.QueryCommon;

namespace QuerySide.QuerySidePorts
{
    public interface IClientNotifier
    {
        void StartClientNotifier(Action newClientCallback);

        Nothing NotifyAll(IView view);
    }
}