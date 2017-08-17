window.onunload = function (e) {
    if (window.opener.$("#verticalXmlExportGrid").is(':visible')) {
        if (!window.opener.$("#verticalXmlExportGrid").html().trim() == '') {
            window.opener.LoadDatabySearchParameters();
            window.opener.LoadSearchParameters();
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
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
    else if ($("#hdnSuccessOrFailedMessage").val() == "Error") {
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

