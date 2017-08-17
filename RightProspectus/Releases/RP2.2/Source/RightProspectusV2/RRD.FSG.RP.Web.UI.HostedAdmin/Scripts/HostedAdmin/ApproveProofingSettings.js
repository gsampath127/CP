$(document).ready(function () {

    Bind_btnApproveProofing();
    Bind_btnPreview();
    OpenProofingWebSite();

    $('#approveProofingPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Preview</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;approveProofingPopOver&apos;)">&times;</button></span>',
        content: $("#dvApproveProofingPopOver").html(),
        trigger: 'click'
    });
});

function ClosePopOver(popoverID) {
    $('#' + popoverID).trigger("click");
}

// hide any open popovers when the anywhere else in the body is clicked
$('body').on('click', function (e) {
    $('[data-toggle=popover]').each(function () {
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            if ($('body div').find('.popover-content').length > 0)
                $('[data-toggle=popover]').trigger("click");
        }
    });
});


function Bind_btnApproveProofing() {
    $("#btnApproveProofing").click(function (e) {





        var kendoWindow = $('<div />').kendoWindow({
            title: "Confirm",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#approveProofingConfirmation").html())
            .center().open();

        kendoWindow
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    kendoWindow.data("kendoWindow").close();
                    kendo.ui.progress($(document.body), true);
                    $.ajax({
                        type: 'POST',
                        ontentType: 'application/json; charset=utf-8',
                        dataType: 'text',
                        url: $("#dvApproveProofingChangesURL").data('request-url'),
                        success: function (result) {

                            kendo.ui.progress($(document.body), false);

                            var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
                                title: "Success",
                                resizable: false,
                                modal: true,
                                draggable: false
                            });

                            kendoWindow.data("kendoWindow")
                                .content($("#divSuccessOrFailedMessage").html())
                                .center()
                                .open();

                            kendoWindow
                           .find(".confirm")
                           .click(function () {
                               kendoWindow.data("kendoWindow").close();
                           });

                        },
                        error: function () {
                            alert("error");
                        }
                    });
                }
                if ($(this).hasClass("cancel")) {
                    kendoWindow.data("kendoWindow").close();
                }
            });

    });
}


function Bind_btnPreview() {
    $("#btnPreview").click(function (e) {
        OpenProofingWebSite();
    });
}

function OpenProofingWebSite() {
    $.ajax({
        type: 'GET',
        url: $("#dvGetHostedSiteProofingURL").data('request-url'),
        cache: false,
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var url = data + '&IsProofing=true';
            window.open(url, "_blank");

        },
        error: function (xhr) {
            alert(xhr.state + xhr.statusText);
        }
    });
}