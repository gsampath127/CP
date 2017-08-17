
function DownloadFile(fileName, dirName) {
    var dirName = encodeURIComponent(dirName);
    return '<a href="' + $("#dvAPIUUrl").data('request-url') + '/Download/DownloadFile?fileLocation=' + dirName + '")>' + fileName + '</a>';    
}

function DateFormat(date) {
    return kendo.toString(new Date(date), "F");
}

//Kendo Window for Validations

function KendoValidationWindow(content) {
    var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
        title: "Alert",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
       .content(content)
        .center()
        .open();
}


//Trigger Popover for Title of module

function TriggerPopOver(content, title) {

    $('.BCSPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>' + title + '</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;BCSPopOver&apos;)">&times;</button></span>',
        content: content,
        trigger: 'click'
    });
}

function ClosePopOver(popover) {
    $('.' + popover).trigger("click");
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
