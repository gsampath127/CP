var requestCompleted = false;
$(document).ready(function () {

    $(document)
      .ajaxStart(function () {
          kendo.ui.progress($(document.body), true);
      })
      .ajaxStop(function () {
          kendo.ui.progress($(document.body), false);
      });

    $('#rootwizard').bootstrapWizard({
        'tabClass': 'nav nav-tabs',
        'onNext': function (tab, navigation, index) {

                if (!requestCompleted) {
                Navigate(index).done(function (data) {
                    if (data)
                    {
                        requestCompleted = true;
                        jQuery('#rootwizard').bootstrapWizard('next');
                    }
                    else {
                        requestCompleted = false;
                    }

                    return true;

                });
                return false;
            }
            else {
                requestCompleted = false;

            }


        },
        'onPrevious': function (tab, navigation, index) {
            if (!requestCompleted) {
                Navigate(index).done(function (data) {
                    if (data) {
                        requestCompleted = true;
                        jQuery('#rootwizard').bootstrapWizard('previous');
                    }
                    else {
                        requestCompleted = false;
                    }

                    return true;

                });
                return false;
            }
            else {
                requestCompleted = false;

            }

        },
        'onTabClick': TabClickHandler

    });
    $('#VerticalImportUIPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Vertical Import UI</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;clientPopOver&apos;)">&times;</button></span>',
        content: $("#dvVerticalImportUIPopOver").html(),
        trigger: 'click'
    });
    LoadSiteCombo();
    popupNotification = $("#popupNotification").kendoNotification({ autoHideAfter: 5000 , height:50}).data("kendoNotification");

});


function ClosePopOver(popoverID) {
    $('#' + popoverID).trigger("click");
}
// hide any open popovers when the anywhere else in the body is clicked
$('body').on('click', function (e) {
    $('[data-toggle=popover]').each(function () {
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            if ($('body div').find('.popover-content').length > 0)
                $('[data-toggle=popover]').trigger("click");
        }
    });
});
function LoadSiteCombo() {
    $('#comboSite').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvSiteNameComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Name',
        dataValueField: 'Id',
        suggest: true,
        placeholder: 'Select Site Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            if ($('.breadcrumb').children().length >3)
            {
                $('.breadcrumb :nth-child(4)').text(e.sender.text());
            }
            else {
                $('.breadcrumb').append("<li class='active'>" + e.sender.text() + "</li>");
            }

            SiteData = this.dataItem();
            if (SiteData.DefaultPage == "TAL")
            {

                $('#rootwizard').bootstrapWizard('enable', 0);
                $('#rootwizard').bootstrapWizard('enable', 1);
                $('#rootwizard').bootstrapWizard('enable', 2);
                $('#rootwizard').bootstrapWizard('enable', 3);
                $('#rootwizard').bootstrapWizard('enable', 4);
                $('#rootwizard').bootstrapWizard('enable', 5);
                $('#rootwizard').bootstrapWizard('disable', 6);
                $('#rootwizard').bootstrapWizard('disable', 7);
                $('#rootwizard').bootstrapWizard('disable', 8);
                $('#rootwizard').bootstrapWizard('enable', 9);
                $('#rootwizard').bootstrapWizard('enable', 10);

                $("#ImportMarketId").show();
                $("#ImportMarketIdCustomer").show();
                $("#ImportProduct").show();
                $("#TaxonomyGroups").hide();
                $("#TaxonomyGroupHierarchy").hide();
                $("#TaxonomyGroupFunds").hide();

                $('#ImportMarketId').text("Add/Edit Product CUSIPs");
                $('#MarketIdLevel').text("Add/Edit Document Types - CUSIPs");
                $('#ImportMarketIdCustomer').text("Add/Edit Underlying Fund CUSIPs");
                
            }
            else if (SiteData.DefaultPage == "TAD")
            {
                $('#rootwizard').bootstrapWizard('enable', 0);
                $('#rootwizard').bootstrapWizard('enable', 1);
                $('#rootwizard').bootstrapWizard('enable', 2);
                $('#rootwizard').bootstrapWizard('disable', 3);
                $('#rootwizard').bootstrapWizard('disable', 4);
                $('#rootwizard').bootstrapWizard('enable', 5);
                $('#rootwizard').bootstrapWizard('disable', 6);
                $('#rootwizard').bootstrapWizard('disable', 7);
                $('#rootwizard').bootstrapWizard('disable', 8);
                $('#rootwizard').bootstrapWizard('enable', 9);
                $('#rootwizard').bootstrapWizard('enable', 10);

                $("#ImportMarketId").show();
                $("#ImportMarketIdCustomer").hide();
                $("#ImportProduct").hide();
                $("#TaxonomyGroups").hide();
                $("#TaxonomyGroupHierarchy").hide();
                $("#TaxonomyGroupFunds").hide();

                $('#ImportMarketId').text("Add/Edit CUSIPs");
                
            }
            else if(SiteData.DefaultPage == "TAGD")
            {
                $('#rootwizard').bootstrapWizard('enable', 0);
                $('#rootwizard').bootstrapWizard('enable', 1);
                $('#rootwizard').bootstrapWizard('disable', 2);
                $('#rootwizard').bootstrapWizard('enable', 3);
                $('#rootwizard').bootstrapWizard('disable', 4);
                $('#rootwizard').bootstrapWizard('enable', 5);
                $('#rootwizard').bootstrapWizard('enable', 6);
                $('#rootwizard').bootstrapWizard('enable', 7);
                $('#rootwizard').bootstrapWizard('enable', 8);
                $('#rootwizard').bootstrapWizard('enable', 9);
                $('#rootwizard').bootstrapWizard('enable', 10);

                $("#ImportMarketId").hide();
                $("#ImportMarketIdCustomer").show();
                $("#ImportProduct").hide();
                $("#TaxonomyGroups").show();
                $("#TaxonomyGroupHierarchy").show();
                $("#TaxonomyGroupFunds").show();

                $('#ImportMarketIdCustomer').text("Add/Edit CUSIPs");
            }
            else
            {
                
            }

        }
    });

    $("#comboSite").data("kendoComboBox").dataSource.read();
}

