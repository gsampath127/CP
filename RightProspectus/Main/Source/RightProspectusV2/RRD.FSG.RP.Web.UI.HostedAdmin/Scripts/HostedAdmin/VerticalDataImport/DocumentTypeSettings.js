
var LoadDocumentTypeData = function (siteId) {
     

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvDocumentTypeAssociation").data('request-url'),
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

        batch: true,
        schema: {
            data: "data",
            total: "total",
            model: {
                id: "DocumentTypeAssociationId",
                fields: {
                    DocumentTypeId: { editable: false },
                    MarketId: {},
                    HeaderText: {},
                    LinkText: {},
                    DescriptionOverride: {},
                    CssClass: {},
                    Order: { type: "number" },
                }
            }

        },
        sort: {
            field: "Order", dir: "asc"
        },
    });

    $("#GridContainer").show();

    if ($("#documentTypeGrid").data("kendoGrid")) {
        $("#documentTypeGrid").data("kendoGrid").destroy();
        $("#documentTypeGrid").empty();
    }

    $("#documentTypeGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: DocumentTypeSettings_gridDataBound,
        toolbar: ["create", "save", "cancel"],
        //excel: {
        //    fileName: "DocumentTypes.xlsx",
        //    filterable: true
        //},

        pageable: false,

        columns: [
            { field: "MarketId", title: "Document Type", headerTemplate: "<span>Document Type</span>", editor: documentTypeDropdown, headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" }, template: '#=documentTypeTemplate(MarketId)#' },
            { field: "HeaderText", title: "Header Text", headerTemplate: "<span>Header Text</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "LinkText", title: "Link Text", headerTemplate: "Link Text", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            { field: "DescriptionOverride", title: "Tooltip Information", headerTemplate: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            { field: "CssClass", width: 100, title: "Css Class", headerTemplate: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            { field: "Order", width: 70, headerTemplate: "<span>Order</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            
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
              { name: "Delete", click: onDocumentTypeDelete }, ]
            , title: "&nbsp;", width: "170px"
            }

        ],
        editable: { mode: "popup", confirmDelete: "No" },
        edit: onDocumentTypeGridEdit,
        cancel: function (e) {$('.selectMultipleDelete').show(); },
        save: function (e) {
            
            e.model.SiteId = SiteData.Id;

            if (ValidateDocumentTypeSave(e.model)) {
                this.refresh();
                //  $("#documentTypeGrid").data("kendoGrid").addRow();
            }

        },
        saveChanges: function (e) {

            $("#documentTypeGrid .k-grid-save-changes").prop('disabled', true);
            $("#documentTypeGrid .k-grid-save-changes").addClass('disabled');

            var changes = SaveAllDocumentTypeAssociationChanges(DocumentTypeAssociationChanges()); // Save All Grid Changes to Server
            changes.done(function () {

                $("#documentTypeGrid .k-grid-save-changes").prop('disabled', false);
                $("#documentTypeGrid .k-grid-save-changes").removeClass('disabled');

                ReloadDocumentTypeAssociation();
                popupNotification.show("Document Types-Site Saved Successfully", "success");

            }).fail(function () {

                $("#documentTypeGrid .k-grid-save-changes").prop('disabled', false);
                $("#documentTypeGrid .k-grid-save-changes").removeClass('disabled');

                popupNotification.show("Failed Saving Document Types-Site", "error");
            });
        },

        remove: function (e) {
            this.refresh();
        }
    });
    EnableDocumentTypeTooltip();
    DocumentTypeAssociationOrder();

    $('#documentTypeGrid .selectALLMultipleDelete').change(function () {
        if ($(this).is(':checked')) {
            $('#documentTypeGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', true);
        }
        else {
            $('#documentTypeGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
        }
    });
}
function EnableDocumentTypeTooltip() {
    var grid = $("#documentTypeGrid").data("kendoGrid");
    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 120,
        content: function (e) {
            var target = e.target; // element for which the tooltip is shown
            switch (target.text()) {
                case "Market Id":
                    return "Document Types";
                case "Header Text":
                    return "Description";
                case "Description Override":
                    return "Tooltip Information";
                default:
                    return $(target).text();
            }
        }
    });
}
function DocumentTypeSettings_gridDataBound(e) {
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
    //$('#checkAll').prop('checked', false);
    // $('#documentTypeGrid :checkbox').each(function () {
    //set checked based on if current checkbox's value is in selectedIds.  
    // $(this).attr('checked', jQuery.inArray($(this).val(), selectedIds) > -1);
    //});
};

