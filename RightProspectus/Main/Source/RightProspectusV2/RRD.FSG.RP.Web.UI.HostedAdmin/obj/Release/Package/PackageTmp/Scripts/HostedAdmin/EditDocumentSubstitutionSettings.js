window.onunload = function (e) {
    if (window.opener.$("#DocumentSubstitutionGrid").is(':visible')) {
        if (!window.opener.$("#DocumentSubstitutionGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
    debugger
    
    BindNDocumentType();
    $('#lstNDocumentType')
     .multiselect({
         numberDisplayed: 0,
         includeSelectAllOption: false,
         enableFiltering: true,
         filterBehavior: 'text',
         enableCaseInsensitiveFiltering: true,
         maxHeight: 200,
         maxWidth: 100
       });

    $("#submitDocumentSubstitution").click(function (e) {

        if (ValidateSave()) {
            var selectedNDocumentType = $('#lstNDocumentType').val();

            $("#selectedNDocumentType").val(selectedNDocumentType);
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
function BindNDocumentType() {
    LoadNDocumentType();
}

function LoadNDocumentType() {

    $.ajax({
        type: 'GET',
        url: $("#dvLoadNDocumentType").data('request-url'),
        data: {  },
        cache: false,
        async: false,
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#lstNDocumentType').multiselect('dataprovider', data);
            var DocumentSubstitutionId = $("#hdnDocumentSubstitutionID").val();            
            if (Number(DocumentSubstitutionId) != 0) {
                PreSelect_NDocumentType(DocumentSubstitutionId);
            }
        },
        error: function (xhr) {
            alert(xhr.state + xhr.statusText);
        }
    });
}
function ValidateSave() {
    var isSuccess = true;
    $("#divDocumentSubstitutionValidations").empty();
    debugger
    var NDocumentType = $("#lstNDocumentType").val();
    if ($("#comboDocumentType").val() != "-1" && ($("#comboSubstituteDocumentType").val() != "-1" || $("#lstNDocumentType").val()!=null)) {

    }

    else {
        $("#divDocumentSubstitutionValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
        $("#divDocumentSubstitutionValidations")[0].innerHTML += "<ul>";
        if ($("#comboDocumentType").val() == "-1") {
            $("#divDocumentSubstitutionValidations")[0].innerHTML += "<li class='message'>Document Type</li>";
            isSuccess = false;
        }

        if ($("#comboSubstituteDocumentType").val() == "-1" || $("#lstNDocumentType").val() != null) {
            $("#divDocumentSubstitutionValidations")[0].innerHTML += "<li class='message'>Either select Substitute Document Type or NDocumentType</li>";
            isSuccess = false;
        }

        $("#divDocumentSubstitutionValidations")[0].innerHTML += "</ul>";
    }
    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });
        kendoWindow.data("kendoWindow")
            .content($("#divDocumentSubstitutionValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

function PreSelect_NDocumentType(DocumentSubstitutionId) {
    $.ajax({
        type: 'GET',
        url: $("#dvPreSelectNDocumentType").data('request-url'),
        data: {
            documentTypeId: DocumentSubstitutionId
        },
        dataType: 'json',
        success: function (data) {
            if (data.length >= 1) {
                var value = "";
                $.each(data, function (i, item) {
                    if (value == "") {
                        value += item.value;
                    } else {
                        value += "," + item.value;
                    }
                });
                value = value.split(',');
                $('#lstNDocumentType').multiselect('select', value);
            }
        }
    });
}


