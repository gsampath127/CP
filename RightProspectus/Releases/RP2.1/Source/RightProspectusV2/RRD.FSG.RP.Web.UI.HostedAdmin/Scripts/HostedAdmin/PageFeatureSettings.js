$(document).ready(function () {
    LoadSearchParameters();
    Bind_btnSearchPageFeature();
    BindEvent_AddPageFeature()
    ClearFilters();
    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchPageFeature').focus().click();
        }
    });
    $('#pageFeaturePopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Page Feature</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;pageFeaturePopOver&apos;)">&times;</button></span>',
        content: $("#dvPageFeaturePopOver").html(),
        trigger: 'click'
    });
});

function ClosePopOver(popoverID) {
    $('#' + popoverID).trigger("click");
}

$('body').on('click', function (e) {
    $('[data-toggle=popover]').each(function () {
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            if ($('body div').find('.popover-content').length > 0)
                $('[data-toggle=popover]').trigger("click");
        }
    });
});

function LoadSearchParameters() {
    LoadPageFeatureKeyCombo();
    LoadPageNames();
}

function BindEvent_AddPageFeature() {
    $("#newPageFeature").click(function (e) {
        ShowEditPageFeaturePopUp(e, 0, "");
    });
}
function ClearFilters() {
    $('#clearSearch').click(function () {
        $("#GridContainer").hide();
        $('#comboKey').data("kendoComboBox").text('');
        $('#comboPageName').data("kendoComboBox").text('');
    });
}


function Bind_btnSearchPageFeature() {
    $("#searchPageFeature").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}
function LoadDatabySearchParameters() {
    var Key = $('#comboKey').data("kendoComboBox").text();
    var PageID = $('#comboPageName').data("kendoComboBox").value();

    $("#GridContainer").show();
    LoadData(Key, PageID);
}
var initialLoad = true;
function LoadData(Key, PageID) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#pageFeatureGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#pageFeatureGrid").data("kendoGrid").dataSource.pageSize();
        $('#pageFeatureGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllPageFeature").data('request-url'),
                data: {
                    Key: Key,
                    PageID: PageID
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
                kendo.ui.progress($("#pageFeatureGrid"), true);
            } else {
                kendo.ui.progress($("#pageFeatureGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#pageFeatureGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#pageFeatureGrid").kendoGrid({
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

        columns: [{ field: "PageName", title: "Page Name" },
                  { field: "PageDescription", title: "Page Description" },
                  { field: "PageKey", title: "Page Key " },
                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditPageFeaturePopUp(event,#= data.PageId #, &apos;#= data.PageKey #&apos; );'></span></a>",

                      attributes: { "class": "templateElements" }

                  },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeletePageTextPopUp(#= data.PageId #, &apos;#= data.PageKey #&apos;);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#pageFeatureGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}
function ShowDeletePageTextPopUp(pageId, pageKey) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDeletePageFeature").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DeletePageFeature(pageId, pageKey);
                kendoWindow.data("kendoWindow").close();

                //var pageKey = $('#comboKey').data("kendoComboBox").text();
                //var pageId = $('#comboPageName').data("kendoComboBox").value();
                LoadData($('#comboKey').data("kendoComboBox").text(), $('#comboPageName').data("kendoComboBox").value());
                $('#comboKey').data("kendoComboBox").text('');
                $('#comboPageName').data("kendoComboBox").text('');

            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });
}

function DeletePageFeature(pageId, pageKey) {
    $.ajax({
        url: $("#dvDeletePageFeature").data('request-url'),
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: { pageId: pageId, pageKey: pageKey },
        success: function (data) {

            $('#comboKey').data("kendoComboBox").text('');
            $('#comboPageName').data("kendoComboBox").text('');

            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}

function ShowEditPageFeaturePopUp(event, PageId, PageKey) {

    var pageURL = $("#dvEditPageFeature").data('request-url') + '?PageID=' + PageId + '&PageKey=' + PageKey;
    var pageTitle = "Edit Page Feature";
    var width = "1000";
    var height = "800";
    PopupCenter(pageURL, pageTitle, width, height);
}

function PopupCenter(pageURL, pageTitle, width, height) {
    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
    //window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}


function BindTopPager(e) {

    var gridView = $('#pageFeatureGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#pageFeatureGrid');
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
    $("#pageFeatureGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function LoadPageFeatureKeyCombo() {
    $('#comboKey').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvGetPageFeatureKey").data('request-url'),
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
        placeholder: 'Select PageFeature key',
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

    $("#comboKey").data("kendoComboBox").dataSource.read();
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



function LoadPageNames() {
    $('#comboPageName').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvGetPageNames").data('request-url'),
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
        placeholder: 'Select Page Name',
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

    $("#comboPageName").data("kendoComboBox").dataSource.read();
}

