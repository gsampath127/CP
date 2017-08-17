
function LoadTaxonomyGroup(siteId) {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociationGroup").data('request-url'),
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
                id: "TaxonomyAssociationGroupId",
                fields: {
                    Name: {},
                    TaxonomyAssociationGroupId:{},
                    ParentTaxonomyAssociationGroupId:{},
                    Description: {},
                    CssClass: {},
                    SiteId: {},
                    Level:{type:"number"}
                }
            }
        },
    });

    $("#GridContainer").show();

    if ($("#taxonomyAssociationGroupGrid").data("kendoGrid")) {
        $("#taxonomyAssociationGroupGrid").data("kendoGrid").destroy();
        $("#taxonomyAssociationGroupGrid").empty();
    }

    $("#taxonomyAssociationGroupGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: TaxonomyGroup_gridDataBound,
        toolbar: ["create", "save", "cancel"],        
        pageable: false,
        columns: [
            { field: "Name", title: "Name", headerTemplate: "<span>Name</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "ParentTaxonomyAssociationGroupId", title: "Parent Group", editor: ParentGroupDropdown, headerTemplate: "<span>Parent Group</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" }, template: '#=TaxonomyGroupTemplate(ParentTaxonomyAssociationGroupId,SiteId)#' },
            { field: "Description", title: "Description", title: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden:true },
            { field: "CssClass", title: "CssClass", title: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            

            {
                command: [{ name: "edit" },], title: "&nbsp;", width: "170px"
            }

        ],

        editable: { mode: "popup", confirmDelete: "No" },
        edit: onTaxonomyAssociationGroupGridEdit,
        save: function (data) {
            debugger;
            if (Number(data.model.ParentTaxonomyAssociationGroupId) <= 0)
            {
                data.model.SiteId = SiteData.Id;
                data.model.Level = 0;
            }
            else {
                var gridData = $("#taxonomyAssociationGroupGrid").data("kendoGrid").dataSource,
                parentData = $.grep(gridData._data, function (e) { return e.TaxonomyAssociationGroupId == data.model.ParentTaxonomyAssociationGroupId });
                data.model.Level = parentData[0].Level + 1;
            }
             
            
            if (ValidateTaxonomyGroupSave(data.model)) {
                this.refresh();
                
            }

        },
        saveChanges: function (e) {
            $("#taxonomyAssociationGroupGrid .k-grid-save-changes").prop('disabled', true);
            $("#taxonomyAssociationGroupGrid .k-grid-save-changes").addClass('disabled');

            var changes = SaveAllTaxonomyAssociationGroupChanges(TaxonomyAssociationGroupChanges()); // Save All Grid Changes to Server

            changes.done(function () {

                $("#taxonomyAssociationGroupGrid .k-grid-save-changes").prop('disabled', false);
                $("#taxonomyAssociationGroupGrid .k-grid-save-changes").removeClass('disabled');

                ReloadTaxonomyAssociationGroup();
                popupNotification.show("Group details Saved Successfully", "success");

            }).fail(function () {

                $("#taxonomyAssociationGroupGrid .k-grid-save-changes").prop('disabled', false);
                $("#taxonomyAssociationGroupGrid .k-grid-save-changes").removeClass('disabled');

                popupNotification.show("Failed to save Group details", "error");
            });
        },

        remove: function (e) {
            this.refresh();

        }

    });
    EnableTaxonomyGroupTooltip();
   
}

function onTaxonomyAssociationGroupGridEdit(arg) {
    
    if (arg.model.isNew()) {
        arg.container.kendoWindow("title", "Add New Taxonomy Group");
    } else {
        arg.container.kendoWindow("title", "Edit Taxonomy Group");
    }

    arg.container.addClass("TaxonomyGroups-EditPopup");
    if (arg.container[0].children[0].children[3].childElementCount == 0 || arg.container[0].children[0].children[3].firstChild.nodeName.toLowerCase() == "label") {
        arg.container[0].children[0].children[3].style.setProperty("padding", ".4em 0 1em");
    }
}

function EnableTaxonomyGroupTooltip()
{
    var grid = $("#taxonomyAssociationGroupGrid").data("kendoGrid");
    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 120,
        content: function (e) {
            var target = e.target; // element for which the tooltip is shown
            switch (target.text()) {
                case "Name":
                    return "Name";
                case "Parent Group":
                    return "Parent Group";
                case "Description":
                    return "Description";
                case "CssClass":
                    return "CssClass";
                default:
                    return $(target).text();
            }
        }
    });
}


