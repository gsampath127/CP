$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchDocumentType();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#clientDocumentTypeGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#clientDocumentTypeGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#clientDocumentTypeGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchClientDocumentType').focus().click();
        }
    });

    $('#ClientDocumentTypePopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Client Document Type</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;ClientDocumentTypePopOver&apos;)">&times;</button></span>',
        content: $("#dvClientDocumentTypePopOver").html(),
        trigger: 'click'
    });
    LoadDatabySearchParameters();
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

function ClearFilters() {
    $('#ClientDocumentTypeClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#NameCombo').data("kendoComboBox").text('');
        $('#DescriptionCombo').data("kendoComboBox").text('');
        $("#clientDocumentTypeGridwithSortingButtons").css('display', 'none');
        $('#clientDocumentTypecontainerDiv').empty();
    });

}

function LoadSearchParameters() {

    LoadNameCombo();
    LoadDescriptionCombo();

}
function Bind_Event_btnAddNewButton() {
    $("#btnAddNewClientDocumentType").click(function (e) {
        initialLoad = true;
        ShowEditClientDocumentTypePopUp(e, 0);
    });
}


function Bind_btnSearchDocumentType() {
    $("#btnSearchClientDocumentType").click(function (e) {
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
    var name = $('#NameCombo').data("kendoComboBox").value();
    var description = $('#DescriptionCombo').data("kendoComboBox").value();
    //var modifiedBy = $('#ModifiedByCombo').data("kendoComboBox").value();
    //var modifiedDate = $('#ModifiedDateCombo').data("kendoComboBox").value();

    $("#clientDocumentTypeGridwithSortingButtons").show();
    LoadData(name, description);
}

function LoadNameCombo() {

    $('#NameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvNameComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#NameCombo").data("kendoComboBox").dataSource.read();
}

function LoadDescriptionCombo() {

    $('#DescriptionCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvDescriptionComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select Description',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#DescriptionCombo").data("kendoComboBox").dataSource.read();
}
var initialLoad = true;
function LoadData(name, description) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#clientDocumentTypeGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#clientDocumentTypeGrid").data("kendoGrid").dataSource.pageSize();
        $('#clientDocumentTypeGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllDocumentType").data('request-url'),
                data: {
                    name: name,
                    description: description,

                },
                dataType: "json",
                type: "POST"
            }

        },
        serverSorting: true,
        serverPaging: true,
        pageSize: pageSize,
        schema: {
            data: "data",
            total: "total"
        },
        requestStart: function () {
            if (initialLoad) {
                kendo.ui.progress($("#clientDocumentTypeGrid"), true);
            } else {
                kendo.ui.progress($("#clientDocumentTypeGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#clientDocumentTypeGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#clientDocumentTypeGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: gridDataBound,
        sortable: {
            mode: "single",
            allowUnsort: false

        },
        pageable: {
            buttonCount: 5,
            pageSize: pageSize,
            pageSizes: [5, 10, 15]

        },

        columns: [

                  { field: "Name", title: "Name" },
                  { field: "Description", title: "Description" },
                  { field: "HostedDocumentsDisplayCount", title: "HostedDocumentsDisplayCount" },
                  

                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditClientDocumentTypePopUp(event,#= data.ClientDocumentTypeId #);'></span></a>",
                      attributes: { "class": "templateElements" }

                  },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteClientDocumentTypePopUp(#= data.ClientDocumentTypeId #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#clientDocumentTypeGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}

function gridDataBound(e) {
    var grid = e.sender;
    if (grid.dataSource.total() == 0) {
        var colCount = grid.columns.length;
        $(e.sender.wrapper)
            .find("tbody")
.append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data"> <b>No Records Found<b> </td></tr>');
        $(e.sender.wrapper)
            .find(".k-grid-content-expander").remove();

    }
}

function BindTopPager(e) {

    var gridView = $('#clientDocumentTypeGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#clientDocumentTypeGrid');
    var topPager;

    if (gridView.topPager == null) {
        // create top pager div
        topPager = $('<div/>', {
            'id': id,
            'class': 'k-pager-wrap pagerTop'
        }).insertBefore($grid.find('.k-grid-header'));

        // copy options for bottom pager to top pager
        gridView.topPager = new kendo.ui.Pager(topPager, $.extend({}, gridView.options.pageable, { dataSource: gridView.dataSource }));

        // cloning the pageable options will use the id from the bottom pager
        gridView.options.pagerId = id;

        // DataSource change event is not fired, so call this manually
        gridView.topPager.refresh();
        pagerPosition();

    }
}

function pagerPosition() {
    $("#clientDocumentTypeGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function ShowEditClientDocumentTypePopUp(event, clientDocumentTypeId) {

    var pageURL = $("#dvEditClientDocumentType").data('request-url') + '?id1=' + clientDocumentTypeId;
    // var pageURL = $("#dvEditTaxonomyLevelExternalId").data('request-url') + '?id1=' + LevelId + '&id2=' +TaxonomyId;
    var pageTitle = "Edit Client Document Type";
    var width = "1000";
    var height = "800";
    PopupCenter(pageURL, pageTitle, width, height);
}

function PopupCenter(pageURL, pageTitle, width, height) {

    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}


function ShowDeleteClientDocumentTypePopUp(clientDocumentTypeId) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisable").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                if (ValidateDelete(clientDocumentTypeId)) {
                    DisableClientDocumentTypeDetails(clientDocumentTypeId);
                }

                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DisableClientDocumentTypeDetails(clientDocumentTypeId) {
    $.ajax({
        url: $("#dvDisable").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { clientDocumentTypeId: clientDocumentTypeId },
        success: function (data) {
            $('#NameCombo').data("kendoComboBox").text('');
            $('#DescriptionCombo').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}

function ValidateDelete(clientDocumentTypeId) {

    var isSuccess = true;
    $("#divDeleteValidations").empty();

    if (clientDocumentTypeId != 0) {

        $.ajax({
            type: 'GET',
            url: $("#dvValidateDelete").data('request-url'),
            data: {
                clientDocumentTypeId: clientDocumentTypeId,

            },
            cache: false,
            dataType: "json",
            async: false,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $("#divDeleteValidations")[0].innerHTML += "<li class='message'>This Client Document type is associated with some Client Document.<br/> Please Delete that first.</li>";
                    isSuccess = false;
                }
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });

    }

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divDeleteValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}
