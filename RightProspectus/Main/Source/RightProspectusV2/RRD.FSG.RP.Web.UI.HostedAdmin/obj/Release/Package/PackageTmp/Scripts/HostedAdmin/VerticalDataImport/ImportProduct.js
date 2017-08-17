
var LoadImportProductData = function (siteId) {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociation").data('request-url'),
                data: { siteId: Number(siteId) },
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

        schema: {
            data: "data",
            total: "total",
            model: {
                id: "TaxonomyAssociationId",
                fields: {
                    TaxonomyId: {},
                    MarketId: {},
                    NameOverride: {},
                    Order: {}
                }
            }
        }
    });
    $("#GridContainer").show();

    if ($("#importProductGrid").data("kendoGrid")) {
        $("#importProductGrid").data("kendoGrid").destroy();
        $("#importProductGrid").empty();
    }

    $("#importProductGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: ImportProductMapping_gridDataBound,
        detailTemplate: kendo.template($("#underlyingFundTemplate").html()),
        detailInit: LoadUnderlyingFundData,//second grid generator
        excel: {
            fileName: "ProductToFundAssociation.xlsx",
            filterable: true
        },
        pageable: false,
        toolbar: [{
            name: 'Save Changes',
            template: '#= SaveChanges()#'
        }],
        columns: [
            { field: "MarketId", width: 120, headerTemplate: "<span>Product CUSIPs</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            {
                field: "NameOverride", headerTemplate: "<span>Product Name</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: {
                    class: "wraptext"
                }
            },

        ],
        editable: false,
    });
    EnableimportProductGridTooltip();
    var grid = $("#importProductGrid").data("kendoGrid");
    if (SiteData.FundOrder == 'CustomizeOrder') {

        grid.dataSource.sort({ field: "Order", dir: "asc" });

    }
    else if (SiteData.FundOrder == 'AlphabeticalAsc') {
        grid.dataSource.sort({ field: "NameOverride", dir: "asc" });
    }
    else if (SiteData.FundOrder == 'AlphabeticalDesc') {
        grid.dataSource.sort({ field: "NameOverride", dir: "desc" });
    }
}
function EnableimportProductGridTooltip() {
    var grid = $("#importProductGrid").data("kendoGrid");

    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 120,
        content: function (e) {
            var target = e.target; // element for which the tooltip is shown
            switch (target.text()) {
                case "Market Id":
                    return "Document Type";
                case "Name Override":
                    return "Tooltip Information";
                default:
                    return $(target).text();
            }

        }
    });

}
function SaveChanges(e) {
    return '<a class="k-button k-button-icontext k-grid-save-changes" href="#" id="toolbar-saveChanges" onclick="SaveChangesResult()"><span class="k-icon k-i-update"></span>Save Changes</a>';
};
function SaveChangesResult() {
    var changes = SaveAllImportProductChanges(ImportProductChanges()); // Save All Grid Changes to Server
    changes.done(function () {
        ReloadImportProduct();
        popupNotification.show("Products to Fund Association saved Successfully", "success");

    }).fail(function () {
        popupNotification.show("Failed to save Products to Fund Association", "error");
    });
};
function LoadUnderlyingFundData(e) {

    var taxonomyAssociationId = e.data.TaxonomyAssociationId, parentMarketId = e.data.MarketId;


    var underlyingFundDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociationHierarchy").data('request-url'),
                data: { siteId: Number(SiteData.Id), parentTaxonomyAssociationId: taxonomyAssociationId },
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
        //   batch:true,
        schema: {
            data: "data",
            total: "total",
            model: {
                id: "ChildTaxonomyAssociationId",
                fields: {

                    ChildTaxonomyId: {},
                    ChildMarketId: {},
                    ChildNameOverride: { editable: false },
                    Order: { type: "number" },
                    ParentTaxonomyAssociationId: {},
                    AddMode: {type:"boolean"}
                }
            }
        },
        sort: { field: "Order", dir: "asc" }
    });
    var detailRow = e.detailRow;

    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    // detailRow.find("#importProductGrid").kendoGrid({
    var underlyingFundGrid = $("<div id=underlyingFundGrid/>").appendTo(e.detailCell).kendoGrid({
        dataSource: underlyingFundDataSource,
        dataBound: ImportProductMapping_underlyingFundGridDataBound,
        toolbar: ["create", "cancel"],
        excel: {
            fileName: "Products1.xlsx"
        },

        pageable: false,
        columns: [
                       { field: "ChildMarketId", width: 150, editor: importMarketIdDropdown, title: "Underlying Fund CUSIPs", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                       { field: "ChildNameOverride", title: "Underlying Fund Name" },
                       { field: "Order", width: 100, title: "Order", hidden: true, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
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
                        { name: "Delete", click: onTaxonomyHierarchyFundDelete }, ]
                        , title: "&nbsp;", width: "170px"
                       }
        ],
        editable: { mode: "popup", confirmDelete: "No" },
        cancel: function (e) { $('.selectMultipleDelete').show(); },
        edit: onunderlyingFundGridEdit,
        save: function (e) {
          
            if (ValidateImportProductSave(e.model)) {
                
                AddMultipleProductChilds_CUSIPs(e.model, $("#ChildMarketId").val(), this, taxonomyAssociationId);
                this.refresh();
            }

        },

        remove: function (e) {
            this.refresh();

        }

    });

    
    // Show Order Column only if Customizable Order Feature is enabled
    var grid = underlyingFundGrid.data("kendoGrid");
    if (SiteData.FundOrder == 'CustomizeOrder') {
    //Re Ordering of rows
    underlyingFundGrid.data("kendoGrid").table.kendoDraggable({
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

    underlyingFundGrid.data("kendoGrid").table.kendoDropTarget({
        group: "gridGroup",//This would recieve the drop target
        drop: function (e) {
            var grid = underlyingFundGrid.data("kendoGrid");//$("#importProductGrid").getKendoGrid();
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



    
        grid.showColumn("Order");
        grid.dataSource.sort({ field: "Order", dir: "asc" });

    }
    else if (SiteData.FundOrder == 'AlphabeticalAsc') {
        grid.dataSource.sort({ field: "ChildNameOverride", dir: "asc" });

    }
    else if (SiteData.FundOrder == 'AlphabeticalDesc') {
        grid.dataSource.sort({ field: "ChildNameOverride", dir: "desc" });

    }
}


function importMarketIdDropdown(container, options) {

    if (options.model.ChildTaxonomyAssociationId <= 0 || options.model.ChildTaxonomyAssociationId == "") {

        var element = $("#importProductGrid")
            .find("tr[data-uid='" + container.prevObject[0].dataset.uid + "']"),
            grid = element.closest(".k-grid").data("kendoGrid");
        gridData = grid.dataSource,
        MarketIds = [],
        DeletedMarketIds = [],

    MarketIds = gridData._data.map(function (a) {
        return a.ChildMarketId;
    });
        DeletedMarketIds = gridData._destroyed.map(function (a) { return a.ChildMarketId; });
        var result = $.grep(TaxonomyData, function (e) { return MarketIds.indexOf(e.MarketId) < 0 || DeletedMarketIds.indexOf(e.MarketId) >= 0 });
        var reformattedArray = result.map(function (obj) {
            var rObj = { label: obj.MarketId + "-" + obj.NameOverride, title: obj.NameOverride, value: obj.TaxonomyAssociationId };
            return rObj;
        });
        $('<select id="ChildMarketId" name="ChildMarketId" multiple="multiple"></select>')
            .appendTo(container)
            .multiselect({
                numberDisplayed: 0,
                includeSelectAllOption: false,
                enableFiltering: true,
                filterBehavior: 'text',
                enableCaseInsensitiveFiltering: true,
                maxHeight: 200,
                maxWidth: 300
            });
        $('#ChildMarketId')
            .empty()
            .multiselect('dataprovider', reformattedArray);        
    }


    else {
        var element = "<label>" + options.model.ChildMarketId + "</label>";

        $(element)
       .appendTo(container);
    }
}
function onunderlyingFundGridEdit(arg) {
    $('.selectMultipleDelete').hide();
    if (arg.model.isNew() && SiteData.FundOrder == 'CustomizeOrder') {

        var grid = this;
        var Orders = grid._data.map(function (a) { return a.Order; });
        if (Orders.length > 0)
            arg.model.set("Order", Math.max.apply(Math, Orders) + 1);
        else
            arg.model.set("Order", 1);
    }

    if (arg.model.isNew()) {
        arg.container.kendoWindow("title", "Add New Underlying Fund");
    } else {
        arg.container.kendoWindow("title", "Edit Underlying Fund");
    }

    arg.container.addClass("ImportProduct-EditPopup");
    if (arg.container[0].children[0].children[3].firstChild == null) {
        arg.container[0].children[0].children[2].style.setProperty("display", "none");
    }
    else {
        arg.container[0].children[0].children[3].style.setProperty("padding", ".4em 0 1em");
    }

    $(this)[0].tbody.find('tr').find(".selectMultipleDelete").prop('checked', false);
    $(this)[0].thead.find('tr').find(".selectALLMultipleDelete").prop('checked', false);
}
function ImportProductMapping_gridDataBound(e) {
     
    //this.expandRow(this.tbody.find("tr.k-master-row"));
    //this.collapseRow(this.tbody.find("tr.k-master-row"));
    
    var grid = e.sender;
    if (grid.dataSource.total() == 0) {
        var colCount = grid.columns.length;
        $(e.sender.wrapper)
            .find("tbody")
.append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data"> <b>No Records Found<b> </td></tr>');
        $(e.sender.wrapper)
            .find(".k-grid-content-expander").remove();

    }
};

function ImportProductMapping_underlyingFundGridDataBound(e) {

    var grid = e.sender;
    if (grid.dataSource.total() == 0) {
        var colCount = grid.columns.length;
        $(e.sender.wrapper)
            .find("tbody")
.append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data"> <b>No Records Found<b> </td></tr>');
        $(e.sender.wrapper)
            .find(".k-grid-content-expander").remove();

    }

    $(grid).find('tbody tr .k-grid-edit').each(function() {
        var currentDataItem = grid.dataItem($(this).closest("tr"));
        if (SiteData.FundOrder != 'CustomizeOrder') {
            $(this).remove();
        }

    });

    $('.selectALLMultipleDelete').change(function () {
        if ($(this).is(':checked')) {
            $(this).closest('#underlyingFundGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', true);
        }
        else {
            $(this).closest('#underlyingFundGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
        }
    });
};
var LoadImportProduct = function (siteId) {
    
    ImportProductSettings.InitialLoad = true;
    InitializeProduct(siteId);

};


function InitializeProduct(siteId) {
    

    var taxonomyMarketIds = new kendo.data.DataSource({
        transport: {
            read: {//Check for the method in the Controller
                url: $("#dvTaxonomyAssociation").data('request-url'),
                cache: false,
                data: { siteId: null },
                type: "POST",
                dataType: "json"
            },

            parameterMap: function (options, operation) {

                return options;
            }
        },
        schema: {
            data: "data",
            total: "total"
        }

    });

    taxonomyMarketIds.fetch(function () {
        TaxonomyData = taxonomyMarketIds._data;
        LoadImportProductData(siteId); // main grid
    });


    deleteDocTypeWindow = $('<div />').kendoWindow({
        title: "are you sure you want to delete this record?",
        visible: false, //the window will not appear before its .open method is called
        width: "500px",
        height: "200px",
        modal: true,
    }).data("kendoWindow");


}

function ValidateImportProductSave(model) {

    var isSuccess = true;
    var content = "";
    content += "<p class='message'>Please Enter/Select the below fields</p>";
    content += "<ul>";
    if (SiteData.FundOrder == 'CustomizeOrder' && (!model.Order || Number(model.Order) < 1)) {
        content += "<li class='message'>Order</li>";
        isSuccess = false;
    }
    else if (SiteData.FundOrder == 'CustomizeOrder') {
        var parentGrid = $("#importProductGrid").getKendoGrid();
        var parentFunds = parentGrid.dataSource._data.map(function (o) {
            return o.TaxonomyAssociationId;
        });


        $("#importProductGrid").find(".k-grid").each(function (index, element) {

            if (parentFunds.indexOf(model.ParentTaxonomyAssociationId) >= 0) {
                var grid = $(element).data("kendoGrid");
                var duplicateOrders = $.grep(grid.dataSource._data, function (e) { return Number(e.Order) == Number(model.Order) });
                if (duplicateOrders.length >= 2) {
                    content += "<li class='message'>A Record with same Order already Exists !!</li>";
                    isSuccess = false;
                }
            }
        });

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

var SaveAllImportProductChanges = function (data) {
    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveDocumentTypeAssociationHierarchy").data('request-url'),
        data: { updated: data.updatedRecords, deleted: data.deletedRecords, added: data.newRecords },
        type: "POST",
        success: function (data) {
            if (data == "Success")
                deferred.resolve();
            else
                deferred.reject();
        },
        error: function () {
            //Handle the server errors using the approach from the previous example
        }

    });
    return deferred.promise();
};
var ImportProductChanges = function () {


    var updatedRecords = [],
           newRecords = [],
           deletedRecords = [];


    $("#importProductGrid").find(".k-grid").each(function (index, element) {
        var grid = $(element).data("kendoGrid");
        if (grid) {
            var currentData = grid.dataSource.data(), updatedKeys = [], added = [], deleted = [], updated = [], deletedKeys = [];
          
            console.log(currentData);
            updated = $.grep(currentData, function (e) { return e.dirty == true && e.isNew() == false }).map(function (a) { return a.toJSON(); });
            updatedKeys = updated.map(function (a) { return a.ParentTaxonomyAssociationId + '-' + a.ChildTaxonomyAssociationId });
            deleted = $.grep(grid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.ParentTaxonomyAssociationId + '-' + e.ChildTaxonomyAssociationId) < 0); });
            deleted = deleted.map(function (a) { return a.toJSON(); });
            deletedKeys = deleted.map(function (a) { return a.Key });
            added = $.grep(currentData, function (e) {
                return (e.isNew() || e.AddMode == true)
            }).map(function (a) {
                return a.toJSON();
            });;

            //Concat data
            newRecords.push.apply(newRecords, added);
            updatedRecords.push.apply(updatedRecords, updated);
            deletedRecords.push.apply(deletedRecords, deleted);


        };
    });

    var data = { newRecords: newRecords, updatedRecords: updatedRecords, deletedRecords: deletedRecords };
    return data;
};
var ReloadImportProduct = function () {
    LoadImportProductData(SiteData.Id);
};
$("#exportTAHD").click(function (e) {
    TaxonomyAssociationHierarchySave().done(function (data) {
        if (data) {
            //var grid = $("#importProductGrid").data("kendoGrid");
            //grid.cancelChanges();
            var url = $("#dvTaxonomyAssociationHierarchyExport").data('request-url') + '?siteId=' + SiteData.Id + '&taxonomyOrderFeatureMode=' + SiteData.FundOrder;
            window.open(url, "_blank");            
            
        }
    });

});
$("#taxonomyAssociationHierarchyUpload").kendoUpload({

    async: {
        saveUrl: $("#dvTaxonomyHierarchyImport").data('request-url'),

    },
    upload: function (e) {
        e.data = {
            siteId: SiteData.Id,
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

        if (e.response.status == "countgreaterthan15000")
        {
            $("#TAHDInvalidDataGridContainer").hide();
            $("#TAHDInvalidDataGridHeader").hide();

            $(".k-upload-files li").removeClass('k-file-success');
            $(".k-upload-files li").addClass('k-file-error');
            $(".k-upload-files .k-upload-pct").text('Please import file with less than 15000 rows.');
        }
        else
        {
            if (e.response.status == "Success") {
                ReloadImportProduct();
                popupNotification.show("Products to Fund Association saved Successfully", "success");
            }
            else {
                popupNotification.show("Failed to save Products to Fund Association", "error");
            }

            BindInvalidTAHDData(e.response.invalidData);
            if (e.response.invalidData.length == 0) {

                $("#importTaxonomyAssociationHierarchy").data("kendoWindow").close();
            }
        }
    }
});

//function ExpandChildGrids() {
//    var parentGrid = $("#importProductGrid").getKendoGrid();
//    parentFunds = parentGrid.dataSource._data.map(function (o) {
//        return o.TaxonomyAssociationId;
//    });


//    parentGrid.expandRow(parentGrid.tbody.find("tr.k-master-row"));
//    var expandedRows = $('.k-detail-row:visible');

//}
$("#TAHDlnkImport").click(function () {

    $("#TAHDInvalidDataGridContainer").hide();
    $("#TAHDInvalidDataGridHeader").hide();

    TaxonomyAssociationHierarchySave().done(function (data) {
        if (data) {
            var grid = $("#importProductGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importTaxonomyAssociationHierarchy").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }
    });

});

function BindInvalidTAHDData(data) {
    var dataSource = new kendo.data.DataSource({
        data: {
            "data": data, "total": data.length
        },
        schema: {
            data: "data",
            total: "total",
        }
    });

    $("#TAHDInvalidDataGridContainer").show();
    $("#TAHDInvalidDataGridHeader").show();

    if($("#TAHDInvalidDataGrid").data("kendoGrid")) {
        $("#TAHDInvalidDataGrid").data("kendoGrid").destroy();
        $("#TAHDInvalidDataGrid").empty();
}

    $("#TAHDInvalidDataGrid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["Export"],
        excel: {
            fileName: "Invalid_ProductToFundAssociation.xlsx",
            filterable: true
        },
        dataBound: ImportProductMapping_gridDataBound,
        columns: [
                       { field: "ParentMarketId", title: "Product CUSIPs", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                       { field: "ChildMarketId", title: "Underlying Fund CUSIPs" },
                       { field: "Order", title: "Order", hidden: true, width: 140, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
        ],
    });

    var grid = $("#TAHDInvalidDataGrid").data("kendoGrid");
    if (SiteData.FundOrder == 'CustomizeOrder') {
        grid.showColumn("Order");
        grid.dataSource.sort({ field: "Order", dir: "asc" });
    }

    $("#TAHDInvalidDataGrid .k-grid-Export").on('click', function (e) {

        TAHDInvalidGridExcelExport('#TAHDInvalidDataGrid');
    });

}

function TAHDInvalidGridExcelExport(gridName) {
   
    var grid = $(gridName).data("kendoGrid");
    var rows = [{
            cells: [
                    { value: "Product CUSIPs", color: "#fff", background: "#08c", bold: true},
                    { value: "Underlying Fund CUSIPs", color: "#fff", background : "#08c", bold: true },
                    { value: "Order", color: "#fff", background : "#08c", bold: true }                    
                   ]
                }];

    if (gridName == "#TAHDInvalidDataGrid")
    {
        var trs = $(gridName).find("tbody").find('tr');
        
        for (var i = 0; i < trs.length; i++) {
        
            var dataItem = grid.dataItem(trs[i]);                   
            rows.push({
                    cells: [
                            { value: dataItem.ParentMarketId },
                            { value: dataItem.ChildMarketId },
                            { value: dataItem.Order }
                            ]
                    });        
            }
    }
    else
    {
        var exportAllRecords = true;
        $("#importProductGrid").find(".k-grid").each(function(index, element) {
            if ($(element).find("tbody").find('tr').find(":checkbox").is(":checked")) {
                exportAllRecords = false;
            }
        });

        $("#importProductGrid").find(".k-grid").each(function(index, element) {
            var grid = $(element).data("kendoGrid");
            var trs = $(element).find("tbody").find('tr');
            if (grid) {
                
                var currentData = grid.dataSource.data();
                for (var i = 0; i < currentData.length; i++) {
                    if (exportAllRecords || $(trs[i]).find(":checkbox").is(":checked")) {
                        rows.push({
                            cells: [
                                    { value: currentData[i].ParentMarketId },
                                    { value: currentData[i].ChildMarketId },
                                    { value: currentData[i].Order }
                                    ]
                            });
                    }
                }
        }});        
                            
    }

    var workbook = new kendo.ooxml.Workbook({
                sheets: [
                {
                freezePane: {
                rowSplit: 1
            },
            columns: [
                { width: 200 },
                { width: 200 },
                { width: 100 }                
            ],
            title: "Product Mapping",
            rows: rows
        }
        ]
    });

    var fileName = "ProductToFundAssociation.xlsx";
    if (gridName == "#TAHDInvalidDataGrid")
    {
        fileName = "Invalid_ProductToFundAssociation.xlsx";
    }
    kendo.saveAs({dataURI: workbook.toDataURL(), fileName: fileName});
}

function onTaxonomyHierarchyFundDelete(e) {
    var grid = this;
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
    $("#importProductGrid").find("input:checked").each(function () {

        var data1 = currentRef.dataItem($(this).closest('tr'));
        list.push({ uid: data1.uid });

    });
    var messageToDisplay = "";
    if (list.length > 0) {
        messageToDisplay = $("#deleteTaxonomyHierarchyFund").html().replace("MarketId", "all selected Underlying Funds's");
    }
    else {
        messageToDisplay = $("#deleteTaxonomyHierarchyFund").html().replace("MarketId", data.ChildMarketId);
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

            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });
}

function AddMultipleProductChilds_CUSIPs(model, selectedValues, grid,parentId) {
    if (Number(model.ChildTaxonomyAssociationId)==0)
    {
        grid.dataSource.remove(model);
    }
    
    if (selectedValues && selectedValues.length >= 0) {
       
        for (var i = 0 ; i < selectedValues.length; i++) {

            var option = $("#ChildMarketId option[value='" + selectedValues[i] + "']");
          
            grid.dataSource.add({
                
                ChildTaxonomyAssociationId: selectedValues[i],
                ChildMarketId: option[0].label.split('-')[0],
                ChildNameOverride: option[0].label.split('-')[1],
                Order: model.Order + i,
                ParentTaxonomyAssociationId: parentId,
                AddMode : true
                
            });
        }
    }
}

var ImportProductSettings =
{
    LoadImportProduct: LoadImportProduct,
    ImportProductChanges: ImportProductChanges,
    SaveAllImportProductChanges: SaveAllImportProductChanges,
    ReloadImportProduct: ReloadImportProduct,
    InitialLoad: true,
    LoadImportProductData: LoadImportProductData
};