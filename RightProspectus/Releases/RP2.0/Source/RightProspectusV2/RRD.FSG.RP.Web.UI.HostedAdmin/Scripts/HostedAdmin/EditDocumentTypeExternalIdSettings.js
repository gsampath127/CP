window.onunload = function (e) {
    if (window.opener.$("#docTypeExternalIdGrid").is(':visible')) {
        if (!window.opener.$("#docTypeExternalIdGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {

    $("#submitDocTypeExternalId").click(function (e) {        

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
    $("#divDocumentTypeExternalIdValidations").empty();

    if ($("#comboDocumentTypes").val() != -1 && $("#txtExternalId").val() != "") {
        var DocumentTypeId = $('#comboDocumentTypes').val();
        var ExternalId = $("#txtExternalId").val();
        if ($('#comboDocumentTypes').is(':enabled')) {
            $.ajax({
                type: 'GET',
                url: $("#dvCheckCombinationDataAlreadyExists").data('request-url'),
                data: {
                    DocumentTypeId: DocumentTypeId,
                    ExternalID: ExternalId
                },
                cache: false,
                dataType: "json",
                async: false,
                traditional: true,
                contentType: "application/json; charset=utf-8",
                success: function(data) {
                    if (data) {
                        $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<li class='message'>This Document Type - ExternalId  combination already exists.</li>";
                        isSuccess = false;
                    }
                },
                error: function(xhr) {
                    alert(xhr.state + xhr.statusText);
                }
            });
            if ($("#txtExternalId").val() != "") {

                var externalID = $("#txtExternalId").val();

                $.ajax({
                    type: 'GET',
                    url: $("#dvCheckExternalIdAlreadyExists").data('request-url'),
                    data: {
                        ExternalId: externalID
                    },
                    cache: false,
                    dataType: "json",
                    async: false,
                    traditional: true,
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        if (data) {
                            $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<li class='message'>This ExternalId already exists.</li>";
                            isSuccess = false;
                        }
                    },
                    error: function(xhr) {

                        alert(xhr.state + xhr.statusText);
                    }
                });
            }
      }
    }

    else {
        $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
        $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<ul>";
        if ($("#comboDocumentTypes").val() == -1) {
            $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<li class='message'>Document Type</li>";
            isSuccess = false;
        }

        if ($("#txtExternalId").val() == "") {
            $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "<li class='message'>External Id</li>";
            isSuccess = false;
        }

        $("#divDocumentTypeExternalIdValidations")[0].innerHTML += "</ul>";
    }
    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });
        kendoWindow.data("kendoWindow")
            .content($("#divDocumentTypeExternalIdValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}