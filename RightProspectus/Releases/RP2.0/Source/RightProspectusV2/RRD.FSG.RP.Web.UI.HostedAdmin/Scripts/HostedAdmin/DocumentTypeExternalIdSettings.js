$(document).ready(function () {
    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchDocumentTypeExternalId();
    Bind_Event_btnAddNewDocumentTypeExternalId();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#docTypeExternalIdGrid").find(".k-pager-wrap").insertBefore(".k-grid-header");
    $("#docTypeExternalIdGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#docTypeExternalIdGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchDocTypeExternalId').focus().click();
        }
    });

    $('#DocTypeExternalPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Document Type ExternalId</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;DocTypeExternalPopOver&apos;)">&times;</button></span>',
        content: $("#dvDocumentTypeExtrenalIdPopOver").html(),
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

function LoadSearchParameters() {

    LoadDocumentTypeCombo();
    LoadExternalIdCombo();

}

function Bind_Event_btnAddNewDocumentTypeExternalId() {
    $("#btnAddNewPageText").click(function (e) {
        ShowEditDocumentTypeExternalIdPopUp(e, 0, '');
    });
}

function ShowEditDocumentTypeExternalIdPopUp(event, DocumentType, ExternalId) {
    var pageURL = $("#dvEditDocumentTypeExternalId").data('request-url') + '?DocumentType=' + DocumentType + '&ExternalId=' + ExternalId;
    var pageTitle = "Edit Document Type External";
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
function ClearFilters() {
    $('#docTypeExternalIdClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#DocumentTypeCombo').data("kendoComboBox").text('');
        $('#ExternalIdCombo').data("kendoComboBox").text('');
        $("#docTypeExternalIdGridwithSortingButtons").css('display', 'none');
        $('#docTypeExternalIdcontainerDiv').empty();
    });

}

function LoadDocumentTypeCombo() {
    $('#DocumentTypeCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvDocumentTypeComboLoad").data('request-url'),
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
        placeholder: 'Select Document Type',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            //// selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#DocumentTypeCombo").data("kendoComboBox").dataSource.read();
}


function LoadExternalIdCombo() {
    $('#ExternalIdCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvExternalIdComboLoad").data('request-url'),
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
        placeholder: 'Select External Id',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            //// selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#ExternalIdCombo").data("kendoComboBox").dataSource.read();
}

function Bind_btnSearchDocumentTypeExternalId() {
    $("#btnSearchDocTypeExternalId").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
    var DocumentTypeId = $('#DocumentTypeCombo').data("kendoComboBox").value();
    var ExternalId = $('#ExternalIdCombo').data("kendoComboBox").value();

    $("#docTypeExternalIdGridwithSortingButtons").show();
    LoadData(DocumentTypeId, ExternalId);
}

var initialLoad = true;
function LoadData(DocumentTypeId, ExternalId) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#docTypeExternalIdGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#docTypeExternalIdGrid").data("kendoGrid").dataSource.pageSize();
        $('#docTypeExternalIdGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllDocumentTypeExtrenalId").data('request-url'),
                data: {
                    DocumentTypeId: DocumentTypeId,
                    ExtrenalID: ExternalId
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
                kendo.ui.progress($("#docTypeExternalIdGrid"), true);
            } else {
                kendo.ui.progress($("#docTypeExternalIdGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#docTypeExternalIdGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#docTypeExternalIdGrid").kendoGrid({
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

        columns: [{ field: "DocumentTypeName", title: "Document Type" },
                  { field: "ExternalId", title: "External Id", width: 200 },
                   { field: "IsPrimary", title: "Is Primary", width: 200 },
                   {
                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditDocumentTypeExternalIdPopUp(event,#= data.DocumentTypeId #, 	&apos;#= data.ExternalId #&apos;);'></span></a>",
                       attributes: { "class": "templateElements" }
                   },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteDocumentTypeExternalIdPopUp(#= data.DocumentTypeId #, 	&apos;#= data.ExternalId #&apos;);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }

        ],
        editable: "popup"
    });
    $("#docTypeExternalIdGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#docTypeExternalIdGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#docTypeExternalIdGrid');
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
    $("#docTypeExternalIdGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function ShowDeleteDocumentTypeExternalIdPopUp(DocumentType, ExternalId) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisableDocTypeExternalId").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisableDocumentTypeExternalIdDetails(DocumentType, ExternalId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DisableDocumentTypeExternalIdDetails(DocumentType, ExternalId) {

    $.ajax({
        url: $("#dvDisableDocumentTypeExtrenalId").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { DocumentType: DocumentType, ExternalId: ExternalId },
        success: function (data) {
            $('#DocumentTypeCombo').data("kendoComboBox").text('');
            $('#ExternalIdCombo').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}





