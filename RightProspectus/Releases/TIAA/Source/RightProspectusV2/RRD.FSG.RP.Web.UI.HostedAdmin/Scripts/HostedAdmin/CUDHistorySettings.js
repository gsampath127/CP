$(document).ready(function () {
    $("#GridContainer").hide();
    LoadSearchParameters();
    Bind_btnSearchHistory();
    ClearFilters();
  

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnSearchHistory').focus().click();
        }
    });
});

function LoadSearchParameters() {

    LoadcomboTableName();
    LoadcomboCUDType();
    LoadcomboUserName();
    $('#txtFromCUDDate').datepicker({
        format: "dd M yyyy",
        autoclose: true
    });
    $('#txtToCUDDate').datepicker({
        format: "dd M yyyy",
        autoclose: true
    });

}

function LoadcomboTableName() {
    $('#comboTableName').kendoComboBox({
        dataSource: {
            transport: {
                read: {
//Check for the method in the Controller
                    url: $("#dvLoadComboTableName").data('request-url'),
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
        placeholder: 'Select Table Name',
        cache: false,
        change: function(e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
          /*  if (cmb.selectedIndex < 0) {
                cmb.value(null); // or set to the first item in combobox
            }*/
        //    if (cmb.selectedIndex < 0) {
        //        cmb.value(null);
        //    }
        }
    });
    $("#comboTableName").data("kendoComboBox").refresh();
}

function LoadcomboCUDType() {
    $('#comboCUDType').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    //Check for the method in the Controller
                    url: $("#dvLoadComboCUDType").data('request-url'),
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
        placeholder: 'Select Type',
        cache: false,
        change: function (e) {

            var cmb = this;
            // selectedIndex of -1 indicates custom value
          /*  if (cmb.selectedIndex < 0 ) {
                cmb.value(null); // or set to the first item in combobox
            }*/
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null);
            //}
        }
    });
    $("#comboCUDType").data("kendoComboBox").refresh();
}


function LoadcomboUserName() {
    $('#comboUserName').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    //Check for the method in the Controller
                    url: $("#dvLoadComboUserName").data('request-url'),
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
        placeholder: 'Select User',
        cache: false,
        change: function (e) {

            var cmb = this;
            // selectedIndex of -1 indicates custom value
            /*  if (cmb.selectedIndex < 0 ) {
                  cmb.value(null); // or set to the first item in combobox
              }*/
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null);
            //}
        }
    });
    $("#comboUserName").data("kendoComboBox").refresh();
}


function Bind_btnSearchHistory() {
    $("#btnSearchHistory").click(function (e) {
        if (ValidateSearchParams()) {
            initialLoad = true;
            LoadDatabySearchParameters();
        }
        });
}

function ClearFilters() {
    $('#historyClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#comboCUDType').data("kendoComboBox").text('');
        $('#comboTableName').data("kendoComboBox").text('');
        $('#comboUserName').data("kendoComboBox").text('');
        $('#txtFromCUDDate').val('');
        $('#txtToCUDDate').val('');
        $("#historyGridwithSortingButtons").css('display', 'none');
        $('#historycontainerDiv').empty();
    });

}