function documentTypeDropdown(container, options) {
    if (options.model.DocumentTypeId <= 0 || options.model.DocumentTypeId == "") {
        var MarketIds = [],
         DeletedMarketIds = [],
         gridData = $("#documentTypeGrid").data("kendoGrid").dataSource;
        MarketIds = gridData._data.map(function (a) {
            return a.MarketId;
        });
        DeletedMarketIds = gridData._destroyed.map(function (a) {
            return a.MarketId;
        });

        var result = $.grep(DocumentTypeData, function (e) { return (MarketIds.indexOf(e.MarketId) < 0) || DeletedMarketIds.indexOf(e.MarketId) >= 0 });
        $('<input id="DocumentType" name="MarketId" data-text-field="Name" data-value-field="MarketId" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: 'Name',
                dataValueField: 'DocumentTypeId',
                dataSource: result,
                change: function (e) {
                    var dataItem = this.dataItem();
                    options.model.HeaderText = dataItem.Name;
                    options.model.LinkText = dataItem.Name;
                    options.model.DocumentTypeId = dataItem.DocumentTypeId;
                    options.model.MarketId = dataItem.MarketId;

                }
            });


    }
    else {
        var element = documentTypeTemplate(options.model.MarketId);
        $(element)
       .appendTo(container);
    }
}

function documentTypeTemplate(marketId) {
    if (marketId) {
        var result = $.grep(DocumentTypeData, function (e) { return e.MarketId == marketId });
        return "<label>" + result[0].Name + "</label>";
    }

}

function onDocumentTypeGridEdit(arg) {
     $('.selectMultipleDelete').hide();
    if (arg.model.isNew() && arg.model.DocumentTypeId == "") {
        var Orders = [];
        Orders = $("#documentTypeGrid").data("kendoGrid").dataSource._data.map(function (a) { return a.Order; });
        if (Orders.length > 0)
            arg.model.set("Order", Math.max.apply(Math, Orders) + 1);
        else
            arg.model.set("Order", 1);
    }
    if (arg.model.isNew()) {
        arg.container.kendoWindow("title", "Add New Document Type");
    } else {
        arg.container.kendoWindow("title", "Edit Document Type");
    }
    arg.container.addClass("DocumentType-EditPopup");
    if (arg.container[0].children[0].children[1].childElementCount == 0 || arg.container[0].children[0].children[1].firstChild.nodeName.toLowerCase() == "label") {
        arg.container[0].children[0].children[1].style.setProperty("padding", ".4em 0 1em");
    }
    $('#documentTypeGrid').find("thead").find('tr').find(".selectALLMultipleDelete").prop('checked', false);
    $('#documentTypeGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
}

function onDocumentTypeDelete(e) {

    var grid = $("#documentTypeGrid").data("kendoGrid");
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
    $("#documentTypeGrid").find("input:checked").each(function () {

        var data1 = currentRef.dataItem($(this).closest('tr'));
        list.push({ uid: data1.uid });

    });
    var messageToDisplay = "";
    if (list.length > 0) {
        messageToDisplay = $("#deleteDocumentTypeAssociationTemplate").html().replace("MarketId", "all selected Document Type's");
    }
    else {
        messageToDisplay = $("#deleteDocumentTypeAssociationTemplate").html().replace("MarketId", data.MarketId);
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
            DocumentTypeAssociationOrder();

        }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });
}


