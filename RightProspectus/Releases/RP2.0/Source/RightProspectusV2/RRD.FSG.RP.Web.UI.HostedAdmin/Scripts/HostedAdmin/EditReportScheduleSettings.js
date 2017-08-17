window.onunload = function(e) {
    if (window.opener.$("#reportScheduleGrid").is(':visible')) {
        if (!window.opener.$("#reportScheduleGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();
        }
    } else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function() {
    var offset = new Date().getTimezoneOffset();
    $("#offsetTime").val(offset);

    if ($("#hdnReportScheduleId").val() > 0) {
        $("#txtFirstScheduledRunDate").val(kendo.parseDate(GetLocalTime($("#txtFirstScheduledRunDate").val(), offset)));
    }

    var firstScheduledRunDate = new Date();
    if ($("#txtFirstScheduledRunDate").val() != "") {
        firstScheduledRunDate = new Date($("#txtFirstScheduledRunDate").val());
    }
    $("#txtFirstScheduledRunDate").kendoDateTimePicker({
        value: firstScheduledRunDate,
        parseFormats: ["MM/dd/yyyy"]
    });

    BindFrequencyInterval();

    BindFTP();
    $("#submitReportSchedule").click(function(e) {
        if (ValidateSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;
    });


    if ($("#hdnSuccessOrFailedMessage").val() == "OK") {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Success",
            resizable: false,
            modal: true,
            draggable: false,
            actions: []
        });

        kendoWindow.data("kendoWindow")
            .content($("#divSuccessOrFailedMessage").html())
            .center()
            .open();

        kendoWindow
            .find(".confirm")
            .click(function() {
                window.close();
            });
    }
});


function BindFrequencyInterval() {
    if (Number($("#ComboFrequencyType").val()) == 2) {

        $("#divFrequencyInterval").show();

    } else {

        $("#divFrequencyInterval").hide();
    }
    $("#ComboFrequencyType").change(function() {
        if (Number($("#ComboFrequencyType").val()) == 2) {

            $("#divFrequencyInterval").show();

        } else {

            $("#divFrequencyInterval").hide();
        }
    });
}

function BindFTP() {

    $(".dvFTP").hide();
    $("#dvEmail").hide();
    $("#ComboTransferType").change(function() {


        if (Number($("#ComboTransferType").val()) == 0) {

            $("#dvEmail").show();
            $(".dvFTP").hide();

        } else if (Number($("#ComboTransferType").val()) == 1) {
            $(".dvFTP").show();
            $("#dvEmail").hide();

        } else {
            $(".dvFTP").hide();
            $("#dvEmail").hide();

        }
    });
    if (Number($("#ComboTransferType").val()) == 0) {

        $("#dvEmail").show();
        $(".dvFTP").hide();

    } else if (Number($("#ComboTransferType").val()) == 1) {
        $(".dvFTP").show();
        $("#dvEmail").hide();

    } else {
        $(".dvFTP").hide();
        $("#dvEmail").hide();
    }
}

function ValidateSave() {

    var isSuccess = true;
    $("#divEditValidations").empty();


    $("#divEditValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divEditValidations")[0].innerHTML += "<ul>"


    if ($("#ComboReportName").val() == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Report</li>";
        isSuccess = false;
    }

    if ($("#ComboFrequencyType").val() == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Frequency Type</li>";
        isSuccess = false;
    }

    var frequencyType = $("#ComboFrequencyType").val();


    if ($("#txtFrequencyInterval").val() == "") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Frequency Interval</li>";
        isSuccess = false;
    } else if ($("#txtFrequencyInterval").val() == "0") {


        if (frequencyType == 2) {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>FrequencyInterval Can't be Zero for this FrequencyType</li>";
            isSuccess = false;
        }

    } else if (Number($("#txtFrequencyInterval").val()) < 0) {

        $("#divEditValidations")[0].innerHTML += "<li class='message'>FrequencyInterval Can't be Negative</li>";
        isSuccess = false;
    } else {

        //if (frequencyType == 3 && (Number($("#txtFrequencyInterval").val()) < 1 || Number($("#txtFrequencyInterval").val()) > 7)) {
        //    $("#divEditValidations")[0].innerHTML += "<li class='message'>FrequencyInterval Can be Between 1 and 7</li>";
        //    isSuccess = false;
        //}
        //if (frequencyType == 4 && (Number($("#txtFrequencyInterval").val()) < 1 || Number($("#txtFrequencyInterval").val()) > 31)) {
        //    $("#divEditValidations")[0].innerHTML += "<li class='message'>FrequencyInterval Can be Between 1 and 31</li>";
        //    isSuccess = false;
        //}
        if ((frequencyType == 1 || frequencyType == 5 || frequencyType == 6 || frequencyType == 7) && (Number($("#txtFrequencyInterval").val()) != 0)) {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>FrequencyInterval can be only Zero for this Frequency Type</li>";
            isSuccess = false;
        }



    }

    if ($("#txtFirstScheduledRunDate").val() == "") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>First Scheduled Run Date</li>";
        isSuccess = false;


    } else if (!kendo.parseDate($('#txtFirstScheduledRunDate').val())) {

        $("#divEditValidations")[0].innerHTML += "<li class='message'>First Scheduled Run Date is invalid.</li>";
        isSuccess = false;
    }

    if ($("#ComboTransferType").val() == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>TransferType</li>";
        isSuccess = false;
    } else if ($("#ComboTransferType").val() == 1) {
        if ($("#txtFTPServerIP").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>FTPServerIP</li>";
            isSuccess = false;
        }
        if ($("#txtFTPFilePath").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>FTPFilePath</li>";
            isSuccess = false;
        }
        if ($("#txtFTPUsername").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>FTPUsername</li>";
            isSuccess = false;
        }
        if ($("#txtFTPPassword").val() == "" && $("#hdnReportScheduleId").val() == 0) {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>FTPPassword</li>";
            isSuccess = false;
        }

    } else {
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (!filter.test($("#txtEmail").val())) {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Valid E-mail Id.</li>";
            isSuccess = false;
        }
    }




    $("#divEditValidations")[0].innerHTML += "</ul>"
    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divEditValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

function GetLocalTime(utcTime, offset) {
    var newDate = "";
    $.ajax({
        url: $("#dvGetLocaltime").data('request-url'),
        dataType: "text",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: {
            utcDate: utcTime,
            offset: offset
        },
        success: function(data) {

            newDate = data;
        },
        error: function(xhr, status, error) {

            alert(error);
        }
    });
    return newDate;
}