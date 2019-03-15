$(document).ready(function() {
    const app = document.getElementById('root');
    var url = "http://localhost:5000/api";

    var exampleSocket = new WebSocket("ws://localhost/ClientHub");
    window.onbeforeunload = function() {
        exampleSocket.onclose = function () {}; // disable onclose handler first
        exampleSocket.close();
    };

    exampleSocket.onopen = function (event) {
        console.log('Connection with server established.');
    };

    var showErrorBoxWith = function(error) {
        const rootErrorBox = document.getElementById('rootErrorBox');
        const errorMessage = document.getElementById('errorMessage');
        errorMessage.innerHTML  = error.error ? error.error : error;
        rootErrorBox.style.display = 'block';
    }

    $("#getConfiguration").on("click", function() {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: url + "/VMax/GetConfiguration",
            success: populateConfigurationTable,
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });
    
    var populateConfigurationTable = function(data) {
        if(!data) return;
        
        $(".configurationCounterDetails").empty();
        $(".configurationOpenTimes").empty();
        
        var counters = data.counters;
        for (i = 0, len = counters.length, text = ""; i < len; i++) {
            var $form = $("<form class=\"configurationCounterDetails\">\n" +
                        "                                    <div class=\"form-row\">\n" +
                        "                                        <div class=\"col\">\n" +
                        "                                            <input id=\"number\" name=\"number\" type=\"number\" class=\"form-control\" placeholder=\"Number\" value=\"" + counters[i].id + "\">\n" +
                        "                                        </div>\n" +
                        "                                        <div class=\"col\">\n" +
                        "                                            <input id=\"name\" name=\"name\" type=\"text\" class=\"form-control\" placeholder=\"Name\" value=\"" + counters[i].name + "\">\n" + 
                        "                                        </div>\n" +
                        "                                    </div>\n" +
                        "                                </form>");
            $(".configurationCounterDetails").last().append($form.clone());
        }
        
        var openTimes = data.openTimes;
        for (i = 0, len = openTimes.length, text = ""; i < len; i++) {
            var $form = $("<form class=\"configurationOpenTimes\">\n" +
                        "                                    <div class=\"form-row\">\n" +
                        "                                        <div class=\"col\">\n" +
                        "                                            <input id=\"day\" name=\"day\" type=\"text\" class=\"form-control\" placeholder=\"Day\" value=\"" + openTimes[i].day + "\">\n" +
                        "                                        </div>\n" +
                        "                                        <div class=\"col\">\n" +
                        "                                            <input id=\"from\" name=\"from\" type=\"text\" class=\"form-control\" placeholder=\"From\" value=\"" + openTimes[i].beginTimestamp + "\">\n" +
                        "                                        </div>\n" +
                        "                                        <div class=\"col\">\n" +
                        "                                            <input id=\"to\" name=\"to\" type=\"text\" class=\"form-control\" placeholder=\"To\" value=\"" + openTimes[i].endTimestamp + "\">\n" +
                        "                                        </div>\n" +
                        "                                    </div>\n" +
                        "                                </form>");
            $(".configurationOpenTimes").last().append($form.clone());
        }
    }
    
    $('#addCounterDetails').on('click', function() {
        var $form = $("<form class=\"configurationCounterDetails\">\n" +
            "                                    <div class=\"form-row\">\n" +
            "                                        <div class=\"col\">\n" +
            "                                            <input id=\"number\" name=\"number\" type=\"number\" class=\"form-control\" placeholder=\"Number\">\n" +
            "                                        </div>\n" +
            "                                        <div class=\"col\">\n" +
            "                                            <input id=\"name\" name=\"name\" type=\"text\" class=\"form-control\" placeholder=\"Name\">\n" +
            "                                        </div>\n" +
            "                                    </div>\n" +
            "                                </form>");
        $(".configurationCounterDetails").last().append($form.clone());
    });

    $('#addOpenTimes').on('click', function() {
        var $form = $("<form class=\"configurationOpenTimes\">\n" +
            "                                    <div class=\"form-row\">\n" +
            "                                        <div class=\"col\">\n" +
            "                                            <input id=\"day\" name=\"day\" type=\"text\" class=\"form-control\" placeholder=\"Day\">\n" +
            "                                        </div>\n" +
            "                                        <div class=\"col\">\n" +
            "                                            <input id=\"from\" name=\"from\" type=\"text\" class=\"form-control\" placeholder=\"From\">\n" +
            "                                        </div>\n" +
            "                                        <div class=\"col\">\n" +
            "                                            <input id=\"to\" name=\"to\" type=\"text\" class=\"form-control\" placeholder=\"To\">\n" +
            "                                        </div>\n" +
            "                                    </div>\n" +
            "                                </form>");
        $(".configurationOpenTimes").last().append($form.clone());
    });
    
    $('#setConfiguration').on('click', function() {
        var postData = {};
        postData.counters = [];
        postData.openTimes = [];
        
        $(".configurationCounterDetails").each(function(){
            var number = $(this).find("input[name='number']").val();
            var name = $(this).find("input[name='name']").val();
            if (number.length > 0 || name.length > 0) {
                postData.counters.push({number: number, name: name});
            }
        });

        $(".configurationOpenTimes").each(function(){
            var day = $(this).find("input[name='day']").val();
            var from = $(this).find("input[name='from']").val();
            var to = $(this).find("input[name='to']").val();
            if (day.length > 0 || from.length > 0 || to.length > 0) {
                postData.openTimes.push({dayOfWeek: day, from: from, to: to});
            }
        });
        
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url + "/VMax/SetConfiguration",
            data: JSON.stringify(postData),
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });
    
    $('#openCounterButton').on('click', function(event) {
        event.preventDefault();
        var counterId = $("#openCounterNumber").val();

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: null,
            url: url + "/VMax/OpenCounter?counterId=" + counterId,
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });

    $('#closeCounterButton').on('click', function(event) {
        event.preventDefault();
        var counterId = $("#closeCounterNumber").val();

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url + "/VMax/CloseCounter?counterId=" + counterId,
            error: function(error) {
                showErrorBoxWith(error.responseJSON);
            }
        });
    });
    
    $('input[type="number"]').keydown(function (e) {
      e.preventDefault();
    });

});