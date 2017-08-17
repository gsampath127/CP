
function LoadTaxonomyCustomerData(siteId) {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociationCustomer").data('request-url'),
                data: { siteId: siteId },
                dataType: "json",
                type: "POST"
            },
            parameterMap: function (options, operation) {
                if (operation !== "read" && options.models) {
                    return {
                        models: kendo.stringify(options.models)
                    };
                }
                else {

                    return options;
                }
            }
        },
        batch: true,
        schema: {
            data: "data",
            total: "total",
            model: {
                id: "TaxonomyAssociationId",
                fields: {
                    MarketId: {},
                    TaxonomyId: {},
                    NameOverride: {},
                    DescriptionOverride: {},
                    CssClass: {},
                    SiteId: {},
                    IsUpdated: {},
                    TabbedPageNameOverride: {},
                    RPFundName:{editable:false}
                   
                }
            }
        },        
    });

    $("#GridContainer").show();

    if ($("#taxonomyAssociationCustomerGrid").data("kendoGrid")) {
        $("#taxonomyAssociationCustomerGrid").data("kendoGrid").destroy();
        $("#taxonomyAssociationCustomerGrid").empty();
    }

    $("#taxonomyAssociationCustomerGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: marketIdCustomerSettings_gridDataBound,
        toolbar: ["create", "save", "cancel"],
        //excel: {
        //    fileName: "UnderlyingFunds.xlsx",
        //    filterable: true
        //},
        pageable: false,
        columns: [
            { field: "MarketId", width: 100, title: "CUSIPs", headerTemplate: "<span>CUSIPs</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "RPFundName", title: "RP Fund Name", headerTemplate: "<span>RP Fund Name</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "NameOverride", title: "Name Override", headerTemplate: "<span>Name Override</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "TabbedPageNameOverride", title: "Tabbed Page NameOverride", headerTemplate: "Tabbed Page Name Override", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "DescriptionOverride", title: "Tooltip Information", headerTemplate: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            { field: "CssClass", width: 70, headerTemplate: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },            
            //{ field: "Order", width: 70, title: "Order", headerAttribute: { class: "wrapheader" }, attribute: { class: "wraptext" } },
            {
                field: "",
                title: "",
                width: "50px",
                headerTemplate: "<input type='checkbox' class='selectALLMultipleDelete' style='text-align:center;margin-left:10px'/>",
                template: "<input type='checkbox' class='selectMultipleDelete' style='text-align:center;margin-left:10px'/>"
            },
            {
                command: [
                     { name: "edit" },
                     { name: "Delete", click: onMarketIdCustomerDelete }, ]
                 , title: "&nbsp;", width: "170px"
            }

        ],
        edit: function (arg) {
            $('.selectMultipleDelete').hide();
            if (arg.model.isNew() && arg.model.MarketId == "") {
                var Orders = [];
                Orders = $("#taxonomyAssociationCustomerGrid").data("kendoGrid").dataSource._data.map(function (a) { return a.Order; });
                if (Orders.length > 0)
                    arg.model.set("Order", Math.max.apply(Math, Orders) + 1);
                else
                    arg.model.set("Order", 1);
            }
            if (arg.model.isNew() == false) {
                $('input[name=MarketId]').parent().html(arg.model.MarketId);

            }

            if (arg.model.isNew()) {
                arg.container.kendoWindow("title", "Add New Fund");
            } else {
                arg.container.kendoWindow("title", "Edit Fund");
            }

            arg.container.addClass("ImportMarketIdCustomer-EditPopup");
            if (arg.container[0].children[0].children[1].childElementCount == 0) {
                arg.container[0].children[0].children[1].style.setProperty("padding", ".4em 0 1em");
            }
            $('#taxonomyAssociationCustomerGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
            $('#taxonomyAssociationCustomerGrid').find("thead").find('tr').find(".selectALLMultipleDelete").prop('checked', false);
        },
        cancel: function (e) { $('.selectMultipleDelete').show(); },
        editable: { mode: "popup", confirmDelete: "No" },
        save: function (e) {
            e.model.SiteId = siteId;
            e.model.IsUpdated = true;
            if (ValidateTaxonomyCustomerSave(e.model)) {
                this.refresh();
                //  $("#documentTypeGrid").data("kendoGrid").addRow();
            }

        },
        saveChanges: function (e) {

            $("#taxonomyAssociationCustomerGrid .k-grid-save-changes").prop('disabled', true);
            $("#taxonomyAssociationCustomerGrid .k-grid-save-changes").addClass('disabled');
            var changes = SaveAllTaxonomyAssociationCustomerChanges(TaxonomyAssociationCustomerChanges()); // Save All Grid Changes to Server

            changes.done(function () {

                $("#taxonomyAssociationCustomerGrid .k-grid-save-changes").prop('disabled', false);
                $("#taxonomyAssociationCustomerGrid .k-grid-save-changes").removeClass('disabled');

                ReloadTaxonomyAssociationCustomer();
                popupNotification.show("Underlying Funds-CUSIPs details Saved Successfully", "success");

            }).fail(function () {

                $("#taxonomyAssociationCustomerGrid .k-grid-save-changes").prop('disabled', false);
                $("#taxonomyAssociationCustomerGrid .k-grid-save-changes").removeClass('disabled');

                popupNotification.show("Failed to save Underlying Funds-CUSIPs details", "error");
            });
        },

        remove: function (e) {
            this.refresh();

        }

    });
    
    EnableDocumentTypeTooltipCustomer();

    $('#taxonomyAssociationCustomerGrid .selectALLMultipleDelete').change(function () {
        if($(this).is(':checked')){
            $('#taxonomyAssociationCustomerGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', true);
        }
        else {
            $('#taxonomyAssociationCustomerGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
        }
    });
}

function EnableDocumentTypeTooltipCustomer() {
    var grid = $("#taxonomyAssociationCustomerGrid").data("kendoGrid");

    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 100,        
        content: function (e) {
            var target = e.target; // element for which the tooltip is shown
            switch (target.text()) {
                case "Market Id":
                    return "CUSIPs";
                case "Name Override":
                    return "Fund Name";
                case "Description Override":
                    return "Tooltip Information";
                case "Tabbed Page Name Override":
                    return "Tabbed Page Name Override";
                default:
                    return $(target).text();
    
            }

        }
    });    

}
function marketIdLbl(container, options) {

    if (options.model.MarketId != "") {
        var element = "<label>" + options.model.MarketId + "</label>";
        $(element)
       .appendTo(container);
    }
    else {
        container.text(options.model.MarketId);
    }

}
$("#CustomerlnkImport").click(function () {

    $("#CustomerTAInvalidDataGridContainer").hide();
    $("#CustomerTAInvalidDataGridHeader").hide();

    TaxonomyAssociationCustomerSave().done(function (data) {
        if (data) {
            var grid = $("#taxonomyAssociationCustomerGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importTaxonomyAssociationCustomer").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }
    });
   
});

