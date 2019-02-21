using System;
using System.Collections.Generic;
using QueryCommon;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebsocketClientNotifier
{
    public sealed class ClientHub : WebSocketBehavior
    {
        private readonly Action _newClientCallback;
        
        private readonly List<WebSocket> _clients = new List<WebSocket>();

        public ClientHub(Action newClientCallback)
        {
            _newClientCallback = newClientCallback;
        }
        
        protected override void OnOpen()
        {
            base.OnOpen();
            lock (_clients)
            {
                _clients.Add(Context.WebSocket);
            }

            _newClientCallback();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            lock (_clients)
            {
                _clients.Remove(Context.WebSocket);
            }
        }

        public IView NotifyAll(IView v)
        {
            lock (_clients)
            {
                foreach (var c in _clients) c.Send(WebsocketMessage.WebsocketMessageFrom(v).Serialize());
            }

            return v;
        }
    }
}