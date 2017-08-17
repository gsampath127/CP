$(document).ready(function () {


    LoadSearchParameters();
    ClearFilters();
    BindEvent_AddStaticResource();

    $("#GridContainer").hide();
    Bind_btnSiteSearch();
    var original = $("#window").clone(true);

    var clone = original.clone(true);
    $("#window").hide();

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchStaticResource').focus().click();
        }
    });

    $('#StaticResourcesPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Static Resources</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;StaticResourcesPopOver&apos;)">&times;</button></span>',
        content: $("#dvStaticResourcePopOver").html(),
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

function Bind_btnSiteSearch() {

    $("#searchStaticResource").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}
function BindEvent_AddStaticResource() {
    $("#newStaticResource").click(function (e) {
        ShowEditPopUp(e, 0);
    });
}

function ClearFilters() {
    $('#clearSearch').click(function () {

        $("#comboFileName").data("kendoComboBox").text('');
        $('#comboMimeType').data("kendoComboBox").text('');
        $("#GridContainer").hide();
    });

}

function ShowEditPopUp(event, id) {
    //$(event.target.parentNode).tooltip('hide');
    var pageURL = $("#dvEditStaticResources").data('request-url') + '?id=' + id;
    var pageTitle = "Edit StaticResource";
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


function LoadDatabySearchParameters() {

    var fileName = $('#comboFileName').data("kendoComboBox").text();

    var mimeType = $('#comboMimeType').data("kendoComboBox").text();

    LoadData(fileName, mimeType);
}
function LoadSearchParameters() {
    LoadFileNameCombo();
    LoadMimeTypeCombo();
}

var initialLoad = true;
function LoadData(fileName, mimeType) {

    // $('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#staticResourceGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#staticResourceGrid").data("kendoGrid").dataSource.pageSize();
        $('#staticResourceGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvStaticResourceLoad").data('request-url'),
                data: {
                    FileName: fileName,
                    MimeType: mimeType

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
                kendo.ui.progress($("#staticResourceGrid"), true);
            } else {
                kendo.ui.progress($("#staticResourceGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#staticResourceGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#staticResourceGrid").kendoGrid({
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

        columns: [{
            field: "FileName",
            title: "File Name",
            template: '#=Getvalue(FileName,MimeType,StaticResourceURL)#',
            width: 300

        },
        {
            field: "MimeType",
            title: "Mime Type",
            width: 100

        },
        {
            field: "ImageURL",
            title: "Image URL",
            sortable: false,
        },

        {
            title: "",
            width: 50,
            template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditPopUp(event,#= data.StaticResourceId #);'></span></a>",
            attributes: {
                "class": "templateElements"
            }

        }, {

            title: "",
            width: 50,
            template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteStaticResourcePopUp(#= data.StaticResourceId #);'></span></a>",
            attributes: {
                "class": "templateElements"
            }
        }
        ],
        editable: "popup"
    });
    $("#staticResourceGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}


function ShowDeleteStaticResourcePopUp(staticResourceId) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDeleteStaticResource").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DeleteStaticResource(staticResourceId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });
}

function DeleteStaticResource(staticResourceId) {
    $.ajax({
        url: $("#dvDeleteStaticResource").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { staticResourceId: staticResourceId },
        success: function (data) {
            $("#comboFileName").data("kendoComboBox").text('');
            $('#comboMimeType').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}

function BindTopPager(e) {

    var gridView = $('#staticResourceGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#staticResourceGrid');
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
    // $('#staticResourceGrid :checkbox').each(function () {
    //set checked based on if current checkbox's value is in selectedIds.  
    // $(this).attr('checked', jQuery.inArray($(this).val(), selectedIds) > -1);
    //});
};

function pagerPosition() {

    $("#staticResourceGrid").find(".k-pager-wrap").css("padding-left", "35%");

}


//Loading Combo Box for File Name

function LoadFileNameCombo() {
    $('#comboFileName').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvGetFileName").data('request-url'),
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
        placeholder: 'Select File Name',
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
            //    $("#comboFileName").data("kendoComboBox").value("");
            //}
        }
    });

    $("#comboFileName").data("kendoComboBox").dataSource.read();
}


//Loading Combo Box for Mime Type
function LoadMimeTypeCombo() {
    $('#comboMimeType').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvGetMimeType").data('request-url'),
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
        placeholder: 'Select Mime Type',
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
            //    $("#comboMimeType").data("kendoComboBox").value("");
            //}
        }
    });

    $("#comboMimeType").data("kendoComboBox").dataSource.read();

}



function Getvalue(fileName, mimeType, staticResourceURL) {

    if (mimeType.indexOf("image") > -1) {
        return "<a    href='javascript:undefined' oncontextmenu='return false' onclick='ShowModal(\"" + fileName + "\",\"" + staticResourceURL + "\")' >" + fileName + "</a>";

    }
    else {
        return fileName;
    }

}

function ShowModal(fileName, staticResourceURL) {
    var staticResource = staticResourceURL;
    $('#imgStaticResource').attr('src', staticResource);
    $('.k-window-title').text(fileName);
    var kendoWindow = $("#window").kendoWindow({
        width: "400px",
        modal: true,
        actions: ["Close"],
        title: fileName,
        draggable: false,
        position: "absolute", resizable: false
    });

    kendoWindow.data("kendoWindow")
        .center().open();


}






