
function RadioCheck(val) {
    if (val == 'Electronic') {
        $("#dvPrintText").hide();
        $("#dvEmailText").show();
    }
    else if (val == 'U.S. Mail') {
        $("#dvEmailText").hide();
        $("#dvPrintText").show();
    }
}

function EmailConfirmBack() {

    $("#dvEmailConfirm").hide();
    $("#dvRequestMaterialEmail").show();
}
function PrintConfirmBack() {
    $("#dvPrintConfirm").hide();
    $("#dvRequestMaterialPrint").show();
}
function dvRequestMaterialEmail_Back() {
    $("#dvRequestMaterialEmail").hide();
    $("#dvRequestMaterialMenu").show();
}
function dvRequestMaterialPrint_Back() {
    $("#dvRequestMaterialPrint").hide();
    $("#dvRequestMaterialMenu").show();
}
function dvRequestMaterialMenu_Next() {
    if ($("#radioUSMail").is(':checked')) {
        $("#dvRequestMaterialMenu").hide();
        $("#dvRequestMaterialPrint").show();

    }
    else {
        $("#dvRequestMaterialMenu").hide();
        $("#dvRequestMaterialEmail").show();

    }
}

function dvRequestMaterialEmail_Back() {
    $(".wizardStep").hide();
    $("#dvRequestMaterialMenu").show();
}
function dvRequestMaterialPrint_Back() {
    $("#dvRequestMaterialPrint").hide();
    $("#dvRequestMaterialMenu").show();
}

function dvRequestMaterialEmail_Next() {

    var allVals = [];
    $('#dvcheckedEmail :checked').each(function () {
        allVals.push($(this).val());
    });
    if (ValidateEmail(allVals)) {
        $("#dvConfirmEmailDocType").empty();
        $("#dvConfirmEmailDocType").append("<ul>");
        for (var i = 0; i < allVals.length; i++) {
            $("#dvConfirmEmailDocType").append("<li>" + allVals[i].split(':')[1] + "</li>");
        }
        $("#dvConfirmEmailDocType").append("</ul>");
        $("#dvConfirmEmailDocType").append("</br>");
        $("#dvConfirmEmailDocType").append("</br>");
        $("#dvConfirmEmailDocType").append("will be delivered to:");
        $("#dvConfirmEmailDocType").append("</br>");
        $("#dvConfirmEmailDocType").append($('#txtEmail').val());
        $("#dvRequestMaterialEmail").hide();
        $("#dvEmailConfirm").show();
    }


}



function dvRequestMaterialConfirm_Request() {
    alert("Process Request.");
}

function cancel() {
    $("#dialog").dialog("close");
}

function dvRequestMaterialPrint_Next() {
    var allVals = [];
    $('#dvCheckedPrintDocType :checked').each(function () {
        allVals.push($(this).val());
    });
    if (ValidatePrint(allVals)) {
        $("#dvConfirmPrintDocType").empty();
        $("#dvConfirmPrintDocType").append("<ul>");
        for (var i = 0; i < allVals.length; i++) {
            $("#dvConfirmPrintDocType").append("<li>" + allVals[i].split(':')[1] + "</li>");
        }
        $("#dvConfirmPrintDocType").append("</ul>");
        $("#dvConfirmPrintDocType").append("</br>");
        $("#dvConfirmPrintDocType").append("</br>");
        $("#dvConfirmPrintDocType").append("will be delivered to:");
        $("#dvConfirmPrintDocType").append("</br>");
        $("#dvConfirmPrintDocType").append($('#txtFirstName').val());
        $("#dvConfirmPrintDocType").append($('#txtLastName').val());
        $("#dvConfirmPrintDocType").append("</br>");
        $("#dvConfirmPrintDocType").append($('#txtCompanyName').val());
        $("#dvConfirmPrintDocType").append("</br>");
        $("#dvConfirmPrintDocType").append($('#txtAddress1').val());
        $("#dvConfirmPrintDocType").append("</br>");
        $("#dvConfirmPrintDocType").append($('#txtAddress2').val());
        $("#dvConfirmPrintDocType").append("</br>");
        $("#dvConfirmPrintDocType").append($('#txtCity').val() + "," + $('#ddlState').val() + " " + $('#txtZip').val());

        $("#dvRequestMaterialPrint").hide();
        $("#dvPrintConfirm").show();
    }

}
function SaveEmailDetails() {


    var clientName = $("#hdnClientName").val();
    var hdnRequestBatchId = $("#hdnRequestBatchId").val();
    var siteName = $("#hdnSiteName").val();

    var email = $("#txtEmail").val();
    var allVals = [];
    $('#dvcheckedEmail :checked').each(function () {
        allVals.push($(this).val());
    });


    var taxonomyAssociationID = $("#taxanomyAssociationId").val();


    var siteEmailData = {
        ClientName: clientName, SiteName: siteName, Email: email, SelectedDocTypes: String(allVals), TaxanomyAssociationId: taxonomyAssociationID, RequestBatchId: hdnRequestBatchId
    };

    $.ajax({
        type: 'POST',
        ontentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        url: $("#dvRequestMaterialAddEmailDetails").data('request-url'),
        data: siteEmailData,
        success: function (result) {
            alert("Success");
        },
        error: function () {
        }
    });

    $("#dvEmailConfirm").hide();
    $("#dvEmailSentConfirm").show();
}

