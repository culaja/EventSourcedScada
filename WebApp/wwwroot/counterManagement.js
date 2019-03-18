$(document).ready(function() {
    const app = document.getElementById('root');
    var url = "http://localhost:5000/api";
    
    var showErrorBoxWith = function(error) {
            const rootErrorBox = document.getElementById('rootErrorBox');
            const errorMessage = document.getElementById('errorMessage');
            errorMessage.innerHTML  = error.error ? error.error : error;
            rootErrorBox.style.display = 'block';
        }
        
    var refreshAssignedCustomerWith = function(assignedCustomer) {
        $("#assignedTicketNumber").text(assignedCustomer.TicketNumber);
        $("#waitingCustomerCount").text(assignedCustomer.WaitingCustomerCount);
        $("#expectedWaitingTimeInSeconds").text(assignedCustomer.ExpectedWaitingTimeInSeconds);
    }
    
    $('#resetOpenTicketsButton').on('click', function(event) {
            event.preventDefault();
            
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: null,
                url: url + "/VMax/ResetOpenTickets",
                error: function(error) {
                    showErrorBoxWith(error.responseJSON);
                }
            });
        });

    $('#nextCustomerCounterNumberButton').on('click', function(event) {
        event.preventDefault();
        var counterId = $("#nextCustomerCounterNumber").val();

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: null,
            url: url + "/VMax/NextCustomer?counterId=" + counterId,
            success: function(data) {
                refreshAssignedCustomerWith(JSON.parse(data));
            },
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });

    $('#recallCustomerCounterNumberButton').on('click', function(event) {
        event.preventDefault();
        var counterId = $("#recallCustomerCounterNumber").val();

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url + "/VMax/RecallCustomer?counterId=" + counterId,
            success: function(data) {
                refreshAssignedCustomerWith(JSON.parse(data));
            },
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });
    
    $('input[type="number"]').keydown(function (e) {
          e.preventDefault();
        });
});