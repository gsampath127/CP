window.onunload = function (e) {
    if (window.opener.$("#clientDocumentTypeGrid").is(':visible')) {
        if (!window.opener.$("#clientDocumentTypeGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};


$(document).ready(function () {

    $("#submitClientDocumentTypeId").click(function (e) {
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
    $("#divEditValidations").empty();

    if ($("#txtName").val() != "" && $("#txtDescription").val() != "") {
        var name = $('#txtName').val();
        var description = $("#txtDescription").val();
        var currentDocTypeID = $("#ClientDocumentTypeId").val();

        $.ajax({
            type: 'GET',
            url: $("#dvCheckDataAlreadyExists").data('request-url'),
            data: {
                name: name,
                description: description,
                currentID: currentDocTypeID
                
            },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divEditValidations")[0].innerHTML += "<li class='message'>This Client Document type already exists.</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
       
    }

    else {
        $("#divEditValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
        $("#divEditValidations")[0].innerHTML += "<ul>"


        if ($("#txtName").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Name</li>";
            isSuccess = false;
        }

        if ($("#txtDescription").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Description</li>";
            isSuccess = false;
        }


        $("#divEditValidations")[0].innerHTML += "</ul>"
    }
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
