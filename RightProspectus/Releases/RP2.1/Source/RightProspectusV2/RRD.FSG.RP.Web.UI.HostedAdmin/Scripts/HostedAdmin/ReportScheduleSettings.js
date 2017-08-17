$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchReportSchedule();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#reportScheduleGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#reportScheduleGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#reportScheduleGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchReportSchedule').focus().click();
        }
    });

    $('#ReportSchedulePopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Report Schedule</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;ReportSchedulePopOver&apos;)">&times;</button></span>',
        content: $("#dvReportSchedulePopOver").html(),
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
    $('#reportScheduleClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#ReportNameCombo').data("kendoComboBox").text('');
        $('#FirstRunScheduleDateCombo').data("kendoComboBox").text('');
        $('#NextRunScheduleDateCombo').data("kendoComboBox").text('');
        $('#FrequencyTypeCombo').data("kendoComboBox").text('');        
        $('#IsEnabledCombo').data("kendoComboBox").text('');
        $("#reportScheduleGridwithSortingButtons").css('display', 'none');
        $('#reportScheduleContainerDiv').empty();
    });

}
function Bind_Event_btnAddNewButton() {
    $("#btnAddNewReportSchedule").click(function (e) {
        ShowEditReportSchedule(e, 0);
    });
}


function LoadSearchParameters() {

    LoadReportNameCombo();
    LoadFirstScheduleRunDateCombo();
    LoadnextScheduleRunDateCombo();
    LoadFrequencyTypeCombo();
    LoadIsEnabledCombo();
}

function LoadReportNameCombo() {

    $('#ReportNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvReportNameComboLoad").data('request-url'),
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
        placeholder: 'Select Report Name',
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

    $("#ReportNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadFirstScheduleRunDateCombo() {

    $('#FirstRunScheduleDateCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvFirstRunScheduleDateCombo").data('request-url'),
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
        placeholder: 'Select First Scheduled Run Date',
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

    $("#FirstRunScheduleDateCombo").data("kendoComboBox").dataSource.read();
}


function LoadnextScheduleRunDateCombo() {

    $('#NextRunScheduleDateCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvNextRunScheduleDateCombo").data('request-url'),
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
        placeholder: 'Select Next Scheduled Run Date',
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

    $("#NextRunScheduleDateCombo").data("kendoComboBox").dataSource.read();


}
function LoadFrequencyTypeCombo() {

    $('#FrequencyTypeCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvFrequencyTypeCombo").data('request-url'),
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
        placeholder: 'Select Frequency Type ',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            //// selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#FrequencyTypeCombo").data("kendoComboBox").dataSource.read();
}


function LoadIsEnabledCombo() {

    $('#IsEnabledCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvIsEnabledCombo").data('request-url'),
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
        placeholder: 'Select Is Enabled ',
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

    $("#IsEnabledCombo").data("kendoComboBox").dataSource.read();
}
function Bind_btnSearchReportSchedule() {
    $("#btnSearchReportSchedule").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}
function LoadDatabySearchParameters() {

    ////var fileName = $('#FileNameCombo').data("kendoComboBox").value();
    var reportName = $('#ReportNameCombo').data("kendoComboBox").value();
    var frequencyType = $('#FrequencyTypeCombo').data("kendoComboBox").value();    
    var isEnabled = $('#IsEnabledCombo').data("kendoComboBox").value();
    var firstScheduleRundate = $('#FirstRunScheduleDateCombo').data("kendoComboBox").value();
    var nextScheduleRunDate = $('#NextRunScheduleDateCombo').data("kendoComboBox").value();
      var offset = new Date().getTimezoneOffset();
    $("#reportScheduleGridwithSortingButtons").show();
    LoadData(reportName, frequencyType, isEnabled, firstScheduleRundate, nextScheduleRunDate, offset);
}

var initialLoad = true;
function LoadData(reportName, frequencyType, isEnabled, firstScheduleRundate, nextScheduleRunDate, offset) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#reportScheduleGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#reportScheduleGrid").data("kendoGrid").dataSource.pageSize();
        $('#reportScheduleGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllReportSchedules").data('request-url'),
                data: {
                    reportName: reportName,
                    frequencyType: frequencyType,                    
                    isEnabled: isEnabled,
                    firstScheduleRundate: firstScheduleRundate,
                    nextScheduleRunDate: nextScheduleRunDate,
                    offset: offset
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
                kendo.ui.progress($("#reportScheduleGrid"), true);
            } else {
                kendo.ui.progress($("#reportScheduleGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#reportScheduleGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#reportScheduleGrid").kendoGrid({
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

                  { field: "ReportName", title: "Report", width: 150 },
                  { field: "FrequencyType", title: "Frequency Type", width: 150},                  
                  { field: "UtcFirstScheduledRunDate", title: "First Scheduled Run Date" },
                  { field: "UtcLastActualRunDate", title: "Last Actual Run Date" },
                  { field: "UtcNextScheduledRunDate", title: "Next Scheduled Run Date" },
                  { field: "IsEnabled", title: "Is Enabled", width: 100 },

                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditReportSchedule(event,#= data.ReportScheduleId #);'></span></a>",
                      attributes: { "class": "templateElements" }

                  },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteReportSchedule(#= data.ReportScheduleId #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#reportScheduleGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#reportScheduleGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#reportScheduleGrid');
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
    $("#reportScheduleGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function ShowEditReportSchedule(event, reportScheduleId) {

    var pageURL = $("#dvEditReportSchedule").data('request-url') + '?id1=' + reportScheduleId;
    // var pageURL = $("#dvEditTaxonomyLevelExternalId").data('request-url') + '?id1=' + LevelId + '&id2=' +TaxonomyId;
    var pageTitle = "Edit Report Schedule";
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


function ShowDeleteReportSchedule(reportScheduleId) {
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
                DisableReportScheduleDetails(reportScheduleId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DisableReportScheduleDetails(reportScheduleId) {
    $.ajax({
        url: $("#dvDisable").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { reportScheduleId: reportScheduleId },
        success: function (data) {
            $('#ReportNameCombo').data("kendoComboBox").text('');
            $('#FirstRunScheduleDateCombo').data("kendoComboBox").text('');
            $('#NextRunScheduleDateCombo').data("kendoComboBox").text('');
            $('#FrequencyTypeCombo').data("kendoComboBox").text('');            
            $('#IsEnabledCombo').data("kendoComboBox").text('');

            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}



function FormatDate(e) {
    var input = e.value;

    var pattern1 = /([0-9]{1,2})\/([0-9]{1,2})\/([0-9]{4})$/;

    var pattern2 = /(.*?)\-([a-z|A-Z]{3})\-(.*?)$/;
    if ((pattern1.test(input)) || pattern2.test(input)) {
        var date = new Date(input);

        if (date.getDate() > 0 && date.getDate() < 32 && date.getMonth() < 12 && date.getMonth() > 0) {
            var result = input.replace(pattern1, function (match, p1, p2, p3) {
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                return (p2 < 10 ? "0" + p2 : p2) + "-" + months[(p1 - 1)] + "-" + p3;
            });

            $(e).data("kendoComboBox").text(result);
            $(e).data("kendoComboBox").value(result);
        }
        else {
            $(e).data("kendoComboBox").text('');
            $(e).data("kendoComboBox").value('');
        }


    }
    else {

        $(e).data("kendoComboBox").text('');
        $(e).data("kendoComboBox").value('');
    }
}
