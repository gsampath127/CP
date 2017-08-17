$(document).ready(function () {
    ClearFilters();
    LoadSearchParameters();

    $("#GridContainer").hide();
    Bind_btnSiteSearch();
    Bind_Event_btnAddNewSiteText();

    $("#urlRewriteGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#urlRewriteGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#urlRewriteGrid").find(".k-pager-info").css("display", "none");
    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchUrlRewrite').focus().click();
        }
    });

    $('#UrlRewritePopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>URL Rewrite</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;UrlRewritePopOver&apos;)">&times;</button></span>',
        content: $("#dvUrlRewritePopOver").html(),
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
    LoadPatternNameCombo();
}
function ClearFilters() {

    $('#clearUrlRewriteSearch').click(function () {

        $("#GridContainer").hide();
        $('#comboPatternName').data("kendoComboBox").text('');

    });
}

function Bind_btnSiteSearch() {
    $("#searchUrlRewrite").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });

}
function Bind_Event_btnAddNewSiteText() {
    $("#newUrlRewrite").click(function (e) {
        ShowEditUrlRewritePopUp(e, 0);
    });
}

function LoadDatabySearchParameters() {
    var patternName = $('#comboPatternName').data("kendoComboBox").text();

    LoadData(patternName);
}


var initialLoad = true;
function LoadData(patternName) {
    // $('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#urlRewriteGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#urlRewriteGrid").data("kendoGrid").dataSource.pageSize();
        $('#urlRewriteGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvUrlRewriteLoad").data('request-url'),
                data: {
                    PatternName: patternName,


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
                kendo.ui.progress($("#urlRewriteGrid"), true);
            } else {
                kendo.ui.progress($("#urlRewriteGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#urlRewriteGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#urlRewriteGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: gridDataBound,
        sortable: {
            //mode: "multiple",
            allowUnsort: false

        },
        pageable: {
            buttonCount: 5,
            pageSize: pageSize,
            pageSizes: [5, 10, 15]

        },


        columns: [
            { field: "PatternName", title: "Pattern Name", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            {
                title: "",
                width: 50,
                template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditUrlRewritePopUp(event,#= data.UrlRewriteId #);'></span></a>",
                attributes: { "class": "templateElements" }
            },
            {

                title: "",
                width: 50,
                template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteUrlRewritePopUp(#= data.UrlRewriteId #);'></span></a>",
                attributes: { "class": "templateElements" }
            }
        ],
        editable: "popup"
    });
    $("#urlRewriteGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}

function BindTopPager(e) {

    var gridView = $('#urlRewriteGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#urlRewriteGrid');
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
    //$('#checkAll').prop('checked', false);
    // $('#urlRewriteGrid :checkbox').each(function () {
    //set checked based on if current checkbox's value is in selectedIds.  
    // $(this).attr('checked', jQuery.inArray($(this).val(), selectedIds) > -1);
    //});
};

function pagerPosition() {

    $("#urlRewriteGrid").find(".k-pager-wrap").css("padding-left", "35%");

}


//Loading Combo Box

function LoadPatternNameCombo() {

    $('#comboPatternName').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvGetPatternName").data('request-url'),
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
        placeholder: 'Select Pattern Name',
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

    $("#comboPatternName").data("kendoComboBox").dataSource.read();
}



function ShowEditUrlRewritePopUp(event, UrlRewriteId) {
    var pageURL = $("#dvEditUrlRewrite").data('request-url') + '?UrlRewriteId=' + UrlRewriteId;
    var pageTitle = "Edit URL Rewrite";
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


function ShowDeleteUrlRewritePopUp(urlRewriteId) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisableUrlRewrite").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisableUrlRewriteDetails(urlRewriteId);
                kendoWindow.data("kendoWindow").close();


            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DisableUrlRewriteDetails(urlRewriteId) {
    $.ajax({
        url: $("#dvDisableUrlRewrite").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { urlRewriteId: urlRewriteId },
        success: function (data) {

            $('#comboPatternName').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();

        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}
