window.onunload = function (e) {
    if (window.opener.$("#urlRewriteGrid").is(':visible')) {
        if (!window.opener.$("#urlRewriteGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

function BindUrlRewriteSave() {
    $("#submitUrlRewrite").click(function (e) {
        if (ValidateUrlRewriteSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;        
    });
}

$(document).ready(function () {
  
    
    BindUrlRewriteSave();

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

function ValidateUrlRewriteSave() {
   

   
    var isSuccess = true;
    $("#divUrlRewriteValidations").empty();
    var hdnUrlRewriteID = Number($("#hdnUrlRewriteID").val());
    
    $("#divUrlRewriteValidations")[0].innerHTML += "<p class='message'>Please Enter the below fields</p>";
    $("#divUrlRewriteValidations")[0].innerHTML += "<ul>"


    

    if ($("#txtPatternName").val() == "") {
        
        $("#divUrlRewriteValidations")[0].innerHTML += "<li class='message'>Pattern Name</li>";
        isSuccess = false;


        
    }
    else if (hdnUrlRewriteID == 0) {
       
        var patternName = $("#txtPatternName").val();
        $.ajax({
            type: 'GET',
            url: $("#dvCheckPatternNameAlreadyExists").data('request-url'),
            data: { patternName: patternName },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divUrlRewriteValidations")[0].innerHTML += "<li class='message'>Pattern Name already Exists</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }

    if ($("textarea#txtMatchPattern").val() == "") {
        $("#divUrlRewriteValidations")[0].innerHTML += "<li class='message'>Match Pattern</li>";
        isSuccess = false;
    }
    if ($("textarea#txtRewriteFormat").val() == "") {
        $("#divUrlRewriteValidations")[0].innerHTML += "<li class='message'>Rewrite Format</li>";
        isSuccess = false;
    }

    $("#divUrlRewriteValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divUrlRewriteValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

