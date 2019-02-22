const app = document.getElementById('root');

var exampleSocket = new WebSocket("ws://localhost/ClientHub");
window.onbeforeunload = function() {
    exampleSocket.onclose = function () {}; // disable onclose handler first
    exampleSocket.close();
};

exampleSocket.onopen = function (event) {
    console.log('Websocket connection opened');
};

exampleSocket.onmessage = function (event) {
    var msg = JSON.parse(event.data);
    var data = JSON.parse(msg.Event);
    switch(msg.Type) {
        case "CountersView":
            console.log(data);
            break;
        case "TicketsPerCounterView":
            console.log(data);
            break;
        case "TicketQueueView":
            console.log(data);
            break;
    }
}