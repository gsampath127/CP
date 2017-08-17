window.onunload = function (e) {
    if (window.opener.$("#taxonomyLevelExternalGrid").is(':visible')) {
        if (!window.opener.$("#taxonomyLevelExternalGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();           
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
  
    $("#submitTaxonomyLeveExternalId").click(function (e) {
       
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

    if ($("#ComboLevel").val() != -1 && $("#comboTaxonomyId").val() != -1 && $("#txtExternalId").val() != "" && $("#hdnSelectedTaxnomyId").val() < 0) {
        var levelID = $('#ComboLevel').val();
        var taxonomyID = $("#comboTaxonomyId").val();
        var externalID = $("#txtExternalId").val();
    
    
        $.ajax({
            type: 'GET',
            url: $("#dvCheckDataAlreadyExists").data('request-url'),
            data: {
                levelID: levelID,
                TaxonomyID: taxonomyID,
                externalID: externalID
            },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divEditValidations")[0].innerHTML += "<li class='message'>This Taxonomy Level External already exists.</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
        if ($("#txtExternalId").val() != "") {

            var externalID = $("#txtExternalId").val();

            $.ajax({
                type: 'GET',
                url: $("#dvCheckExternalIdAlreadyExists").data('request-url'),
                data: {

                    externalID: externalID
                },
                cache: false,
                dataType: "json",
                async: false,
                traditional: true,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data) {
                        $("#divEditValidations")[0].innerHTML += "<li class='message'>This ExternalId already exists.</li>";
                        isSuccess = false;
                    }
                },
                error: function (xhr) {

                    alert(xhr.state + xhr.statusText);
                }
            });
        }
    }
   
    else
    { 
    $("#divEditValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divEditValidations")[0].innerHTML += "<ul>"
   

    if ($("#ComboLevel").val() == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Level</li>";
        isSuccess = false;
    } 
    
    if ($("#comboTaxonomyId").val() == "-1") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>Taxonomy Id</li>";
        isSuccess = false;
    }

    if ($("#txtExternalId").val() == "") {
        $("#divEditValidations")[0].innerHTML += "<li class='message'>External Id</li>";
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