function ValidateNavigation()
{
    var deferred = $.Deferred();
    var isSuccess = true;
    var content = "";
    content += "<p class='message'>Please Enter/Select the below fields</p>";
    content += "<ul>";
    if ($('#comboSite').val() == "" || $('#comboSite').val() ==null) {
        content += "<li class='message'>Site</li>";
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

    if (isSuccess)
    {
        deferred.resolve(true);
    }
    else {
        deferred.resolve(false);
    }
    return deferred.promise();

}

function TabClickHandler(activeTab, navigation, currentIndex, clickedIndex)
{
    if (!requestCompleted) {
        Navigate(clickedIndex,currentIndex).done(function (data) {
            if (data) {
                requestCompleted = true;
                $('#rootwizard').bootstrapWizard('show', clickedIndex);
                TabClickHandler(activeTab, navigation, currentIndex, clickedIndex);

            }
            else {
                requestCompleted = false;
            }

            return true;

        });
        return false;
    }
    else {

        requestCompleted = false;

    }
}

var Navigate = function (index,previousIndex) {
    var deferred = $.Deferred();
    if (!previousIndex)
        previousIndex = index - 1;
    var changes = ValidateSaveChanges(previousIndex);
    changes.done(function (data) {
        if (data)
        {
            if (index == 10 && $("#comboSite").val()) {
                ApproveProofingSettings.NavigateToHostedEngine(true);
            } else {
                LoadVerticalData(index);
            }
        }

        deferred.resolve(data);
    });


    return deferred.promise();

};

var LoadVerticalData = function (index) {
    switch(index)
    {
        case 1: // Document Type Association
            DocumentTypeSettings.LoadDocumentTypeAssociation(SiteData.Id);
            break;
        case 2: // Taxonomy Association at Site Level
            TaxonomySettings.LoadTaxonomyAssociation(SiteData.Id);
            break;
        case 3: // Taxonomy Association at Customer Level
            TaxonomyCustomerSettings.LoadTaxonomyAssociationCustomer(null);
            break;
        case 4: // Taxonomy Association Hierarchy
            ImportProductSettings.LoadImportProduct(SiteData.Id);
            break;
        case 5: // Market Level Document Types
            MarketLevelDocumentTypeSettings.LoadMarketLevelDocumentTypeAssociation(SiteData.Id);
            break;
        case 6: //Taxonomy Groups 
            TaxonomyGroupSettings.LoadTaxonomyAssociationGroups(SiteData.Id);
            break;
        case 7: //Taxonomy Groups 
            TaxonomyGroupMappingAndOrderingSettings.LoadTaxonomyGroupMappingAndOrdering(SiteData.Id);
            break;
        case 8: //Taxonomy group fund mapping 
            UnderlyingFundSettings.LoadIUnderLyingFund(SiteData.Id);
            break;
        case 9: // Footnotes - CUSIPs
            FootnoteSettings.LoadFootnote();
            break;
        default:
            break;
    }
};

var  ValidateSaveChanges = function(index)
{
    var deferred = $.Deferred();
    ValidateNavigation().done(function (data) {
        if (data) {
            switch (index) {
                case 1:
                    DocumentTypeAssociationSave().done(function (data) { deferred.resolve(data); });
                    break;
                case 2:
                    TaxonomyAssociationSave().done(function (data) { deferred.resolve(data); });
                    break;
                case 3:
                    TaxonomyAssociationCustomerSave().done(function (data) { deferred.resolve(data); });
                    break;
                case 4:
                    TaxonomyAssociationHierarchySave().done(function (data) { deferred.resolve(data); });
                    break;
                case 5:
                    MarketLevelDocumentTypeAssociationSave().done(function (data) { deferred.resolve(data); });
                    break;
                case 6:
                    TaxonomyGroupSave().done(function (data) { deferred.resolve(data); });
                    break;

                case 8: TaxonomyGroupFundSave().done(function (data) { deferred.resolve(data); });
                    break;
                case 9:
                    FootnoteSave().done(function (data) { deferred.resolve(data); });
                    break;
                    
               
                    //case 5:
                    //    ApproveProofingSettings.NavigateToHostedEngine(true);
                    //    break;
                default: deferred.resolve(true);
                    break;

            }
        }
        else {
            deferred.resolve(data);
        }


    });


    return deferred.promise();
}

var ConfirmWindow = function (titleText) {
    var kendoWindow = $('<div />').kendoWindow({
        title: titleText,
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#confirmSave").html())
        .center().open();

    return kendoWindow;

};

var DocumentTypeAssociationSave = function () {
    var data = DocumentTypeSettings.DocumentTypeAssociationChanges(),deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("DocumentTypeAssociation at Site Level");
        window
            .find(".confirm,.cancel")
            .click(function () {

                if ($(this).hasClass("confirm")) {
                    var changes = DocumentTypeSettings.SaveAllDocumentTypeAssociationChanges(data);
                    changes.done(function () {
                        DocumentTypeSettings.ReloadDocumentTypeAssociation();
                        popupNotification.show("Document Types-Site Saved Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);

                    }).fail(function () {
                        popupNotification.show("Failed Saving Level Document Types-Site", "error");
                        deferred.resolve(false);
                    });


                }
                if ($(this).hasClass("cancel")) {

                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();
};
var TaxonomyAssociationSave = function () {
    var data = TaxonomySettings.TaxonomyAssociationChanges(), deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("TaxonomyAssociation at Site Level");
        window
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    var changes = TaxonomySettings.SaveAllTaxonomyAssociationChanges(data);
                    changes.done(function () {
                        TaxonomySettings.ReloadTaxonomyAssociation();
                        popupNotification.show("CUSIPs-Site Saved Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);
                    }).fail(function () {
                        popupNotification.show("Failed Saving CUSIPs-Site", "error");
                        deferred.resolve(false);
                    });

                }
                if ($(this).hasClass("cancel")) {
                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();

};
var MarketLevelDocumentTypeAssociationSave = function () {
    var data = MarketLevelDocumentTypeSettings.MarketLevelDocumentTypeAssociationChanges(), deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("Market Level DocumentType");
        window
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    var changes = MarketLevelDocumentTypeSettings.SaveAllMarketLevelDocumentTypeAssociationChanges(data);
                    changes.done(function () {
                        MarketLevelDocumentTypeSettings.ReloadMarketLevelDocumentTypeAssociation();
                        popupNotification.show("Document Types-CUSIPs Saved Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);
                    }).fail(function () {
                        popupNotification.show("Failed Saving Document Types-CUSIPs", "error");
                        deferred.resolve(false);
                    });

                }
                if ($(this).hasClass("cancel")) {
                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();
};
var TaxonomyAssociationHierarchySave = function () {
    var data = ImportProductSettings.ImportProductChanges(), deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("Import Products");
        window
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    var changes = ImportProductSettings.SaveAllImportProductChanges(data);
                    changes.done(function () {
                        ImportProductSettings.ReloadImportProduct();
                        popupNotification.show("Products to Fund Association Mapped Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);
                    }).fail(function () {
                        popupNotification.show("Failed mapping Products to Fund Association", "error");
                        deferred.resolve(false);
                    });

                }
                if ($(this).hasClass("cancel")) {
                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();
};
var TaxonomyAssociationCustomerSave = function () {
    var data = TaxonomyCustomerSettings.TaxonomyAssociationCustomerChanges(), deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("Taxonomy Association Customer Level");
        window
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    var changes = TaxonomyCustomerSettings.SaveAllTaxonomyAssociationCustomerChanges(data);
                    changes.done(function () {
                        TaxonomyCustomerSettings.ReloadTaxonomyAssociationCustomer();
                        popupNotification.show("Underlying Funds-CUSIPs Saved Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);
                    }).fail(function () {
                        popupNotification.show("Failed Saving Underlying Funds-CUSIPs", "error");
                        deferred.resolve(false);
                    });

                }
                if ($(this).hasClass("cancel")) {
                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();
};
var FootnoteSave = function () {
    var data = FootnoteSettings.FootnoteChanges(), deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("Market Level DocumentType");
        window
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    var changes = FootnoteSettings.SaveAllFootnoteChanges(data);
                    changes.done(function () {
                        FootnoteSettings.ReloadFootnote();
                        popupNotification.show("Footnotes-CUSIPs Saved Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);
                    }).fail(function () {
                        popupNotification.show("Failed Saving Footnotes-CUSIPs", "error");
                        deferred.resolve(false);
                    });

                }
                if ($(this).hasClass("cancel")) {
                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();
};
var TaxonomyGroupSave = function () {
    var data = TaxonomyGroupSettings.TaxonomyAssociationGroupChanges(), deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("Taxonomy Groups");
        window
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    var changes = TaxonomyGroupSettings.SaveAllTaxonomyAssociationGroupChanges(data);
                    changes.done(function () {
                        TaxonomyGroupSettings.ReloadTaxonomyAssociationGroup();
                        popupNotification.show("Group Saved Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);
                    }).fail(function () {
                        popupNotification.show("Failed Saving Group", "error");
                        deferred.resolve(false);
                    });

                }
                if ($(this).hasClass("cancel")) {
                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();
};
var TaxonomyGroupFundSave = function () {
    var data = UnderlyingFundSettings.ImportGroupFundChanges(), deferred = $.Deferred();
    if (data.newRecords.length > 0 || data.updatedRecords.length > 0 || data.deletedRecords.length > 0) {
        var window = ConfirmWindow("Taxonomy GroupFunds");
        window
            .find(".confirm,.cancel")
            .click(function () {
                if ($(this).hasClass("confirm")) {
                    var changes = UnderlyingFundSettings.SaveAllGroupFundChanges(data);
                    changes.done(function () {
                        UnderlyingFundSettings.ReloadUnderLyingGroupFund();
                        popupNotification.show("Group Funds mapping Saved Successfully", "success");
                        window.data("kendoWindow").close();
                        deferred.resolve(true);
                    }).fail(function () {
                        popupNotification.show("Failed Saving Group Funds mapping", "error");
                        deferred.resolve(false);
                    });

                }
                if ($(this).hasClass("cancel")) {
                    window.data("kendoWindow").close();
                    deferred.resolve(true);
                }
            });
    }
    else {
        deferred.resolve(true);
    }
    return deferred.promise();
};

