using System;
using QueryCommon;

namespace QuerySidePorts
{
    public interface IClientNotifier
    {
        void StartClientNotifier(Action newClientCallback);
        
        IView NotifyAll(IView view);
    }
}