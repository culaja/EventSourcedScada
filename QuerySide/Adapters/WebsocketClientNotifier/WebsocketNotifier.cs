using System;
using QuerySide.QueryCommon;
using QuerySide.QuerySidePorts;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace QuerySide.Adapters.WebsocketClientNotifier
{
    public sealed class WebSocketNotifier : IClientNotifier, IDisposable
    {
        private WebSocketServer _server;
        private Action _newClientCallback = () => { };
        private readonly OpenedSocketsHub _socketHub = new OpenedSocketsHub();
        
        public void StartClientNotifier(Action newClientCallback)
        {
            _server = new WebSocketServer("ws://localhost");
            _newClientCallback = newClientCallback;
            _server.AddWebSocketService("/ClientHub", () => new Connection(OnConnectionOpened, OnConnectionClosed)); 
           
            _server.AuthenticationSchemes = AuthenticationSchemes.Basic;
            _server.UserCredentialsFinder = id =>
            {
                var name = id.Name;
                return name == "AnyClient"
                    ? new NetworkCredential(name, "AnyClientPass", "all")
                    : null;
            };
            
            _server.Start();
        }

        private void OnConnectionOpened(WebSocket socket)
        {
            _socketHub.Add(socket);
            _newClientCallback();
        }

        private void OnConnectionClosed(WebSocket socket) => _socketHub.Remove(socket);

        public IView NotifyAll(IView v) => _socketHub.NotifyAll(v);

        public void Dispose()
        {
            _server?.Stop();
        }
    }
}