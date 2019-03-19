$(document).ready(function() {
    const app = document.getElementById('root');
    var url = "http://localhost:5000/api";

    var showErrorBoxWith = function (error) {
        const rootErrorBox = document.getElementById('rootErrorBox');
        const errorMessage = document.getElementById('errorMessage');
        errorMessage.innerHTML = error.error ? error.error : error;
        rootErrorBox.style.display = 'block';
    }

    $("#getServerVersion").on("click", function() {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: url + "/VMax/GetServerVersion",
            success: populateSystemInfo,
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });

    $("#getSystemStatus").on("click", function() {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: url + "/VMax/GetSystemStatus",
            success: populateSystemInfo,
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });

    $("#resetOpenTickets").on("click", function() {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url + "/VMax/ResetOpenTickets",
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });

    var populateSystemInfo = function(data) {
        if (!data) return;

        $("#systemInfo pre").remove();
        $("#systemInfo").append("<pre>" + JSON.stringify(data, null, " ") + "</pre>");
    }
        
});