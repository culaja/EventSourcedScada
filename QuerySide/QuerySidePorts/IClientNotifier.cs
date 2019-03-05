using System;
using QuerySide.QueryCommon;

namespace QuerySide.QuerySidePorts
{
    public interface IClientNotifier
    {
        void StartClientNotifier(Action newClientCallback);
        
        IView NotifyAll(IView view);
    }
}