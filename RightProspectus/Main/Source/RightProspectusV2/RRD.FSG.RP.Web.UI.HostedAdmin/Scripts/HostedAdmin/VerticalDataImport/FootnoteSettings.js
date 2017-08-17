var LoadFootnoteData = function () {


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvFootnote").data('request-url'),
                data: { siteId: Number(SiteData.Id) },
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
                id: "FootnoteId",
                fields: {
                    TaxonomyAssociationId: { type: "number" },
                    TaxonomyMarketId: {},
                    LanguageCulture: {},
                    Text: {},
                    Order: { type: "number" }
                }
            }

        },
        sort: {
            field: "Order", dir: "asc"
        },
    });

    $("#GridContainer").show();

    if ($("#footNoteGrid").data("kendoGrid")) {
        $("#footNoteGrid").data("kendoGrid").destroy();
        $("#footNoteGrid").empty();
    }

    $("#footNoteGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: FootNoteSetting_gridDataBound,
        toolbar: ["create", "save", "cancel"],
        //excel: {
        //    fileName: "Footnotes.xlsx",
        //    filterable: true
        //},

        pageable: false,

        columns: [

            { field: "Text", headerTemplate: "<span>Text</span>", headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" } },
            { field: "TaxonomyMarketId", title: "CUSIPs", headerTemplate: "<span>CUSIPs</span>", editor: taxonomyTypeFootnoteDropdown, headerAttributes: { class: "wrapheader showtooltip" }, attributes: { class: "wraptext" }, template: '#=FootnoteSettings_ReplaceTaxonomy(TaxonomyAssociationId)#' },
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
              { name: "Delete", click: onFootnoteGridDelete }, ]
            , title: "&nbsp;", width: "170px"
            }

        ],
        editable: { mode: "popup", confirmDelete: "No" },
        edit: onFootnoteGridEdit,
        cancel: function (e) { $('.selectMultipleDelete').show(); },
        //excelExport: CustomFootnoteExport,
        save: function (e) {


            if (ValidateFootnoteSave(e.model)) {
                this.refresh();

            }

        },

        saveChanges: function (e) {

            $("#footNoteGrid .k-grid-save-changes").prop('disabled', true);
            $("#footNoteGrid .k-grid-save-changes").addClass('disabled');

            var changes = SaveAllFootnoteChanges(FootnoteChanges()); // Save All Grid Changes to Server
            changes.done(function () {

                $("#footNoteGrid .k-grid-save-changes").prop('disabled', false);
                $("#footNoteGrid .k-grid-save-changes").removeClass('disabled');

                ReloadFootnote();
                popupNotification.show("Footnotes-CUSIPs details Saved Successfully", "success");

            }).fail(function () {

                $("#footNoteGrid .k-grid-save-changes").prop('disabled', false);
                $("#footNoteGrid .k-grid-save-changes").removeClass('disabled');

                popupNotification.show("Failed Saving Footnotes-CUSIPs details", "error");
            });
        },
        remove: function (e) {
            this.refresh();
        }
    });
    EnableFootnoteTooltip();
    FootnoteOrder();
    $('#footNoteGrid .selectALLMultipleDelete').change(function () {
        if ($(this).is(':checked')) {
            $('#footNoteGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', true);
        }
        else {
            $('#footNoteGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
        }
    });
}
function FootnoteOrder() {
    $("#footNoteGrid").getKendoGrid().table.kendoDraggable({
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

    $("#footNoteGrid").getKendoGrid().table.kendoDropTarget({
        group: "gridGroup",//This would recieve the drop target
        drop: function (e) {

            var grid = $("#footNoteGrid").getKendoGrid();
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
function EnableFootnoteTooltip() {
    var grid = $("#footNoteGrid").data("kendoGrid");

    grid.thead.kendoTooltip({
        filter: ".showtooltip span",
        position: "top",
        width: 100,
        content: function (e) {
            var target = e.target;
            return $(target).text();

        }
    });

}
function FootNoteSetting_gridDataBound(e) {
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

function taxonomyTypeFootnoteDropdown(container, options) {
    if (options.model.TaxonomyAssociationId <= 0 || options.model.TaxonomyAssociationId == "") {
        var MarketIds = [],
        DeletedMarketIds = [],
        gridData = $("#footNoteGrid").data("kendoGrid").dataSource;
        MarketIds = gridData._data.map(function (a) {
            return a.TaxonomyAssociationId;
        });
        DeletedMarketIds = gridData._destroyed.map(function (a) {
            return a.TaxonomyAssociationId;
        });
        var result = $.grep(TaxonomyFootnoteData, function (e) { return (MarketIds.indexOf(e.TaxonomyAssociationId) < 0) || DeletedMarketIds.indexOf(e.TaxonomyAssociationId) >= 0 });
        $('<input id="TaxonomyType" name="TaxonomyAssociationId" data-text-field="MarketId" data-value-field="TaxonomyAssociationId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    autoBind: true,
                    dataTextField: 'MarketId',
                    dataValueField: 'TaxonomyAssociationId',
                    dataSource: result,
                    change: function (e) {
                        options.model.TaxonomyMarketId = this.dataItem().MarketId;
                        options.model.TaxonomyAssociationId = this.dataItem().TaxonomyAssociationId;
                    }
                });
    }

    else {
        var element = FootnoteSettings_ReplaceTaxonomy(options.model.TaxonomyAssociationId);
        $(element)
       .appendTo(container);
    }



}
function FootnoteSettings_ReplaceTaxonomy(taxonomyAssociationId, isOnlyValue) {
    if (!TaxonomyFootnoteData) {
        taxonomyMarketIds.fetch(function () {
            TaxonomyFootnoteData = taxonomyMarketIds._data;
        });
    }
    if (taxonomyAssociationId != "") {
        var taxonomy = $.grep(TaxonomyFootnoteData, function (e) { return e.TaxonomyAssociationId == taxonomyAssociationId });
        if (isOnlyValue) {
            return taxonomy[0].MarketId;
        }
        else {
            return "<label>" + taxonomy[0].MarketId + "</label>";
        }

        //return "<label>To Do</label>";
    }

}

function ValidateFootnoteSave(model) {
    var isSuccess = true;
    var content = "";
    var grid = $("#footNoteGrid").data("kendoGrid");
    content += "<p class='message'>Please Enter/Select the below fields</p>";
    content += "<ul>";

    if (Number(model.TaxonomyAssociationId) <= 0) {
        content += "<li class='message'>CUSIP</li>";
        isSuccess = false;
    }
    if (!model.Text) {
        content += "<li class='message'>Text</li>";
        isSuccess = false;
    }

    if (!model.Order || Number(model.Order) < 1) {
        content += "<li class='message'>Order</li>";
        isSuccess = false;
    }
    else {

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

function onFootnoteGridEdit(arg) {
    $('.selectMultipleDelete').hide();
    if (arg.model.isNew()) {
        var Orders = [];
        Orders = $("#footNoteGrid").data("kendoGrid").dataSource._data.map(function (a) { return a.Order; });
        if (Orders.length > 0)
            arg.model.set("Order", Math.max.apply(Math, Orders) + 1);
        else
            arg.model.set("Order", 1);
    }

    if (arg.model.isNew()) {
        arg.container.kendoWindow("title", "Add New Footnote");
    } else {
        arg.container.kendoWindow("title", "Edit Footnote");
    }

    arg.container.addClass("Footnotes-EditPopup");
    if (arg.container[0].children[0].children[3].childElementCount == 0 || arg.container[0].children[0].children[3].firstChild.nodeName.toLowerCase() == "label") {
        arg.container[0].children[0].children[3].style.setProperty("padding", ".4em 0 1em");
    }
    $('#footNoteGrid').find("thead").find('tr').find(".selectALLMultipleDelete").prop('checked', false);
    $('#footNoteGrid').find("tbody").find('tr').find(".selectMultipleDelete").prop('checked', false);
}

function onFootnoteGridDelete(e) {


    var grid = $("#footNoteGrid").data("kendoGrid");
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
    $("#footNoteGrid").find("input:checked").each(function () {

        var data1 = currentRef.dataItem($(this).closest('tr'));
        list.push({ uid: data1.uid });

    });
    var messageToDisplay = "";
    if (list.length > 0) {
        messageToDisplay = $("#deleteFootNoteTemplate").html().replace("Text", "all selected FootNotes");
    }
    else {
        var mId = FootnoteSettings_ReplaceTaxonomy(data.TaxonomyAssociationId, true);
        messageToDisplay = $("#deleteFootNoteTemplate").html().replace("Text", "FootNote - " + mId);
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



function InitializeFootnoteComponents() {
    
    $("#btnFootNoteSearch").kendoButton({
        click: onFootnoteSearch
    });


    var taxonomyFootnoteMarketIds = new kendo.data.DataSource({
        transport: {
            read: {//Check for the method in the Controller
                url: $("#dvGetTaxonomyByTemplateForFootnote").data('request-url'),
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


    taxonomyFootnoteMarketIds.fetch(function () { // Load grid only after reading marketids (callback function)
        TaxonomyFootnoteData = taxonomyFootnoteMarketIds._data;
        FootnoteSettings.LoadFootnoteData();
    });




}

function onFootnoteSearch() {
    var query = $("#txtSearchFootNoteString").val();
    var grid = $("#footNoteGrid").data("kendoGrid");
    grid.dataSource.query({
        page: 1,
        pageSize: 20,
        filter: {
            logic: "or",
            filters: [
              { field: "TaxonomyMarketId", operator: "startswith", value: query },
            { field: "Text", operator: "startswith", value: query }

            ]
        }
    });
}

var FootnoteChanges = function () {
    var footNoteGrid = $("#footNoteGrid").data("kendoGrid"),
           updatedRecords = [],
           newRecords = [],
           updatedKeys = [],
           deletedRecords = [];
    if (footNoteGrid) {
        var currentData = footNoteGrid.dataSource.data();

        newRecords = $.grep(currentData, function (e) { return e.isNew() || Number(e.FootnoteId) == 0 }).map(function (a) { return a.toJSON(); });;
        updatedRecords = $.grep(currentData, function (e) { return e.dirty == true && e.isNew() == false }).map(function (a) { return a.toJSON(); });
        updatedKeys = updatedRecords.map(function (a) { return a.Key });
        deletedRecords = $.grep(footNoteGrid.dataSource._destroyed, function (e) { return (updatedKeys.length == 0 || updatedKeys.indexOf(e.Key) < 0) && Number(e.FootnoteId) >= 0; });
        deletedRecords = deletedRecords.map(function (a) { return a.toJSON(); });

    }
    var data = { newRecords: newRecords, updatedRecords: updatedRecords, deletedRecords: deletedRecords };
    return data;
};
var LoadFootnote = function (SiteId) {
    FootnoteSettings.InitialLoad = true;
    InitializeFootnoteComponents();
};
var SaveAllFootnoteChanges = function (data) {
    var deferred = $.Deferred();
    $.ajax({
        url: $("#dvSaveFootnote").data('request-url'),
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
var ReloadFootnote = function () {

    FootnoteSettings.LoadFootnoteData();
};

$("#footNoteUpload").kendoUpload({
    async: {
        saveUrl: $("#dvFootnoteImport").data('request-url'),
    },
    upload: function (e) {
        e.data = {
            siteId: SiteData.Id
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
            $("#FootNoteInvalidDataGridContainer").hide();
            $("#FootNoteInvalidDataGridHeader").hide();

            $(".k-upload-files li").removeClass('k-file-success');
            $(".k-upload-files li").addClass('k-file-error');
            $(".k-upload-files .k-upload-pct").text('Please import file with less than 5000 rows.');
        }
        else
        {

            if (e.response.status == "Success") {
                ReloadFootnote();
                popupNotification.show("Footnotes-CUSIPs details Saved Successfully", "success");
            }
            else {
                popupNotification.show("Failed Saving Footnotes-CUSIPs details", "error");
            }
            
            BindInvalidDataFootnote(e.response.invalidData);
            if (e.response.invalidData.length == 0) {

                $("#importFootnote").data("kendoWindow").close();
            }
        }
    }
});

$("#lnkImportFootNote").click(function () {

    $("#FootNoteInvalidDataGridContainer").hide();
    $("#FootNoteInvalidDataGridHeader").hide();

    FootnoteSave().done(function (data) {
        if (data) {
            var grid = $("#footNoteGrid").data("kendoGrid");
            grid.cancelChanges();
            $("#importFootnote").kendoWindow({
                title: "Import",
                width: "800px",
                modal: true
            }).data("kendoWindow").center().open();
        }
    });


});
$("#exportFootNote").click(function (e) {
    FootnoteSave().done(function (data) {
        if (data) {

            excelFootNoteExport('#footNoteGrid');
        }
    });
});

function excelFootNoteExport(gridName) {
    var grid = $(gridName).data("kendoGrid");
    var rows = [{
        cells: [
                { value: "Text", color: "#fff", background: "#08c", bold: true },
                { value: "CUSIPs", color: "#fff", background: "#08c", bold: true },
                { value: "Order", color: "#fff", background: "#08c", bold: true }
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
                var cusip = dataItem.TaxonomyMarketId;
                if (gridName == "#footNoteGrid") {
                    cusip = FootnoteSettings_ReplaceTaxonomy(dataItem.TaxonomyAssociationId, true);
                }

                rows.push({
                    cells: [
                            { value: dataItem.Text },
                            { value: cusip },
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
                { width: 400 },
                { width: 100 },
                { width: 100 }
            ],
            title: "Footnotes",
            rows: rows
        }
        ]
    });

    var fileName = "Footnotes.xlsx";
    if (gridName == "#FootNoteInvalidDataGrid") {
        fileName = "Invalid_Footnotes.xlsx";
    }
    kendo.saveAs({ dataURI: workbook.toDataURL(), fileName: fileName });
}

function BindInvalidDataFootnote(data) {
    var dataSource = new kendo.data.DataSource({
        data: {
            "data": data, "total": data.length
        },
        schema: {
            data: "data",
            total: "total",
        }
    });

    $("#FootNoteInvalidDataGridContainer").show();
    $("#FootNoteInvalidDataGridHeader").show();

    if ($("#FootNoteInvalidDataGrid").data("kendoGrid")) {
        $("#FootNoteInvalidDataGrid").data("kendoGrid").destroy();
        $("#FootNoteInvalidDataGrid").empty();
    }

    $("#FootNoteInvalidDataGrid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["Export"],
        excel: {
            fileName: "Invalid_Footnotes_CUSIPs.xlsx",
            filterable: true
        },
        dataBound: FootNoteSetting_gridDataBound,
        columns: [
                        { field: "Text", title: "Text", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "TaxonomyMarketId", title: "CUSIP", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
                        { field: "Order", width: 75, title: "Order", headerAttributes: { class: "wrapheader" }, attributes: { class: "wraptext" } },
        ],
    });

    $("#FootNoteInvalidDataGrid .k-grid-Export").on('click', function (e) {

        excelFootNoteExport('#FootNoteInvalidDataGrid');
    });
}

var FootnoteSettings =
    {
        LoadFootnote: LoadFootnote,
        FootnoteChanges: FootnoteChanges,
        SaveAllFootnoteChanges: SaveAllFootnoteChanges,
        ReloadFootnote: ReloadFootnote,
        LoadFootnoteData: LoadFootnoteData,
        InitialLoad: true

    };