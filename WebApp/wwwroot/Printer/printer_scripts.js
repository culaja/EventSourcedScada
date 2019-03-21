$(document).ready(function() {
    const app = document.getElementById('root');
    var url = "http://localhost:5000/api";

    var showErrorBoxWith = function(error) {
        const rootErrorBox = document.getElementById('rootErrorBox');
        const errorMessage = document.getElementById('errorMessage');
        errorMessage.innerHTML  = error.error ? error.error : error;
        rootErrorBox.style.display = 'block';
    }
    
    var incrementTicketNumber = function() {
        var ticketNumber = parseInt($("#ticketNumber").val(), 10);
        $("#ticketNumber").val(ticketNumber + 1);
    }

    $("#issueATicketButton").on("click", function() {
        event.preventDefault();
        var ticketNumber = $("#ticketNumber").val();
        
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url + "/Printer/IssueATicket?ticketNumber=" + ticketNumber,
            success: incrementTicketNumber,
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });
    
    $('input[type="number"]').keydown(function (e) {
      e.preventDefault();
    });
});