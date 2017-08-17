window.onunload = function (e) {
    if (!isParentReload) {        
        if (window.opener.$("#siteGrid").is(':visible')) {
            if (!window.opener.$("#siteGrid").html().trim() == '') {
                window.opener.LoadSearchParameters();
                window.opener.LoadDatabySearchParameters();                
            }
        }
        else {
            window.opener.LoadSearchParameters();
        }
    }
};
var isParentReload = false;
$(document).ready(function () {

    $("#submitSite").click(function (e) {

        if (ValidateSiteSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;        
    });

    $("#comboTemplateNames").change(function (e) {
        LoadDefaultPageNames();
    });

    $("#comboDefaultPageNames").change(function (e) {
        LoadSamplePreviewImage();
    });
    LoadSamplePreviewImage();

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

           if ($("#hdnDisableDefaultSiteCheckbox").val() != null && $("#hdnDisableDefaultSiteCheckbox").val().toLowerCase() == 'true') {
               isParentReload = true;
               window.opener.Reload();               
           }
           window.close();
       });
    }
});


function ValidateSiteSave() {
    var isSuccess = true;
    $("#divSiteValidations").empty();

    $("#divSiteValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divSiteValidations")[0].innerHTML += "<ul>"


    if ($("#txtSiteName").val() == "") {
        $("#divSiteValidations")[0].innerHTML += "<li class='message'>Site Name</li>";
        isSuccess = false;
    }
    else if ($("#hdnSiteID").val() == 0 || $("#hdnSiteName").val() != $("#txtSiteName").val()) {
        var siteName = $("#txtSiteName").val()
        $.ajax({
            type: 'GET',
            url: $("#dvCheckSiteNameAlreadyExists").data('request-url'),
            data: { siteName: siteName },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data)
                {
                    $("#divSiteValidations")[0].innerHTML += "<li class='message'>Site Name already exists.</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });       
    }

    if ($("#comboTemplateNames").val() == "-1") {
        $("#divSiteValidations")[0].innerHTML += "<li class='message'>Template Name</li>";
        isSuccess = false;
    }

    if ($("#comboDefaultPageNames").val() == "-1") {
        $("#divSiteValidations")[0].innerHTML += "<li class='message'>Default Page Name</li>";
        isSuccess = false;
    }

    if ($("#txtDescription").val() == "") {
        $("#divSiteValidations")[0].innerHTML += "<li class='message'>Description</li>";
        isSuccess = false;
    }

    $("#divSiteValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divSiteValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

function LoadDefaultPageNames() {
    var selectedTemplateID = $("#comboTemplateNames option:selected").val();
    if (selectedTemplateID == -1) {
        $('#comboDefaultPageNames').empty();
        $('#comboDefaultPageNames').append($('<option>', {
            value: -1,
            text: "--Please select Template First--"
        }));
        LoadSamplePreviewImage();
    }
    else {
        $.ajax({
            type: 'GET',
            url: $("#dvLoadDefaultPageNames").data('request-url'),
            data: { selectedTemplateID: selectedTemplateID },
            cache: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#comboDefaultPageNames').empty();

                $.each(data, function (i, item) {

                    $('#comboDefaultPageNames').append($('<option>', {
                        value: item.Value,
                        text: item.Display
                    }));

                });
                LoadSamplePreviewImage();
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }
}


function LoadSamplePreviewImage() {
    var selectedTemplateID = $("#comboTemplateNames option:selected").val();
    var selectedPageID = $("#comboDefaultPageNames option:selected").val();    

    if (selectedTemplateID == -1 || selectedPageID == -1) {
        $('#divTemplatePageImage').hide();
    }
    else {
        $('#divTemplatePageImage').show();
        $('#imgTemplatePage').attr('src', $('#hdnBaseURL').val() +'/Content/Images/TemplatePageImages/' + selectedTemplateID + '_' + selectedPageID + '.JPG');
    }
    
}
