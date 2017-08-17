$(document).ready(function () {
    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchPageNavigation();
    Bind_Event_btnAddNewPageNavigation();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#pageNavigationGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#pageNavigationGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#pageNavigationGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchPageNavigation').focus().click();
        }
    });
    $('#pageNavigationPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Page Navigation</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;pageNavigationPopOver&apos;)">&times;</button></span>',
        content: $("#dvPageNavigationPopOver").html(),
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

    LoadNavigationKeyCombo();
    LoadPageNameCombo();
    LoadVersionCombo();
}

function ClearFilters() {
    $('#pageNavigationClearFilters').click(function () {

        $("#GridContainer").hide();
        $('#NavigationKeyCombo').data("kendoComboBox").text('');
        $('#PageNameCombo').data("kendoComboBox").text('');
        $('#VersionCombo').data("kendoComboBox").text('');
        $("#pageNavigationGridwithSortingButtons").css('display', 'none');
        $('#pageNavigationcontainerDiv').empty();
    });

}

function LoadNavigationKeyCombo() {
    $('#NavigationKeyCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvNavigationKeyComboLoad").data('request-url'),
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
        placeholder: 'Select Navigation Key',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
            //if (cmb.selectedIndex < 0) {
            //    //cmb.value(null); // or set to the first item in combobox
            //    $("#NavigationKeyCombo").data("kendoComboBox").value("");
            //}
        }
    });

    $("#NavigationKeyCombo").data("kendoComboBox").dataSource.read();
}

function LoadPageNameCombo() {

    $('#PageNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvPageNameComboLoad").data('request-url'),
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
        placeholder: 'Select Page',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}

            //if (cmb.selectedIndex < 0) {
            //    // cmb.value(null); // or set to the first item in combobox
            //    $("#PageNameCombo").data("kendoComboBox").value("");
            //}
        }
    });

    $("#PageNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadVersionCombo() {
    $('#VersionCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvVersionComboLoad").data('request-url'),
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
        placeholder: 'Select Version',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
            //if (cmb.selectedIndex < 0) {
            //    // cmb.value(null); // or set to the first item in combobox
            //    $("#VersionCombo").data("kendoComboBox").value("");
            //}
        }
    });

    $("#VersionCombo").data("kendoComboBox").dataSource.read();
}

function Bind_btnSearchPageNavigation() {
    $("#btnSearchPageNavigation").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
    var NavigationKey = $('#NavigationKeyCombo').data("kendoComboBox").text();
    var PageID = $('#PageNameCombo').data("kendoComboBox").value();
    var Version = $('#VersionCombo').data("kendoComboBox").text();

    $("#pageNavigationGridwithSortingButtons").show();
    LoadData(NavigationKey, PageID, Version);
}

var initialLoad = true;
function LoadData(NavigationKey, PageID, Version) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#pageNavigationGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#pageNavigationGrid").data("kendoGrid").dataSource.pageSize();
        $('#pageNavigationGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllPageNavigation").data('request-url'),
                data: {
                    NavigationKey: NavigationKey,
                    PageID: PageID,
                    Version: Version
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
                kendo.ui.progress($("#pageNavigationGrid"), true);
            } else {
                kendo.ui.progress($("#pageNavigationGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#pageNavigationGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#pageNavigationGrid").kendoGrid({
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

        columns: [{ field: "NavigationKey", title: "Navigation Key" },
                  { field: "PageName", title: "Page Name", width: 100 },
                  { field: "PageDescription", title: "Page Description" },
                   { field: "Version", title: "Version", width: 100 },
                       {
                           title: "",
                           width: 50,
                           template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditPageNavigationPopUp(event,#= data.PageNavigationId #, #= data.VersionID #);'></span></a>",
                           attributes: { "class": "templateElements" }

                       },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeletePageNavigationPopUp(#= data.PageNavigationId #, #= data.VersionID #, #= data.IsProofing #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#pageNavigationGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}



function ShowEditPageNavigationPopUp(event, PageNavigationId, VersionID) {
    var pageURL = $("#dvEditPageNavigation").data('request-url') + '?PageNavigationID=' + PageNavigationId + '&VersionID=' + VersionID;
    var pageTitle = "Edit Page Navigation";
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

function ShowDeletePageNavigationPopUp(pageNavigationId, versionID, isProofing) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisablePageNavigation").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisablePageNavigationDetails(pageNavigationId, versionID, isProofing);
                kendoWindow.data("kendoWindow").close();

                //var PatternName = $('#NavigationKeyCombo').data("kendoComboBox").text();
                $('#NavigationKeyCombo').data("kendoComboBox").text('');
                $('#PageNameCombo').data("kendoComboBox").text('');
                $('#VersionCombo').data("kendoComboBox").text('');

                //LoadData(PatternName);
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });

}


function DisablePageNavigationDetails(pageNavigationId, versionID, isProofing) {

    $.ajax({
        url: $("#dvDisablePageNavigation").data('request-url'),
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: { pageNavigationId: pageNavigationId, versionID: versionID, isProofing: isProofing },
        success: function (data) {
            $('#NavigationKeyCombo').data("kendoComboBox").text('');
            $('#PageNameCombo').data("kendoComboBox").text('');
            $('#VersionCombo').data("kendoComboBox").text('');
            LoadSearchParameters();

            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

function Bind_Event_btnAddNewPageNavigation() {
    $("#btnAddNewPageNavigation").click(function (e) {
        ShowEditPageNavigationPopUp(e, 0, 0);
    });
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

    var gridView = $('#pageNavigationGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#pageNavigationGrid');
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
    $("#pageNavigationGrid").find(".k-pager-wrap").css("padding-left", "35%");

}
