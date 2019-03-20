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
            success: populateStatusData,
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
            success: populateStatusData,
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });
    
    $("#getQueueStatus").on("click", function() {
            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: url + "/VMax/GetQueueStatus",
                success: populateStatusData,
                error: function(error) {
                    showErrorBoxWith(error.responseJSON);
                }
            });
        });

    var populateStatusData = function(data) {
        if (!data) return;

        $("#statusData pre").remove();
        $("#statusData").append("<pre>" + JSON.stringify(data, null, " ") + "</pre>");
    }
        
});