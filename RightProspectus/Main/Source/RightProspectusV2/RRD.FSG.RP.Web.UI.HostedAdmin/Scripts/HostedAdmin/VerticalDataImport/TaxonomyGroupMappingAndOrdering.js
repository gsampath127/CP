function requestEndHandler() {
    setTimeout("expandNodes()");
}

function expandNodes() {
    $("#taxonomyAssociationGroupTreeView").data("kendoTreeView").expand(".k-item");
}

function loadTaxonomyGroupMappingAndOrdering(siteId) {

    if ($("#taxonomyAssociationGroupTreeView").data("kendoTreeView")) {
        $("#taxonomyAssociationGroupTreeView").data("kendoTreeView").destroy();
        $("#taxonomyAssociationGroupTreeView").empty();
    }

    $.ajax({
        type: "POST",
        data: { "siteId": siteId },
        dataType: "JSON",
        url: $("#dvTaxonomyAssociationGroupMapping").data('request-url'),
        async: false,
        success: function (data) {
            var dataSource = new kendo.data.HierarchicalDataSource({
                data: data,
                requestEnd: requestEndHandler,
                schema: {
                    data: "data",
                    model: {
                        id: "TaxonomyAssociationGroupId",
                        hasChildren: "HasChildren",
                        children: "ChildTAGData",
                        expanded: true,
                    }
                }
            });
            
            $("#taxonomyAssociationGroupTreeView").kendoTreeView({
                template: kendo.template($("#taxonomyAssociationGroupTreeView-template").html()),
                dataSource: dataSource,
                dataTextField: "Name",
                dragAndDrop: true,
                drag: function (e) {

                    var destinationItem = this.dataItem(e.dropTarget);
                    if (e.statusClass.indexOf("add") >= 0 && destinationItem.HasFundData) {                        
                        e.setStatusClass("k-denied");
                    }                    
                },
                drop: function (e) {                    
                    var destinationItem = this.dataItem(e.destinationNode);

                    if (destinationItem == null || (e.dropPosition == "over" && destinationItem.HasFundData)) {
                        e.setValid(false);
                    }
                   
                }
            });

            // Delete button behavior
            $(document).on("click", ".removeTreeNode", function (e) {
                e.preventDefault();
                var element = $(this);

                if ($("#deleteTAGMappingWindow").data("kendoWindow")) {
                    $("#deleteTAGMappingWindow").data("kendoWindow").destroy();
                    $("#deleteTAGMappingWindow").empty();
                }
                
                var kendoWindow = $('<div id=deleteTAGMappingWindow/>').kendoWindow({
                    title: "Confirm",
                    resizable: false,
                    modal: true,
                    draggable: false                    
                });

                kendoWindow.data("kendoWindow")
                .content($("#deleteTAGMappingTemplate").html())
                .center().open();

                        kendoWindow
                            .find(".confirm,.cancel")
                            .click(function () {
                                if ($(this).hasClass("confirm")) {
                                    var treeview = $("#taxonomyAssociationGroupTreeView").data("kendoTreeView");
                                    treeview.remove(element.closest(".k-item"));

                                    kendoWindow.data("kendoWindow").close();
                                }
                                if ($(this).hasClass("cancel")) {
                                    kendoWindow.data("kendoWindow").close();
                                }
                            });
                
            });

        }
    }); 
}

$("#btnSaveGroupMappingChanges").click(function () {
    SaveGroupMappingChanges();
});

$("#btnCancelGroupMappingChanges").click(function () {
    ReloadTaxonomyGroupMappingAndOrdering();
});

var SaveGroupMappingChanges = function () {

    var SaveGroupMappingChanges_Loader = new kendo.data.DataSource({
        transport: {
            read: {
                url: "",
                cache: true
            },
            type: "GET"
        }
    });
    SaveGroupMappingChanges_Loader.fetch(function () {
    });
    
    var treeview = $("#taxonomyAssociationGroupTreeView").data("kendoTreeView");
    var data = treeview.dataSource.view().map(function (a) {
        return a.toJSON();
    });

    $.ajax({
        type: "POST",
        data: { siteId: Number(SiteData.Id), groupData: data },
        url: $("#dvSaveTaxonomyAssociationGroupMapping").data('request-url'),
        async: false,
        success: function (data) {
            if (data == "Success") {
                ReloadTaxonomyGroupMappingAndOrdering();
                popupNotification.show("Group Mapping details saved successfully", "success");
            }
            else {
                popupNotification.show("Failed to save Group Mapping details", "error");
            }
        },
        error: function () {
            popupNotification.show("Failed to save Group Mapping details", "error");//Handle the server errors using the approach from the previous example
        }
    })    
};

var ReloadTaxonomyGroupMappingAndOrdering = function () {
    loadTaxonomyGroupMappingAndOrdering(SiteData.Id);
};


var LoadTaxonomyGroupMappingAndOrdering = function (siteId) {

    var LoadTaxonomyGroupMappingAndOrdering_loader = new kendo.data.DataSource({
        transport: {
            read: {
                url: "",
                cache: true
            },
            type: "GET"
        }
    });
    LoadTaxonomyGroupMappingAndOrdering_loader.fetch(function () {
    });

    TaxonomySettings.InitialLoad = true;
    TaxonomySettings.SiteId = siteId;

    InitializeTaxonomyGroupMappingAndOrderingComponents(siteId);

};
function InitializeTaxonomyGroupMappingAndOrderingComponents(siteId) {
        
    loadTaxonomyGroupMappingAndOrdering(siteId);
}

var TaxonomyGroupMappingAndOrderingSettings =
{
    LoadTaxonomyGroupMappingAndOrdering: LoadTaxonomyGroupMappingAndOrdering,
    SaveGroupMappingChanges: SaveGroupMappingChanges,
    ReloadTaxonomyGroupMappingAndOrdering: ReloadTaxonomyGroupMappingAndOrdering,
    InitialLoad: true,
    SiteId: null
};