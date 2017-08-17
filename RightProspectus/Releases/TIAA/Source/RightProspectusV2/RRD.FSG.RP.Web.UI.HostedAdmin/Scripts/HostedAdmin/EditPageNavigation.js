window.onunload = function (e) {
    if (window.opener.$("#pageNavigationGrid").is(':visible')) {
        if (!window.opener.$("#pageNavigationGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};
$(document).ready(function () {

    $("#submitPageNavigation").click(function (e) {

        if (ValidatePageNavigationSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;
    });

    $("#comboPageNames").change(function () {
       LoadNavigationKeys();
      //LoadDefaultTextForNavigationKey();
    });
    
    $("#comboNavigationKey").change(function () {
        LoadDefaultTextForNavigationKey();
        
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

function ValidatePageNavigationSave() {
  
    var isSuccess = true;
    $("#divPageNavigationValidations").empty();

    $("#divPageNavigationValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divPageNavigationValidations")[0].innerHTML += "<ul>"


    if ($("#comboPageNames").val() == "-1") {
        $("#divPageNavigationValidations")[0].innerHTML += "<li class='message'>Page Name</li>";
        isSuccess = false;
    }

    if ($("#comboNavigationKey").val() == "-1") {
        $("#divPageNavigationValidations")[0].innerHTML += "<li class='message'>Navigation Key</li>";
        isSuccess = false;
    }
    

    if ($("#txtNavigationXML").val() == "") {
        $("#divPageNavigationValidations")[0].innerHTML += "<li class='message'>Navigation XML</li>";
        isSuccess = false;
    }
    else {
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
                    $("#divPageNavigationValidations")[0].innerHTML += "<li class='message'>" + item + ".</li>";
                    isSuccess = false;
                });


            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }

    $("#divPageNavigationValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divPageNavigationValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

function LoadNavigationKeys() {
    var selectedPageID = $("#comboPageNames option:selected").val();
   
    if (selectedPageID == -1) {
        $('#comboNavigationKey').empty();
        $('#comboNavigationKey').append($('<option>', {
            value: -1,
            text: "--Please select Page Name First--"
        }));
        LoadDefaultTextForNavigationKey();
    }
    else {
        $.ajax({
            type: 'GET',
            url: $("#dvLoadNavigationKey").data('request-url'),
            data: { pageID: selectedPageID },
            cache: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                
                 $('#comboNavigationKey').empty();
            
                $.each(data, function (i, item) {

                    $('#comboNavigationKey').append($('<option>', {
                        value: item.Value,
                        text: item.Display
                    }));

                });
                LoadDefaultTextForNavigationKey();
            },
            error: function (xhr) {
               
                alert(xhr.state + xhr.statusText);
            }
        });
    }
}



function LoadDefaultTextForNavigationKey() {
    var selectedPageID = $("#comboPageNames option:selected").val();
    var selectedNavigationKey = $("#comboNavigationKey option:selected").val();
    if (selectedNavigationKey == -1) {
        $('#txtNavigationXML').empty();
        $('#txtNavigationXML').val('');
    }
    else {
        $.ajax({
            type: 'GET',
            url: $("#dvLoadDefaultTextForNavigationKey").data('request-url'),
            data: { pageID: selectedPageID, navigationkey: selectedNavigationKey },
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
