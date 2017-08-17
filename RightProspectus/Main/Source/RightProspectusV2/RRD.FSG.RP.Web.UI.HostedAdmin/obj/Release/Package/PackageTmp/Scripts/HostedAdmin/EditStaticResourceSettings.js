window.onunload = function (e) {
    if (window.opener.$("#staticResourceGrid").is(':visible')) {
        if (!window.opener.$("#staticResourceGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
    BindStaticResourceSubmit();
    if ($("#hdnSuccessOrFailedMessage").val() == "OK") {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
                title : "Success",
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


function BindStaticResourceSubmit() {
    $("#Submit").click(function (e) {
       
            if (ValidateStaticResourceSave()) {
                var element = $(document.body);
                kendo.ui.progress(element, true);

                return true;
            }
            return false;            
        
    });
}

function ValidateStaticResourceSave() {

    var isSuccess = true;
    $("#divStaticResourceValidations").empty();

    $("#divStaticResourceValidations")[0].innerHTML += "<p class='message'>Please Enter the below fields</p>";
    $("#divStaticResourceValidations")[0].innerHTML += "<ul>"
    var fileName = $("#uploadFile").val();
    
    if ($("#txtFileName").val() == "") {

        $("#divStaticResourceValidations")[0].innerHTML += "<li class='message'>File Name</li>";
        isSuccess = false;
    }
    else if ($("#hdnStaticResourceId").val() == 0) {
        var fileName = $("#txtFileName").val();
        $.ajax({
            type: 'GET',
            url: $("#dvCheckUniqueFileName").data('request-url'),
            data: { fileName: fileName },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divStaticResourceValidations")[0].innerHTML = "";
                    $("#divStaticResourceValidations")[0].innerHTML += "<li class='message'>File Name already Exists</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }
  
    
    if ($("#uploadFile").val().length == 0) {
        $("#divStaticResourceValidations")[0].innerHTML += "<li class='message'>Upload a File</li>";
        isSuccess = false;
    }

    $("#divStaticResourceValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divStaticResourceValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