function ParentGroupDropdown(container, options) {
    var gridData = $("#taxonomyAssociationGroupGrid").data("kendoGrid").dataSource,
        DeletedTaxonomyAssociationGroupId = gridData._destroyed.map(function (a) {
                return a.TaxonomyAssociationGroupId;
        });
    
    if (Number(options.model.TaxonomyAssociationGroupId) <= 0) {
       
        var result = $.grep(gridData._data, function (e) { return !e.HasFundData });        
        result = $.grep(result, function (e) {
            return DeletedTaxonomyAssociationGroupId.indexOf(e.TaxonomyAssociationGroupId) < 0
            && Number(e.TaxonomyAssociationGroupId) > 0

        });

        
        $('<input id="TaxonomyGroup" name="TaxonomyAssociationGroupId"  data-text-field="Name" data-value-field="TaxonomyAssociationGroupId" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                optionLabel: " ",
                dataTextField: 'Name',
                dataValueField: 'TaxonomyAssociationGroupId',
                dataSource: result,
            });


    }
    else {
       
            var element = TaxonomyGroupTemplate(options.model.ParentTaxonomyAssociationGroupId,options.model.SiteId);
            $(element)
           .appendTo(container);
       
    }
}
function TaxonomyGroupTemplate(parentId,siteId) {
    var gridData = $("#taxonomyAssociationGroupGrid").data("kendoGrid").dataSource;
    if (parentId) {
        var result = $.grep(gridData._data, function (e) { return !e.HasFundData && e.TaxonomyAssociationGroupId==parentId});
        return "<label>" + result[0].Name + "</label>";
    }
    else if(Number(siteId)>0){
        return "<label> <b>Highlevel</b> </label>";
    }
    else {
        return "<label> Unassigned </label>";
    }
    

}
function TaxonomyGroup_gridDataBound(e) {
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

function ValidateTaxonomyGroupSave(model) {
    
    var isSuccess = true;
    var content = "";
    var grid = $("#taxonomyAssociationGroupGrid").data("kendoGrid");
    content += "<p class='message'>Please Enter the below fields</p>";
    content += "<ul>";
    if (!model.Name) {
        content += "<li class='message'> Name </li>";
        isSuccess = false;
    }
    else {
        var duplicateName = $.grep(grid.dataSource._data, function (e) { return e.Name.toLowerCase() == model.Name.toLowerCase() });
        if (duplicateName.length >= 2) {
            content += "<li class='message'>A Record with same Name already Exists !!</li>";
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

var SaveAllTaxonomyAssociationGroupChanges = function (data) {
    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveTaxonomyAssociationGroup").data('request-url'),
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

var ReloadTaxonomyAssociationGroup = function () {
    LoadTaxonomyGroup(SiteData.Id);
};

var TaxonomyAssociationGroupChanges = function () {  //change needed
    var taxonomyAssociationGrid = $("#taxonomyAssociationGroupGrid").data("kendoGrid"),
           updatedRecords = [],
           newRecords = [],
           updatedKeys = [],
           deletedRecords = [];
    if (taxonomyAssociationGrid) {
        var currentData = taxonomyAssociationGrid.dataSource.data();

        newRecords = $.grep(currentData, function (e) { return e.isNew() || Number(e.TaxonomyAssociationGroupId) == 0 }).map(function (a) { return a.toJSON(); });;
        updatedRecords = $.grep(currentData, function (e) { return (e.dirty || e.IsUpdated) && Number(e.TaxonomyAssociationGroupId) > 0 && !e.isNew() }).map(function (a) { return a.toJSON(); });
        updatedKeys = updatedRecords.map(function (a) { return a.Key });
        deletedRecords = $.grep(taxonomyAssociationGrid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.Key) < 0) && Number(e.TaxonomyAssociationGroupId) >= 0 });
        deletedRecords = deletedRecords.map(function (a) { return a.toJSON(); });
    }
    var data = { newRecords: newRecords, updatedRecords: updatedRecords, deletedRecords: deletedRecords };
    return data;
};

var LoadTaxonomyAssociationGroups = function (siteId) {
    TaxonomySettings.InitialLoad = true;
    TaxonomySettings.SiteId = siteId;
    
    InitializeTaxonomyComponents(siteId);

};
function InitializeTaxonomyComponents(siteId) {      
    TaxonomyGroupsData = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvTaxonomyAssociationGroup").data('request-url'),
                cache: false
            },
            type: "GET",
            dataType: "json"
        }
    });
    TaxonomyGroupsData.fetch(function () { // Load grid only after reading document types (callback function)
        TaxonomyGroupData = TaxonomyGroupsData._data;       
        LoadTaxonomyGroup(siteId);
    });
    //deleteDocTypeWindow = $('<div />').kendoWindow({
    //    title: "Are you sure you want to delete this record?",
    //    visible: false, //the window will not appear before its .open method is called
    //    width: "500px",
    //    height: "200px",
    //    modal: true
    //}).data("kendoWindow");

}
function onTaxonomyGroupSearch() {
    var query = $("#txtSearchTAString").val();
    var grid = $("#taxonomyAssociationGroupGrid").data("kendoGrid");
    grid.dataSource.query({
        page: 1,
        pageSize: 20,
        filter: {
            logic: "or",
            filters: [
              { field: "Name", operator: "startswith", value: query }, 
            { field: "ParentGroup", operator: "startswith", value: query }
            ]
        }
    });
}
function BindInvalidTAGDData(data) {
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
    $("#TAGDInvalidDataGridHeader").show();

    if ($("#TAGDinvalidGrid").data("kendoGrid")) {
        $("#TAGDinvalidGrid").data("kendoGrid").destroy();
        $("#TAGDinvalidGrid").empty();
    }
    $("#TAGDinvalidGrid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["Export"],       
        dataBound: TaxonomyGroup_gridDataBound,
        columns: [
            { field: "Name", title: "Name", headerTemplate: "<span>Name</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" }},
            { field: "Description", title: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
            { field: "CssClass", title: "CssClass", title: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }},
        ],
    });

    $("#TAGDinvalidGrid .k-grid-Export").on('click', function (e) {

        TaxonomyGroupExcelExport('#TAGDinvalidGrid');
    });
    var grid = $("#TAGDinvalidGrid").data("kendoGrid");
    if (SiteData.FundOrder == 'CustomizeOrder') {
        grid.showColumn("Order");
        grid.dataSource.sort({ field: "Order", dir: "asc" });
    }

}
$("#exportTAGD").click(function (e) {
    TaxonomyGroupSave().done(function (data) {
        if (data) {
            TaxonomyGroupExcelExport('#taxonomyAssociationGroupGrid');
        }
    });
});

