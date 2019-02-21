using System;
using QueryCommon;

namespace Ports
{
    public interface IClientNotifier
    {
        void StartClientNotifier(Action newClientCallback);
        
        IView NotifyAll(IView view);
    }
}