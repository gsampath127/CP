
function openTACDFPDF(id, pdfURL) {

    var toolsURL = $('#dvToolsURL').data('tools-url');
    if ($('#hdnISHTTPS').val().toLowerCase() == "https") {
        toolsURL = $('#dvHTTPSToolsURL').data('tools-url');
    }

    $('#pnlFrameContainer').show();
    $('#idTACDFFormNMFPPlaceHolderDiv').hide();

    $('#docFrame').attr('src', toolsURL + pdfURL);

    $("#pnlFrameContainer").height(($(window).height() - $(".cssTACDFBody").outerHeight()) + "px");
    $("#docFrame").height(($(window).height() - $(".cssTACDFBody").outerHeight()) + "px");
    $("#pnlFrameContainer").css('top', ($(".cssTACDFBody").outerHeight()) + "px");

    $('.top').removeClass("cssMenuTdActive").addClass("cssMenuTdInActive");
    $('#' + id).removeClass("cssMenuTdInActive");
    $('#' + id).addClass("cssMenuTdActive");   
}

function ShowFormNMFP() {

    $('#pnlFrameContainer').hide();
    $('#idTACDFFormNMFPPlaceHolderDiv').show();

    $('.top').removeClass("cssMenuTdActive").addClass("cssMenuTdInActive");
    $('#idFormNMFP').removeClass("cssMenuTdInActive");
    $('#idFormNMFP').addClass("cssMenuTdActive");
}

function trackSiteActivity(customer, site, externalID1, clientDocumentId, isInternalTAID) {
    var siteActivityData = { Customer: customer, Site: site, ExternalID1: externalID1, ClientDocumentId: clientDocumentId, IsInternalTAID: isInternalTAID, RequestBatchId: $('#hdnRequestBatchId').val()};
    $.ajax({
        type: 'POST',
        ontentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        url: $("#dvTrackSiteActivityURL").data('request-url'),
        data: siteActivityData,
        success: function (result) {

        },
        error: function () {
        }
    });
}

window.onload = function () {
    $('.cssMenuTd').addClass("cssMenuTdInActive");    
    openTACDFPDF($('#hdnPageLoadMenuID').val(), $('#hdnPageLoadPDFURL').val());
};

$(window).resize(function () {

    if ($(window).height() - $(".cssTACDFBody").outerHeight() > 0) {
        $("#pnlFrameContainer").height(($(window).height() - $(".cssTACDFBody").outerHeight()) + "px");
        $("#docFrame").height(($(window).height() - $(".cssTACDFBody").outerHeight()) + "px");
        $("#pnlFrameContainer").css('top', ($(".cssTACDFBody").outerHeight()) + "px");
    }
});