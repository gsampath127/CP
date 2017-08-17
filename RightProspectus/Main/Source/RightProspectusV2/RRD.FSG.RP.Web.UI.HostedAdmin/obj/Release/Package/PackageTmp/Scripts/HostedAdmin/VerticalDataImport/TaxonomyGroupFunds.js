
var LoadFunds = function (siteId) {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociationGroup").data('request-url'),
                data: { siteId: Number(siteId), isFunds: true },
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
                id: "TaxonomyAssociationGroup",
                fields: {

                    TaxonomyAssociationGroupId: {},
                    Name: {},

                }
            }
        }
    });
    $("#GridContainer").show();

    if ($("#importgroupHierarchyGrid").data("kendoGrid")) {
        $("#importgroupHierarchyGrid").data("kendoGrid").destroy();
        $("#importgroupHierarchyGrid").empty();
    }

    $("#importgroupHierarchyGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: ImportgroupHierarchy_gridDataBound,
        detailTemplate: kendo.template($("#underlyingFundTemplate").html()),
        detailInit: LoadUnderlyingFund,//second grid generator
        excel: {
            fileName: "ProductToFundAssociation.xlsx",
            filterable: true
        },
        pageable: false,
        toolbar: [{
            name: 'Save Changes',
            template: '#= SaveGroupFundChanges()#'
        }],
        columns: [
            { field: "Name", width: 120, headerTemplate: "<span>Group Name</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },


        ],
        editable: false,
    });
    EnableTooltipTaxonomyGroupFunds();

}
function EnableTooltipTaxonomyGroupFunds() {
    var grid = $("#importgroupHierarchyGrid").data("kendoGrid");

    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 120,
        content: function (e) {
            var target = e.target; // element for which the tooltip is shown
            switch (target.text()) {
                default:
                    return $(target).text();
            }

        }
    });

}
function SaveGroupFundChanges(e) {
    return '<a class="k-button k-button-icontext k-grid-save-changes" href="#" id="toolbar-saveChanges" onclick="SaveGroupFundChangesResult()"><span class="k-icon k-i-update"></span>Save Changes</a>';
};
function SaveGroupFundChangesResult() {

    var changes = SaveAllGroupFundChanges(ImportGroupFundChanges()); // Save All Grid Changes to Server
    changes.done(function () {
        ReloadUnderLyingGroupFund();
        popupNotification.show("Group to Fund Association details saved Successfully", "success");

    }).fail(function () {
        popupNotification.show("Failed to save Group to Fund Association details", "error");
    });
};
function LoadUnderlyingFund(e) {

    var taxonomyAssociationGroupId = e.data.TaxonomyAssociationGroupId;
    var underlyingFundDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociationGroupTaxonomyAssociation").data('request-url'),//fetching data from TaxonomyAssociationGroupTaxonomyAssociation using site id and taxonomyassociation group id
                data: { siteID: Number(SiteData.Id), TaxonomyGroupId: taxonomyAssociationGroupId },
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
                id: "TaxonomyAssociationId",
                fields: {
                    TaxonomyAssociationId: {},
                    TaxonomyId: {},
                    MarketId: {},
                    NameOverride: { editable: false },
                    Order: { type: "number" },
                    AddMode: { type: "boolean" }

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

    
    
    var underlyingGroupFundGrid = $("<div id=underlyingGroupFundGrid/>").appendTo(e.detailCell).kendoGrid({
        dataSource: underlyingFundDataSource,
        dataBound: TaxonomyGroupFund_underlyingGroupFundGridDataBound,
        toolbar: ["create", "cancel"],
        excel: {
            fileName: "GroupedFunds.xlsx"
        },

        pageable: false,
        columns: [
                       { field: "MarketId", width: 100, editor: importFundDropdown, title: "Fund Cusip", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                       { field: "NameOverride", title: "Underlying Fund Name" },
                       { field: "Order", width: 60, title: "Order", hidden: true, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
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
                        { name: "Delete", click: onTaxonomyGroupFundDelete }, ]
                        , title: "&nbsp;", width: "170px"
                       }
        ],
        editable: { mode: "popup", confirmDelete: "No" },
        cancel: function (e) {
            $('.selectMultipleDelete').show();
        },
        edit: onunderlyingGroupFundGridEdit,
        save: function (e) {
           
            if (ValidateTaxonomyGroupFundsSave(e.model)) {
                AddMultipleGroup_CUSIPs(e.model, $("#MarketId").val(), this, taxonomyAssociationGroupId);
                this.refresh();
            }

        },

        remove: function (e) {
            this.refresh();

        }

    });

    var grid = underlyingGroupFundGrid.data("kendoGrid");
    if(SiteData.FundOrder == 'CustomizeOrder') {
   
        //Re Ordering of rows
        underlyingGroupFundGrid.data("kendoGrid").table.kendoDraggable({
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

        underlyingGroupFundGrid.data("kendoGrid").table.kendoDropTarget({
            group: "gridGroup",//This would recieve the drop target
            drop: function (e) {
                var grid = underlyingGroupFundGrid.data("kendoGrid");//$("#importProductGrid").getKendoGrid();
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



        // Show Order Column only if Customizable Order Feature is enabled
   
        grid.showColumn("Order");
        grid.dataSource.sort({ field: "Order", dir: "asc" });

    }
    else if (SiteData.FundOrder == 'AlphabeticalAsc') {
        grid.dataSource.sort({ field: "NameOverride", dir: "asc" });

    }
    else if (SiteData.FundOrder == 'AlphabeticalDesc') {
        grid.dataSource.sort({ field: "NameOverride", dir: "desc" });

    }

}
function importFundDropdown(container, options) {

    if (options.model.TaxonomyAssociationId <= 0 || options.model.TaxonomyAssociationId == "") {

        var element = $("#importgroupHierarchyGrid")
            .find("tr[data-uid='" + container.prevObject[0].dataset.uid + "']"),
            grid = element.closest(".k-grid").data("kendoGrid");
        gridData = grid.dataSource,
        MarketIds = [],
        DeletedMarketIds = [],

    MarketIds = gridData._data.map(function (a) {
        return a.MarketId;
    });
        DeletedMarketIds = gridData._destroyed.map(function (a) { return a.MarketId; });
        var result = $.grep(TaxonomyGroupFundData, function (e) { return MarketIds.indexOf(e.MarketId) < 0 || DeletedMarketIds.indexOf(e.MarketId) >= 0 });
        var reformattedArray = result.map(function (obj) {
            var rObj = { label: obj.MarketId + "-" + obj.NameOverride, title: obj.NameOverride, value: obj.TaxonomyAssociationId };
            return rObj;
        });
       $('<select id="MarketId" name="MarketId" multiple="multiple"></select>')
            .appendTo(container)
            .multiselect({
                numberDisplayed: 0,
                includeSelectAllOption: false,
                enableFiltering: true,
                filterBehavior: 'text',
                enableCaseInsensitiveFiltering: true,
                maxHeight: 200,
                maxWidth: 100
            });
        $('#MarketId')
            .empty()
            .multiselect('dataprovider', reformattedArray);
    }

    else {
        var element = "<label>" + options.model.MarketId + "</label>";

        $(element)
       .appendTo(container);
    }
}
function onunderlyingGroupFundGridEdit(arg) {   
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
        arg.container.kendoWindow("title", "Add New Taxonomy Group Fund");
    } else {
        arg.container.kendoWindow("title", "Edit Taxonomy Group Fund");
    }

    if (SiteData.FundOrder != 'CustomizeOrder') {
        $('[name="Order"]').hide();

    }
    arg.container.addClass("TaxonomyGroupFunds-EditPopup");
    arg.container[0].children[0].children[2].style.setProperty("display", "none");

    $(this)[0].tbody.find('tr').find(".selectMultipleDelete").prop('checked', false);
    $(this)[0].thead.find('tr').find(".selectALLMultipleDelete").prop('checked', false);
}
function ImportgroupHierarchy_gridDataBound(e) {
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

function TaxonomyGroupFund_underlyingGroupFundGridDataBound(e) {
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
        if (SiteData.Order != 'CustomizeOrder') {
            $(this).remove();
        }
    });

    $('.selectALLMultipleDelete').change(function () {
        if ($(this).is(':checked')) {
            $(this).closest('#underlyingGroupFundGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', true);
        }
        else {
            $(this).closest('#underlyingGroupFundGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
        }
    });

};
var LoadIUnderLyingFund = function (siteId) {
    
    UnderlyingFundSettings.InitialLoad = true;
    InitializeUnderlyingFund(siteId);

};


function InitializeUnderlyingFund(siteId) {

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

        TaxonomyGroupFundData = taxonomyMarketIds._data;
        LoadFunds(siteId); // main grid
    });


    deleteDocTypeWindow = $('<div />').kendoWindow({
        title: "are you sure you want to delete this record?",
        visible: false, //the window will not appear before its .open method is called
        width: "500px",
        height: "200px",
        modal: true,
    }).data("kendoWindow");


}

function ValidateTaxonomyGroupFundsSave(model) {

    var isSuccess = true;
    var content = "";
    content += "<p class='message'>Please Enter/Select the below fields</p>";
    content += "<ul>";
    if (SiteData.Order == 'CustomizeOrder' && (!model.Order || Number(model.Order) < 1)) {
        content += "<li class='message'>Order</li>";
        isSuccess = false;
    }
    else if (SiteData.Order == 'CustomizeOrder') {
        var parentGrid = $("#importgroupHierarchyGrid").getKendoGrid();
        var parentGroup = parentGrid.dataSource._data.map(function (o) {
            return o.TaxonomyAssociationGroupId;
        });


        $("#importgroupHierarchyGrid").find(".k-grid").each(function (index, element) {

            if (parentGroup.indexOf(model.TaxonomyAssociationGroupId) >= 0) {
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

var SaveAllGroupFundChanges = function (data) {

    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveGroupFundHierarchy").data('request-url'),
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
var ImportGroupFundChanges = function () {
    var updatedRecords = [],
           newRecords = [],
           deletedRecords = [];
    $("#importgroupHierarchyGrid").find("tbody tr .k-grid").each(function (index, element) {
        var grid = $(element).data("kendoGrid");
        if (grid) {
            var currentData = grid.dataSource.data(), updatedKeys = [], added = [], deleted = [], updated = [], deletedKeys = [];
            updated = $.grep(currentData, function (e) { return e.dirty == true && e.isNew() == false }).map(function (a) { return a.toJSON(); });
            updatedKeys = updated.map(function (a) { return a.TaxonomyAssociationGroupId + '-' + a.TaxonomyAssociationId });
            deleted = $.grep(grid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.TaxonomyAssociationGroupId + '-' + e.TaxonomyAssociationId) < 0); });
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
var ReloadUnderLyingGroupFund = function () {
    LoadFunds(SiteData.Id);
};
$("#exportlnkTAGD").click(function (e) {
    var grid = $("#importgroupHierarchyGrid").data("kendoGrid");
    grid.cancelChanges();
    var url = $("#dvTaxonomyAssociationGroupFundsExport").data('request-url') + '?siteId=' + SiteData.Id + '&taxonomyOrderFeatureMode=' +SiteData.FundOrder;
    window.open(url, "_blank");

});


$("#groupFundsUpload").kendoUpload({

    async: {
        saveUrl: $("#dvTaxonomyAssociationGroupFundsImport").data('request-url'),

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
            $("#TAGDInvalidDataGridContainer").hide();
            $("#TAGDInvalidDataGridHeader").hide();

            $(".k-upload-files li").removeClass('k-file-success');
            $(".k-upload-files li").addClass('k-file-error');
            $(".k-upload-files .k-upload-pct").text('Please import file with less than 15000 rows.');
        }
        else
        {
            if (e.response.status == "Success") {
                ReloadUnderLyingGroupFund();
                popupNotification.show("Group to Fund Association details saved Successfully", "success");
            }
            else {
                popupNotification.show("Failed to save Group to Fund Association details", "error");
            }


            BindInvalidData(e.response.invalidData);
            if (e.response.invalidData.length == 0) {

                $("#importGroupFundsHierarchy").data("kendoWindow").close();
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
$("#TAGDlnkImport").click(function () {

    $("#TAGDInvalidDataGridContainer").hide();
    $("#TAGDInvalidDataGridHeader").hide();

    TaxonomyGroupFundSave().done(function (data) {
        if (data) {
            var grid = $("#importgroupHierarchyGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importGroupFundsHierarchy").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }
    });

});



function BindInvalidData(data) {
    var dataSource = new kendo.data.DataSource({
        data: {
            "data": data, "total": data.length
        },
        schema: {
            data: "data",
            total: "total",
        }
    });

    $("#TAGDInvalidDataGridContainer").show();
    $("#TAGDInvalidDataGrid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["Export"],
        excel: {
            fileName: "Inavlid_GroupFundAssociation.xlsx",
            filterable: true
        },
        dataBound: ImportgroupHierarchy_gridDataBound,
        columns: [
                       { field: "Name", title: "Group Name", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                       { field: "MarketId", title: "CUSIP", width: 150 },
                       { field: "Order", title: "Order", hidden: true, width: 150, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
        ],
    });

    var grid = $("#TAGDInvalidDataGrid").data("kendoGrid");
    if (SiteData.FundOrder == 'CustomizeOrder') {
        grid.showColumn("Order");
        grid.dataSource.sort({ field: "Order", dir: "asc" });
    }

    $("#TAGDInvalidDataGrid .k-grid-Export").on('click', function (e) {

        TAGTAInvalidGridExcelExport('#TAGDInvalidDataGrid');
    });


}

function TAGTAInvalidGridExcelExport(gridName) {
   
    var grid = $(gridName).data("kendoGrid");
    var rows = [{
            cells: [
                    { value: "Group Name", color: "#fff", background: "#08c", bold: true},
                    { value: "CUSIP", color: "#fff", background : "#08c", bold: true },
                    { value: "Order", color: "#fff", background : "#08c", bold: true }                    
                   ]
                }];

    if (gridName == "#TAGDInvalidDataGrid")
    {
        var trs = $(gridName).find("tbody").find('tr');
        
        for (var i = 0; i < trs.length; i++) {
        
            var dataItem = grid.dataItem(trs[i]);                   
            rows.push({
                    cells: [
                            { value: dataItem.Name },
                            { value: dataItem.MarketId },
                            { value: dataItem.Order }
                            ]
                    });        
            }
    }    

    var workbook = new kendo.ooxml.Workbook({
                sheets: [
                {
                freezePane: {
                rowSplit: 1
            },
            columns: [
                { width: 250 },
                { width: 100 },
                { width: 100 }                
            ],
            title: "Group to Fund Mapping",
            rows: rows
        }
        ]
    });

    var fileName = "GroupToFundAssociation.xlsx";
    if (gridName == "#TAHDInvalidDataGrid")
    {
        fileName = "Invalid_GroupToFundAssociation.xlsx";
    }
    kendo.saveAs({dataURI: workbook.toDataURL(), fileName: fileName});
}

function onTaxonomyGroupFundDelete(e) {
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
    $("#importgroupHierarchyGrid").find("input:checked").each(function () {

        var data1 = currentRef.dataItem($(this).closest('tr'));
        list.push({ uid: data1.uid });

    });
    var messageToDisplay = "";
    if (list.length > 0) {
        messageToDisplay = $("#deleteTaxonomyAssociationGroupFundTemplate").html().replace("MarketId", "all selected Fund's");
    }
    else {
        messageToDisplay = $("#deleteTaxonomyAssociationGroupFundTemplate").html().replace("MarketId", data.MarketId);
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

function AddMultipleGroup_CUSIPs(model, selectedValues,grid,groupId) {
    if (Number(model.TaxonomyAssociationId)==0)
    {
        grid.dataSource.remove(model);
    }
    if (selectedValues && selectedValues.length >= 0) {
        
        for (var i = 0 ; i < selectedValues.length; i++) {
           
            var option = $("#MarketId option[value='" + selectedValues[i] + "']");
            grid.dataSource.add({
                TaxonomyAssociationId: selectedValues[i],
                MarketId: option[0].label.split('-')[0],
                NameOverride: option[0].label.split('-')[1],
                TaxonomyAssociationGroupId:groupId,
                Order: model.Order + i,
                AddMode: true
            });
        }
    }
}

var UnderlyingFundSettings =
{
    LoadIUnderLyingFund: LoadIUnderLyingFund,
    ImportGroupFundChanges: ImportGroupFundChanges,
    SaveAllGroupFundChanges: SaveAllGroupFundChanges,
    ReloadUnderLyingGroupFund: ReloadUnderLyingGroupFund,
    InitialLoad: true,
    LoadFunds: LoadFunds
};