$("#taxonomyAssociationCustomerUpload").kendoUpload({

    async: {
        saveUrl: $("#dvTaxonomyImportCustomer").data('request-url'),

    },
    upload: function (e) {
        e.data = {
            siteId: null
        };
    },
    multiple: false,
    localization: {
        "select": "Select file to import..."
    },
    select: function (e) {
        var extension = e.files[0].extension.toLowerCase();
        if (extension != ".xlsx") {
            alert("Please select a supported file format - xlsx");
            e.preventDefault();
        }
    },
    progress: function () {
        kendo.ui.progress($(".k-window"), true);
    },
    error: function () {
        kendo.ui.progress($(".k-window"), false);
    },
    success: function (e) {

        kendo.ui.progress($(".k-window"), false);

        if (e.response.status == "countgreaterthan5000")
        {            
            $("#CustomerTAInvalidDataGridContainer").hide();
            $("#CustomerTAInvalidDataGridHeader").hide();
            
            $(".k-upload-files li").removeClass('k-file-success');
            $(".k-upload-files li").addClass('k-file-error');
            $(".k-upload-files .k-upload-pct").text('Please import file with less than 5000 rows.');
        }
        else
        {
            if (e.response.status == "Success") {
                ReloadTaxonomyAssociationCustomer();
                popupNotification.show("Underlying Funds-CUSIPs details Saved Successfully", "success");
            }
            else {
                popupNotification.show("Failed to save Underlying Funds-CUSIPs details", "error");
            }

            BindInvalidCustomerTAData(e.response.invalidData);
            if (e.response.invalidData.length == 0) {

                $("#importTaxonomyAssociationCustomer").data("kendoWindow").close();
            }
        }        

    }
});

