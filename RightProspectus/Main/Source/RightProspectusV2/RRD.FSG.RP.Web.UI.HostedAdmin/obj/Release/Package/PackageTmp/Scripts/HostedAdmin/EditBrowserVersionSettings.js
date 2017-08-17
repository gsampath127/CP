window.onunload = function (e) {
    if (window.opener.$("#BrowserVersionGrid").is(':visible')) {
        if (!window.opener.$("#BrowserVersionGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {

    $("#submitBrowserVersion").click(function (e) {

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

function ValidateSave() {
    var isSuccess = true;
    $("#divBrowserVersiondValidations").empty();

    if ($("#comboBrowserName").val() != "-1" && $("#txtVersion").val() != "" && $("#txtDownloadURL").val() != "" && isUrlValid($("#txtDownloadURL").val())) {
        var Name = $('#txtName').val();
        var Version = $("#txtVersion").val();
        var DownloadUrl = $("#txtDownloadURL").val();

        //if ($('#comboDocumentTypes').is(':enabled')) {
        //    $.ajax({
        //        type: 'GET',
        //        url: $("#dvCheckCombinationDataAlreadyExists").data('request-url'),
        //        data: {
        //            DocumentTypeId: DocumentTypeId,
        //            ExternalID: ExternalId
        //        },
        //        cache: false,
        //        dataType: "json",
        //        async: false,
        //        traditional: true,
        //        contentType: "application/json; charset=utf-8",
        //        success: function (data) {
        //            if (data) {
        //                $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<li class='message'>This Document Type - ExternalId  combination already exists.</li>";
        //                isSuccess = false;
        //            }
        //        },
        //        error: function (xhr) {
        //            alert(xhr.state + xhr.statusText);
        //        }
        //    });
        //    if ($("#txtExternalId").val() != "") {

        //        var externalID = $("#txtExternalId").val();

        //        $.ajax({
        //            type: 'GET',
        //            url: $("#dvCheckExternalIdAlreadyExists").data('request-url'),
        //            data: {
        //                ExternalId: externalID
        //            },
        //            cache: false,
        //            dataType: "json",
        //            async: false,
        //            traditional: true,
        //            contentType: "application/json; charset=utf-8",
        //            success: function (data) {
        //                if (data) {
        //                    $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<li class='message'>This ExternalId already exists.</li>";
        //                    isSuccess = false;
        //                }
        //            },
        //            error: function (xhr) {

        //                alert(xhr.state + xhr.statusText);
        //            }
        //        });
        //    }
        //}
    }

    else {
        $("#divBrowserVersiondValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
        $("#divBrowserVersiondValidations")[0].innerHTML += "<ul>";
        if ($("#comboBrowserName").val() == "-1") {
            $("#divBrowserVersiondValidations")[0].innerHTML += "<li class='message'>Name</li>";
            isSuccess = false;
        }

        if ($("#txtVersion").val() == "") {
            $("#divBrowserVersiondValidations")[0].innerHTML += "<li class='message'>Version</li>";
            isSuccess = false;
        }        

        if ($("#txtDownloadURL").val() == "") {
            $("#divBrowserVersiondValidations")[0].innerHTML += "<li class='message'>Download URL</li>";
            isSuccess = false;
        }
        else if (!isUrlValid($("#txtDownloadURL").val())) {
            $("#divBrowserVersiondValidations")[0].innerHTML += "<li class='message'>Download URL is not valid</li>";
            isSuccess = false;
        }

        $("#divBrowserVersiondValidations")[0].innerHTML += "</ul>";
    }
    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });
        kendoWindow.data("kendoWindow")
            .content($("#divBrowserVersiondValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}


function isUrlValid(url) {
    var res = url.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    if (res == null)
        return false;
    else
        return true;
}