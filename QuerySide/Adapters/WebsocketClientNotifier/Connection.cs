using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace QuerySide.Adapters.WebsocketClientNotifier
{
    public sealed class Connection : WebSocketBehavior
    {
        private readonly Action<WebSocket> _onConnectionOpened;
        private readonly Action<WebSocket> _onConnectionClosed;

        public Connection(Action<WebSocket> onConnectionOpened, Action<WebSocket> onConnectionClosed)
        {
            _onConnectionOpened = onConnectionOpened;
            _onConnectionClosed = onConnectionClosed;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _onConnectionOpened(Context.WebSocket);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            _onConnectionClosed(Context.WebSocket);
        }
    }
}