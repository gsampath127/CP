window.onunload = function (e) {
    if (window.opener.$("#reportScheduleGrid").is(':visible')) {
        if (!window.opener.$("#reportScheduleGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();
        }
    } else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
    var offset = new Date().getTimezoneOffset();
    $("#offsetTime").val(offset);

    if ($("#hdnReportScheduleId").val() > 0) {
        $("#txtFirstScheduledRunDate").val(kendo.parseDate(GetLocalTime($("#txtFirstScheduledRunDate").val(), offset)));
        $("#txtSDate").val(kendo.parseDate(GetLocalTime($("#txtSDate").val(), offset)));
        $("#txtEDate").val(kendo.parseDate(GetLocalTime($("#txtEDate").val(), offset)));
        $("#txtScheduledEndDate").val(kendo.parseDate(GetLocalTime($("#txtScheduledEndDate").val(), offset)));
    }
    SetDateValues();
    BindStartDate();
    BindFrequencyInterval();

    $("#ComboFrequencyType").change(function () {
        BindFrequencyInterval();
    });

    $("#ComboFrequencyType").prop("disabled", true);
    $("#ComboReportName").change(function () {

        $("#ComboFrequencyType").empty();
        $("#ComboFrequencyType").prop("disabled", true);

        $.ajax({
            type: 'GET',
            url: $("#dvGetFrequencyType").data('request-url'),
            data: {
                reportId: $("#ComboReportName").val()
            },
            cache: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#ComboFrequencyType').empty();

                $.each(data, function (i, item) {

                    $('#ComboFrequencyType').append($('<option>', {
                        value: item.Value,
                        text: item.Display
                        }));

                });
            },
                error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
       
        if ($("#ComboReportName").val() != -1) {

            $("#ComboFrequencyType").prop("disabled", false);
        }
        else {
            $("#ComboFrequencyType").prop("enabled", false);
        }
    });

    BindFTP();
    $("#submitReportSchedule").click(function (e) {
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
            .click(function () {
                window.close();
            });
    }
});

function SetDateValues() {
    var firstScheduledRunDate = new Date();
    var startDate = new Date();
    startDate.setHours(startDate.getHours() - 1);
    var endDate = new Date();
    var scheduledEndDate = new Date();

    if ($("#txtFirstScheduledRunDate").val() != "") {
        firstScheduledRunDate = new Date($("#txtFirstScheduledRunDate").val());
    }
    if ($("#txtSDate").val() != "") {
        startDate = new Date($("#txtSDate").val());
    }
    if ($("#txtEDate").val() != "") {
        endDate = new Date($("#txtEDate").val());
    }
    if ($("#txtScheduledEndDate").val() != "") {
        scheduledEndDate = new Date($("#txtScheduledEndDate").val());
    }

    $("#txtFirstScheduledRunDate").kendoDateTimePicker({
        value: firstScheduledRunDate,
        parseFormats: ["MM/dd/yyyy"]
    });
    $("#txtSDate").kendoDateTimePicker({
        value: startDate,
        parseFormats: ["MM/dd/yyyy"]
    });
    $("#txtEDate").kendoDateTimePicker({
        value: endDate,
        parseFormats: ["MM/dd/yyyy"]
    });
    $("#txtScheduledEndDate").kendoDateTimePicker({
        value: scheduledEndDate,
        parseFormats: ["MM/dd/yyyy"]
    });
}

function BindFrequencyInterval() {



}
function BindFrequencyInterval() {
    if (Number($("#ComboFrequencyType").val()) == 3) {

        $("#divWeeklyInterval").show();

    } else {

        $("#divWeeklyInterval").hide();
    }
    if (Number($("#ComboFrequencyType").val()) == 2 || Number($("#ComboFrequencyType").val()) == 8) {

        $("#divFrequencyInterval").show();

    } else {

        $("#divFrequencyInterval").hide();
    }
    $("#ComboFrequencyType").change(function () {
        if (Number($("#ComboFrequencyType").val()) == 3) {

            $("#divWeeklyInterval").show();

        } else {

            $("#divWeeklyInterval").hide();
        }
        if (Number($("#ComboFrequencyType").val()) != 1) {
            $("#dvScheduledEndDate").show();
        }
        else {
            $("#dvScheduledEndDate").hide();
        }

        if (Number($("#ComboFrequencyType").val()) == 2 || Number($("#ComboFrequencyType").val()) == 8) {

            $("#divFrequencyInterval").show();

        }
        else {

            $("#divFrequencyInterval").hide();
        }
    });
}
function BindStartDate() {
    if (Number($("#ComboFrequencyType").val()) == 1) {

        $("#divStartDate").show();

    } else {

        $("#divStartDate").hide();
    }
    $("#ComboFrequencyType").change(function () {
        if (Number($("#ComboFrequencyType").val()) == 1) {

            $("#divStartDate").show();

        } else {

            $("#divStartDate").hide();
        }
    });
}
function BindFTP() {

    $(".dvFTP").hide();
    $("#dvEmail").hide();
    $("#ComboTransferType").change(function () {
        var transferType = $("#ComboTransferType").val();
        
        if (Number(transferType) == 0) {

            $("#dvEmail").show();
            $("#dvErrorEmail").show();
            $(".dvFTP").hide();

        } else if (Number(transferType) == 1 || Number(transferType) == 2) {
            $("#txtFTPServerIP").val('');
            $("#txtFTPUsername").val('');
            $("#txtFTPPassword").val('');
            var label = ((transferType == 1) ? "S" : "");

            $("#lblTransServerIp").text(label + "FTPServerIP");
            $("#txtFTPServerIP").attr("placeholder", label + "FTPServerIP");

            $("#lblTransUserName").text(label + "FTPUserName");
            $("#txtFTPUsername").attr("placeholder", label + "FTPUserName");

            $("#lblTransPassword").text(label + "FTPPassword");
            $("#txtFTPPassword").attr("placeholder", label + "FTPPassword");

            $(".dvFTP").show();
            $("#dvEmail").hide();
            $("#dvErrorEmail").show();

        } else {
            $(".dvFTP").hide();
            $("#dvEmail").hide();
            $("#dvErrorEmail").hide();
           

        }
    });
    var transferType = $("#ComboTransferType").val();
    if (Number(transferType) == 0) {

        $("#dvEmail").show();
        $("#dvErrorEmail").show();
        $(".dvFTP").hide();


    } else if (Number(transferType) == 1 || Number(transferType) == 2) {
        $(".dvFTP").show();
        $("#dvEmail").hide();
        $("#dvErrorEmail").show();

    } else {
        $(".dvFTP").hide();
        $("#dvEmail").hide();
        $("#dvErrorEmail").hide();
    }
}

function ValidateSave() {
    var isSuccess = true, frequencyType = $("#ComboFrequencyType").val(), transferType = $("#ComboTransferType").val();
    $("#divEditValidations").empty();


    $("#divEditValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divEditValidations")[0].innerHTML += "<ul>"


    if ($("#ComboReportName").val() == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Report</li>";
        isSuccess = false;
    }
    if (frequencyType == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Frequency Type</li>";
        isSuccess = false;
    }
    else {

        var todaysDate = new Date();
        todaysDate.setHours(0, 0, 0, 0);
        firstScheduledRunDate = new Date($("#txtFirstScheduledRunDate").val());

        if (todaysDate > firstScheduledRunDate) {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Starts on: date should not be past date</li>";
            isSuccess = false;
        }

        if (frequencyType != 1) {
            scheduledEndDate = new Date($("#txtScheduledEndDate").val());           

            if (scheduledEndDate == "Invalid Date") {
                $("#divEditValidations")[0].innerHTML += "<li class='message'>Ends on: Date</li>";
                isSuccess = false;
            } else if (!kendo.parseDate(scheduledEndDate)) {
                $("#divEditValidations")[0].innerHTML += "<li class='message'>Ends on: is invalid.</li>";
                isSuccess = false;
            } else if (scheduledEndDate < firstScheduledRunDate) {
                $("#divEditValidations")[0].innerHTML += "<li class='message'>Ends on: date should be greater than Starts on: date</li>";
                isSuccess = false;
            }
        }
    }

    if ($("#txtFirstScheduledRunDate").val() == "") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Starts on: Date</li>";
        isSuccess = false;
    } else if (!kendo.parseDate($('#txtFirstScheduledRunDate').val())) {

        $("#divEditValidations")[0].innerHTML += "<li class='message'>Starts on: Date is invalid.</li>";
        isSuccess = false;
    }
    else if (frequencyType == 3) {
        var resultObject = ValidateFirstScheduledDate();
        if (!resultObject.isChecked) {
            //error msg
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Select WeekDay(s)</li>";
            isSuccess = false;
        }
        else if (resultObject.Date.getTime() != (new Date($("#txtFirstScheduledRunDate").val())).getTime()) {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Starts on: Date should be greater than/or " + resultObject.Date.toString().slice(3, 16) + " </li>";
            isSuccess = false;
        }
    }
    if ($("#txtSDate").val() == "") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Data Capture - Start Date/Time</li>";
        isSuccess = false;


    } else if (!kendo.parseDate($('#txtSDate').val())) {

        $("#divEditValidations")[0].innerHTML += "<li class='message'>Data Capture - Start Date/Time is invalid.</li>";
        isSuccess = false;
    }
    else if (frequencyType == 1 && (kendo.parseDate($('#txtEDate').val()) != null) && (new Date($('#txtSDate').val()) >= new Date($('#txtEDate').val()))) {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Data Capture - Start Date/Time should be less than Data Capture - End Date/Time</li>";
        isSuccess = false;
    }
    if ($("#txtEDate").val() == "") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Data Capture - End Date/Time</li>";
        isSuccess = false;
    } else if (!kendo.parseDate($('#txtEDate').val())) {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Data Capture - End Date/Time is invalid.</li>";
        isSuccess = false;
    } else {
        var firstScheduledRunDate = kendo.parseDate($('#txtFirstScheduledRunDate').val());
        if (firstScheduledRunDate != null && frequencyType != -1) {
            var endDate = new Date($("#txtEDate").val());
            if (frequencyType == 1) {
                if (endDate > firstScheduledRunDate) {
                    $("#divEditValidations")[0].innerHTML += "<li class='message'>Data Capture - End Date/Time should be less than/or Starts on: Date</li>";
                    isSuccess = false;
                }
            }
            //else if ((frequencyType == 3 || frequencyType == 8) && (endDate.getTime() > firstScheduledRunDate.getTime() || endDate.getDate() != firstScheduledRunDate.getDate() || endDate.getMonth() != firstScheduledRunDate.getMonth() || endDate.getYear() != firstScheduledRunDate.getYear())) {

            //    $("#divEditValidations")[0].innerHTML += "<li class='message'>Time for End Date should be less than Scheduled Run Date and Dates shoud be equal</li>";
            //    isSuccess = false;
            //}
            else {

                if ($("#ComboReportName").val() == 5)
                {
                    var todaysDate = new Date(firstScheduledRunDate);
                    todaysDate.setHours(0, 0, 0, 0);

                    if (todaysDate > endDate) {
                        $("#divEditValidations")[0].innerHTML += "<li class='message'>For Document Update Report, Data Capture - End Date/Time should not be past date</li>";
                        isSuccess = false;
                    }
                    if (endDate > firstScheduledRunDate) {
                        $("#divEditValidations")[0].innerHTML += "<li class='message'>For Document Update Report, Data Capture - End Date/Time should not be greater than Starts on: Date</li>";
                        isSuccess = false;
                    }
                } else
                {
                    var today = new Date(firstScheduledRunDate);
                    firstDay = new Date(firstScheduledRunDate);
                    firstDay.setDate(1);
                    firstDay.setMonth(firstDay.getMonth());
                    firstDay.setHours(0, 0, 0, 0);

                    var showFirstDate = firstDay.toString().slice(3, 16);
                    var showTodaydate = today.toString().slice(3, 16);
                    if (endDate > today || endDate < firstDay) {
                        $("#divEditValidations")[0].innerHTML += "<li class='message'>Data Capture - End Date/Time should be between " + showFirstDate + " and </li>" + showTodaydate;
                        isSuccess = false;
                    }
                }                
            }
        }
    }

    if (transferType == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>TransferType</li>";
        isSuccess = false;
    }
    else if (transferType == 0) {
        if ($("#txtEmail").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Email</li>";
            isSuccess = false;
        }        
        else {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            var emails = $("#txtEmail").val();
            emails = emails.toLowerCase();
            var emailArray = emails.split(",");
            outer:
                for (i = 0; i <= (emailArray.length - 1) ; i++) {
                    $("#divEditValidations")[0].innerHTML + emailArray[i];
                    for (k = i + 1; k <= (emailArray.length - 1) ; k++) {
                        if (emailArray[i] == emailArray[k]) {
                            $("#divEditValidations")[0].innerHTML += "<li class='message'>Please enter a Unique E-mail Id.</li>";
                            isSuccess = false;
                            break outer;
                        }
                    }
                    if (!filter.test(emailArray[i])) {
                        $("#divEditValidations")[0].innerHTML += "<li class='message'>Please enter a Valid E-mail Id.</li>";
                        isSuccess = false;
                        break;
                    }
                }
        }
    } else {
        var serverName = ((transferType == 1) ? "S" : "");
        if ($("#txtFTPServerIP").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>" + serverName + "FTPServerIP</li>";
            isSuccess = false;
        }
        if ($("#txtFTPFilePath").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>" + serverName + "FTPFilePath</li>";
            isSuccess = false;
        }
        if ($("#txtFTPUsername").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>" + serverName + "FTPUsername</li>";
            isSuccess = false;
        }
        if ($("#txtFTPPassword").val() == "" && $("#hdnReportScheduleId").val() == 0) {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>" + serverName + "FTPPassword</li>";
            isSuccess = false;
        }
    }
    if (transferType != "-1") {
        if ($("#txtErrorEmail").val() != "") {
            var filter1 = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            var errorEmails = $("#txtErrorEmail").val();
            errorEmails = errorEmails.toLowerCase();
            var errorEmailArray = errorEmails.split(",");
            outer:
                for (i = 0; i <= (errorEmailArray.length - 1) ; i++) {
                    $("#divEditValidations")[0].innerHTML + errorEmailArray[i];
                    for (k = i + 1; k <= (errorEmailArray.length - 1) ; k++) {
                        if (errorEmailArray[i] == errorEmailArray[k]) {
                            $("#divEditValidations")[0].innerHTML += "<li class='message'>Please enter a Unique Error E-mail Id.</li>";
                            isSuccess = false;
                            break outer;
                        }
                    }
                    if (!filter1.test(errorEmailArray[i])) {
                        $("#divEditValidations")[0].innerHTML += "<li class='message'>Please enter a Valid E-mail Id.</li>";
                        isSuccess = false;
                        break;
                    }
                }
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
        success: function (data) {

            newDate = data;
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });
    return newDate;
}

function ValidateFirstScheduledDate() {
    var checkedElements = $("#divWeeklyInterval input:checked"), firstScheduledRunDate = new Date($("#txtFirstScheduledRunDate").val());
    if (checkedElements.length > 0) {
        var tempDate = firstScheduledRunDate;
        var firstScheduledRunDateDay = firstScheduledRunDate.getDay();
        var weekElements = $("#divWeeklyInterval input[type=checkbox]");
        var i = firstScheduledRunDateDay;
        while (i < weekElements.length) {
            if (weekElements[i].checked) {
                tempDate.setDate(firstScheduledRunDate.getDate() + (i - firstScheduledRunDateDay));
                return { isChecked: true, Date: tempDate };
            }
            i++;
        }
        i = 0;
        while (i < firstScheduledRunDateDay){
            if (weekElements[i].checked) {
                tempDate.setDate(firstScheduledRunDate.getDate() + 7 + (i - firstScheduledRunDateDay));
                return { isChecked: true, Date: tempDate };
            }
            i++;
        }        
    }
    else {
        return { isChecked: false, Date: firstScheduledRunDate };
    }
}
