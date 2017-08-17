window.onunload = function (e) {
    if (window.opener.$("#siteFeatureGrid").is(':visible')) {
        if (!window.opener.$("#siteFeatureGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {
   
    var selectedFeatureModes = $('#lstFeatureMode').val();

   $("#selectedFeatureModes").val(selectedFeatureModes);
    BindFeatureModes();

    BindSiteFeatureSave();

    $("#comboFeatureKey").change(function (e) {
      
        BindFeatureModes();
    });
    $('#lstFeatureMode')
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

    
    if ($("#hdnSuccessOrFailedMessage").val() == "OK") {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Success",
            resizable: false,
            modal: true,
            draggable: false,
            actions: []
        });

        kendoWindow.data("kendoWindow")
            .content($("#divSuccessOrFailedMessage").html())
            .center()
            .open();

        kendoWindow
       .find(".confirm")
       .click(function () {
           window.close();
       });
    }
});

function BindSiteFeatureSave() {
    $("#submitSiteFeature").click(function (e) {
        //if (ValidateSiteFeatureSave()) {
        //    var element = $(document.body);
        //    kendo.ui.progress(element, true);

        //    return true;
        //}
        //return false;

       

        var selectedFeatureModes = $('#lstFeatureMode').val();

        $("#selectedFeatureModes").val(selectedFeatureModes);

        return ValidateSiteFeatureSave();
    });
}

function BindFeatureModes()
{
  
    var siteFeatureKey = $("#comboFeatureKey").val();
      LoadFeatureModes(siteFeatureKey);
  
}

function LoadFeatureModes(siteFeatureKey) {
   
        $.ajax({
            type: 'GET',
            url: $("#dvLoadFeatureMode").data('request-url'),
            data: { siteFeatureKey: siteFeatureKey },
            cache: false,
            async: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
              
              
                $('#lstFeatureMode').empty();
               

                $('#lstFeatureMode').multiselect('dataprovider', data);

                if ($("#hdnSelectedFeatureKey").val() != "-1")
                {
                   
                    PreSelect_FeatureModes(siteFeatureKey);
                }
                  
               
              
            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }
        });
    
    
      
    
}


function PreSelect_FeatureModes(siteFeatureKey) {
    // $('#' + bdSubDrpDownID).multiselect("clearSelection");
    $.ajax({
        type: 'GET',
        url: $("#dvLoadFeatureModeByKey").data('request-url'),
        data: {
            siteFeatureKey: siteFeatureKey,
            
        },
        dataType: 'json',
        success: function (data) {
            if (data.length >= 1) {
                var value = "";
                $.each(data, function (i, item) {
                    if (value == "") {
                        value += item.value;
                    } else {
                        value += "," + item.value;
                    }
                });

                value = value.split(',');
                $('#lstFeatureMode').multiselect('select', value);
            }
        }
    });

}


function ValidateSiteFeatureSave()
{
   

    var isSuccess = true;
    $("#divSiteFeatureValidations").empty();

    $("#divSiteFeatureValidations")[0].innerHTML += "<p class='message'>Please Enter the below fields</p>";
    $("#divSiteFeatureValidations")[0].innerHTML += "<ul>"




    if ($("#comboFeatureKey").val() == "-1") {

        $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Feature Key</li>";
        isSuccess = false;
    }
    else if ($('#lstFeatureMode').val() == null) {

        $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Feature Mode</li>";
        isSuccess = false;
    }
    else {
        if($("#comboFeatureKey").val()=="XBRL")
        {
            var xbrlMode = $('#lstFeatureMode').val();
            if (xbrlMode != null) {
                if (String(xbrlMode).indexOf("1,") != -1) {
                    $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Disabled can not be selected with other Feature Mode</li>";
                    isSuccess = false;
                }
                var result = jQuery.grep(xbrlMode, function (a) {
                    return a == 2;
                });

                if (result.length == 1) {
                    result = jQuery.grep(xbrlMode, function (a) {
                        return a == 4 || a == 8;
                    });

                    if (result.length == 0) {
                        $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>ShowXBRLInNewTab or ShowXBRLInTabbedView should be selected with Enabled</li>";
                        isSuccess = false;
                    }
                }
                else {
                    result = jQuery.grep(xbrlMode, function (a) {
                        return a != 1;
                    });
                    if (result.length > 0) {
                        $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Please select Enabled with other Feature Modes</li>";
                        isSuccess = false;
                    }
                }

                result = jQuery.grep(xbrlMode, function (a) {
                    return a == 4 || a == 8;
                });

                if (result.length == 2) {
                    $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>ShowXBRLInNewTab and ShowXBRLInTabbedView can not be selected together</li>";
                    isSuccess = false;
                }

                result = jQuery.grep(xbrlMode, function (a) {
                    return a == 16;
                });

                if (result.length == 1) {
                    result = jQuery.grep(xbrlMode, function (a) {
                        return a == 4 || a == 8;
                    });

                    if (result.length == 0) {
                        $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>ShowXBRLInNewTab or ShowXBRLInTabbedView should be selected with ShowXBRLInLandingPage</li>";
                        isSuccess = false;
                    }
                }
            }
            
        }
        if($("#comboFeatureKey").val()=="RequestMaterial")
        {
            var requestMaterialMode = $('#lstFeatureMode').val();
            if (String(requestMaterialMode).indexOf("1,") != -1) {
                $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        if ($("#comboFeatureKey").val() == "SARValidation") {
            var sarValidationMode = $('#lstFeatureMode').val();
            if (String(sarValidationMode).indexOf("1,") != -1) {
                $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        if ($("#comboFeatureKey").val() == "BrowserAlert") {
            var browserAlertMode = $('#lstFeatureMode').val();
            if (String(browserAlertMode).indexOf("1,") != -1) {
                $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        if ($("#comboFeatureKey").val() == "FundOrder") {
            var taxonomyOrderMode = $('#lstFeatureMode').val();
            if (taxonomyOrderMode.length>1) {
                $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Only One Feature mode is allowed for this Feature</li>";
                isSuccess = false;
            }

        }
        if ($("#comboFeatureKey").val() == "FundNameFormat") {
            var fundNameFormatMode = $('#lstFeatureMode').val();
            if (fundNameFormatMode.length > 1) {
                $("#divSiteFeatureValidations")[0].innerHTML += "<li class='message'>Only One Feature mode is allowed for this Feature</li>";
                isSuccess = false;
            }

        }
       

      
    }
   

    $("#divSiteFeatureValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divSiteFeatureValidations").html())
            .center()
            .open();
    }
    return isSuccess;



    
}