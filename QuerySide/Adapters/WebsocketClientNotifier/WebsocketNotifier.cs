using System;
using Ports;
using QueryCommon;
using WebSocketSharp.Server;

namespace WebsocketClientNotifier
{
    public sealed class WebSocketNotifier : IClientNotifier, IDisposable
    {
        private WebSocketServer _server;
        private Action _newClientCallback = () => { };
        private readonly ClientHub _clientHub;
        
        public WebSocketNotifier()
        {
            _clientHub = new ClientHub(() => _newClientCallback());
        }
        
        public void StartClientNotifier(Action newClientCallback)
        {
            _server = new WebSocketServer("ws://localhost");
            _newClientCallback = newClientCallback;
            _server.AddWebSocketService("/ClientHub", () => _clientHub);
            _server.Start();
        }

        public IView NotifyAll(IView v) => _clientHub.NotifyAll(v);

        public void Dispose()
        {
            _server?.Stop();
        }
    }
}