function ValidateDocumentTypeSave(model) {

    var isSuccess = true;
    var content = "";
    content += "<p class='message'>Please Enter/Select the below fields</p>";
    content += "<ul>";
    if (Number(model.DocumentTypeId) <= 0) {
        content += "<li class='message'>Document Type</li>";
        isSuccess = false;
    }
    if (!model.HeaderText) {
        content += "<li class='message'>Header Text</li>";
        isSuccess = false;
    }

    if (!model.Order || Number(model.Order) < 1) {
        content += "<li class='message'>Order</li>";
        isSuccess = false;
    }
    else {
        var grid = $("#documentTypeGrid").data("kendoGrid");
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

function DocumentTypeAssociationOrder() {
    $("#documentTypeGrid").getKendoGrid().table.kendoDraggable({
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

    $("#documentTypeGrid").getKendoGrid().table.kendoDropTarget({
        group: "gridGroup",//This would recieve the drop target
        drop: function (e) {
            var grid = $("#documentTypeGrid").getKendoGrid();
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


$("#documentTypeAssociationUpload").kendoUpload({
    async: {
        saveUrl: $("#dvDocumentTypeImport").data('request-url'),
    },
    upload: function (e) {
        e.data = {
            siteId: DocumentTypeSettings.SiteId
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
            popupNotification.show("Document Types- Site details Saved Successfully", "success");
            ReloadDocumentTypeAssociation();
        }
        else {
            popupNotification.show("Failed Saving Document Types- Site details", "error");
        }

        BindInvalidDataDT(e.response.invalidData);
        if (e.response.invalidData.length == 0) {

            $("#importDocumentTypeAssociation").data("kendoWindow").close();
        }
    }
});

$("#lnkImport").click(function () {

    $("#DTInvalidDataGridContainer").hide();
    $("#DTInvalidDataGridHeader").hide();

    DocumentTypeAssociationSave().done(function (data) {
        if (data) {

            var grid = $("#documentTypeGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importDocumentTypeAssociation").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }

    });


});
$("#export").click(function (e) {
    DocumentTypeAssociationSave().done(function (data) {
        if (data) {
            DocumentTypeAssociationExcelExport('#documentTypeGrid');
        }
    })
});

function DocumentTypeAssociationExcelExport(gridName) {

    var grid = $(gridName).data("kendoGrid");
    var rows = [{
            cells: [
                    { value: "Document Type", color: "#fff", background: "#08c", bold: true},
                    { value: "Header Text", color: "#fff", background : "#08c", bold: true },
                    { value: "Link Text", color: "#fff", background : "#08c", bold: true },
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
                                { value: dataItem.HeaderText },
                                { value: dataItem.LinkText },
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
                { width: 250 },
                { width: 250 },
                { width: 200 },
                { width: 100 },
                { width: 100 },
            ],
            title: "Document Types",
            rows: rows
        }
        ]
    });

    var fileName = "DocumentTypes.xlsx";
    if (gridName == "#DTInvalidDataGrid")
    {
        fileName = "Invalid_DocumentTypes.xlsx";
    }
    kendo.saveAs({dataURI: workbook.toDataURL(), fileName: fileName});
}

var SaveAllDocumentTypeAssociationChanges = function (data) {
    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveDocumentTypeAssociation").data('request-url'),
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

var DocumentTypeAssociationChanges = function () {
    var documentTypeGrid = $("#documentTypeGrid").data("kendoGrid"),
           updatedRecords = [],
           newRecords = [],
           updatedKeys = [],
           deletedRecords = [];

    if (documentTypeGrid) {
        var currentData = documentTypeGrid.dataSource.data();
        newRecords = $.grep(currentData, function (e) { return e.isNew() || Number(e.DocumentTypeAssociationId) == 0 }).map(function (a) { return a.toJSON(); });;
        updatedRecords = $.grep(currentData, function (e) { return e.dirty && Number(e.DocumentTypeAssociationId) > 0 && !e.isNew() }).map(function (a) { return a.toJSON(); });
        updatedKeys = updatedRecords.map(function (a) { return a.Key });
        deletedRecords = $.grep(documentTypeGrid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.Key) < 0) && Number(e.DocumentTypeAssociationId) >= 0; });
        deletedRecords = deletedRecords.map(function (a) { return a.toJSON(); });
    }


    var data = { newRecords: newRecords, updatedRecords: updatedRecords, deletedRecords: deletedRecords };
    return data;
};

var ReloadDocumentTypeAssociation = function () {
    DocumentTypeSettings.LoadDocumentTypeData(SiteData.Id);
};

var LoadDocumentTypeAssociation = function (siteId) {
    DocumentTypeSettings.SiteId = siteId;
    DocumentTypeSettings.InitialLoad = true;
    InitializeDocTypeComponents(siteId);
};

function InitializeDocTypeComponents(siteId) {
    
    $("#btnDocumentTypeSearch").kendoButton({
        click: onDocumentTypeSearch
    })
    verticalSDocumentTypes = new kendo.data.DataSource({
        transport: {
            read: {//Check for the method in the Controller
                url: $("#dvDocumentTypes").data('request-url'),
                cache: false
            },
            type: "GET",
            dataType: "json"
        }
    });
    verticalSDocumentTypes.fetch(function () { // Load grid only after reading document types (callback function)
        DocumentTypeData = verticalSDocumentTypes._data;
        DocumentTypeSettings.LoadDocumentTypeData(siteId);
    });
    deleteDocTypeWindow = $('<div />').kendoWindow({
        title: "Are you sure you want to delete this record?",
        visible: false, //the window will not appear before its .open method is called
        width: "500px",
        height: "200px",
        modal: true
    }).data("kendoWindow");

}
function onDocumentTypeSearch() {

    var query = $("#txtSearchString").val();
    var grid = $("#documentTypeGrid").data("kendoGrid");
    grid.dataSource.query({
        page: 1,
        pageSize: 20,
        filter: {
            logic: "or",
            filters: [
        { field: "MarketId", operator: "startswith", value: query },
        { field: "HeaderText", operator: "startswith", value: query }

            ]
        }
    });
}
function BindInvalidDataDT(data) {
    var dataSource = new kendo.data.DataSource({
        data: {
            "data": data, "total": data.length
        },
        schema: {
            data: "data",
            total: "total",
        }
    });

    $("#DTInvalidDataGridContainer").show();

    $("#DTInvalidDataGridHeader").show();

    if($("#DTInvalidDataGrid").data("kendoGrid")) {
        $("#DTInvalidDataGrid").data("kendoGrid").destroy();
        $("#DTInvalidDataGrid").empty();
    }

    $("#DTInvalidDataGrid").kendoGrid({

        dataSource: dataSource,
        toolbar: ["Export"],
        //excel: {
        //    fileName: "Inavlid_DocumentTypes.xlsx",
        //    filterable: true
        //},
        dataBound: DocumentTypeSettings_gridDataBound,
        columns: [
                       { field: "MarketId", editor: documentTypeDropdown, title: "Document Type", width: 140, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                       { field: "HeaderText", title: "Header Text" },
                       { field: "LinkText", title: "Link Text", width: 140 },
                       { field: "DescriptionOverride", title: "Tooltip Information", width: 140, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                       { field: "CssClass", title: "Css Class", width: 140 },
                       { field: "Order", title: "Order", width: 140 },
        ],
    });

    $("#DTInvalidDataGrid .k-grid-Export").on('click', function (e) {

        DocumentTypeAssociationExcelExport('#DTInvalidDataGrid');
    });
}
var DocumentTypeSettings =
            {
                LoadDocumentTypeAssociation: LoadDocumentTypeAssociation,
                DocumentTypeAssociationChanges: DocumentTypeAssociationChanges,
                SaveAllDocumentTypeAssociationChanges: SaveAllDocumentTypeAssociationChanges,
                ReloadDocumentTypeAssociation: ReloadDocumentTypeAssociation,
                InitialLoad: true,
                LoadDocumentTypeData: LoadDocumentTypeData,
                SiteId: null

            };
