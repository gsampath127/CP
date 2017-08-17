$(document).ready(function () {
    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchPageText();
    Bind_Event_btnAddNewPageText();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#pageTextGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#pageTextGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#pageTextGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchPageText').focus().click();
        }
    });
    $('#pageTextPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Page Text</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;pageTextPopOver&apos;)">&times;</button></span>',
        content: $("#dvPageTextPopOver").html(),
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

function LoadSearchParameters() {

    LoadResourceKeyCombo();
    LoadPageNameCombo();
    LoadVersionCombo();
}

function ClearFilters() {
    $('#pageTextClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#ResourceKeyCombo').data("kendoComboBox").text('');
        $('#PageNameCombo').data("kendoComboBox").text('');
        $('#VersionCombo').data("kendoComboBox").text('');
        $("#pageTextGridwithSortingButtons").css('display', 'none');
        $('#pageTextcontainerDiv').empty();
    });

}

function LoadResourceKeyCombo() {
    $('#ResourceKeyCombo').kendoComboBox({
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
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#ResourceKeyCombo").data("kendoComboBox").dataSource.read();
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
        }
    });

    $("#VersionCombo").data("kendoComboBox").dataSource.read();
}

function Bind_btnSearchPageText() {
    $("#btnSearchPageText").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
    var ResourceKey = $('#ResourceKeyCombo').data("kendoComboBox").text();
    var PageID = $('#PageNameCombo').data("kendoComboBox").value();
    var Version = $('#VersionCombo').data("kendoComboBox").text();

    $("#pageTextGridwithSortingButtons").show();
    LoadData(ResourceKey, PageID, Version);
}

var initialLoad = true;
function LoadData(ResourceKey, PageID, Version) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#pageTextGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#pageTextGrid").data("kendoGrid").dataSource.pageSize();
        $('#pageTextGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllPageText").data('request-url'),
                data: {
                    ResourceKey: ResourceKey,
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
                kendo.ui.progress($("#pageTextGrid"), true);
            } else {
                kendo.ui.progress($("#pageTextGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#pageTextGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#pageTextGrid").kendoGrid({
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
                  { field: "PageName", title: "Page Name", width: 100 },
                  { field: "PageDescription", title: "Page Description" },
                   { field: "Version", title: "Version", width: 100 },
                  { field: "Text", title: "Text", attributes: { class: "wraptext" } },

                        {
                            title: "",
                            width: 50,
                            template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditPageTextPopUp(event,#= data.PageTextID #, #= data.VersionID #);'></span></a>",
                            attributes: { "class": "templateElements" }

                        },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeletePageTextPopUp(#= data.PageTextID #, #= data.VersionID #, #= data.IsProofing #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#pageTextGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}


function ShowEditPageTextPopUp(event, PageTextID, VersionID) {
    var pageURL = $("#dvEditPageText").data('request-url') + '?PageTextID=' + PageTextID + '&VersionID=' + VersionID;
    var pageTitle = "Edit Page Text";
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

function ShowDeletePageTextPopUp(pageTextID, versionID, isProofing) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisablePageText").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisablePageTextDetails(pageTextID, versionID, isProofing);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DisablePageTextDetails(pageTextID, versionID, isProofing) {
    $.ajax({
        url: $("#dvDisablePageText").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { pageTextID: pageTextID, versionID: versionID, isProofing: isProofing },
        success: function (data) {
            $('#ResourceKeyCombo').data("kendoComboBox").text('');
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

function Bind_Event_btnAddNewPageText() {
    $("#btnAddNewPageText").click(function (e) {
        ShowEditPageTextPopUp(e, 0, 0);
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

    var gridView = $('#pageTextGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#pageTextGrid');
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
    $("#pageTextGrid").find(".k-pager-wrap").css("padding-left", "35%");

}
