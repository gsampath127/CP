$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchSite();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#taxonomyLevelExternalGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#taxonomyLevelExternalGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#taxonomyLevelExternalGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchTaxonomyLevelExternal').focus().click();
        }
    });

    $('#TaxonomyLevelExternalPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Taxonomy Level External</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;TaxonomyLevelExternalPopOver&apos;)">&times;</button></span>',
        content: $("#dvTaxonomyLevelExternalPopOver").html(),
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
    $('#taxonomyLevelExternalClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#TaxonomylevelCombo').data("kendoComboBox").text('');
        $('#TaxonomyCombo').data("kendoComboBox").text('');
        $('#ExternalIdCombo').data("kendoComboBox").text('');
        $("#taxonomyLevelExternalGridwithSortingButtons").css('display', 'none');
        $('#taxonomyLevelExternalcontainerDiv').empty();
    });

}
function LoadSearchParameters() {

    LoadLevelCombo();
    LoadTaxonomyCombo();
    LoadExternalIdCombo();
}

function Bind_Event_btnAddNewButton() {
    $("#btnAddNewTaxonomylevelexternal").click(function (e) {
        ShowEditTaxonomyLevelExternalIdPopUp(e, 0, 0, '0');
    });
}

function LoadLevelCombo() {

    $('#TaxonomylevelCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvTaxonomylevelComboLoad").data('request-url'),
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
        placeholder: 'Select Level',
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

    $("#TaxonomylevelCombo").data("kendoComboBox").dataSource.read();
}

function LoadTaxonomyCombo() {

    $('#TaxonomyCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvTaxonomyComboLoad").data('request-url'),
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
        placeholder: 'Select Taxonomy',
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

    $("#TaxonomyCombo").data("kendoComboBox").dataSource.read();
}


function LoadExternalIdCombo() {

    $('#ExternalIdCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvExternalIdComboLoad").data('request-url'),
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
        placeholder: 'Select ExternalId',
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

    $("#ExternalIdCombo").data("kendoComboBox").dataSource.read();
}
function Bind_btnSearchSite() {
    $("#btnSearchTaxonomyLevelExternal").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
    var levelID = $('#TaxonomylevelCombo').data("kendoComboBox").value();
    var taxonomyID = $('#TaxonomyCombo').data("kendoComboBox").value();
    var externalID = $('#ExternalIdCombo').data("kendoComboBox").value();

    $("#taxonomyLevelExternalGridwithSortingButtons").show();
    LoadData(levelID, taxonomyID, externalID);
}

var initialLoad = true;
function LoadData(levelID, taxonomyID, externalID) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#taxonomyLevelExternalGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#taxonomyLevelExternalGrid").data("kendoGrid").dataSource.pageSize();
        $('#taxonomyLevelExternalGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllTaxonomyLevelExternalId").data('request-url'),
                data: {
                    levelID: levelID,
                    taxonomyID: taxonomyID,
                    externalID: externalID
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
                kendo.ui.progress($("#taxonomyLevelExternalGrid"), true);
            } else {
                kendo.ui.progress($("#taxonomyLevelExternalGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#taxonomyLevelExternalGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#taxonomyLevelExternalGrid").kendoGrid({
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

                  { field: "Level", title: "Level", width: 150 },
                  { field: "Taxonomy", title: "Taxonomy" },
                  { field: "ExternalId", title: "ExternalId", width: 150 },
                   { field: "IsPrimary", title: "Is Primary", width: 150 },


                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditTaxonomyLevelExternalIdPopUp(event,#= data.LevelId #, #= data.TaxonomyId #, 	&apos;#= data.ExternalId #&apos;);'></span></a>",
                      attributes: { "class": "templateElements" }

                  },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteTaxonomyLevelExternalIdPopUp(#= data.LevelId #, #= data.TaxonomyId #, 	&apos;#= data.ExternalId #&apos;);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#taxonomyLevelExternalGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#taxonomyLevelExternalGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#taxonomyLevelExternalGrid');
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
    $("#taxonomyLevelExternalGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function ShowEditTaxonomyLevelExternalIdPopUp(event, LevelId, TaxonomyId, ExternalId) {

    var pageURL = $("#dvEditTaxonomyLevelExternalId").data('request-url') + '?levelId=' + LevelId + '&taxonomyIdentifier=' + TaxonomyId + '&externalIdentifier=' + ExternalId;
    // var pageURL = $("#dvEditTaxonomyLevelExternalId").data('request-url') + '?id1=' + LevelId + '&id2=' +TaxonomyId;
    var pageTitle = "Edit Taxonomy Level External";
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

function ShowDeleteTaxonomyLevelExternalIdPopUp(LevelId, TaxonomyId, ExternalId) {
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
                DisableTaxonomyLevelExternalIdDetails(LevelId, TaxonomyId, ExternalId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DisableTaxonomyLevelExternalIdDetails(LevelId, TaxonomyId, ExternalId) {
    $.ajax({
        url: $("#dvDisable").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { LevelId: LevelId, TaxonomyId: TaxonomyId, ExternalId: ExternalId },
        success: function (data) {
            $('#TaxonomylevelCombo').data("kendoComboBox").text('');
            $('#TaxonomyCombo').data("kendoComboBox").text('');
            $('#ExternalIdCombo').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}
