var selectedIds = [];
$(document).ready(function () {

    ClearFilters();
    LoadSearchParameters();

    Bind_btnSearchSiteFeature();
    Bind_Event_btnAddNewSiteFeature();

    $("#siteFeatureGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#siteFeatureGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#siteFeatureGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchSiteFeature').focus().click();
        }
    });
    $('#siteFeaturePopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Site Feature</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;siteFeaturePopOver&apos;)">&times;</button></span>',
        content: $("#dvSiteFeaturePopOver").html(),
        trigger: 'click'
    });
});

function ClearFilters() {

    $('#clearSiteFeatureSearch').click(function () {
        $("#GridContainer").hide();
        $('#comboFeatureKey').data("kendoComboBox").text('');

        $("#siteFeatureGridwithSortingButtons").css('display', 'none');
        $('#siteFeatureContainerDiv').empty();
    });
}

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

function Bind_btnSearchSiteFeature() {
    $("#searchSiteFeature").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}


var initialLoad = true;
function LoadDatabySearchParameters() {
    var valFeatureKey = $('#comboFeatureKey').data("kendoComboBox").text();

    $("#siteFeatureGridwithSortingButtons").show();
    LoadData(valFeatureKey);
}

function LoadData(valFeatureKey) {
    var pageSize = 10;
    if (typeof $("#siteFeatureGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#siteFeatureGrid").data("kendoGrid").dataSource.pageSize();
        $('#siteFeatureGrid').empty();   // to clear the previous data of Grid.
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvSiteFeatureLoad").data('request-url'),
                data: {
                    siteFeatureKey: valFeatureKey,

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
                kendo.ui.progress($("#siteFeatureGrid"), true);
            } else {
                kendo.ui.progress($("#siteFeatureGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#siteFeatureGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#siteFeatureGrid").kendoGrid({
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

        columns: [{ field: "SiteKey", title: "Feature Key" },
                 // { field: "FeatureModes", title: "Feature Mode" },

                    {
                        title: "",
                        width: 50,
                        template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditSiteFeaturePopUp(event, #= data.SiteId #, &apos;#= data.SiteKey #&apos;);'></span></a>",
                        attributes: { "class": "templateElements" }

                    },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteSiteFeaturePopUp(&apos;#= data.SiteKey #&apos;);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#siteFeatureGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}
function BindTopPager(e) {

    var gridView = $('#siteFeatureGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#siteFeatureGrid');
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
    $("#siteFeatureGrid").find(".k-pager-wrap").css("padding-left", "35%");

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

function Bind_Event_btnAddNewSiteFeature() {
    $("#newSiteFeature").click(function (e) {
        ShowEditSiteFeaturePopUp(e, 0, "");
    });
}

function ShowEditSiteFeaturePopUp(event, SiteId, SiteKey) {
    var pageURL = $("#dvEditSiteFeature").data('request-url') + '?siteId=' + SiteId + '&siteFeatureKey=' + SiteKey;
    var pageTitle = "Edit Site Text";
    var width = "1000";
    var height = "800";
    PopupCenter(pageURL, pageTitle, width, height);
}

function ShowDeleteSiteFeaturePopUp(siteKey) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisableSiteFeature").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisableSiteFeatureDetails(siteKey);
                kendoWindow.data("kendoWindow").close();
                $('#comboFeatureKey').data("kendoComboBox").text('');
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}
function DisableSiteFeatureDetails(siteKey) {
    $.ajax({
        url: $("#dvDisableSiteFeature").data('request-url'),
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: { SiteFeatureKey: siteKey },
        success: function (data) {

            $('#comboFeatureKey').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}

function PopupCenter(pageURL, pageTitle, width, height) {

    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}

function LoadSearchParameters() {
    LoadFeatureKey();
    // LoadFeatureMode();
}

function LoadFeatureKey() {
    $('#comboFeatureKey').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvGetFeatureKeys").data('request-url'),
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
        placeholder: 'Select Feature Key',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
            //if (cmb.selectedIndex < 0) {
            //  //  cmb.value(null); // or set to the first item in combobox
            //    $("#comboFeatureKey").data("kendoComboBox").value("");
            //}
        }
    });

    $("#comboFeatureKey").data("kendoComboBox").dataSource.read();
}

//$('#comboFeatureKey').focusout(function () {
//    var $elem = $("#comboFeatureKey").data("kendoComboBox");
//    $elem.dataSource.filter([]); //reset filters
//    $elem.value($(this).val());
//});