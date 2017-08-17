
window.onunload = function (e) {
    if (window.opener.$("#clientGrid").is(':visible')) {
        if (!window.opener.$("#clientGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};


$(document).ready(function () {
    LoadClientUsers();
    BindClientSave();
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

function LoadClientUsers() {   
    $.ajax({
        url: $("#dvGetClientUsers").data('request-url'),
        type: 'GET',
        data: { clientID: $('#hdnClientID').val() },
        dataType: 'json',
        cache: false,
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#optClientUsers').empty();
            $.each(data, function (i, item) {
                $('#optClientUsers').append($('<option />').val(this.Value).text(this.Display).prop('selected', this.Selected));
            });
            SetDualListBox();
        },
        error: (function (jqXHR, textStatus, errorThrown) {
            alert('Failed:' + errorThrown);
        })
    });
}

function SetDualListBox() {
    var clientUserDualLstBox = $('#optClientUsers').bootstrapDualListbox({
        nonSelectedListLabel: 'Users Not Associated',
        selectedListLabel: 'Associated Users',
        preserveSelectionOnMove: 'moved',
        moveOnSelect: false
    });
}

function BindClientSave() {
    $("#submitClient").click(function (e) {        
        $("#hdnUserIDs").val(GetSelectedUsers());
        if (ValidateClientSave())
        {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;
    });

}
function ValidateClientSave() {

    var isSuccess = true;
    $("#divEditClientValidations").empty();

    $("#divEditClientValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divEditClientValidations")[0].innerHTML += "<ul>"

    if ($("#txtClientName").val() == "") {
        $("#divEditClientValidations")[0].innerHTML += "<li class='message'>Client Name</li>";
        isSuccess = false;
    }
    else if ($("#hdnClientID").val() == 0 || $("#hdnClientName").val() != $("#txtClientName").val()) {
        var clientName = $("#txtClientName").val()
        $.ajax({
            type: 'GET',
            url: $("#dvCheckClientNameAlreadyExists").data('request-url'),
            data: { clientName: clientName },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divEditClientValidations")[0].innerHTML += "<li class='message'>Client Name already exists.</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }


    if ($("#ComboClientConnectionStringNames").val() == "-1") {
        $("#divEditClientValidations")[0].innerHTML += "<li class='message'>Client Connection String Name</li>";
        isSuccess = false;
    }
    if ($("#ComboVerticalMarket").val() == "-1") {
        $("#divEditClientValidations")[0].innerHTML += "<li class='message'>Vertical Market</li>";
        isSuccess = false;
    }
    if ($("#txtDatabaseName").val() == "") {
        $("#divEditClientValidations")[0].innerHTML += "<li class='message'>Database Name</li>";
        isSuccess = false;
    }
    if ($("#txtClientDescription").val() == "") {
        $("#divEditClientValidations")[0].innerHTML += "<li class='message'>Description</li>";
        isSuccess = false;
    }

    $("#divEditClientValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divEditClientValidations").html())
            .center()
            .open();
    }
    return isSuccess;
    
}
function GetSelectedUsers() {
    var selectedUserIds = '';
    $('#optClientUsers :selected').each(function (i, selected) {
        if (selectedUserIds != '') {
            selectedUserIds = selectedUserIds + ',';
        }
        selectedUserIds = selectedUserIds + $(selected).val();
    });
    return selectedUserIds;
}
