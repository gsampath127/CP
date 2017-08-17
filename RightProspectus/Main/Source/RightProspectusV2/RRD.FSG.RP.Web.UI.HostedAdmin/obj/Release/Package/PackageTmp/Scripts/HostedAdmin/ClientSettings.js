$(document).ready(function () {
    $("#GridContainer").hide();
    ClearFilters();
    Bind_btnSearchSite();
    LoadSearchParameters();
    Bind_Event_btnAddNewClient();
    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#clientGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#clientGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#clientGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchClients').focus().click();
        }
    });

    $('#clientPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Clients</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;clientPopOver&apos;)">&times;</button></span>',
        content: $("#dvClientPopOver").html(),
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

function Bind_Event_btnAddNewClient() {
    $("#newClient").click(function (e) {
        ShowEditClientPopUp(e, 0);
    });
}

function LoadSearchParameters() {

    LoadClientNameCombo();
    LoadVerticalMarketCombo();
    LoadDatabaseNameCombo();
}

function LoadClientNameCombo() {
    $('#ClientNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvClientNameComboLoad").data('request-url'),
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
        placeholder: 'Select Client Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            // //selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#ClientNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadVerticalMarketCombo() {
    $('#VerticalMarketCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvVerticalMarketComboLoad").data('request-url'),
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
        placeholder: 'Select Vertical Market',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            //    // selectedIndex of -1 indicates custom value
            //    if (cmb.selectedIndex < 0 ) {
            //        cmb.value(null); // or set to the first item in combobox
            //    }
            //}
        }
    });

    $("#VerticalMarketCombo").data("kendoComboBox").dataSource.read();
}

function LoadDatabaseNameCombo() {
    $('#DatabaseNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvDatabaseNameComboLoad").data('request-url'),
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
        placeholder: 'Select Database Name',
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

    $("#DatabaseNameCombo").data("kendoComboBox").dataSource.read();
}

function Bind_btnSearchSite() {
    $("#searchClients").click(function (e) {
        initialLoad = true;
        $("#GridContainer").show();
        LoadDatabySearchParameters();
    });
}

function ClearFilters() {
    $('#clearClientSearch').click(function () {
        $("#GridContainer").hide();
        $('#ClientNameCombo').data("kendoComboBox").text('');
        $('#VerticalMarketCombo').data("kendoComboBox").text('');
        $('#DatabaseNameCombo').data("kendoComboBox").text('');
    });

}

function LoadDatabySearchParameters() {

    var clientName = $('#ClientNameCombo').data("kendoComboBox").text();
    var verticalMarket = $('#VerticalMarketCombo').data("kendoComboBox").value();
    var databaseName = $('#DatabaseNameCombo').data("kendoComboBox").text();

    $("#siteGridwithSortingButtons").show();
    LoadData(clientName, verticalMarket, databaseName);
}

var initialLoad = true;
function LoadData(searchClientName, searchVerticalMarket, searchDatabaseName) {

    var pageSize = 10;
    if (typeof $("#clientGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#clientGrid").data("kendoGrid").dataSource.pageSize();
        $('#clientGrid').empty();   // to clear the previous data of Grid.
    }


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllClients").data('request-url'),
                data: {
                    clientName: searchClientName,
                    verticalMarket: searchVerticalMarket,
                    databaseName: searchDatabaseName
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
        requestStart: function() {
            if (initialLoad) {
                kendo.ui.progress($("#clientGrid"), true);
            } else {
                kendo.ui.progress($("#clientGrid"), false);
            }
        },
        requestEnd: function() {
            if (initialLoad) {
                kendo.ui.progress($("#clientGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#clientGrid").kendoGrid({
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
                          field: "ClientName",
                          title: "Client Name",
                          template: "<a class='ancorCoursor' oncontextmenu='return false' href='" +$("#dvWelComeClientPageURL").data('request-url') + "?clientId=#= data.ClientID #&clientName=#= data.ClientName #'><span><u>#= data.ClientName #</u></span></a>",
                          attributes: { "class": "templateElements" },
                          headerAttributes: { class: "wrapheader" }
                      },
                  {
                      field: "VerticalMarketName", title: "Vertical Market" },
                  {
                      field: "ClientDatabaseName", title: "Client Database Name" },
                  {
                      field: "SelectedClientConnectionStringName", title: "Client Connection String Name" },
                  {
                          title: "",
                          width: 50,
                          template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditClientPopUp(event,#= data.ClientID #);'></span></a>",
                          attributes: { "class": "templateElements" }

                      },
                       //{

                       //    title: "",
                       //    width: 50,
                       //    template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteClientPopUp(#= data.ClientID #);'></span></a>",
                       //    attributes: { "class": "templateElements" }
                       //}
                  ],
            editable: "popup"
        });
    $("#clientGrid").data("kendoGrid").bind("dataBound", BindTopPager);

}

function ShowDeleteClientPopUp(clientID) {
    var kendoWindow = $('<div />').kendoWindow({
            title: "Confirm",
            resizable: false,
            modal: true,
            draggable: false
        });

    kendoWindow.data("kendoWindow")
        .content($("#alertDeleteClient").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function() {
            if($(this).hasClass("confirm")) {
                DeleteClientDetails(clientID);
                kendoWindow.data("kendoWindow").close();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DeleteClientDetails(clientID) {
    $.ajax({
            url: $("#dvDeleteClientDetails").data('request-url'),
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            data: { clientID: clientID},
            success: function (data) {

                $('#ClientNameCombo').data("kendoComboBox").text('');
                $('#VerticalMarketCombo').data("kendoComboBox").text('');
                $('#DatabaseNameCombo').data("kendoComboBox").text('');

                LoadSearchParameters();
                LoadDatabySearchParameters();
            },
            error: function (xhr, status, error) {

                alert(error);
            }
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

    var gridView = $('#clientGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#clientGrid');
    var topPager;

    if (gridView.topPager == null) {
        // create top pager div
        topPager = $('<div/>', {
            'id': id,
            'class': 'k-pager-wrap pagerTop'
            }).insertBefore($grid.find('.k-grid-header'));

        // copy options for bottom pager to top pager
        gridView.topPager = new kendo.ui.Pager(topPager, $.extend({ }, gridView.options.pageable, {
            dataSource: gridView.dataSource }));

        // cloning the pageable options will use the id from the bottom pager
        gridView.options.pagerId = id;

        // DataSource change event is not fired, so call this manually
        gridView.topPager.refresh();
        pagerPosition();

    }
    }
function pagerPosition() {
    $("#clientGrid").find(".k-pager-wrap").css("padding-left", "35%");

    }
function ShowEditClientPopUp(event, clientId) {
    var pageURL = $("#dvEditClient").data('request-url') + '/' + clientId;
    var pageTitle = "Edit Client";
    var width = "1000";
    var height = "800";
    PopupCenter(pageURL, pageTitle, width, height);
    }

function PopupCenter(pageURL, pageTitle, width, height) {

    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' +top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
    }



