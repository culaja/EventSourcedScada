$(document).ready(function(){
    const app = document.getElementById('root');
    var url = "http://localhost:5000/api";
    var maxTicketNumber = 0;
    
    var exampleSocket = new WebSocket("ws://localhost/ClientHub");
    window.onbeforeunload = function() {
        exampleSocket.onclose = function () {}; // disable onclose handler first
        exampleSocket.close();
    };
    
    exampleSocket.onopen = function (event) {
        console.log('Connection with server established.');
    };
    
    exampleSocket.onmessage = function (event) {
        var msg = JSON.parse(event.data);
        var data = JSON.parse(msg.Event);
        switch(msg.Type) {
            case "CountersView":
                populateCounterStates(data.CountersState);
                break;
            case "TicketsPerCounterView":
                populateCounterStats(data.CountersDetails);
                break;
            case "TicketQueueView":
                populateTicketQueue(data.TicketStates);
                break;
            default:
            console.log("Unsupported event received: " + msg.Type);        
        }
    }
    
    var request = new XMLHttpRequest();
    request.open('POST', 'http://localhost:5000/api/CustomerQueue/AddTicket', true);
    request.onload = function () {
      var data = JSON.parse(this.response);
      if (data.status != 200) {
        const errorMessage = document.createElement('marquee');
        errorMessage.textContent = `Gah, it's not working!`;
        app.appendChild(errorMessage);
      }
    }
    
    var ticketStateToString = function(ticketState) {
        switch (ticketState)
        {
            case 'W': return "Waiting";
            case 'C': return "Serving";
            case 'S': return "Served";
            case 'R': return "Revoked";
        }
        return "";
    }
    
    var ticketStateToColor = function(ticketState) {
         switch (ticketState)
         {
             case 'W': return "#e8deca";
             case 'C': return "#ccffcc";
             case 'S': return "#99ccff";
             case 'R': return "#ff9999";
         }
         return "";
     }
    
    var populateTicketQueue = function(data) {
        if(!data.length) return;
        
        var ticketQueue = $("#ticketQueue");
        ticketQueue.empty();
        
        $.each(data, function(index, elem) {
            var entry = $("<div>");
            entry.addClass("singleTicket");
            entry.css("background-color", ticketStateToColor(elem.State));
            
            var num = parseInt(elem.Number);            
            if(num > maxTicketNumber) {
                maxTicketNumber = num;
            }
            
            var number = $("<span>").text(elem.Number);
            var state = $("<span>").text(ticketStateToString(elem.State));
            
            entry.append(number);
            entry.append(state);
            
            ticketQueue.append(entry);
        });        
    }
    
    var populateCounterStats = function(data) {
        if(!data) return;
                
        var tableBody = $('#ticketsPerCounter tbody');
        tableBody.empty();
        
        for(key in data) {
            var row = $("<tr>");
            var counter = $("<td>").text(key);
            var served = $("<td>").text(data[key].ServedTickets);
            var revoked = $("<td>").text(data[key].RevokedTickets);
            
            row.append(counter);
            row.append(served);
            row.append(revoked);
            
            tableBody.append(row);
        }           
    }    
    
    var populateCounterStates = function(data) {
        if(!data) return;
        
        var tableBody = $('#countersState tbody');
        tableBody.empty();
        
        for(key in data) {
            var row = $("<tr>");
            var counter = $("<td>");
            var counterName = $("<span>").text(key).addClass("counterName");
                        
            var next = $("<button>").text("Next");
            var revoke = $("<button>").text("Revoke");
            
            next.on("click", {counterName: key, action: "TakeNextCustomer"}, counterAction);
            revoke.on("click", {counterName: key, action: "RevokeCustomer"}, counterAction);
            
            counter.append(counterName);
            counter.append(next);
            counter.append(revoke);
            
            var ticket = $("<td>").text(data[key]);
            row.append(counter);
            row.append(ticket);
            tableBody.append(row);
        }
    }
    
    $(".fa-print").on("mousedown", function() {
        $(this).addClass("active-print");
    });
    
    $(".fa-print").on("mouseup", function() {
            $(this).removeClass("active-print");
    });
    
    var showErrorBoxWith = function(error) {
        const rootErrorBox = document.getElementById('rootErrorBox');
        const errorMessage = document.getElementById('errorMessage');
        errorMessage.innerHTML  = error;
        rootErrorBox.style.display = 'block';
    }
    
    $(".fa-print").on("click", function() {       
       var postData = {};
       postData.TicketNumber = maxTicketNumber + 1;
       
       $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url + "/CustomerQueue/AddTicket",
            data: JSON.stringify(postData),
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
       });
       
       $(this).removeClass("active-print");
    });
    
    $("#submitCounter").submit(function(event) {
       event.preventDefault();
       
       var postData = {};
       postData.CounterName = $(this).find("#name").val();
       
       $.ajax({
        type: "POST",
        contentType: "application/json",
        url: url + "/CustomerQueue/AddCounter",
        data: JSON.stringify(postData),
        error: function(error) {
            showErrorBoxWith(error.responseJSON);
        }
       });
    });
    
    var counterAction = function(event) {
        var counterName = event.data.counterName;
        var action = event.data.action;
        var postData = {};
        postData.CounterName = counterName;
        
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url + "/CustomerQueue/" + action,
            data: JSON.stringify(postData),
            error: function(result) {
                showErrorBoxWith(error.responseJSON);
            }
         });
    }
});

