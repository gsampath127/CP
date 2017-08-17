
function LoadTaxonomyData(siteId) {
    
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociation").data('request-url'),
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
                    Order: { type: "number" },
                    TabbedPageNameOverride: {},
                    RPFundName: {editable:false}

                }
            }
        },        
    });

    $("#GridContainer").show();

    if ($("#taxonomyAssociationGrid").data("kendoGrid")) {
        $("#taxonomyAssociationGrid").data("kendoGrid").destroy();
        $("#taxonomyAssociationGrid").empty();
    }

    $("#taxonomyAssociationGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: marketIdSettings_gridDataBound,
        toolbar: ["create", "save", "cancel"],
        //excel: {
        //    fileName: "CUSIPs.xlsx",
        //    filterable: true
        //},
        pageable: false,
        columns: [
            { field: "MarketId", title: "CUSIPs", width: 85, headerTemplate: "<span>CUSIPs</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "RPFundName", title: "RP Fund Name", headerTemplate: "<span>RP Fund Name</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "NameOverride", title: "Name Override", headerTemplate: "<span>Name Override</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "TabbedPageNameOverride", title: "Tabbed Page NameOverride", headerTemplate: "<span>Tabbed Page NameOverride</span>", headerAttribute: { class: "wrapheader showtooltip" }, attribute: { class: "wraptext" } },
            { field: "DescriptionOverride", title: "Tooltip Information", headerTemplate: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            { field: "CssClass", width: 100, headerTemplate: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },            
            { field: "Order", width: 70, hidden: true, headerTemplate: "<span>Order</span>", headerAttribute: { class: "wrapheader showtooltip" }, attribute: { class: "wraptext" } },
            

           
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
                     { name: "Delete", click: onMarketIdDelete }, ]
                 , title: "&nbsp;", width: "170px"
            }

        ],
        edit: function (arg) {
            $('.selectMultipleDelete').hide();
            if (arg.model.isNew() && arg.model.MarketId == "" && SiteData.FundOrder == 'CustomizeOrder') {
                var Orders = [];
                Orders = $("#taxonomyAssociationGrid").data("kendoGrid").dataSource._data.map(function (a) { return a.Order; });
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

            arg.container.addClass("ImportMarketId-EditPopup");
            if (arg.container[0].children[0].children[1].childElementCount == 0) {
                arg.container[0].children[0].children[1].style.setProperty("padding", ".4em 0 1em");
            }

            $('#taxonomyAssociationGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
            $('#taxonomyAssociationGrid').find("thead").find('tr').find(".selectALLMultipleDelete").prop('checked', false);
            
           
        },      
        editable: { mode: "popup", confirmDelete: "No" },
        cancel: function (e) {
            $('.selectMultipleDelete').show();
        },
        save: function (e) {
            e.model.SiteId = siteId;
            e.model.IsUpdated = true;
            if (ValidateTaxonomySave(e.model)) {
                this.refresh();
                //  $("#documentTypeGrid").data("kendoGrid").addRow();
            }

        },
        saveChanges: function (e) {
            $("#taxonomyAssociationGrid .k-grid-save-changes").prop('disabled', true);
            $("#taxonomyAssociationGrid .k-grid-save-changes").addClass('disabled');

            var changes = SaveAllTaxonomyAssociationChanges(TaxonomyAssociationChanges()); // Save All Grid Changes to Server
           
            changes.done(function () {

                $("#taxonomyAssociationGrid .k-grid-save-changes").prop('disabled', false);
                $("#taxonomyAssociationGrid .k-grid-save-changes").removeClass('disabled');

                ReloadTaxonomyAssociation();
                popupNotification.show("CUSIPs-Site Saved Successfully", "success");
           
            }).fail(function () {

                $("#taxonomyAssociationGrid .k-grid-save-changes").prop('disabled', false);
                $("#taxonomyAssociationGrid .k-grid-save-changes").removeClass('disabled');

                popupNotification.show("Failed Saving CUSIPs-Site", "error");
            });
        },

        remove: function (e) {
            this.refresh();

        }

    });
    EnableTooltipMarketId();

    
    var grid = $("#taxonomyAssociationGrid").data("kendoGrid");
    if (SiteData.FundOrder == 'CustomizeOrder') {
        grid.showColumn("Order");
        grid.dataSource.sort({ field: "Order", dir: "asc" });
        TaxonomyAssociationOrder();
    }
    else if (SiteData.FundOrder == 'AlphabeticalAsc') {
        grid.dataSource.sort({ field: "NameOverride", dir: "asc" });
    }
    else if (SiteData.FundOrder == 'AlphabeticalDesc') {
        grid.dataSource.sort({ field: "NameOverride", dir: "desc" });
    }

    $('#taxonomyAssociationGrid .selectALLMultipleDelete').change(function() {
        if($(this).is(':checked')){
            $('#taxonomyAssociationGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', true);
        }
        else {
            $('#taxonomyAssociationGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
        }
    });
   
}
function EnableTooltipMarketId() {
    var grid = $("#taxonomyAssociationGrid").data("kendoGrid");

    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 120,
        content: function (e) {
            var target = e.target; // element for which the tooltip is shown
            switch (target.text()) {
                case "Market Id":
                    return "CUSIPs";
                case "Name Override":
                    return "Fund Name";
                case "Description Override":
                    return "Tooltip Information";
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
$("#TAlnkImport").click(function () {

    $("#TAInvalidDataGridContainer").hide();
    $("#TAInvalidDataGridHeader").hide();

    TaxonomyAssociationSave().done(function (data) {
        if (data) {
            var grid = $("#taxonomyAssociationGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importTaxonomyAssociation").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }
    });

  
});
function TaxonomyAssociationOrder() {
    $("#taxonomyAssociationGrid").getKendoGrid().table.kendoDraggable({
        filter: "tbody > tr:not(.k-grid-edit-row)",
        //Setting filter element on which drag is applicable
        cursorOffset: { top: 10, left: 10 },
        group: "gridGroup",
        //Setting the hint element that would appear during the transition/actually a clone of the documentTypeGrid row.

        hint: function (e) {

            return $('<div class="k-grid k-widget"><table><tbody><tr><b>' + e.html() + '</b></tr></tbody></table></div>');

        }
   });

    //Set the drop target for the rows

    $("#taxonomyAssociationGrid").getKendoGrid().table.kendoDropTarget({
        group: "gridGroup",//This would recieve the drop target
        drop: function (e) {

            var grid = $("#taxonomyAssociationGrid").getKendoGrid();
            //Getting the dragged element
            var source = grid.dataSource.getByUid($(e.draggable.currentTarget).data("uid"));

            var dest = grid.dataSource.getByUid($(e.target).parent().data("uid"));
            // source.set("Order", dest.Order+1);
            var oldIndex = grid.dataSource.view().indexOf(source);
            var newIndex = grid.dataSource.view().indexOf(dest);

            var view = grid.dataSource.view();

            if (oldIndex != newIndex) {

                source.Order = dest.Order; // Update the order
                source.dirty = true;
                grid.dataSource.remove(source);
                grid.dataSource.insert(newIndex, source);

                if (oldIndex < newIndex) {
                    for (var i = oldIndex + 1; i <= newIndex; i++) {
                        view[i].Order--;
                        view[i].dirty = true;
                        var item = grid.dataSource.remove(view[i]);
                        grid.dataSource.insert(i, item);
                    }
                } else {
                    for (var i = oldIndex - 1; i >= newIndex; i--) {
                        view[i].Order++;
                        view[i].dirty = true;
                        var item = grid.dataSource.remove(view[i]);
                        grid.dataSource.insert(i, item);
                    }
                }
            }
        }
    });


}
$("#taxonomyAssociationUpload").kendoUpload({

    async: {
        saveUrl: $("#dvTaxonomyImport").data('request-url'),

    },
    upload: function (e) {
        e.data = {
            siteId: TaxonomySettings.SiteId,
            taxonomyOrderFeatureMode: SiteData.FundOrder
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
            $("#TAInvalidDataGridContainer").hide();
            $("#TAInvalidDataGridHeader").hide();

            $(".k-upload-files li").removeClass('k-file-success');
            $(".k-upload-files li").addClass('k-file-error');
            $(".k-upload-files .k-upload-pct").text('Please import file with less than 5000 rows.');
        }
        else
        {
            if (e.response.status == "Success") {
                popupNotification.show("CUSIPs-Site details Saved Successfully", "success");
                ReloadTaxonomyAssociation();
            }
            else {
                popupNotification.show("Failed to save CUSIPs-Site details", "error");
            }


            BindInvalidTAData(e.response.invalidData);
            if (e.response.invalidData.length == 0) {

                $("#importTaxonomyAssociation").data("kendoWindow").close();
            }
        }
    }
});



function onMarketIdDelete(e) {
    var grid = $("#taxonomyAssociationGrid").data("kendoGrid");
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
    $("#taxonomyAssociationGrid").find("input:checked").each(function () {

        var data1 = currentRef.dataItem($(this).closest('tr'));
        list.push({ uid: data1.uid });

    });
    var messageToDisplay = "";
    if (list.length > 0) {
        messageToDisplay = $("#deleteTaxonomyAssociationTemplate").html().replace("MarketId", "all selected CUSIP's");
    }
    else {
        messageToDisplay = $("#deleteTaxonomyAssociationTemplate").html().replace("MarketId", data.MarketId);
    }
    kendoWindow.data("kendoWindow")
        .content(messageToDisplay)
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {

                if (list.length > 0) {
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
                TaxonomyAssociationOrder();
            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });
}
function marketIdSettings_gridDataBound(e) {
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
function ValidateTaxonomySave(model) {
    var isSuccess = true;
    var content = "";
    var grid = $("#taxonomyAssociationGrid").data("kendoGrid");
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
    if (SiteData.FundOrder == 'CustomizeOrder' && (!model.Order || Number(model.Order) < 1)) {
        content += "<li class='message'>Order</li>";
        isSuccess = false;
    }
    else if (SiteData.FundOrder == 'CustomizeOrder') {
        
        var duplicateOrders = $.grep(grid.dataSource._data, function (e) { return Number(e.Order) == Number(model.Order) });
        if (duplicateOrders.length >= 2) {
            content += "<li class='message'>A Record with same Order already Exists !!</li>";
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
var SaveAllTaxonomyAssociationChanges = function (data) {
    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveTaxonomyAssociation").data('request-url'),
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
var ReloadTaxonomyAssociation = function () {

    
    LoadTaxonomyData(SiteData.Id);
};
var TaxonomyAssociationChanges = function () {
    var taxonomyAssociationGrid = $("#taxonomyAssociationGrid").data("kendoGrid"),
           updatedRecords = [],
           newRecords = [],
           updatedKeys = [],
           deletedRecords = [];

    if (taxonomyAssociationGrid) {
        var currentData = taxonomyAssociationGrid.dataSource.data();

        newRecords = $.grep(currentData, function (e) { return e.isNew() || Number(e.TaxonomyAssociationId) == 0 }).map(function (a) { return a.toJSON(); });;
        updatedRecords = $.grep(currentData, function (e) { return (e.dirty || e.IsUpdated) && Number(e.TaxonomyAssociationId) > 0 && !e.isNew() }).map(function (a) { return a.toJSON(); });
        updatedKeys = updatedRecords.map(function (a) { return a.Key });
        deletedRecords = $.grep(taxonomyAssociationGrid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.Key) < 0) && Number(e.TaxonomyAssociationId) >= 0 });
        deletedRecords = deletedRecords.map(function (a) { return a.toJSON(); });

    }
    var data = { newRecords: newRecords, updatedRecords: updatedRecords, deletedRecords: deletedRecords };
    return data;
};
$("#exportTA").click(function (e) {
    TaxonomyAssociationSave().done(function (data) {
        if (data) {
            TaxonomyMarketIdExcelExport('#taxonomyAssociationGrid');
        }
    });  
});


function TaxonomyMarketIdExcelExport(gridName) {
    var grid = $(gridName).data("kendoGrid");
    var rows = [{
            cells: [
                    { value: "CUSIPs", color: "#fff", background: "#08c", bold: true},
                    { value: "Name Override", color: "#fff", background : "#08c", bold: true },
                    { value: "Tabbed Page Name Override", color: "#fff", background : "#08c", bold: true },
                    { value: "Tooltip Information", color: "#fff", background : "#08c", bold: true },
                    { value: "Css Class", color: "#fff", background : "#08c", bold: true },
                    { value: "Order", color: "#fff", background : "#08c", bold: true }
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
                                { value: dataItem.Order }
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
                { width: 100 },
            ],
            title: "CUSIPs",
            rows: rows
        }
        ]
    });

    var fileName = "TaxonomyMarketId.xlsx";
    if (gridName == "#TAInvalidDataGrid")
    {
        fileName = "Invalid_TaxonomyMarketId.xlsx";
    }
    kendo.saveAs({dataURI: workbook.toDataURL(), fileName: fileName});
}

var LoadTaxonomyAssociation = function (siteId) {
    
    LoadTaxonomyAssociation_loader = new kendo.data.DataSource({
        transport: {
            read: {
                url: "",
                cache: true
            },
            type: "GET"
        }
    });
    LoadTaxonomyAssociation_loader.fetch(function () {
    });

    TaxonomySettings.InitialLoad = true;
    TaxonomySettings.SiteId = siteId;
    LoadTaxonomyData(siteId);
    $("#btnMarketIDSearch").kendoButton({
        click: onMarketIdSiteLevelSearch
    });
    deleteDocTypeWindow = $('<div />').kendoWindow({
        title: "Are you sure you want to delete this record?",
        visible: false, //the window will not appear before its .open method is called
        width: "500px",
        height: "200px",
        modal: true
    }).data("kendoWindow");
};
function onMarketIdSiteLevelSearch() {
    var query = $("#txtSearchTAString").val();
    var grid = $("#taxonomyAssociationGrid").data("kendoGrid");
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
function BindInvalidTAData(data) {
    var dataSource = new kendo.data.DataSource({
        data: {
            "data": data, "total": data.length
        },
        schema: {
            data: "data",
            total: "total",
        }
    });

    $("#TAInvalidDataGridContainer").show();
    $("#TAInvalidDataGridHeader").show();

    if($("#TAInvalidDataGrid").data("kendoGrid")) {
        $("#TAInvalidDataGrid").data("kendoGrid").destroy();
        $("#TAInvalidDataGrid").empty();
    }

    $("#TAInvalidDataGrid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["Export"],
        //excel: {
        //    fileName: "Inavlid_SiteLevel_CUSIPs.xlsx",
        //    filterable: true
        //},
        dataBound: marketIdSettings_gridDataBound,
        columns: [
            { field: "MarketId", width: 85, title: "CUSIPs", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "NameOverride", title: "Name Override", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "TabbedPageNameOverride", title: "Tabbed Page Name Override", headerAttribute: { class: "wrapheader" }, attribute: { class: "wraptext" } },
            { field: "DescriptionOverride", title: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "CssClass", width: 100, title: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },            
            { field: "Order", hidden: true, width: 75, title: "Order", headerAttribute: { class: "wrapheader" }, attribute: { class: "wraptext" } },
        ],
    });

    $("#TAInvalidDataGrid .k-grid-Export").on('click', function (e) {

        TaxonomyMarketIdExcelExport('#TAInvalidDataGrid');
    });
    
    var grid = $("#TAInvalidDataGrid").data("kendoGrid");
    if (SiteData.FundOrder == 'CustomizeOrder') {
        grid.showColumn("Order");
        grid.dataSource.sort({ field: "Order", dir: "asc" });
    }

}
var TaxonomySettings =
{
    LoadTaxonomyAssociation: LoadTaxonomyAssociation,
TaxonomyAssociationChanges: TaxonomyAssociationChanges,
SaveAllTaxonomyAssociationChanges: SaveAllTaxonomyAssociationChanges,
ReloadTaxonomyAssociation: ReloadTaxonomyAssociation,
InitialLoad: true,
    SiteId: null


};