function ValidateSearchParams() {
    var isSuccess = true;
    $("#divCUDHistoryValidations").empty();

    $("#divCUDHistoryValidations")[0].innerHTML += "<p class='message'>Validation Errors</p>";
    $("#divCUDHistoryValidations")[0].innerHTML += "<ul>"

    var valFromDate = $('#txtFromCUDDate').val();
    var valToDate = $('#txtToCUDDate').val();

    if (valFromDate == "" && valToDate != "") {
        $("#divCUDHistoryValidations")[0].innerHTML += "<li class='message'>Please select From Date </li>";
        isSuccess = false;
    }
    else if (valFromDate != "" && valToDate == "") {
        $("#divCUDHistoryValidations")[0].innerHTML += "<li class='message'>Please select To Date </li>";
        isSuccess = false;
    }
    else if (valFromDate != "" && valToDate != "") {
        var currentdate = new Date();
        var fromDt = new Date(valFromDate);
        var toDt = new Date(valToDate);
        if (fromDt > currentdate) {
            $("#divCUDHistoryValidations")[0].innerHTML += "<li class='message'>From Date shouldn't be greater than current date </li>";
            isSuccess = false;
        }
        if (toDt > currentdate) {
            $("#divCUDHistoryValidations")[0].innerHTML += "<li class='message'>To Date shouldn't be greater than current date </li>";
            isSuccess = false;
        }
        if (fromDt > toDt) {
            $("#divCUDHistoryValidations")[0].innerHTML += "<li class='message'>From Date shouldn't be greater than To Date </li>";
            isSuccess = false;
        }
    }
    $("#divCUDHistoryValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divCUDHistoryValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}


function LoadDatabySearchParameters() {
    var tableName = $('#comboTableName').data("kendoComboBox").value();
    var cudType = $('#comboCUDType').data("kendoComboBox").value();
    var userId = $('#comboUserName').data("kendoComboBox").value();
    var valFromDate = $('#txtFromCUDDate').val();
    var valToDate = $('#txtToCUDDate').val();

    $("#historyGridwithSortingButtons").show();
    LoadData(tableName, cudType, valFromDate, valToDate, userId);
    }


var initialLoad = true;
function LoadData(tableName, cudType, valFromDate,valToDate,userId) {

  var pageSize = 10;
    if (typeof $("#historyGrid").data("kendoGrid") != 'undefined') // if grid exists   
    {
        pageSize = $("#historyGrid").data("kendoGrid").dataSource.pageSize();        
        $('#historyGrid').empty();   // to clear the previous data of Grid.
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllHistory").data('request-url'),
                data: {
                    tableName: tableName,
                    cudType: cudType,
                    fromDate: valFromDate,
                    toDate: valToDate,
                    userId: userId
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
                kendo.ui.progress($("#historyGrid"), true);
            } else {
                kendo.ui.progress($("#historyGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#historyGrid"), false);
                initialLoad = false;
            }
        }
    });

      
    $("#GridContainer").show();

                    $("#historyGrid").kendoGrid({
                        dataSource: dataSource,
                        dataBound:gridDataBound,
                       
                        sortable: {
                             mode: "single",
                             allowUnsort: false

                                  },
                        pageable: {
                            buttonCount: 5,
                            pageSize: pageSize,
                            pageSizes: [5, 10, 15]

                        },
                        detailTemplate: kendo.template($("#template").html()),
                        detailInit: LoadCUDHistoryData,//second grid generator
                        columns: [
                            { field: "TableName", title: "Table Name", width: 10 },
                            { field: "CUDType", title: "CUD Type", width: 10 },
                            { field: "UtcCUDDate", title: "CUD Date", width: 10 },
                            { field: "UserName", title: "User Name", width: 10, sortable: false}
                        ],
                        editable: "popup"
                    });
                    $("#historyGrid").data("kendoGrid").bind("dataBound", BindTopPager);                    
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

    var gridView = $('#historyGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#historyGrid');
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

    function pagerPosition() {
        $("#historyGrid").find(".k-pager-wrap").css("padding-left", "35%");

    }
 
}


function LoadCUDHistoryData(e) {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllDataHistory").data('request-url'),
                data: {
                    cudHistoryId: e.data.CUDHistoryId
                },
                dataType: "json",
                type: "POST"
            
            }

        },
        serverSorting: true,
        serverPaging: true,
        pageSize: 10,
        schema: {
            data: "data",
            total: "total"
        }
       // filter: { field: "CUDHistoryId", operator: "eq", value:e.data.CUDHistoryId }
    });
    var detailRow = e.detailRow;

    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find("#historyDataGrid").kendoGrid({
        dataSource:dataSource,
        scrollable: false,
        pageable: true,
        columns: [
                       { field: "ColumnName", title: "Column Name", width: 15 },
                       {
                           title: "Old Value",
                           width: 35,
                           template: "#if(data.IsBinaryImage && data.OldValue != '') {# <img src='#= data.OldImageDataURL #'></img> #}else{# <span>#= data.OldValue #</span> # } #",
                           attributes: { "class": "templateElements" },
                           headerAttributes: { class: "wrapheader" }

                       },
                       {
                           title: "New Value",
                           width: 35,
                           template: "#if(data.IsBinaryImage && data.NewValue != '') {# <img src='#= data.NewImageDataURL #'></img> #}else{# <span>#= data.NewValue #</span> # } #",
                           attributes: { "class": "templateElements" },
                           headerAttributes: { class: "wrapheader" }

                       }
        ]
    });
}