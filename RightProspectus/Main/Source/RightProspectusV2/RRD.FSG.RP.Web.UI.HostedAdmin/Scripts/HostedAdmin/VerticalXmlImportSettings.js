var selectedIds = [];

$(document).ready(function () {
    ClearFilters();
    LoadSearchParameters();
    $("#dvRollbackVerticalXmlImport").hide();
    Bind_btnSearchVerticalXmlImport();
    Bind_Event_btnAddNewVerticalXmlImport();
    Bind_Event_RollbackXmlImport();

    $("#verticalXmlImportGrid").find(".k-pager-wrap").insertBefore(".k-grid-header");
    $("#verticalXmlImportGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#verticalXmlImportGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchVerticalXmlImport').focus().click();
        }
    });
    $('#verticalXmlImportPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Vertical Xml Import</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;verticalXmlImportPopOver&apos;)">&times;</button></span>',
        content: $("#dvVerticalXmlImportPopOver").html(),
        trigger: 'click'
    });

    $('#dvRollbackVerticalXmlImport').popover({
        container: 'body',
        placement: 'top',
        html: 'true',
        title: '<span class="text-info"><strong>Rollback</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;verticalXmlImportPopOver&apos;)">&times;</button></span>',
        content: $("#dvRollbackPopOver").html(),
        trigger: 'hover'
    });

    $('#txtFromImportDate').datepicker({
        format: "dd M yyyy",
        autoclose: true
    });
    $('#txtToImportDate').datepicker({
        format: "dd M yyyy",
        autoclose: true
    });
    LoadDatabySearchParameters();
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

function Bind_Event_RollbackXmlImport() {
    $("#dvRollbackVerticalXmlImport").click(function (e) {
        var kendoWindow = $('<div />').kendoWindow({
            title: "Confirm",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#alertRollback").html())
            .center().open();

        kendoWindow
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    RollbackXmlImport();
                    kendoWindow.data("kendoWindow").close();
                }
                if ($(this).hasClass("cancel")) {
                    kendoWindow.data("kendoWindow").close();
                }
            });        
    });
}

function RollbackXmlImport()
{
    $.ajax({
        type: 'GET',
        url: $("#dvRollbackLastJob").data('request-url'),
        success: function (data) {
            var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
                title: "Rollback",
                resizable: false,
                modal: true,
                draggable: false
            });

            kendoWindow.data("kendoWindow")
                .content(data)
                .center()
                .open();

            kendoWindow
           .find(".confirm")
           .click(function () {
               window.close();
           });

            LoadDatabySearchParameters();
            $("#dvRollbackVerticalXmlImport").hide();
        },
        error: function (err) {
            alert(err);            
        }

    });
}

function ClosePopOver(popoverID) {
    $('#' + popoverID).trigger("click");
}

function Bind_Event_btnAddNewVerticalXmlImport() {
    $("#newVerticalXmlImport").click(function (e) {
        ShowAddVerticalXmlImportPopUp(e);
    });
}

function ShowAddVerticalXmlImportPopUp(event) {
    var pageURL = $("#dvAddVerticalXmlImport").data('request-url');
    var pageTitle = "Add New Import Job";
    var width = "1000";
    var height = "500";
    PopupCenter(pageURL, pageTitle, width, height);
}

function PopupCenter(pageURL, pageTitle, width, height) {

    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}

function  Bind_btnSearchVerticalXmlImport() {
    $("#searchVerticalXmlImport").click(function (e) {
        if (ValidateSearchParams()) {
            initialLoad = true;
            LoadDatabySearchParameters();
            EnableRollback();
        }
    });
}

function EnableRollback()
{
    $.ajax({
        type: 'GET',
        url: $("#dvGetLatestJobStatus").data('request-url'),
        success: function (data) {
            if(data == 3)
            {
                $("#dvRollbackVerticalXmlImport").show();
            }
            else
            {
                $("#dvRollbackVerticalXmlImport").hide();
            }
        },
        error: function (err) {
            alert(err);
            $("#dvRollbackVerticalXmlImport").hide();
        }

    });
}

function ValidateSearchParams()
{
    var isSuccess = true;
    $("#divVerticalXmlImportValidations").empty();

    $("#divVerticalXmlImportValidations")[0].innerHTML += "<p class='message'>Validation Errors</p>";
    $("#divVerticalXmlImportValidations")[0].innerHTML += "<ul>"

    var valFromDate = $('#txtFromImportDate').val();
    var valToDate = $('#txtToImportDate').val();
    
    if (valFromDate == "" && valToDate != "")
    {
        $("#divVerticalXmlImportValidations")[0].innerHTML += "<li class='message'>Please select From Date </li>";
        isSuccess = false;
    }
    else if (valFromDate != "" && valToDate == "")
    {
        $("#divVerticalXmlImportValidations")[0].innerHTML += "<li class='message'>Please select To Date </li>";
        isSuccess = false;
    }
    else if (valFromDate != "" && valToDate != "")
    {
        var currentdate = new Date();
        var fromDt =new Date(valFromDate);
        var toDt = new Date(valToDate);
        if (fromDt > currentdate)
        {
            $("#divVerticalXmlImportValidations")[0].innerHTML += "<li class='message'>From Date shouldn't be greater than current date </li>";
            isSuccess = false;
        }
        if (toDt > currentdate) {
            $("#divVerticalXmlImportValidations")[0].innerHTML += "<li class='message'>To Date shouldn't be greater than current date </li>";
            isSuccess = false;
        }
        if(fromDt > toDt)
        {
            $("#divVerticalXmlImportValidations")[0].innerHTML += "<li class='message'>From Date shouldn't be greater than To Date </li>";
            isSuccess = false;
        }
    }
    $("#divVerticalXmlImportValidations")[0].innerHTML += "</ul>"

        if (!isSuccess) {
            var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
                title: "Alert",
                resizable: false,
                modal: true,
                draggable: false
            });

            kendoWindow.data("kendoWindow")
                .content($("#divVerticalXmlImportValidations").html())
                .center()
                .open();
        }
    return isSuccess;
}


