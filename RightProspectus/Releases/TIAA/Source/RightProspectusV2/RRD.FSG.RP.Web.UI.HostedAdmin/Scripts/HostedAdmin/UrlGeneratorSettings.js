$(document).ready(function () {
    $("#GridContainer").hide();
    ClearFilters();
    LoadSearchParameters();
    
    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#siteGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#siteGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#siteGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#btnUrlGenerate').focus().click();
        }
    });
    $('#sitePopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Sites</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;sitePopOver&apos;)">&times;</button></span>',
        content: $("#dvSitePopOver").html(),
        trigger: 'click'
    });

    ExportToExcel();
});



function ExportToExcel() {
    $('#btnUrlGenerate').click(function () {
        var siteName = $('#SiteNameCombo').data("kendoComboBox").text();
        var marketId = $('#MarketIdCombo').data("kendoComboBox").text();

        var url = $("#dvLoadAllUrl").data('request-url');
        url = url + "?siteName=" + siteName + "&marketID=" + marketId;
        $("#btnUrlGenerate").prop("href", url);
        
    });
}



//Added below fundction to reload VerticalXmlImport and VerticalXmlExport options in layout page
function Reload() {
    location.reload(true);
}
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

function LoadSearchParameters() {

    LoadSiteNameCombo();
    LoadMarketIdCombo();
    
}

function ClearFilters() {
    $('#siteClearFilters').click(function () {
        $("#GridContainer").hide();
        $('#SiteNameCombo').data("kendoComboBox").text('');
        $('#MarketIdCombo').data("kendoComboBox").text('');
        $("#siteGridwithSortingButtons").css('display', 'none');
        $('#sitecontainerDiv').empty();
    });

}

function LoadSiteNameCombo() {
    $('#SiteNameCombo').kendoComboBox({
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
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select Site Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#SiteNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadMarketIdCombo() {
    $('#MarketIdCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {//Check for the method in the Controller
                    url: $("#dvMarketIdComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select Cusip Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#MarketIdCombo").data("kendoComboBox").dataSource.read();
}