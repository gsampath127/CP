$(document).ready(function () {

    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    Bind_btnSearchClientDocumentGroup();
    Bind_Event_btnAddNewButton();

    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#clientDocumentGroupGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#clientDocumentGroupGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#clientDocumentGroupGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchClientDocumentGroup').focus().click();
        }
    });

    $('#ClientDocumentGroupPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Client Document</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;ClientDocumentGroupPopOver&apos;)">&times;</button></span>',
        content: $("#dvClientDocumentGroupPopOver").html(),
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
    $('#clientDocumentGroupClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#NameCombo').data("kendoComboBox").text('');
        $('#ParentCombo').data("kendoComboBox").text('');
        $("#clientDocumentGroupGridwithSortingButtons").css('display', 'none');
        $('#clientDocumentGroupContainerDiv').empty();
    });

}

function LoadSearchParameters() {
    LoadNameCombo();
    LoadParentCombo();
}

function Bind_Event_btnAddNewButton() {
    $("#btnAddNewClientDocumentGroup").click(function (e) {
        ShowEditClientDocumentGroup(e, 0);
    });
}

function LoadNameCombo() {
    $('#NameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvNameComboLoad").data('request-url'),
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
        placeholder: 'Select Name',
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
    $("#NameCombo").data("kendoComboBox").dataSource.read();
}

function LoadParentCombo() {
    $('#ParentCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvParentComboLoad").data('request-url'),
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
        placeholder: 'Select Parent Document Group',
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
    $("#ParentCombo").data("kendoComboBox").dataSource.read();
}

function Bind_btnSearchClientDocumentGroup() {
    $("#btnSearchClientDocumentGroup").click(function (e) {
        initialLoad = true;
        LoadDatabySearchParameters();
    });
}

function LoadDatabySearchParameters() {
    var name = $('#NameCombo').data("kendoComboBox").value();
    var parentClientDocumentGroupId = $('#ParentCombo').data("kendoComboBox").value();
    $("#clientDocumentGroupGridwithSortingButtons").show();
    LoadData(name, parentClientDocumentGroupId);
}

var initialLoad = true;
function LoadData(name, parentClientDocumentGroupId) {
    var pageSize = 10;
    if (typeof $("#clientDocumentGroupGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#clientDocumentGroupGrid").data("kendoGrid").dataSource.pageSize();
        $('#clientDocumentGroupGrid').empty();   // to clear the previous data of Grid.
    }
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllClientDocumentGroup").data('request-url'),
                data: {
                    name: name,
                    parentClientDocumentGroupId: parentClientDocumentGroupId
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
                kendo.ui.progress($("#clientDocumentGroupGrid"), true);
            } else {
                kendo.ui.progress($("#clientDocumentGroupGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#clientDocumentGroupGrid"), false);
                initialLoad = false;
            }
        }
    });

    $("#GridContainer").show();
    $("#clientDocumentGroupGrid").kendoGrid({
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

                  { field: "Name", title: "Name" },
                  { field: "Description", title: "Description" },
                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditClientDocumentGroup(event,#= data.ClientDocumentGroupId #);'></span></a>",
                      attributes: { "class": "templateElements" }

                  },
                   {
                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteClientDocumentGroup(#= data.ClientDocumentGroupId #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#clientDocumentGroupGrid").data("kendoGrid").bind("dataBound", BindTopPager);
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

    var gridView = $('#clientDocumentGroupGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#clientDocumentGroupGrid');
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
    $("#clientDocumentGroupGrid").find(".k-pager-wrap").css("padding-left", "35%");

}

function PopupCenter(pageURL, pageTitle, width, height) {

    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}

function ShowEditClientDocumentGroup(event, ClientDocumentGroupId) {

    var pageURL = $("#dvEditClientDocumentGroup").data('request-url') + '?id1=' + ClientDocumentGroupId;
    var pageTitle = "Edit Client Document Group";
    var width = "1000";
    var height = "800";
    PopupCenter(pageURL, pageTitle, width, height);
}

function ShowDeleteClientDocumentGroup(clientDocumentGroupId) {
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
                DeleteClientDocumentGroupDetails(clientDocumentGroupId);

                kendoWindow.data("kendoWindow").close();

            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();

            }
        });
}

function DeleteClientDocumentGroupDetails(clientDocumentGroupId) {
    $.ajax({
        url: $("#dvDisable").data('request-url'),
        dataType: "json",
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: {
            clientDocumentGroupId: clientDocumentGroupId
        },
        success: function (data) {
            $('#NameCombo').data("kendoComboBox").text('');
            $('#ParentCombo').data("kendoComboBox").text('');
            LoadSearchParameters();
            LoadDatabySearchParameters();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}