function onMarketIdCustomerDelete(e) {
    var grid = $("#taxonomyAssociationCustomerGrid").data("kendoGrid");
    var tr = $(e.target).closest("tr"); //get the row for deletion
    var data = this.dataItem(tr); //get the row data so it can be referred later
    var currentRef = this;
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    var list = [];
    $("#taxonomyAssociationCustomerGrid").find("input:checked").each(function () {

        var data1 = currentRef.dataItem($(this).closest('tr'));
        list.push({ uid: data1.uid });

    });

    var messageToDisplay = "";
    if (list.length > 0)
    {
        messageToDisplay = $("#deleteTaxonomyAssociationCustomerTemplate").html().replace("MarketId", "all selected CUSIP's");
    }
    else
    {
        messageToDisplay = $("#deleteTaxonomyAssociationCustomerTemplate").html().replace("MarketId", data.MarketId);
    }

    kendoWindow.data("kendoWindow")
        .content(messageToDisplay)
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                
                if (list.length > 0)
                {
                    for (i = 0; i < list.length; i++) {
                        var dataRow = grid.dataSource.getByUid(list[i].uid);
                        if (dataRow != null) {
                            grid.dataSource.remove(dataRow);
                        }
                    }
                }
                else {
                        var dataRow = grid.dataSource.getByUid(data.uid);
                        if (dataRow != null) {
                            grid.dataSource.remove(dataRow);
                        }
                }               

                kendoWindow.data("kendoWindow").close();
               
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });
}
function marketIdCustomerSettings_gridDataBound(e) {
    var grid = e.sender;
    $(e.sender.wrapper).find("th").prop('title', ' ');
    if (grid.dataSource.total() == 0) {
        var colCount = grid.columns.length;
        $(e.sender.wrapper)
            .find("tbody")
.append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data"> <b>No Records Found<b> </td></tr>');
        $(e.sender.wrapper)
            .find(".k-grid-content-expander").remove();

    }
};
function ValidateTaxonomyCustomerSave(model) {
    var isSuccess = true;
    var content = "";
    var grid = $("#taxonomyAssociationCustomerGrid").data("kendoGrid");
    content += "<p class='message'>Please Enter the below fields</p>";
    content += "<ul>";
    if (!model.MarketId) {
        content += "<li class='message'>CUSIP</li>";
        isSuccess = false;
    }
    else {
       
        var duplicateMarketIds = $.grep(grid.dataSource._data, function (e) { return e.MarketId == model.MarketId });
        if (duplicateMarketIds.length >= 2) {
            content += "<li class='message'>A Record with same MarketId already Exists !!</li>";
            isSuccess = false;
        }
    }    
   
    content += "</ul>";
    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });
        kendoWindow.data("kendoWindow")
            .content(content)
            .center()
            .open();
    }
    return isSuccess;
}
var SaveAllTaxonomyAssociationCustomerChanges = function (data) {
    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveTaxonomyAssociationCustomer").data('request-url'),
        data: { updated: data.updatedRecords, deleted: data.deletedRecords, added: data.newRecords },
        type: "POST",
        success: function (data) {
            if (data == "Success") {
                deferred.resolve();

            }
            else
                deferred.reject();
        },
        error: function () {
            //Handle the server errors using the approach from the previous example
        }

    });

    return deferred.promise();
};
var ReloadTaxonomyAssociationCustomer = function () {
    LoadTaxonomyCustomerData(TaxonomyCustomerSettings.SiteId);
};
var TaxonomyAssociationCustomerChanges = function () {
    var taxonomyAssociationGrid = $("#taxonomyAssociationCustomerGrid").data("kendoGrid"),
           updatedRecords = [],
           newRecords = [],
           updatedKeys = [],
           deletedRecords = [];


    if (taxonomyAssociationGrid) {
        var currentData = taxonomyAssociationGrid.dataSource.data();
        newRecords = $.grep(currentData, function (e) { return e.isNew() || Number(e.TaxonomyAssociationId) == 0 }).map(function (a) { return a.toJSON(); });;
        updatedRecords = $.grep(currentData, function (e) { return (e.dirty || e.IsUpdated) && Number(e.TaxonomyAssociationId) > 0  &&!e.isNew() && !e.destroyed }).map(function (a) { return a.toJSON(); });
        updatedKeys = updatedRecords.map(function (a) { return a.Key });
        deletedRecords = $.grep(taxonomyAssociationGrid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.Key) < 0) && Number(e.TaxonomyAssociationId) >= 0; });
        deletedRecords = deletedRecords.map(function (a) { return a.toJSON(); });

    }
    var data = { newRecords: newRecords, updatedRecords: updatedRecords, deletedRecords: deletedRecords };
    return data;
};
$("#exportCustomer").click(function (e) {
    TaxonomyAssociationCustomerSave().done(function (data) {
        if (data) {
            
            CustomerTaxonomyMarketIdExcelExport('#taxonomyAssociationCustomerGrid');
        }
    })
 
});

