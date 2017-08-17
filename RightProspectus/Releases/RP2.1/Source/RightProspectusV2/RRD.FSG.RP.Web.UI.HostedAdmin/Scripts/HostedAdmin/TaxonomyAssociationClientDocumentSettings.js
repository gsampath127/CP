$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchTaxonomyAssociation();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#taxonomyAssociationClientDocumentGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#taxonomyAssociationClientDocumentGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#taxonomyAssociationClientDocumentGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchTaxonomyAssociationClientDocument').focus().click();
        }
    });

    $('#TaxonomyAssociationClientDocumentPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Taxonomy Association Client Document</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;TaxonomyAssociationClientDocumentPopOver&apos;)">&times;</button></span>',
        content: $("#dvTaxonomyAssociationClientDocument").html(),
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
    $('#taxonomyAssociationClientDocumentFilters').click(function () {
        $("#GridContainer").hide();
       
        $('#TaxonomyCombo').data("kendoComboBox").text('');
        $('#ClientDocumentTypeCombo').data("kendoComboBox").text('');
        $('#ClientDocumentNameCombo').data("kendoComboBox").text('');
      
        $("#taxonomyAssociationClientDocumentGridwithSortingButtons").css('display', 'none');
        $('#taxonomyAssociationClientDocumentcontainerDiv').empty();
    });

}
function LoadSearchParameters() {
    LoadTaxonomyCombo();
    LoadClientDocumentTypeCombo();
    LoadClientDocumentNameCombo();
}

function Bind_Event_btnAddNewButton() {
    $("#btnAddNewTaxonomyAssociationClientDocument").click(function (e) {
        ShowTaxonomyAssociationClientDocument(e);
    });
}

function ShowTaxonomyAssociationClientDocument(event) {

    var pageURL = $("#dvEditTaxonomyAssociationClientDocument").data('request-url');
    var pageTitle = "Edit Taxonomy Association Client Document";
    var width = "1000";
    var height = "500";

    PopupCenter(pageURL, pageTitle, width, height);
}

function LoadClientDocumentTypeCombo() {
  
    $('#ClientDocumentTypeCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvClientDocumentTypeCombo").data('request-url'),
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
        placeholder: 'Select Client Document Type',
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

    $("#ClientDocumentTypeCombo").data("kendoComboBox").dataSource.read();


    
}

function LoadClientDocumentNameCombo() {

    $('#ClientDocumentNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvClientDocumentNameCombo").data('request-url'),
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
        placeholder: 'Select Client Document Name',
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

    $("#ClientDocumentNameCombo").data("kendoComboBox").dataSource.read();
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


function Bind_btnSearchTaxonomyAssociation() {
    $("#btnSearchTaxonomyAssociationClientDocument").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
      var taxonomyId = $('#TaxonomyCombo').data("kendoComboBox").value();
      var clientDocumentTypeID = $('#ClientDocumentTypeCombo').data("kendoComboBox").value();
      
      var clientDocumentID = $('#ClientDocumentNameCombo').data("kendoComboBox").value();
     
    $("#taxonomyAssociationClientDocumentGridwithSortingButtons").show();
    LoadData(taxonomyId, clientDocumentTypeID, clientDocumentID);
}

var initialLoad = true;
function LoadData(taxonomyId, clientDocumentTypeID, clientDocumentID) {
    //$('[data-toggle=tooltip]').tooltip('hide');
    var pageSize = 10;
    if (typeof $("#taxonomyAssociationClientDocumentGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#taxonomyAssociationClientDocumentGrid").data("kendoGrid").dataSource.pageSize();
        $('#taxonomyAssociationClientDocumentGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllTaxonomyAssociationClientDocument").data('request-url'),
                data: {
                    taxonomyId: taxonomyId,
                    clientDocumentTypeID: clientDocumentTypeID,
                    clientDocumentID: clientDocumentID
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
                kendo.ui.progress($("#taxonomyAssociationClientDocumentGrid"), true);
            } else {
                kendo.ui.progress($("#taxonomyAssociationClientDocumentGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#taxonomyAssociationClientDocumentGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#taxonomyAssociationClientDocumentGrid").kendoGrid({
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

                  { field: "TaxonomyAssociationName", title: "Taxonomy" },
                  { field: "ClientDocumentTypeName", title: "Client Document Type", width: 150 },
                  { field: "ClientDocumentName", title: "Client Document Name", width: 150 },
                   { field: "ClientDocumentFileName", title: "File Name", width: 150 },
                 
                 
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteTaxonomyAssociationClientDocumentPopUp(#= data.TaxonomyId #, 	&apos;#= data.ClientDocumentId #&apos;);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#taxonomyAssociationClientDocumentGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#taxonomyAssociationClientDocumentGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#taxonomyAssociationClientDocumentGrid');
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
    $("#taxonomyAssociationClientDocumentGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function PopupCenter(pageURL, pageTitle, width, height) {

    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}


function ShowDeleteTaxonomyAssociationClientDocumentPopUp(taxonomyId, ClientDocumentId) {
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
                DisableTaxonomyAssociationClientDocumentDetails(taxonomyId, ClientDocumentId);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}


    
function DisableTaxonomyAssociationClientDocumentDetails(taxonomyId, ClientDocumentId) {

    $.ajax({
    url: $("#dvDisableTaxonomyAssociationClientDocument").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
            data: {
                TaxonomyId: taxonomyId, ClientDocumentId: ClientDocumentId
    },
        success: function (data) {
            $('#TaxonomyCombo').data("kendoComboBox").text('');
            $('#ClientDocumentTypeCombo').data("kendoComboBox").text('');
        
            $('#ClientDocumentNameCombo').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}
