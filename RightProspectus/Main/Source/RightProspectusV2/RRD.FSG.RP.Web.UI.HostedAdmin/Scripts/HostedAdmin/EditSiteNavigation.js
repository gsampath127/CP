window.onunload = function (e) {
    if (window.opener.$("#SiteNavigationGrid").is(':visible')) {
        if (!window.opener.$("#SiteNavigationGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};
$(document).ready(function () {

    $("#submitSiteNavigation").click(function (e) {

        if (ValidateSiteNavigationSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;
    });

    $("#PageNameCombo").change(function (e) {
        LoadNavigationKeys();
    });

    $("#ComboNavigationKey").change(function (e) {
        LoadPagesForNavigationKeys();
        LoadDefaultNavigationXMLForNavigationKey();
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


function ValidateSiteNavigationSave() {
    var isSuccess = true;
    $("#divSiteNavigationValidations").empty();

    $("#divSiteNavigationValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divSiteNavigationValidations")[0].innerHTML += "<ul>"

    if ($("#ComboNavigationKey").val() == "-1") {
        $("#divSiteNavigationValidations")[0].innerHTML += "<li class='message'>Navigation Key</li>";
        isSuccess = false;
    }

    if ($("#txtNavigationXML").val() == "") {
        $("#divSiteNavigationValidations")[0].innerHTML += "<li class='message'>NavigationXML</li>";
        isSuccess = false;
    }
    else
    {
        $.ajax({
            type: 'GET',
            url: $("#dvValidateXml").data('request-url'),
            data: { navXml: $("#txtNavigationXML").val() },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {                
                $.each(data, function (i, item) {
                    $("#divSiteNavigationValidations")[0].innerHTML += "<li class='message'>" + item + ".</li>";
                    isSuccess = false;
                });
                    
                
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }

    if ($("#ComboNavigationKey").val() != "" && $("#txtNavigationXML").val() != "") {
        var navigationKey = $("#ComboNavigationKey").val()
        var pageId = $("#comboPageNames").val()
        $.ajax({
            type: 'GET',
            url: $("#dvLoadCheckSiteNavigationAlreadyExists").data('request-url'),
            data: { navigationKey: navigationKey,pageId: pageId },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divSiteNavigationValidations")[0].innerHTML += "<li class='message'>Navigation Key already exists.</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }

    $("#divSiteNavigationValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divSiteNavigationValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

function LoadPagesForNavigationKeys() {
    var selectedNavigationKey = $("#ComboNavigationKey option:selected").val();
    if (selectedNavigationKey == "") {
        $('#txtNavigationXML').empty();
        $('#txtNavigationXML').val('');
    }
    else {
        $.ajax({
            type: 'GET',
            url: $("#dvLoadPagesForNavigationKey").data('request-url'),
            data: { navigationkey: selectedNavigationKey },
            cache: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#comboPageNames').empty();
                $.each(data, function (i, item) {
                    $('#comboPageNames').append($('<option>', {
                        value: item.Value,
                        text: item.Display
                    }));

                });
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }
}
function LoadDefaultNavigationXMLForNavigationKey() {
    var selectedNavigationKey = $("#ComboNavigationKey option:selected").val();
    if (selectedNavigationKey == -1) {
        $('#txtNavigationXML').empty();
        $('#txtNavigationXML').val('');
    }
    else {
        $.ajax({
            type: 'GET',
            url: $("#dvLoadDefaultNavigationXMLForNavigationKey").data('request-url'),
            data: { navigationkey: selectedNavigationKey },
            cache: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                $('#txtNavigationXML').empty();
                $('#txtNavigationXML').val(data);
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }

        });
    }
}
