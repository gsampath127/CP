window.onunload = function (e) {
    if (window.opener.$("#verticalXmlImportGrid").is(':visible')) {
        if (!window.opener.$("#verticalXmlImportGrid").html().trim() == '') {
            window.opener.LoadDatabySearchParameters();
            window.opener.LoadSearchParameters();
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
    BindVerticalXmlImportSubmit();
    if ($("#hdnSuccessOrFailedMessage").val() == "OK") {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Success",
            resizable: false,
            modal: true,
            draggable: false
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
    else if ($("#hdnSuccessOrFailedMessage").val() == "Error")
    {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Failed",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divErrorMessage").html())
            .center()
            .open();

        kendoWindow
       .find(".confirm")
       .click(function () {
           window.close();
       });
    }


});

function BindVerticalXmlImportSubmit() {
    $("#Submit").click(function (e) {
        if (ValidateVerticalXmlImportSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);
            return true;
        }
        return false;
    });
}

function ValidateVerticalXmlImportSave() {
    var isSuccess = true;
    $("#divAddVerticalXmlImportValidations").empty();

    $("#divAddVerticalXmlImportValidations")[0].innerHTML += "<p class='message'>Please Enter the below fields</p>";
    $("#divAddVerticalXmlImportValidations")[0].innerHTML += "<ul>"

    var upload = document.getElementById('uploadFile').value;

    if (upload == "") {
        $("#divAddVerticalXmlImportValidations")[0].innerHTML += "<li class='message'>Upload Vertical Import Xml file</li>";
        isSuccess = false;
    }

    $("#divAddVerticalXmlImportValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divAddVerticalXmlImportValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}
