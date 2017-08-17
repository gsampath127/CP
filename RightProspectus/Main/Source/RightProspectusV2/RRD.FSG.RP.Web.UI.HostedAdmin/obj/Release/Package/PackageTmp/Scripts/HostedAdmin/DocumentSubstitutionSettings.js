$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchDocumentSubstitution();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#DocumentSubstitutionGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#DocumentSubstitutionGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#DocumentSubstitutionGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchDocumentSubstitution').focus().click();
        }
    });

    $('#DocumentSubstitutionGridPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Document Substitution</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;DocumentSubstitutionGridPopOver&apos;)">&times;</button></span>',
        content: $("#dvDocumentSubstitutionPopOver").html(),
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
    $('#DocumentSubstitutionClearFilters').click(function () {
        $("#GridContainer").hide();

        $('#comboDocumentType').data("kendoComboBox").text('');
        $("#DocumentSubstitutionGridwithSortingButtons").css('display', 'none');
        $('#DocumentSubstitutioncontainerDiv').empty();
    });

}
function LoadSearchParameters() {
    LoadDocumentTypeCombo();
 
}

function Bind_Event_btnAddNewButton() {
    $("#btnAddNewDocumentSubstitution").click(function (e) {
        ShowEditDocumentSubstitutionPopUp(e, 0);
    });
}


function LoadDocumentTypeCombo() {

    $('#comboDocumentType').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvDocumentSubstitutionDocumentTypeComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
                filter: 'startswith',
                dataTextField: 'Name',
                dataValueField: 'MarketId',
                suggest: true,
                placeholder: 'Select Document Type',
                cache: false
    });


}


function Bind_btnSearchDocumentSubstitution() {
    $("#btnSearchDocumentSubstitution").click(function (e) {

        initialLoad = true;
        LoadDatabySearchParameters();

    });
}

function LoadDatabySearchParameters() {
    var documentType = $('#comboDocumentType').data("kendoComboBox").value();   
    $("#DocumentSubstitutionGridwithSortingButtons").show();
    LoadData(documentType);
}

var initialLoad = true;
function LoadData(documentType) {
    var pageSize = 10;
    if (typeof $("#DocumentSubstitutionGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#DocumentSubstitutionGrid").data("kendoGrid").dataSource.pageSize();
        $('#DocumentSubstitutionGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllDocumentSubstitution").data('request-url'),
                data: {
                    documentType: documentType
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
                kendo.ui.progress($("#DocumentSubstitutionGrid"), true);
            } else {
                kendo.ui.progress($("#DocumentSubstitutionGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#DocumentSubstitutionGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#DocumentSubstitutionGrid").kendoGrid({
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

                { field: "DocumentType", title: "Document Type", width: 100 },
                  { field: "SubstituteDocumentType", title: "Substitute DocumentType", width: 100},
                  { field: "NDocumentType", title: "NDocument Types", width: 150 },


                 {
                     title: "",
                     width: 15,
                     template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditDocumentSubstitutionPopUp(event,#= data.Id #);'></span></a>",
                     attributes: { "class": "templateElements" }
                 },
                   {

                       title: "",
                       width: 15,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteDocumentSubstitutionPopUp(event,#= data.Id #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#DocumentSubstitutionGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#DocumentSubstitutionGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#DocumentSubstitutionGrid');
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
    $("#DocumentSubstitutionGrid").find(".k-pager-wrap").css("padding-left", "35%");

}
function ShowEditDocumentSubstitutionPopUp(event, DsId) {
    debugger
    var pageURL = $("#dvEditDocumentSubstitution").data('request-url') + '?DocumentSubstitutionID=' + DsId;
    var pageTitle = "Edit Document Substitution";
    var width = "1000";
    var height = "550";
    PopupCenter(pageURL, pageTitle, width, height);
}
function PopupCenter(pageURL, pageTitle, width, height) {
    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}


function ShowDeleteDocumentSubstitutionPopUp(event, DocumentSubstitutionId) {
    debugger
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisableDocumentSubstitution").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisableDocumentSubstitutionDetails(DocumentSubstitutionId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}



function DisableDocumentSubstitutionDetails(DocumentSubstitutionId) {
    $.ajax({
        url: $("#dvDisableDocumentSubstitution").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: {
            DocumentSubstitutionId: DocumentSubstitutionId
        },
        success: function (data) {

            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}