function SavePrintDetails() {

    var clientName = $("#hdnClientName").val();
    var hdnRequestBatchId = $("#hdnRequestBatchId").val();

    var siteName = $("#hdnSiteName").val();

    var allVals = [];
    $('#dvCheckedPrintDocType :checked').each(function () {
        allVals.push($(this).val());
    });

    var companyName = $("#txtCompanyName").val();
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtLastName").val();
    var address1 = $("#txtAddress1").val();
    var address2 = $("#txtAddress2").val();
    var city = $("#txtCity").val();
    var state = $("#ddlState").val();
    var postalCode = $("#txtZip").val();


    var taxonomyAssociationID = $("#taxanomyAssociationId").val();


    var sitePrintData = {
        ClientName: clientName, SiteName: siteName, SelectedDocTypes: String(allVals), TaxanomyAssociationId: taxonomyAssociationID, RequestBatchId: hdnRequestBatchId,
        CompanyName: companyName, FirstName: firstName, LastName: lastName, Address1: address1,
        Address2: address2, City: city, StateOrProvince: state, PostalCode: postalCode
    };

    $.ajax({
        type: 'POST',
        ontentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        url: $("#dvRequestMaterialSavePrintDetails").data('request-url'),
        data: sitePrintData,
        success: function (result) {

        },
        error: function () {
        }
    });

    $("#dvPrintConfirm").hide();
    $("#dvPrintSentConfirm").show();
    $("#dvPrintConfirmtext").append("Delivery information for ");
    $("#dvPrintConfirmtext").append($("#txtFirstName").val());
    $("#dvPrintConfirmtext").append(" ");
    $("#dvPrintConfirmtext").append($("#txtLastName").val());
    $("#dvPrintConfirmtext").append(" has been saved successfully!");
}

function ValidateEmail(checkedDocs) {

    var isSuccess = true;
    $("#divRequestMaterialValidations").empty();

    $("#divRequestMaterialValidations")[0].innerHTML += "<p class='message'>Please Enter the below fields</p>";
    $("#divRequestMaterialValidations")[0].innerHTML += "<ul></ul>"

    var allVals = [];
    allVals = checkedDocs;

    if ($("#txtEmail").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Email</li>";
        isSuccess = false;
    }
    else {
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (!filter.test($("#txtEmail").val())) {
            $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Please enter valid E-mail Id.</li>";
            isSuccess = false;
        }
    }


    if (allVals.length <= 0) {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Please Select atleast one Document(s)</li>";
        isSuccess = false;
    }

    if (!isSuccess) {
        $("#divRequestMaterialValidations").show();
        $("#divRequestMaterialValidations").dialog({
            resizable: false,
            draggable: false,
            hide: "scale",
            show: { effect: "scale" },
            modal: true,
            title: "Alert"
        });
        setTimeout(function () { $(".ui-dialog-titlebar button").blur(); }, 500);
    }
    return isSuccess;

}

function ValidatePrint(checkedDocs) {
    var isSuccess = true;
    $("#divRequestMaterialValidations").empty();

    $("#divRequestMaterialValidations")[0].innerHTML += "<p class='message'>Please Enter the below fields</p>";
    $("#divRequestMaterialValidations")[0].innerHTML += "<ul></ul>"

    var allVals = [];
    allVals = checkedDocs;


    if ($("#txtFirstName").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>First Name</li>";
        isSuccess = false;
    }
    if ($("#txtLastName").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Last Name</li>";
        isSuccess = false;
    }
    if ($("#txtAddress1").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Address1</li>";
        isSuccess = false;
    }
    if ($("#txtAddress2").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Address2</li>";
        isSuccess = false;
    }
    if ($("#txtCity").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>City</li>";
        isSuccess = false;
    }
    if ($("#ddlState").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>State</li>";
        isSuccess = false;
    }
    if ($("#txtZip").val() == "") {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Zip</li>";
        isSuccess = false;
    }
    if (allVals.length <= 0) {
        $("#divRequestMaterialValidations").find("ul")[0].innerHTML += "<li class='message'>Please Select atleast one Document(s)</li>";
        isSuccess = false;
    }

    if (!isSuccess) {
        $("#divRequestMaterialValidations").show();
        $("#divRequestMaterialValidations").dialog({
            resizable: false,
            draggable: false,
            hide: "scale",
            show: { effect: "scale" },
            modal: true,
            title: "Alert"
        });
        setTimeout(function () { $(".ui-dialog-titlebar button").blur(); }, 500);
    }
    return isSuccess;
}