function CustomerTaxonomyMarketIdExcelExport(gridName) {

    var grid = $(gridName).data("kendoGrid");
    var rows = [{
            cells: [
                    { value: "CUSIPs", color: "#fff", background: "#08c", bold: true},
                    { value: "Name Override", color: "#fff", background : "#08c", bold: true },
                    { value: "Tabbed Page Name Override", color: "#fff", background : "#08c", bold: true },
                    { value: "Tooltip Information", color: "#fff", background : "#08c", bold: true },
                    { value: "Css Class", color: "#fff", background : "#08c", bold: true },
                   ]
                }];

    var trs = $(gridName).find("tbody").find('tr');

    var exportAllRecords = true;
    if ($(trs).find(":checkbox").is(":checked"))
    {
        exportAllRecords = false;
    }

    for (var i = 0; i < trs.length; i++) {
            
        if (exportAllRecords || $(trs[i]).find(":checkbox").is(":checked")) {
            var dataItem = grid.dataItem(trs[i]);
            if (dataItem != null)
            {
                rows.push({
                        cells: [
                                { value: dataItem.MarketId },
                                { value: dataItem.NameOverride },
                                { value: dataItem.TabbedPageNameOverride },
                                { value: dataItem.DescriptionOverride},
                                { value: dataItem.CssClass },
                               ]
                        }); 
            }
        }
    }

    var workbook = new kendo.ooxml.Workbook({
        sheets: [
        {
            freezePane: {
                rowSplit: 1
            },
            columns: [
                { width: 120 },
                { width: 300 },
                { width: 300 },
                { width: 200 },
                { width: 100 },
            ],
            title: "CUSIPs",
            rows: rows
        }
        ]
    });

    var fileName = "CustomerLevelTaxonomyMarketId.xlsx";
    if (gridName == "#CustomerTAInvalidDataGrid")
    {
        fileName = "Invalid_CustomerLevelTaxonomyMarketId.xlsx";
    }
    kendo.saveAs({dataURI: workbook.toDataURL(), fileName: fileName});
}

var LoadTaxonomyAssociationCustomer = function (siteId) {

    LoadTaxonomyAssociationCustomer_loader = new kendo.data.DataSource({
        transport: {
            read: {
                url: "",
                cache: true
            },
            type: "GET"
        }
    });
    LoadTaxonomyAssociationCustomer_loader.fetch(function () {
    });

    $("#btnMarketIDCustomerSearch").kendoButton({
        click: onMarketIdSiteLevelCustomerSearch
    });
    TaxonomyCustomerSettings.InitialLoad = true;
    TaxonomyCustomerSettings.SiteId = siteId;
    LoadTaxonomyCustomerData(siteId);

};
function onMarketIdSiteLevelCustomerSearch() {
    var query = $("#txtSearchCustomerTAString").val();
    var grid = $("#taxonomyAssociationCustomerGrid").data("kendoGrid");
    grid.dataSource.query({
        page: 1,
        pageSize: 20,
        filter: {
            logic: "or",
            filters: [
              { field: "MarketId", operator: "startswith", value: query },
            { field: "NameOverride", operator: "startswith", value: query }

            ]
        }
    });
}
function BindInvalidCustomerTAData(data) {
    var dataSource = new kendo.data.DataSource({
        data: {
            "data": data, "total": data.length
        },
        schema: {
            data: "data",
            total: "total",
        }
    });

    $("#CustomerTAInvalidDataGridContainer").show();

    $("#CustomerTAInvalidDataGridHeader").show();

    if($("#CustomerTAInvalidDataGrid").data("kendoGrid")) {
        $("#CustomerTAInvalidDataGrid").data("kendoGrid").destroy();
        $("#CustomerTAInvalidDataGrid").empty();
    }


    $("#CustomerTAInvalidDataGrid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["Export"],
        //excel: {
        //    fileName: "Inavlid_UnderlyingFunds.xlsx",
        //    filterable: true
        //},
        dataBound: marketIdCustomerSettings_gridDataBound,
        columns: [
          { field: "MarketId", width: 85, title: "CUSIPs", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "NameOverride", title: "Name Override", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "TabbedPageNameOverride", headerTemplate: "Tabbed Page NameOverride", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "DescriptionOverride", title: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "CssClass", width: 100, title: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },            
        ],
    });

     $("#CustomerTAInvalidDataGrid .k-grid-Export").on('click', function (e) {

         CustomerTaxonomyMarketIdExcelExport('#CustomerTAInvalidDataGrid');
    });
}

var TaxonomyCustomerSettings =
{
    LoadTaxonomyAssociationCustomer: LoadTaxonomyAssociationCustomer,
    TaxonomyAssociationCustomerChanges: TaxonomyAssociationCustomerChanges,
    SaveAllTaxonomyAssociationCustomerChanges: SaveAllTaxonomyAssociationCustomerChanges,
    ReloadTaxonomyAssociationCustomer: ReloadTaxonomyAssociationCustomer,
    InitialLoad: true,
    SiteId: null


};