function TaxonomyGroupExcelExport(gridName) {
    var grid = $(gridName).data("kendoGrid");
    var rows = [{
        cells: [
                { value: "Name", color: "#fff", background: "#08c", bold: true },
                { value: "Tooltip Information", color: "#fff", background: "#08c", bold: true },                               
                { value: "Css Class", color: "#fff", background: "#08c", bold: true },
        ]
    }];

    var trs = $(gridName).find("tbody").find('tr');

    var exportAllRecords = true;
    if ($(trs).find(":checkbox").is(":checked")) {
        exportAllRecords = false;
    }

    for (var i = 0; i < trs.length; i++) {
        if (exportAllRecords || $(trs[i]).find(":checkbox").is(":checked")) {
            var dataItem = grid.dataItem(trs[i]);
            if (dataItem != null) {
                rows.push({
                    cells: [
                            { value: dataItem.Name },
                            { value: dataItem.Description},
                            { value: dataItem.CssClass }                        
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
                { width: 300 },
                { width: 300 },
                { width: 220 },
            ],
            title: "Groups",
            rows: rows
        }
        ]
    });
    var fileName = "TaxonomyGroups.xlsx";
    if (gridName == "#TiAGDnvalidGrid") {
        fileName = "Invalid_TaxonomyGroups.xlsx";
    }
    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: fileName });
}
$("#taxonomyAssociationGroupsUpload").kendoUpload({

    
    async: {
        saveUrl: $("#dvTaxonomyGroupImport").data('request-url'),

    },
    upload: function (e) {
        e.data = {
            siteId: SiteData.Id,

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
        if (e.response.status == "Success") {
            ReloadTaxonomyAssociationGroup();
            popupNotification.show("Group details Saved Successfully", "success");
        }
        else {
            popupNotification.show("Failed to save Group details", "error");
        }
        BindInvalidTAGDData(e.response.invalidData);
        if (e.response.invalidData.length == 0) {
            $("#importTaxonomygroups").data("kendoWindow").close();
        }
    }
});
$("#importTAGD").click(function () {
    TaxonomyGroupSave().done(function (data) {

        $("#TAGDInvalidDataGridContainer").hide();
        $("#TAGDInvalidDataGridHeader").hide();

        if (data) {
            var grid = $("#taxonomyAssociationGroupGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importTaxonomygroups").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }
    });

});
function TaxonomyGroups_gridDataBound(e) {
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

var TaxonomyGroupSettings =
{
    LoadTaxonomyAssociationGroups: LoadTaxonomyAssociationGroups,
    TaxonomyAssociationGroupChanges: TaxonomyAssociationGroupChanges,
    SaveAllTaxonomyAssociationGroupChanges: SaveAllTaxonomyAssociationGroupChanges,
    ReloadTaxonomyAssociationGroup: ReloadTaxonomyAssociationGroup,
    InitialLoad: true,
    SiteId: null
};