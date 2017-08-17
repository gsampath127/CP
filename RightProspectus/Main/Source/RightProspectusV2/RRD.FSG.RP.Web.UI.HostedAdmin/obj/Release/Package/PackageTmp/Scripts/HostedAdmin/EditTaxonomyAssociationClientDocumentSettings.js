window.onunload = function (e) {
    if (window.opener.$("#taxonomyAssociationClientDocumentGrid").is(':visible')) {
        if (!window.opener.$("#taxonomyAssociationClientDocumentGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {

    $("#comboClientDocumentType").prop("disabled", true);
    $("#comboClientDocument").prop("disabled", true);
    $("#txtFileName").prop("disabled", true);

    $("#comboTaxonomyAssociation").change(function (e) {
        $("#comboClientDocumentType").val(-1);
        $("#comboClientDocument").val(-1);
        $("#txtFileName").val('');


      
        $("#comboClientDocument").prop("disabled", true);        
       

        if ($("#comboTaxonomyAssociation").val() != -1) {
            $("#comboClientDocumentType").prop("disabled", false);
        }
        else {
            $("#comboClientDocumentType").prop("disabled", true);
            $("#comboClientDocument").prop("disabled", true);
        }

    });
        
    $("#comboClientDocumentType").change(function (e) {
        $("#comboClientDocument").empty();
        $("#comboClientDocument").prop("disabled", true);
        $("#txtFileName").val('');

        $.ajax({
            type: 'GET',
                        url: $("#dvGetClientDocument").data('request-url'),
                        data: {
                            ClientDocTypeId: $("#comboClientDocumentType").val(),
                TaxonomyId: $("#comboTaxonomyAssociation").val()
            },
            cache: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#comboClientDocument').empty();

                $.each(data, function (i, item) {

                    $('#comboClientDocument').append($('<option>', {
                        value: item.Value,
                        text: item.Display
                    }));

                });                
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
     
        if ($("#comboClientDocumentType").val() != -1) {

            $("#comboClientDocument").prop("disabled", false);
        }
        else {
            $("#comboClientDocument").prop("enabled", false);
        }

    });


    $("#submitTaxonomyAssocClientDoc").click(function (e) {

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

    $("#comboClientDocument").change(function (e) {
        var ClientDocId = $('#comboClientDocument').val();


    });

    $("#comboClientDocument").change(function (e) {
        var ClientDocId = $("#comboClientDocument").val();
        if (ClientDocId != '') {
            $.ajax({
                url: $("#dvGetFileName").data('request-url'),
                data: {
                    ClientDocId: ClientDocId
                },
                success: function (f) {
                    $("#txtFileName").val(f);
                }
            })
        }
    }
)

});

function ValidateSave() {
    var isSuccess = true;
    $("#divTaxonomyAssociationClientDocValidations").empty();

    var TaxonomyId = $('#comboTaxonomyAssociation').val();
    var ClientDocType = $("#comboClientDocumentType").val();
    var ClientDocument = $("#comboClientDocument").val();
    if ($("#comboTaxonomyAssociation").val() != -1 && $("#comboClientDocumentType").val() != -1 && ($("#comboClientDocument").val() != -1
        && $("#comboClientDocument").val() != '--Please select Client Document--')
        ) {
       }

    else {
        $("#divTaxonomyAssociationClientDocValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
        $("#divTaxonomyAssociationClientDocValidations")[0].innerHTML += "<ul>";
        if ($("#comboTaxonomyAssociation").val() == -1) {
            $("#divTaxonomyAssociationClientDocValidations")[0].innerHTML += "<li class='message'>Taxonomy Association</li>";
            isSuccess = false;
        }

        if ($("#comboClientDocumentType").val() == -1) {
            $("#divTaxonomyAssociationClientDocValidations")[0].innerHTML += "<li class='message'>Client DocumentType</li>";
            isSuccess = false;
        }

        if ($("#comboClientDocument").val() == -1 || $("#comboClientDocument").val() != '--Please select Client Document--') {
            $("#divTaxonomyAssociationClientDocValidations")[0].innerHTML += "<li class='message'>Client Document</li>";
            isSuccess = false;
        }

        $("#divTaxonomyAssociationClientDocValidations")[0].innerHTML += "</ul>";
    }
    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });
        kendoWindow.data("kendoWindow")
            .content($("#divTaxonomyAssociationClientDocValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