function LoadDatabySearchParameters() {
    var valUserID = $('#comboImportedBy').data("kendoComboBox").value();
    var valFromDate = $('#txtFromImportDate').val();
    var valToDate = $('#txtToImportDate').val();
    $("#verticalImportGridwithSortingButtons").show();
    $("#dvRollbackVerticalXmlImport").hide();
    LoadData(valUserID, valFromDate, valToDate);
}

var importURL = $("#dvGetImportedXml").data('request-url');
var backupURL = $("#dvGetBackupXml").data('request-url');
var errorURL = $("#dvGetErrorLogs").data('request-url');

var initialLoad = true;
function LoadData(valUserID, valFromDate, valToDate) {
    var pageSize = 10;
    if (typeof $("#verticalXmlImportGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#verticalXmlImportGrid").data("kendoGrid").dataSource.pageSize();        
        $('#verticalXmlImportGrid').empty();   // to clear the previous data of Grid.
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllVerticalXmlImport").data('request-url'),
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
                kendo.ui.progress($("#verticalXmlImportGrid"), true);
            } else {
                kendo.ui.progress($("#verticalXmlImportGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#verticalXmlImportGrid"), false);
                initialLoad = false;
            }
        }
    });

    
    $("#GridContainer").show();
    
    $("#verticalXmlImportGrid").kendoGrid({
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
        columns: [{ field: "ImportDate", title: "Date" },
                  { field: "ImportDescription", title: "Description" },
                  { field: "ImportedByName", title: "Imported By" },
                  { field: "Status", title: "Status", template: "#=data.Status# #if(data.StatusID == -1 || data.StatusID == 7) {# <a id='dvErrorSign' href='#= errorURL #?ImportXmlID=#= data.VerticalXmlImportId #'><span class='glyphicon glyphicon-exclamation-sign' style='color:maroon'></span> #}#" },
                  
                  {

                      title: "Imported",
                      width: 70,
                      template: "#if(data.StatusID == 3 || data.StatusID == 6 || data.StatusID == 7 || data.StatusID == -1) {# <a href='#= importURL #?ImportXmlID=#= data.VerticalXmlImportId #' class='text-info' data-toggle='tooltip' data-original-title='Detail Status' data-placement='top' data-container='body'><span class='glyphicon glyphicon-download-alt'></span></a> #}#",
                      attributes: { "class": "templateElements" }

                  },
                  {
                      title: "Backup",
                      width: 60,
                      template: "#if(data.StatusID == 2  || data.StatusID == 3 || data.StatusID == 6 || data.StatusID == 7 || data.StatusID == -1 && data.ExportBackupId != null) {# <a href='#= backupURL #?BackupXmlID=#= data.ExportBackupId #' class='text-info' data-toggle='tooltip' data-original-title='Detail Status' data-placement='top' data-container='body'><span class='glyphicon glyphicon-save'></span></a> #}#",
                      attributes: { "class": "templateElements" }

                  }        
        ],
        editable: "popup"
    });
    $("#verticalXmlImportGrid").data("kendoGrid").bind("dataBound", BindTopPager);
    

    $("#verticalXmlImportGrid").kendoTooltip({
        autoHide: true,
        showOn: "mouseenter",
        filter: "td:nth-child(5):not(:empty)",
        position: "top",
        content: $("#dvDownloadImportXmlPopOver").html()
    }).data("kendoTooltip");
    $("#verticalXmlImportGrid").kendoTooltip({
        autoHide: true,
        showOn: "mouseenter",
        filter: "td:nth-child(6):not(:empty)",
        position: "top",
        content: $("#dvDownloadBackupXmlPopOver").html()
    }).data("kendoTooltip");
    $("#verticalXmlImportGrid").kendoTooltip({
        autoHide: true,
        showOn: "mouseenter",
        filter: "td:nth-child(4):contains(Error)",
        position: "top",
        content: $("#dvErrorLogPopOver").html()
    }).data("kendoTooltip");
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

    var gridView = $('#verticalXmlImportGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#verticalXmlImportGrid');
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
    $("#verticalXmlImportGrid").find(".k-pager-wrap").css("padding-left", "35%");
}
function LoadSearchParameters() {
    LoadUsers();
}

function LoadUsers() {
    $('#comboImportedBy').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvImportedByComboLoad").data('request-url')
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

    $("#comboImportedBy").data("kendoComboBox").refresh();
}

function ClearFilters() {
    $('#clearVerticalXmlImportSearch').click(function () {
        $("#GridContainer").hide();
        $('#comboImportedBy').data("kendoComboBox").text('');
        $('#txtFromImportDate').val('');
        $('#txtToImportDate').val('');
        $("#verticalImportGridwithSortingButtons").css('display', 'none');
        $('#verticalXmlImportContainerDiv').empty();       
    });

}