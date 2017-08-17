
function openpdf(id, pdfURL) {
  
    if (!pdfURL.trim()) {
        $('#pnlFrameContainer').hide();
        $('#idTADFDocumentNotAvailablePlaceHolderDiv').show();

    }
    else {
        var toolsURL = $('#dvToolsURL').data('tools-url');
        if ($('#hdnISHTTPS').val().toLowerCase() == "https")
        {            
            toolsURL = $('#dvHTTPSToolsURL').data('tools-url');            
        }

        $('#pnlFrameContainer').show();
        $('#idTADFDocumentNotAvailablePlaceHolderDiv').hide();

        $('#docFrame').attr('src', toolsURL + pdfURL);

        $("#pnlFrameContainer").height(($(window).height() - $(".cssTADFBody").outerHeight()) + "px");
        $("#docFrame").height(($(window).height() - $(".cssTADFBody").outerHeight()) + "px");
        $("#pnlFrameContainer").css('top', ($(".cssTADFBody").outerHeight()) + "px");

        $('.top').removeClass("cssMenuTdActive").addClass("cssMenuTdInActive");
        $('#' + id).removeClass("cssMenuTdInActive");
        $('#' + id).addClass("cssMenuTdActive");

        $('#docFrame').load(function () {

            $('#pnlFrameContainer').removeClass("cssTADFXBRLPlaceHolderDiv");
            $('#pnlFrameContainer').addClass("cssTADFPDFPlaceHolderDiv");
        });
    }
}

function loadXBRL(id) {
    
    $('#pnlFrameContainer').show();
    $('#idTADFDocumentNotAvailablePlaceHolderDiv').hide();

    var xbrlHrefURL = $('#dvXbrlHrefURL').data('xbrlhref-url');
    xbrlHrefURL = xbrlHrefURL.replace(/\&amp;/g, '&');
    $('#docFrame').attr('src', xbrlHrefURL);

    if ($(window).height() - $(".cssTADFBody").outerHeight() > 0) {
        $("#pnlFrameContainer").height(($(window).height() - $(".cssTADFBody").outerHeight()) + "px");
        $("#docFrame").height(($(window).height() - $(".cssTADFBody").outerHeight()) + "px");
        $("#pnlFrameContainer").css('top', ($(".cssTADFBody").outerHeight()) + "px");
    }
    $('.top').removeClass("cssMenuTdActive").addClass("cssMenuTdInActive");
    $('#' + id).removeClass("cssMenuTdInActive");
    $('#' + id).addClass("cssMenuTdActive");

    $('#docFrame').load(function () {

        $('#pnlFrameContainer').removeClass("cssTADFPDFPlaceHolderDiv");
        $('#pnlFrameContainer').addClass("cssTADFXBRLPlaceHolderDiv");
    });
}

function trackSiteActivity(customer, site, externalID1, externalID2, isInternalTAID) {
    var siteActivityData = { Customer: customer, Site: site, ExternalID1: externalID1, ExternalID2: externalID2, IsInternalTAID: isInternalTAID, RequestBatchId: $('#hdnRequestBatchId').val(), IsInternalDTID: true };
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
    if ($('#hdnPageLoadMenuID').val().toLowerCase().indexOf("xbrl") >= 0) {
        loadXBRL($('#hdnPageLoadMenuID').val());
    }
    else {
        openpdf($('#hdnPageLoadMenuID').val(), $('#hdnPageLoadPDFURL').val());
    }
};

$(window).resize(function () {

    if ($(window).height() - $(".cssTADFBody").outerHeight() > 0) {
        $("#pnlFrameContainer").height(($(window).height() - $(".cssTADFBody").outerHeight()) + "px");
        $("#docFrame").height(($(window).height() - $(".cssTADFBody").outerHeight()) + "px");
        $("#pnlFrameContainer").css('top', ($(".cssTADFBody").outerHeight()) + "px");
    }
});


function ShowRequestMaterialPopup(FundName) {
    var requestMaterialURL = $('#dvRequestMaterialURL').data('requestmaterial-url');
    requestMaterialURL = requestMaterialURL.replace(/\&amp;/g, '&');
    $("#dialog").load(requestMaterialURL).show();
    $("#dialog").dialog({
        width: 700,
        height: 480,
        title: " On Request Material - " + FundName,
        resizable: false,
        draggable: false,
        hide: "scale",
        show: { effect: "scale" },
        modal: true
    });
    $("[aria-describedby=dialog]").find(".ui-dialog-titlebar").addClass("myBorder");
    setTimeout(function () { $(".ui-dialog-titlebar button").blur(); }, 500);
}