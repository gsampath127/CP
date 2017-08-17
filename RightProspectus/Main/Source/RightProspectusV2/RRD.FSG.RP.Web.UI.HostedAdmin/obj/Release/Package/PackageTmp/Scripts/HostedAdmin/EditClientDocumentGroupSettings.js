window.onunload = function (e) {
    if (window.opener.$("#clientDocumentGroupGrid").is(':visible')) {
        if (!window.opener.$("#clientDocumentGroupGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
    $('#lstClientDocuments')
      .multiselect({
          numberDisplayed: 0,
          includeSelectAllOption: false,
          enableFiltering: true,
          filterBehavior: 'text',
          enableCaseInsensitiveFiltering: true,
          maxHeight: 200,
          maxWidth: 100
      }
   );
    $("#selectedClientDocuments").val($("#lstClientDocuments").val());
    LoadClientDocuments();
    $("#submitClientDocumentGroupId").click(function (e) {
        $("#selectedClientDocuments").val($("#lstClientDocuments").val());
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
    if ($("#txtName").val() != ""/* && $("#lstClientDocuments").val() != null*/) {
        var name = $('#txtName').val();
        var ClientDocumentGroupId = $("#ClientDocumentGroupId").val();
       if (Number(ClientDocumentGroupId) == 0) {
            $.ajax({
                type: 'GET',
                url: $("#dvCheckDataAlreadyExists").data('request-url'),
                data: {
                    name: name
                },
                cache: false,
                dataType: "json",
                async: false,
                traditional: true,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data) {
                        $("#divEditValidations")[0].innerHTML += "<li class='message'>This Client Document Group already exists.</li>";
                        isSuccess = false;
                    }
                },
                error: function (xhr) {

                    alert(xhr.state + xhr.statusText);
                }
            });
        }
        
    }

    else {
        $("#divEditValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
        $("#divEditValidations")[0].innerHTML += "<ul>"


        if ($("#txtName").val() == "") {
            $("#divEditValidations")[0].innerHTML += "<li class='message'>Name</li>";
            isSuccess = false;
        }

        //if ($("#lstClientDocuments").val() == null) {
        //    $("#divEditValidations")[0].innerHTML += "<li class='message'>Client Documents</li>";
        //    isSuccess = false;
        //}

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

function LoadClientDocuments()
{
    $.ajax({
        type: 'GET',
        url: $("#dvLoadClientDocuments").data('request-url'),
        cache: false,
        async: false,
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#lstClientDocuments').empty();
            $('#lstClientDocuments').multiselect('dataprovider', data);
            var ClientDocumentGroupId = $("#ClientDocumentGroupId").val();
            if (Number(ClientDocumentGroupId) != 0) {
                PreSelect_ClientDocuments(ClientDocumentGroupId);
            }
        },
        error: function (xhr) {
            alert(xhr.state + xhr.statusText);
        }
    });
}

function PreSelect_ClientDocuments(ClientDocumentGroupId) {
    $.ajax({
        type: 'GET',
        url: $("#dvPreSelectClientDocuments").data('request-url'),
        data: {
            clientDocumentGroupId: ClientDocumentGroupId
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
                $('#lstClientDocuments').multiselect('select', value);
            }
        }
    });
}
