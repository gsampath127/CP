var selectedIds = [];
$(document).ready(function () {
    ClearFilters();
    LoadSearchParameters();

    Bind_btnSearchSiteText();
    Bind_Event_btnAddNewSiteText();

    $("#siteTextGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#siteTextGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#siteTextGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchSiteText').focus().click();
        }
    });
    $('#siteTextPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Site Text</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;siteTextPopOver&apos;)">&times;</button></span>',
        content: $("#dvSiteTextPopOver").html(),
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

function Bind_btnSearchSiteText() {
    $("#searchSiteText").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

var initialLoad = true;
function LoadDatabySearchParameters() {
    var valResourceKey = $('#comboResourceKey').data("kendoComboBox").text();
    var valVersion = $('#comboVersion').data("kendoComboBox").text();
    $("#siteTextGridwithSortingButtons").show();
    LoadData(valResourceKey, valVersion);
}

function LoadData(valResourceKey, valVersion) {
    var pageSize = 10;
    if (typeof $("#siteTextGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#siteTextGrid").data("kendoGrid").dataSource.pageSize();
        $('#siteTextGrid').empty();   // to clear the previous data of Grid.
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllSiteText").data('request-url'),
                data: {
                    resourceKey: valResourceKey,
                    version: valVersion
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
                kendo.ui.progress($("#siteTextGrid"), true);
            } else {
                kendo.ui.progress($("#siteTextGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#siteTextGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#siteTextGrid").kendoGrid({
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

        columns: [{ field: "ResourceKey", title: "Resource Key" },
                  { field: "Version", title: "Version" },

                    {
                        title: "",
                        width: 50,
                        template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditSiteTextPopUp(event,#= data.SiteTextID #, #= data.VersionID #);'></span></a>",
                        attributes: { "class": "templateElements" }

                    },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteSiteTextPopUp(#= data.SiteTextID #, #= data.VersionID #, #= data.IsProofing #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#siteTextGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

function Bind_Event_btnAddNewSiteText() {
    $("#newSiteText").click(function (e) {
        ShowEditSiteTextPopUp(e, 0, 0);
    });
}

function ShowEditSiteTextPopUp(event, SiteTextID, VersionID) {
    var pageURL = $("#dvEditSiteText").data('request-url') + '?SiteTextID=' + SiteTextID + '&VersionID=' + VersionID;
    var pageTitle = "Edit Site Text";
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

function LoadSearchParameters() {
    LoadResourceKey();
    LoadVersion();
}

function LoadResourceKey() {
    $('#comboResourceKey').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvResourceKeyComboLoad").data('request-url'),
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
        placeholder: 'Select Resource Key',
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

    $("#comboResourceKey").data("kendoComboBox").dataSource.read();
}

function LoadVersion() {
    $("#comboVersion").kendoComboBox({
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
        }

    });

    $("#comboVersion").data("kendoComboBox").dataSource.read();
}

function ClearFilters() {
    $('#clearSiteTextSearch').click(function () {
        $("#GridContainer").hide();
        $('#comboResourceKey').data("kendoComboBox").text('');
        $('#comboVersion').data("kendoComboBox").text('');
        $("#siteTextGridwithSortingButtons").css('display', 'none');
        $('#siteTextContainerDiv').empty();
    });

}

function BindTopPager(e) {

    var gridView = $('#siteTextGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#siteTextGrid');
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
    $("#siteTextGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function ShowDeleteSiteTextPopUp(siteTextID, versionID, isProofing) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisableSiteText").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisableSiteTextDetails(siteTextID, versionID, isProofing);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DisableSiteTextDetails(siteTextID, versionID, isProofing) {
    $.ajax({
        url: $("#dvDisableSiteText").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { siteTextID: siteTextID, versionID: versionID, isProofing: isProofing },
        success: function (data) {
            $('#comboResourceKey').data("kendoComboBox").text('');
            $('#comboVersion').data("kendoComboBox").text('');

            LoadSearchParameters();

            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}

