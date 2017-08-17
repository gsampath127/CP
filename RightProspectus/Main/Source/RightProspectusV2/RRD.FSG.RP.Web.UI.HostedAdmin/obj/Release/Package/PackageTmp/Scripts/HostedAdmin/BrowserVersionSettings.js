$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchBrowserVersion();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#BrowserVersionGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#BrowserVersionGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#BrowserVersionGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchBrowserVersion').focus().click();
        }
    });

    $('#BrowserVersionGridPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Browser Version</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;BrowserVersionGridPopOver&apos;)">&times;</button></span>',
        content: $("#dvBrowserVersionPopOver").html(),
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

function ClearFilters() {
    $('#BrowserVersionClearFilters').click(function () {
        $("#GridContainer").hide();
       
        $('#comboBrowserName').data("kendoComboBox").text('');
      
        $("#BrowserVersionGridwithSortingButtons").css('display', 'none');
        $('#BrowserVersioncontainerDiv').empty();
    });

}
function LoadSearchParameters() {
    LoadBrowserVersionNameCombo();
    
}

function Bind_Event_btnAddNewButton() {
    $("#btnAddNewBrowserVersion").click(function (e) {
        ShowEditBrowserVersionPopUp(e,0);
    });
}
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

//    PopupCenter(pageURL, pageTitle, width, height);
//}

function LoadBrowserVersionNameCombo() {
  
    $('#comboBrowserName').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvBrowserVersionNameComboLoad").data('request-url'),
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
        placeholder: 'Select Browser Name',
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

    $("#comboBrowserName").data("kendoComboBox").dataSource.read();


    
}

function Bind_btnSearchBrowserVersion() {
    $("#btnSearchBrowserVersion").click(function (e) {
        
            initialLoad = true;
            LoadDatabySearchParameters();
      
       
   
        
    });
}

function LoadDatabySearchParameters() {
    var browserName = $('#comboBrowserName').data("kendoComboBox").value();
    var version = null;
    var downloadUrl = null;    
     
    $("#BrowserVersionGridwithSortingButtons").show();
    LoadData(browserName);
}

var initialLoad = true;
function LoadData(browserName) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#BrowserVersionGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#BrowserVersionGrid").data("kendoGrid").dataSource.pageSize();
        $('#BrowserVersionGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllBrowserVersion").data('request-url'),
                data: {
                    Name: browserName
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
                kendo.ui.progress($("#BrowserVersionGrid"), true);
            } else {
                kendo.ui.progress($("#BrowserVersionGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#BrowserVersionGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#BrowserVersionGrid").kendoGrid({
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
            
                { field: "Name", title: "Name", width: 30 },
                  { field: "Version", title: "Minimum Version", width: 25 },
                  { field: "DownLoadUrl", title: "Download Url", width: 150 },
                   
                 
                 {
                     title: "",
                     width: 15,
                     template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditBrowserVersionPopUp(event,#= data.Id #);'></span></a>",
                     attributes: { "class": "templateElements" }
                 },
                   {

                       title: "",
                       width: 15,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteBrowserVersionPopUp(event,#= data.Id #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#BrowserVersionGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#BrowserVersionGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#BrowserVersionGrid');
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
    $("#BrowserVersionGrid").find(".k-pager-wrap").css("padding-left", "35%");

}
function ShowEditBrowserVersionPopUp(event, BvId) {
    var pageURL = $("#dvEditBrowserVersion").data('request-url') + '?BrowserVersionID=' + BvId;
    var pageTitle = "Edit Browser Version";
    var width = "1000";
    var height = "550";
    PopupCenter(pageURL, pageTitle, width, height);
}
function PopupCenter(pageURL, pageTitle, width, height) {
    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}


function ShowDeleteBrowserVersionPopUp(event,BrowserVersionId) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDisableBrowserVersion").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DisableBrowserVersionDetails(BrowserVersionId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}


    
function DisableBrowserVersionDetails(BrowserVersionId) {
    $.ajax({
        url: $("#dvDisableBrowserVersion").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
            data: {
                BrowserVersionId: BrowserVersionId
    },
        success: function (data) {
        
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}
