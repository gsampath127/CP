$(document).ready(function () {
    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchSite();
    Bind_Event_btnAddNewSite();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#siteGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#siteGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#siteGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchSite').focus().click();
        }
    });
    $('#sitePopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Sites</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;sitePopOver&apos;)">&times;</button></span>',
        content: $("#dvSitePopOver").html(),
        trigger: 'click'
    });
});

//Added below fundction to reload VerticalXmlImport and VerticalXmlExport options in layout page
function Reload() {
    location.reload(true);
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

function LoadSearchParameters() {

    LoadSiteNameCombo();
    LoadTemplateNameCombo();
    LoadDefaultPageNameCombo();
}

function ClearFilters() {
    $('#siteClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#SiteNameCombo').data("kendoComboBox").text('');
        $('#TemplateNameCombo').data("kendoComboBox").text('');
        $('#DefaultPageNameCombo').data("kendoComboBox").text('');
        $("#siteGridwithSortingButtons").css('display', 'none');
        $('#sitecontainerDiv').empty();
    });

}

function LoadSiteNameCombo() {
    $('#SiteNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvSiteNameComboLoad").data('request-url'),
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
        placeholder: 'Select Site Name',
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

    $("#SiteNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadTemplateNameCombo() {
    $('#TemplateNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvTemplateNameComboLoad").data('request-url'),
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
        placeholder: 'Select Template  Name',
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

    $("#TemplateNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadDefaultPageNameCombo() {
    $('#DefaultPageNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvDefaultPageNameComboLoad").data('request-url'),
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
        placeholder: 'Select Default Page',
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

    $("#DefaultPageNameCombo").data("kendoComboBox").dataSource.read();
}

function Bind_btnSearchSite() {
    $("#btnSearchSite").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
    var siteName = $('#SiteNameCombo').data("kendoComboBox").text();
    var templateID = $('#TemplateNameCombo').data("kendoComboBox").value();
    var pageID = $('#DefaultPageNameCombo').data("kendoComboBox").value();

    $("#siteGridwithSortingButtons").show();
    LoadData(siteName, templateID, pageID);
}

var initialLoad = true;
function LoadData(siteName, templateID, pageID) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#siteGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#siteGrid").data("kendoGrid").dataSource.pageSize();
        $('#siteGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllSites").data('request-url'),
                data: {
                    siteName: siteName,
                    templateID: templateID,
                    pageID: pageID
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
                kendo.ui.progress($("#siteGrid"), true);
            } else {
                kendo.ui.progress($("#siteGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#siteGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#siteGrid").kendoGrid({
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

                  {
                      field: "SiteName",
                      title: "Site Name",
                      width: 200,
                      template: "<a class='ancorCoursor' oncontextmenu='return false' href='" + $("#dvSiteConfigurationURL").data('request-url') + "?SiteID=#= data.SiteID #&SiteName=#= data.SiteName #'><span><u>#= data.SiteName #</u></span></a>",
                      attributes: { "class": "templateElements" },
                      headerAttributes: { class: "wrapheader" }
                  },
                  { field: "TemplateName", title: "Template Name", width: 200 },
                  { field: "DefaultPageName", title: "Default Page Name", width: 200 },
                  { field: "PageDescription", title: "Page Description" },
                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditSitePopUp(event,#= data.SiteID #);'></span></a>",
                      attributes: { "class": "templateElements" }

                  },
                   //{

                   //    title: "",
                   //    width: 50,
                   //    template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteSitePopUp(#= data.SiteID #);'></span></a>",
                   //    attributes: { "class": "templateElements" }
                   //}
        ],
        editable: "popup"
    });
    $("#siteGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}



function ShowEditSitePopUp(event, siteID) {
    var pageURL = $("#dvEditSite").data('request-url') + '?siteID=' + siteID;
    var pageTitle = "Edit Site";
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

function ShowDeleteSitePopUp(siteID) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDeleteSite").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DeleteSiteDetails(siteID);
                kendoWindow.data("kendoWindow").close();

                //var ResourceKey = $('#ResourceKeyCombo').data("kendoComboBox").text();
                //var PageID = $('#PageNameCombo').data("kendoComboBox").value();
                //var Version = $('#VersionCombo').data("kendoComboBox").text();

                //LoadData(ResourceKey, PageID, Version);
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DeleteSiteDetails(siteID) {
    $.ajax({
        url: $("#dvDeleteSite").data('request-url'),
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: { siteID: siteID },
        success: function (data) {
            $('#SiteNameCombo').data("kendoComboBox").text('');
            $('#TemplateNameCombo').data("kendoComboBox").text('');
            $('#DefaultPageNameCombo').data("kendoComboBox").text('');
            LoadSearchParameters()
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}

function Bind_Event_btnAddNewSite() {
    $("#btnAddNewSite").click(function (e) {
        ShowEditSitePopUp(e, 0, 0);
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

    var gridView = $('#siteGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#siteGrid');
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
    $("#siteGrid").find(".k-pager-wrap").css("padding-left", "35%");

}