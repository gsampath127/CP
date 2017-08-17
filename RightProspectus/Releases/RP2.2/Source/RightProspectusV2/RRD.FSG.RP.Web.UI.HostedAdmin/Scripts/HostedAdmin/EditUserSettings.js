window.onunload = function (e) {
    if (window.opener.$("#userGrid").is(':visible')) {
        if (!window.opener.$("#userGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};


$(document).ready(function () {

    $('#spnADPopulate').mouseover(function (event) {
        var item = $(event.target);
        // $('#tooltipText').css({ 'top': $('#' + item).offset().top - 30, 'left': $('#' + item).offset().left + 40, 'position': 'absolute' });
        $('#dvtooltipText').css({ 'top': $(item).offset().top - 120, 'left': $(item).offset().left - 72, 'position': 'absolute', 'z-index': '2147483647' });

        $('#dvtooltipText').show();
    });
    $('#spnADPopulate').mouseout(function (event) {
        $('#dvtooltipText').hide();
    });
    $('#spnADPopulate').click(function () {
        
        GetUserDetailsFromAD($("#txtUserName").val());
    });

    LoadUserClients();
    BindUserSave();

   

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

function LoadUserClients() {
    $.ajax({
        url: $("#dvGetUserClients").data('request-url'),
        type: 'GET',
        data: { userID: $('#hdnUserID').val() },
        dataType: 'json',
        cache: false,
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#optUserClients').empty();
            $.each(data, function (i, item) {
                $('#optUserClients').append($('<option />').val(this.Value).text(this.Display).prop('selected', this.Selected));
            });
            SetDualListBox();
        },
        error: (function (jqXHR, textStatus, errorThrown) {
            alert('Failed:' + errorThrown);
        })
    });
}

function SetDualListBox() {
    var userClientDualLstBox = $('#optUserClients').bootstrapDualListbox({
        nonSelectedListLabel: 'Clients Not Associated',
        selectedListLabel: 'Associated Clients',
        preserveSelectionOnMove: 'moved',
        moveOnSelect: false
    });
}

function BindUserSave() {
    $("#submitUser").click(function (e) {
        $("#hdnClientIDs").val(GetSelectedClients());

        if (ValidateUserSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;        
    });

}
function ValidateUserSave() {

    var isSuccess = true;
    $("#divEditUserValidations").empty();

    $("#divEditUserValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divEditUserValidations")[0].innerHTML += "<ul>"

    if ($("#txtUserName").val() == "") {
        $("#divEditUserValidations")[0].innerHTML += "<li class='message'>User Name</li>";
        isSuccess = false;
    }
    else if ($("#hdnUserID").val() == 0 || $("#hdnUserName").val() != $("#txtUserName").val()) {
        var userName = $("#txtUserName").val()
        $.ajax({
            type: 'GET',
            url: $("#dvCheckUserNameAlreadyExists").data('request-url'),
            data: { userName: userName },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divEditUserValidations")[0].innerHTML += "<li class='message'>User Name already exists.</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    }


    if ($("#ComboClientConnectionStringNames").val() == "-1") {
        $("#divEditUserValidations")[0].innerHTML += "<li class='message'>User Name</li>";
        isSuccess = false;
    }
     
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (!filter.test($("#txtEmail").val())) {
            $("#divEditUserValidations")[0].innerHTML += "<li class='message'>Valid E-mail Id.</li>";
            isSuccess = false;
     }
   
    

    $("#divEditUserValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divEditUserValidations").html())
            .center()
            .open();
    }
    return isSuccess;

}
function GetSelectedClients() {
    var selectedClientIds = '';
    $('#optUserClients :selected').each(function (i, selected) {
        if (selectedClientIds != '') {
            selectedClientIds = selectedClientIds + ',';
        }
        selectedClientIds = selectedClientIds + $(selected).val();
    });
    return selectedClientIds;
}

function GetUserDetailsFromAD(strADID) {

    var element = $(document.body);
    kendo.ui.progress(element, true);

    $.ajax({

        type: 'GET',
        url: $("#dvUserDetails").data('request-url'),
        data: {
            LoggedInUserID: strADID
        },
        dataType: 'json',
        success: function (data) {
            var element = $(document.body);
            kendo.ui.progress(element, false);

            if (data != null) {
                $("#txtFirstName").val(data.FirstName);
                $("#txtLastName").val(data.LastName);
                $("#txtEmail").val(data.Email);
            }
            else {
                alert("Active Directory Fetch failed. Please enter manually.")
            }

        }
    });
}
