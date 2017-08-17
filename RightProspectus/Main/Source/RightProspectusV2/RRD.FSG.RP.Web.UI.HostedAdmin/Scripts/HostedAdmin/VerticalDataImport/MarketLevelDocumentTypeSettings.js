
var LoadMarketLevelDocumentTypeData = function () {


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvMarketLevelDocumentTypeAssociation").data('request-url'),
                data: { siteId: MarketLevelDocumentTypeSettings.SiteId },
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
                    TaxonomyAssociationId: { type: "number" },
                    TaxonomyMarketId: {},
                    HeaderText: {},
                    LinkText: {},
                    DescriptionOverride: {},
                    CssClass: {},
                    Order: { type: "number" },


                }
            }

        },
        sort: { field: "Order", dir: "asc" },
    });

    $("#GridContainer").show();

    if ($("#marketLevelDocumentTypeGrid").data("kendoGrid")) {
        $("#marketLevelDocumentTypeGrid").data("kendoGrid").destroy();
        $("#marketLevelDocumentTypeGrid").empty();
    }

    $("#marketLevelDocumentTypeGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: MLDTASettings_gridDataBound,
        toolbar: ["create", "save", "cancel"],
        //excel: {
        //    fileName: "DocumentTypes_CUSIPs.xlsx",
        //    filterable: true
        //},

        pageable: false,

        columns: [
            { field: "MarketId", title: "Document Type", headerTemplate: "<span>Document Type</span>", editor: documentTypeMDropdown, headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" }, template: '#=documentTypeMarketLevelTemplate(MarketId)#' },
            { field: "TaxonomyAssociationId", headerTemplate: "<span>CUSIPs</span>", hidden: true, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, },
            { field: "TaxonomyMarketId", width: 100, title: "CUSIP", headerTemplate: "<span>CUSIPs</span>", editor: taxonomyTypeDropdown, headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" }, template: '#=MarketLevelDocTypeSettings_ReplaceTaxonomy(TaxonomyAssociationId)#' },
            { field: "HeaderText", title: "Header Text", headerTemplate: "<span>Header Text</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "LinkText", title: "Link Text", headerTemplate: "Link Text", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            { field: "DescriptionOverride", title: "Tooltip Information", headerTemplate: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
            { field: "CssClass", title: "Css Class", headerTemplate: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" }, hidden: true },
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
               { name: "Delete", click: onMarketLevelDocumentTypeDelete }, ]
            , title: "&nbsp;", width: "170px"
            }

        ],
        editable: { mode: "popup", confirmDelete: "No" },
        edit: onMarketLevelDocumentTypeGridEdit,
        cancel: function (e) {
       $('.selectMultipleDelete').show();
       },
        //excelExport: CustomMarketLevelDocumentTypeExport,
        save: function (e) {
           
            if (ValidateMarketLevelDocumentTypeSave(e.model)) {
                AddMultipleDocumentTypes_CUSIPs(e.model, $("#TaxonomyType").val());
                this.refresh();
                //  $("#documentTypeGrid").data("kendoGrid").addRow();
            }

        },
        saveChanges: function (e) {

            $("#marketLevelDocumentTypeGrid .k-grid-save-changes").prop('disabled', true);
            $("#marketLevelDocumentTypeGrid .k-grid-save-changes").addClass('disabled');

            var changes = SaveAllMarketLevelDocumentTypeAssociationChanges(MarketLevelDocumentTypeAssociationChanges()); // Save All Grid Changes to Server
            changes.done(function () {

                $("#marketLevelDocumentTypeGrid .k-grid-save-changes").prop('disabled', false);
                $("#marketLevelDocumentTypeGrid .k-grid-save-changes").removeClass('disabled');

                ReloadMarketLevelDocumentTypeAssociation();
                popupNotification.show("Document Types-CUSIPs details Saved Successfully", "success");

            }).fail(function () {

                $("#marketLevelDocumentTypeGrid .k-grid-save-changes").prop('disabled', false);
                $("#marketLevelDocumentTypeGrid .k-grid-save-changes").removeClass('disabled');

                popupNotification.show("Failed to save Document Types-CUSIPs details", "error");
            });
        },

        remove: function (e) {
            this.refresh();

        }

    });
   
    EnableTooltipMLDT();
    $('#marketLevelDocumentTypeGrid .selectALLMultipleDelete').change(function () {
        if ($(this).is(':checked')) {
            $('#marketLevelDocumentTypeGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', true);
        }
        else {
            $('#marketLevelDocumentTypeGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
        }
    });
}
function EnableTooltipMLDT() {
    var grid = $("#marketLevelDocumentTypeGrid").data("kendoGrid");

    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 120,
        content: function (e) {
            var target = e.target; // element for which the tooltip is shown
            switch (target.text()) {
                case "Market Id":
                    return "Document Type";
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
function MLDTASettings_gridDataBound(e) {
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

function documentTypeMDropdown(container, options) {

    if (options.model.DocumentTypeId <= 0 || options.model.DocumentTypeId == "") {

        $('<input id="DocumentType" name="MarketId" data-text-field="Name" data-value-field="MarketId" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: true,
                dataTextField: 'Name',
                dataValueField: 'DocumentTypeId',
                dataSource: DocumentTypeMData,
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
        var element = documentTypeMarketLevelTemplate(options.model.MarketId);
        $(element)
       .appendTo(container);
    }
}

function taxonomyTypeDropdown(container, options) {
    if (options.model.TaxonomyAssociationId <= 0 || options.model.TaxonomyAssociationId == "") {
        var reformattedArray = TaxonomyData.map(function (obj) {
            var rObj = { label: obj.MarketId, title: obj.MarketId, value: obj.TaxonomyAssociationId };
            return rObj;
        });
        $('<select id="TaxonomyType" name="TaxonomyAssociationId" multiple="multiple"></select>')
            .appendTo(container)
            .multiselect({
                numberDisplayed: 0,
                includeSelectAllOption: false,
                enableFiltering: true,
                filterBehavior: 'text',
                enableCaseInsensitiveFiltering: true,
                maxHeight: 200,
                maxWidth: 100



            }


     );
        $('#TaxonomyType')
            .empty()
            .multiselect('dataprovider', reformattedArray);
    }

    else {
        var element = MarketLevelDocTypeSettings_ReplaceTaxonomy(options.model.TaxonomyAssociationId);
        $(element)
       .appendTo(container);
    }



}

function documentTypeMarketLevelTemplate(marketId) {
    if (marketId) {
        var result = $.grep(DocumentTypeMData, function (e) { return e.MarketId == marketId });
        return "<label>" + result[0].Name + "</label>";
    }

}



function onMarketLevelDocumentTypeGridEdit(arg) {
   
       $('.selectMultipleDelete').hide();
      
       if (arg.model.isNew() && Number(arg.model.TaxonomyAssociationId) <= 0) {
        var Orders = [];
        Orders = $("#marketLevelDocumentTypeGrid").data("kendoGrid").dataSource._data.map(function (a) { return a.Order; });
        if (Orders.length > 0)
            arg.model.set("Order", Math.max.apply(Math, Orders) + 1);
        else
            arg.model.set("Order", 1);
    }

    if (arg.model.isNew()) {
        arg.container.kendoWindow("title", "Add New Fund Level Document Type");
    } else {
        arg.container.kendoWindow("title", "Edit Fund Level Document Type");
    }

    arg.container.addClass("MarketIdLevelDocumentType-EditPopup");
    arg.container[0].children[0].children[2].style.setProperty("display", "none");
    arg.container[0].children[0].children[3].style.setProperty("display", "none");
    if (arg.container[0].children[0].children[1].childElementCount == 0 || arg.container[0].children[0].children[1].firstChild.nodeName.toLowerCase() == "label") {
        arg.container[0].children[0].children[1].style.setProperty("padding", ".4em 0 1em");
    }

    $('#marketLevelDocumentTypeGrid').find("thead").find('tr').find(".selectALLMultipleDelete").prop('checked', false);
    $('#marketLevelDocumentTypeGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
}

function onMarketLevelDocumentTypeDelete(e) {


    var grid = $("#marketLevelDocumentTypeGrid").data("kendoGrid");
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
    $("#marketLevelDocumentTypeGrid").find("input:checked").each(function () {

        var data1 = currentRef.dataItem($(this).closest('tr'));
        list.push({ uid: data1.uid });

    });
    var messageToDisplay = "";
    if (list.length > 0) {
        messageToDisplay = $("#deleteMarketLevelDocumentTypeAssociationTemplate").html().replace("MarketId", "all selected Document Type's");
    }
    else {
        messageToDisplay = $("#deleteMarketLevelDocumentTypeAssociationTemplate").html().replace("MarketId", data.MarketId);
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


function ValidateMarketLevelDocumentTypeSave(model) {
    var isSuccess = true;
    var content = "";
    var grid = $("#marketLevelDocumentTypeGrid").data("kendoGrid");
    content += "<p class='message'>Please Enter/Select the below fields</p>";
    content += "<ul>";
    if (Number(model.DocumentTypeId) <= 0) {
        content += "<li class='message'>Document Type</li>";
        isSuccess = false;
    }
    if (Number(model.TaxonomyAssociationId) <= 0) {
        content += "<li class='message'>CUSIP</li>";
        isSuccess = false;
    }
    if (!model.HeaderText) {
        content += "<li class='message'>Header Text</li>";
        isSuccess = false;
    }

    if (Number(model.DocumentTypeId) > 0 && model.TaxonomyAssociationId > 0) {

        var duplicates = $.grep(grid.dataSource._data, function (e) { return e.DocumentTypeId == model.DocumentTypeId && e.TaxonomyAssociationId == model.TaxonomyAssociationId });
        if (duplicates.length >= 2) {
            isSuccess = false;
            content += "<li class='message'>Combination of this Document Type and CUSIP already Exists !!</li>";
        }
    }
    if (!model.Order || Number(model.Order) < 1) {
        content += "<li class='message'>Order</li>";
        isSuccess = false;
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

$("#marketLevelDocumentTypeAssociationUpload").kendoUpload({
    async: {
        saveUrl: $("#dvMarketLevelDocumentTypeImport").data('request-url'),
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

        if (e.response.status == "countgreaterthan5000")
        {   
            $("#MLDTInvalidDataGridContainer").hide();
            $("#MLDTInvalidDataGridHeader").hide();

            $(".k-upload-files li").removeClass('k-file-success');
            $(".k-upload-files li").addClass('k-file-error');
            $(".k-upload-files .k-upload-pct").text('Please import file with less than 5000 rows.');
        }
        else
        {
            if (e.response.status == "Success") {
                ReloadMarketLevelDocumentTypeAssociation();
                popupNotification.show("Document Types-CUSIPs details Saved Successfully", "success");
            }
            else {
                popupNotification.show("Failed to save Document Types-CUSIPs details", "error");
            }

            BindInvalidDataMLDT(e.response.invalidData);
            if (e.response.invalidData.length == 0) {

                $("#importMarketLevelDocumentTypeAssociation").data("kendoWindow").close();
            }
        }        
    }
});

$("#lnkImportMarketLevel").click(function () {

    $("#MLDTInvalidDataGridContainer").hide();
    $("#MLDTInvalidDataGridHeader").hide();

    MarketLevelDocumentTypeAssociationSave().done(function (data) {
        if (data) {
            var grid = $("#marketLevelDocumentTypeGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importMarketLevelDocumentTypeAssociation").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }
    });


});
$("#exportMarketLevel").click(function (e) {
    MarketLevelDocumentTypeAssociationSave().done(function (data) {
        if (data) {
            
            MarketLevelDocumentTypeAssociationExcelExport('#marketLevelDocumentTypeGrid');
        }
    });
});


function MarketLevelDocumentTypeAssociationExcelExport(gridName) {

    var grid = $(gridName).data("kendoGrid");
    var rows = [{
            cells: [
                    { value: "Document Type", color: "#fff", background: "#08c", bold: true},
                    { value: "CUSIPs", color: "#fff", background: "#08c", bold: true},
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
                var cusip = dataItem.TaxonomyMarketId;
                if (gridName =="#marketLevelDocumentTypeGrid")
                {
                    cusip = MarketLevelDocTypeSettings_ReplaceTaxonomy(dataItem.TaxonomyAssociationId, true);
                }

                rows.push({
                        cells: [
                                { value: dataItem.MarketId },
                                { value: cusip},
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
                { width: 100 },
                { width: 200 },
                { width: 200 },
                { width: 200 },
                { width: 100 },
                { width: 100 },
            ],
            title: "Market Level Document Types",
            rows: rows
        }
        ]
    });

    var fileName = "MarketLevelDocumentTypes.xlsx";
    if (gridName == "#MLDTInvalidDataGrid")
    {
        fileName = "Invalid_MarketLevelDocumentTypes.xlsx";
    }
    kendo.saveAs({dataURI: workbook.toDataURL(), fileName: fileName});
}

var SaveAllMarketLevelDocumentTypeAssociationChanges = function (data) {
    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveMarketLevelDocumentTypeAssociation").data('request-url'),
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

var MarketLevelDocumentTypeAssociationChanges = function () {
    var documentTypeGrid = $("#marketLevelDocumentTypeGrid").data("kendoGrid"),
           updatedRecords = [],
           newRecords = [],
           updatedKeys = [],
           deletedRecords = [];
    if (documentTypeGrid) {
        var currentData = documentTypeGrid.dataSource.data();

        newRecords = $.grep(currentData, function (e) { return e.isNew() || Number(e.DocumentTypeAssociationId) == 0 }).map(function (a) { return a.toJSON(); });;
        updatedRecords = $.grep(currentData, function (e) { return e.dirty == true && e.isNew() == false }).map(function (a) { return a.toJSON(); });
        updatedKeys = updatedRecords.map(function (a) { return a.Key });
        deletedRecords = $.grep(documentTypeGrid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.Key) < 0) && Number(e.DocumentTypeAssociationId) >= 0; });
        deletedRecords = deletedRecords.map(function (a) { return a.toJSON(); });

    }
    var data = { newRecords: newRecords, updatedRecords: updatedRecords, deletedRecords: deletedRecords };
    return data;
};

var ReloadMarketLevelDocumentTypeAssociation = function () {

    MarketLevelDocumentTypeSettings.LoadMarketLevelDocumentTypeData();
};

var LoadMarketLevelDocumentTypeAssociation = function (SiteId) {
    MarketLevelDocumentTypeSettings.InitialLoad = true;
    MarketLevelDocumentTypeSettings.SiteId = SiteId;
    InitializeMDocTypeComponents();


};

function InitializeMDocTypeComponents() {
    
    $("#btnMLDSearch").kendoButton({
        click: onMarketLevelDocumentTypeSearch
    });
    verticalDocumentTypes = new kendo.data.DataSource({
        transport: {
            read: {//Check for the method in the Controller
                url: $("#dvMarketLevelDocumentTypes").data('request-url'),
                cache: false
            },
            type: "GET",
            dataType: "json"
        }
    });

    var taxonomyMarketIds = new kendo.data.DataSource({
        transport: {
            read: {//Check for the method in the Controller
                url: $("#dvGetTaxonomyByTemplateForMLDTA").data('request-url'),
                cache: false,
                data: { siteId: SiteData.Id },
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

    verticalDocumentTypes.fetch(function () { // Load grid only after reading taxonomy types (callback function)

        DocumentTypeMData = verticalDocumentTypes._data;
        taxonomyMarketIds.fetch(function () { // Load grid only after reading document types (callback function)
            TaxonomyData = taxonomyMarketIds._data;
            MarketLevelDocumentTypeSettings.LoadMarketLevelDocumentTypeData();
        });
    });

    deleteDocTypeWindow = $('<div />').kendoWindow({
        title: "are you sure you want to delete this record?",
        visible: false, //the window will not appear before its .open method is called
        width: "500px",
        height: "200px",
        modal: true,
    }).data("kendoWindow");


}
function MarketLevelDocTypeSettings_ReplaceTaxonomy(taxonomyAssociationId, isOnlyValue) {
    if (!TaxonomyData) {
        taxonomyMarketIds.fetch(function () {
            TaxonomyData = taxonomyMarketIds._data;
        });
    }
    //if (taxonomyAssociationId != "") {
    if (taxonomyAssociationId) {
        var taxonomy = $.grep(TaxonomyData, function (e) { return e.TaxonomyAssociationId == taxonomyAssociationId });
        if (isOnlyValue) {
            return taxonomy[0].MarketId;
        }
        else {
            return "<label>" + taxonomy[0].MarketId + "</label>";
        }

        //return "<label>To Do</label>";
    }

}
function onMarketLevelDocumentTypeSearch() {
    var query = $("#txtSearchMLDString").val();
    var grid = $("#marketLevelDocumentTypeGrid").data("kendoGrid");
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
function BindInvalidDataMLDT(data) {
    var dataSource = new kendo.data.DataSource({
        data: {
            "data": data, "total": data.length
        },
        schema: {
            data: "data",
            total: "total",
        }
    });

    $("#MLDTInvalidDataGridContainer").show();
    $("#MLDTInvalidDataGridHeader").show();

    if($("#MLDTInvalidDataGrid").data("kendoGrid")) {
        $("#MLDTInvalidDataGrid").data("kendoGrid").destroy();
        $("#MLDTInvalidDataGrid").empty();
    }

    $("#MLDTInvalidDataGrid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["Export"],
        //excel: {
        //    fileName: "Inavlid_DocumentTypes_CUSIPs.xlsx",
        //    filterable: true
        //},
        dataBound: MLDTASettings_gridDataBound,
        columns: [
                       { field: "MarketId", width: 85, title: "Document Type", editor: documentTypeMDropdown, headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "TaxonomyMarketId", title: "CUSIPs", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "HeaderText", title: "Header Text", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "LinkText", title: "Link Text", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "DescriptionOverride", title: "Tooltip Information", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "CssClass", width: 100, title: "Css Class", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "Order", width: 75, title: "Order", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
        ],
    });                     

    $("#MLDTInvalidDataGrid .k-grid-Export").on('click', function (e) {

        MarketLevelDocumentTypeAssociationExcelExport('#MLDTInvalidDataGrid');
    });

}

function AddMultipleDocumentTypes_CUSIPs(model, selectedValues)
{
    if (selectedValues && selectedValues.length>=1)
    {
        var grid = $("#marketLevelDocumentTypeGrid").data("kendoGrid");
        for (var i = 1 ; i < selectedValues.length; i++) {
            grid.dataSource.add({
                DocumentTypeId: model.DocumentTypeId,
                MarketId: model.MarketId,
                TaxonomyAssociationId: selectedValues[i],
                TaxonomyMarketId: $("#TaxonomyType option[value='" + selectedValues[i] + "']").text(),
                HeaderText: model.HeaderText,
                LinkText: model.HeaderText,
                DescriptionOverride: model.HeaderText,
                CssClass: model.HeaderText,
                Order: model.Order + 1
            });
        }
    }
   
   

    
}

var MarketLevelDocumentTypeSettings =
    {
        LoadMarketLevelDocumentTypeAssociation: LoadMarketLevelDocumentTypeAssociation,
        MarketLevelDocumentTypeAssociationChanges: MarketLevelDocumentTypeAssociationChanges,
        SaveAllMarketLevelDocumentTypeAssociationChanges: SaveAllMarketLevelDocumentTypeAssociationChanges,
        ReloadMarketLevelDocumentTypeAssociation: ReloadMarketLevelDocumentTypeAssociation,
        LoadMarketLevelDocumentTypeData: LoadMarketLevelDocumentTypeData,
        InitialLoad: true,
        SiteId: null

    };
