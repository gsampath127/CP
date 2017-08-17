var selectedIds = [];

$(document).ready(function () {
    ClearFilters();
    LoadSearchParameters();

    Bind_btnSearchVerticalXmlExport();
    Bind_Event_btnAddNewVerticalXmlExport();

    $("#verticalXmlExportGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#verticalXmlExportGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#verticalXmlExportGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchVerticalXmlExport').focus().click();
        }
    });
    $('#verticalXmlExportPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Vertical Xml Export</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;verticalXmlExportPopOver&apos;)">&times;</button></span>',
        content: $("#dvVerticalXmlExportPopOver").html(),
        trigger: 'click'
    });

    $('#txtFromExportDate').datepicker({
        format: "dd M yyyy",
        autoclose: true
    });
    $('#txtToExportDate').datepicker({
        format: "dd M yyyy",
        autoclose: true
    });
});

// hide any open popovers when the anywhere else in the body is clicked
$('body').on('click', function (e) {
    $('[data-toggle=popover]').each(function () {
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            if ($('body div').find('.popover-content').length > 0)
                $('[data-toggle=popover]').trigger("click");
        }
    });
});

function Bind_Event_btnAddNewVerticalXmlExport() {
    $("#newVerticalXmlExport").click(function (e) {
        ShowAddVerticalXmlImportPopUp(e);
    });
}

function ShowAddVerticalXmlImportPopUp() {
    var pageURL = $("#dvAddVerticalXmlExport").data('request-url');
    var pageTitle = "Add New Export Job";
    var width = "1000";
    var height = "300";
    PopupCenter(pageURL, pageTitle, width, height);
}

function PopupCenter(pageURL, pageTitle, width, height) {

    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}

function ClosePopOver(popoverID) {
    $('#' + popoverID).trigger("click");
}

function Bind_btnSearchVerticalXmlExport() {
    $("#searchVerticalXmlExport").click(function (e) {
        if (ValidateSearchParams()) {
            initialLoad = true;
            LoadDatabySearchParameters();
        }
    });
}

function ValidateSearchParams() {
    var isSuccess = true;
    $("#divVerticalXmlExportValidations").empty();

    $("#divVerticalXmlExportValidations")[0].innerHTML += "<p class='message'>Validation Errors</p>";
    $("#divVerticalXmlExportValidations")[0].innerHTML += "<ul>"

    var valFromDate = $('#txtFromExportDate').val();
    var valToDate = $('#txtToExportDate').val();

    if (valFromDate == "" && valToDate != "") {
        $("#divVerticalXmlExportValidations")[0].innerHTML += "<li class='message'>Please select From Date </li>";
        isSuccess = false;
    }
    else if (valFromDate != "" && valToDate == "") {
        $("#divVerticalXmlExportValidations")[0].innerHTML += "<li class='message'>Please select To Date </li>";
        isSuccess = false;
    }
    else if (valFromDate != "" && valToDate != "") {
        var currentdate = new Date();
        var fromDt = new Date(valFromDate);
        var toDt = new Date(valToDate);
        if (fromDt > currentdate) {
            $("#divVerticalXmlExportValidations")[0].innerHTML += "<li class='message'>From Date shouldn't be greater than current date </li>";
            isSuccess = false;
        }
        if (toDt > currentdate) {
            $("#divVerticalXmlExportValidations")[0].innerHTML += "<li class='message'>To Date shouldn't be greater than current date </li>";
            isSuccess = false;
        }
        if (fromDt > toDt) {
            $("#divVerticalXmlExportValidations")[0].innerHTML += "<li class='message'>From Date shouldn't be greater than To Date </li>";
            isSuccess = false;
        }
    }
    $("#divVerticalXmlExportValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divVerticalXmlExportValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

var initialLoad = true;
function LoadDatabySearchParameters() {
    var valUserID = $('#comboExportedBy').data("kendoComboBox").value();
    var valFromDate = $('#txtFromExportDate').val();
    var valToDate = $('#txtToExportDate').val();
    $("#verticalExportGridwithSortingButtons").show();
    LoadData(valUserID, valFromDate, valToDate);
}
var exportURL = $("#dvGetExportedXml").data('request-url');
function LoadData(valUserID, valFromDate, valToDate) {

    var pageSize = 10;
    if (typeof $("#verticalXmlExportGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#verticalXmlExportGrid").data("kendoGrid").dataSource.pageSize();
        $('#verticalXmlExportGrid').empty();   // to clear the previous data of Grid.
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllVerticalXmlExport").data('request-url'),
                data: {
                    fromDate: valFromDate,
                    toDate: valToDate,
                    userID: valUserID
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
                kendo.ui.progress($("#verticalXmlExportGrid"), true);
            } else {
                kendo.ui.progress($("#verticalXmlExportGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#verticalXmlExportGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#verticalXmlExportGrid").kendoGrid({
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
                  { field: "ExportDate", title: "Date" },
                  { field: "ExportDescription", title: "Description" },
                  { field: "ExportedByName", title: "Imported By" },
                  { field: "Status", title: "Status" },
                  {
                      title: "Exported",
                      width: 70,
                      template: "#if(data.StatusID == 2) {# <a href='#= exportURL #?ExportXmlID=#= data.VerticalXmlExportId #' class='text-info' data-toggle='tooltip' data-original-title='Download Exported Xml' data-placement='top' data-container='body'><span class='glyphicon glyphicon-download-alt'></span></a> #}#",
                      attributes: { "class": "templateElements" }

                  }
        ],
        editable: "popup"
    });
    $("#verticalXmlExportGrid").data("kendoGrid").bind("dataBound", BindTopPager);


    $("#verticalXmlExportGrid").kendoTooltip({
        autoHide: true,
        showOn: "mouseenter",
        filter: "td:nth-child(5):not(:empty)",
        position: "top",
        content: $("#dvDownloadXmlPopOver").html()
    }).data("kendoTooltip");
}
function BindTopPager(e) {

    var gridView = $('#verticalXmlExportGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#verticalXmlExportGrid');
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
    $("#verticalXmlExportGrid").find(".k-pager-wrap").css("padding-left", "35%");
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

function LoadSearchParameters() {
    LoadUsers();
}

function LoadUsers() {
    $('#comboExportedBy').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvExportedByComboLoad").data('request-url')
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
        placeholder: 'Select User',
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

    $("#comboExportedBy").data("kendoComboBox").refresh();
}


function ClearFilters() {
    $('#clearVerticalXmlExportSearch').click(function () {
        $("#GridContainer").hide();
        $('#comboExportedBy').data("kendoComboBox").text('');
        $('#txtFromExportDate').val('');
        $('#txtToExportDate').val('');

        $("#verticalExportGridwithSortingButtons").css('display', 'none');
        $('#verticalXmlExportContainerDiv').empty();
    });

}