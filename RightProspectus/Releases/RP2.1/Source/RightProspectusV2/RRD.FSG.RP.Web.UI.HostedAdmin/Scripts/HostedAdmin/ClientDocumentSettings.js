$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchClientDocument();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#clientDocumentGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#clientDocumentGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#clientDocumentGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchClientDocument').focus().click();
        }
    });

    $('#ClientDocumentPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Client Document</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;ClientDocumentPopOver&apos;)">&times;</button></span>',
        content: $("#dvClientDocumentPopOver").html(),
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

function ClearFilters() {
    $('#clientDocumentClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#DocumentTypeCombo').data("kendoComboBox").text('');
        $('#NameCombo').data("kendoComboBox").text('');
        $('#FileNameCombo').data("kendoComboBox").text('');
        $('#MimeTypeCombo').data("kendoComboBox").text('');
        $('#IsPrivateCombo').data("kendoComboBox").text('');
        $("#clientDocumentGridwithSortingButtons").css('display', 'none');
        $('#clientDocumentContainerDiv').empty();
    });

}

function LoadSearchParameters() {
    LoadFileName();
    LoadNameCombo();
    LoadDocumentTypeCombo();
    LoadMimeType();
    LoadIsPrivate();

}
function Bind_Event_btnAddNewButton() {
    $("#btnAddNewClientDocument").click(function (e) {
        ShowEditClientDocument(e, 0);
    });
}
function LoadFileName() {

    $('#FileNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvFileNameComboLoad").data('request-url'),
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
        placeholder: 'Select File Name',
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

    $("#FileNameCombo").data("kendoComboBox").dataSource.read();
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
        placeholder: 'Select  Name',
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

    $("#NameCombo").data("kendoComboBox").dataSource.read();
}

function LoadDocumentTypeCombo() {

    $('#DocumentTypeCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvClientDocumentTypeName").data('request-url'),
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
        placeholder: 'Select  Document Type',
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

    $("#DocumentTypeCombo").data("kendoComboBox").dataSource.read();
}

function LoadMimeType() {

    $('#MimeTypeCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvMimeTypeComboLoad").data('request-url'),
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
        placeholder: 'Select Mime Type',
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

    $("#MimeTypeCombo").data("kendoComboBox").dataSource.read();
}

function LoadIsPrivate() {

    $('#IsPrivateCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvIsPrivateComboLoad").data('request-url'),
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
        placeholder: 'Select IsPrivate',
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

    $("#IsPrivateCombo").data("kendoComboBox").dataSource.read();
}

function Bind_btnSearchClientDocument() {
    $("#btnSearchClientDocument").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}
function LoadDatabySearchParameters() {
    var fileName = $('#FileNameCombo').data("kendoComboBox").value();
    var name = $('#NameCombo').data("kendoComboBox").value();
    var documentType = $('#DocumentTypeCombo').data("kendoComboBox").value();
    var mimeType = $('#MimeTypeCombo').data("kendoComboBox").value();
    var isPrivate = $('#IsPrivateCombo').data("kendoComboBox").value();

    $("#clientDocumentGridwithSortingButtons").show();
    LoadData(fileName, name, documentType, mimeType, isPrivate);
}

var initialLoad = true;
function LoadData(fileName, name, documentType, mimeType, isPrivate) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#clientDocumentGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#clientDocumentGrid").data("kendoGrid").dataSource.pageSize();
        $('#clientDocumentGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllClientDocument").data('request-url'),
                data: {
                    fileName: fileName,
                    name: name,
                    clientDocumentTypeId: documentType,
                    mimeType: mimeType,
                    isPrivate: isPrivate
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
                kendo.ui.progress($("#clientDocumentGrid"), true);
            } else {
                kendo.ui.progress($("#clientDocumentGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#clientDocumentGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#clientDocumentGrid").kendoGrid({
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

                  { field: "FileName", title: "File Name" },
                  { field: "ClientDocumentTypeName", title: "DocumentType" },
                  { field: "Name", title: "Name" },
                  { field: "MimeType", title: "Mime Type" },
                  { field: "IsPrivateString", title: "IsPrivate" },

                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditClientDocument(event,#= data.ClientDocumentId #);'></span></a>",
                      attributes: { "class": "templateElements" }

                  },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteClientDocument(#= data.ClientDocumentId #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#clientDocumentGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#clientDocumentGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#clientDocumentGrid');
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
    $("#clientDocumentGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function ShowEditClientDocument(event, ClientDocumentId) {

    var pageURL = $("#dvEditClientDocument").data('request-url') + '?id1=' + ClientDocumentId;
    // var pageURL = $("#dvEditTaxonomyLevelExternalId").data('request-url') + '?id1=' + LevelId + '&id2=' +TaxonomyId;
    var pageTitle = "Edit Client Document";
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

function ShowDeleteClientDocument(clientDocumentId) {
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
                DisableClientDocumentDetails(clientDocumentId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });
}

function DisableClientDocumentDetails(clientDocumentId) {
    $.ajax({
        url: $("#dvDisable").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { clientDocumentId: clientDocumentId },
        success: function (data) {
            $('#DocumentTypeCombo').data("kendoComboBox").text('');
            $('#NameCombo').data("kendoComboBox").text('');
            $('#FileNameCombo').data("kendoComboBox").text('');
            $('#MimeTypeCombo').data("kendoComboBox").text('');
            $('#IsPrivateCombo').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}