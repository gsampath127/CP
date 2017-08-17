$(document).ready(function () {

    $("#btnSelectCustomer").click(function (e) {
        return ValidateCustomerSelection();
    });

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSelectCustomer').focus().click();
        }
    });
});


function ValidateCustomerSelection() {

    var isSuccess = true;
    $("#divCustomerSelectionValidations").empty();

    $("#divCustomerSelectionValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divCustomerSelectionValidations")[0].innerHTML += "<ul>"


    if ($("#comboCustomerNames").val() == "-1") {
        $("#divCustomerSelectionValidations")[0].innerHTML += "<li class='message'>Customer Name</li>";
        isSuccess = false;
    }   

    $("#divCustomerSelectionValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divCustomerSelectionValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}