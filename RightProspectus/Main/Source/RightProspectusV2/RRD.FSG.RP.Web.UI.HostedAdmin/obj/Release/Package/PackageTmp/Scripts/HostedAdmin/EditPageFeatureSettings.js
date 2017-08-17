window.onunload = function (e) {
    if (window.opener.$("#pageFeatureGrid").is(':visible')) {
        if (!window.opener.$("#pageFeatureGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {

    var selectedPageFeatureModes = $('#lstPageFeature').val();

    var pageId = $("#comboPageNames option:selected").val();

    $("#selectedPageFeatureModes").val(selectedPageFeatureModes);
    //BindPageKey(pageId);

    $("#submitPageFeature").click(function (e) {
        var selectedFeatureModes = $('#lstPageFeature').val();
        $("#selectedFeatureModes").val(selectedFeatureModes);


        return ValidatePageFeatureSave();

    });

    $("#comboPageNames").change(function (e) {

        var pageId = $("#comboPageNames option:selected").val();

        BindPageKey(pageId);


    });

    $("#comboPageKey").change(function (e) {
        BindPageFeatureModes();
    });

    if ($("#hdnSelectedPageId").val() != "0") {
        BindPageFeatureModes();
    }

    $('#lstPageFeature')
     .multiselect({
         numberDisplayed: 0,
         includeSelectAllOption: false,
         enableFiltering: true,
         filterBehavior: 'text',
         enableCaseInsensitiveFiltering: true,
         maxHeight: 200,
         maxWidth: 200

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

function BindPageFeatureModes() {
    var pageKey = $("#comboPageKey option:selected").val();
    var pageId = $("#comboPageNames option:selected").val();
    LoadPageFeatureModes(pageKey, pageId);
}


function ValidatePageFeatureSave() {
    var isSuccess = true;
    $("#divPageFeatureValidations").empty();

    $("#divPageFeatureValidations")[0].innerHTML += "<p class='message'>Please Enter the below fields</p>";
    $("#divPageFeatureValidations")[0].innerHTML += "<ul>"


    if (Number($("#comboPageNames").val()) == -1) {

        $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Page</li>";
        isSuccess = false;
    }

    if (Number($("#comboPageKey").val()) == -1) {

        $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Feature Key</li>";
        isSuccess = false;
    }

    if ($('#lstPageFeature').val() == null) {
        $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Feature Mode</li>";
        isSuccess = false;
    } else {
        if ($("#comboPageKey").val() == "XBRL") {

            var xbrlMode = $('#lstPageFeature').val();
            if (xbrlMode != null) {
                if (String(xbrlMode).indexOf("1,") != -1) {
                    $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled can not be selected with other Feature Mode</li>";
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
                        $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>ShowXBRLInNewTab or ShowXBRLInTabbedView should be selected with Enabled</li>";
                        isSuccess = false;
                    }
                }
                else {
                    result = jQuery.grep(xbrlMode, function (a) {
                        return a != 1;
                    });
                    if (result.length > 0) {
                        $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Please select Enabled with other Feature Modes</li>";
                        isSuccess = false;
                    }
                }

                result = jQuery.grep(xbrlMode, function (a) {
                    return a == 4 || a == 8;
                });

                if (result.length == 2) {
                    $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>ShowXBRLInNewTab and ShowXBRLInTabbedView can not be selected together</li>";
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
                        $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>ShowXBRLInNewTab or ShowXBRLInTabbedView should be selected with ShowXBRLInLandingPage</li>";
                        isSuccess = false;
                    }
                }
            }

        } else if ($("#comboPageKey").val() == "RequestMaterial") {

            var requestMaterialMode = $('#lstPageFeature').val();

            if (String(requestMaterialMode).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        else if ($("#comboPageKey").val() == "FormN-MFP") {

            var FormN_MFP = $('#lstPageFeature').val();

            if (String(FormN_MFP).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        else if ($("#comboPageKey").val() == "AllCategories") {

            var All_Categories = $('#lstPageFeature').val();

            if (String(All_Categories).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        else if ($("#comboPageKey").val() == "ShowDocumentDate") {

            var ShowDocumentDate = $('#lstPageFeature').val();

            if (String(ShowDocumentDate).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        else if ($("#comboPageKey").val() == "DocTypeGridHeader") {

            var DocTypeGridHeader = $('#lstPageFeature').val();

            if (String(DocTypeGridHeader).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }


        else if ($("#comboPageKey").val() == "DailyMoneyMarketDisclosure") {

            var DailyMoneyMarketDisclosure = $('#lstPageFeature').val();

            if (String(DailyMoneyMarketDisclosure).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }
        else if ($("#comboPageKey").val() == "N-CR") {

            var N_CR = $('#lstPageFeature').val();

            if (String(N_CR).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }

        }

        else if ($("#comboPageKey").val() == "SinglePdfView") {

            var singlePdfViewMode = $('#lstPageFeature').val();


            if (String(singlePdfViewMode).indexOf("1,") != -1) {

                $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>Disabled Can't be Selected with Other Feature Mode</li>";
                isSuccess = false;
            }
            else {
                var SinglePdfViewResult = jQuery.grep(singlePdfViewMode, function (a) {
                    return a == 4;
                });
                if (SinglePdfViewResult.length == 1) {

                    var SinglePdfViewResult = jQuery.grep(singlePdfViewMode, function (a) {
                        return a == 2;
                    });

                    if (SinglePdfViewResult.length == 0) {
                        $("#divPageFeatureValidations")[0].innerHTML += "<li class='message'>ShowClientLogoFrame should be selected with Enabled</li>";
                        isSuccess = false;
                    }
                }

            }
        }

    }

    $("#divPageFeatureValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divPageFeatureValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

function BindPageKey(pageId) {
    $.ajax({

        type: 'GET',
        url: $("#dvGetPageKeyByPageId").data('request-url'),
        data: { pageId: pageId },
        cache: false,
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (pageId > 0) {
                $('#comboPageKey').empty();
                $('#comboPageKey').append($('<option>', {
                    value: -1,
                    text: "-- Please select Page Key--"
                }));
            }
            $.each(data, function (i, item) {

                $('#comboPageKey').append($('<option>', {
                    value: item.Value,
                    text: item.Display
                }));
            });
        },
        error: function (xhr) {

            alert(xhr.state + xhr.statusText);
        }
    });

}

function LoadPageFeatureModes(pageKey, pageId) {
    $.ajax({
        type: 'GET',
        url: $("#dvLoadPageFeatureMode").data('request-url'),
        data: { pageKey: pageKey },
        cache: false,
        async: false,
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#lstPageFeature').empty();
            $('#lstPageFeature').multiselect('dataprovider', data);
            if ($("#hdnSelectedPageId").val() != "0" && $("#hdnSelectedPageKey").val() != "-1") {
                PreSelect_FeatureModes(pageKey, pageId);
            }
            //else {
            //    if ($("#comboPageKey option:selected").val() == "XBRL" || $("#comboPageKey option:selected").val() == "RequestMaterial") {
            //        PreSelect_FeatureModes(pageKey, pageId);
            //    }
            //}
        },
        error: function (xhr) {

            alert(xhr.state + xhr.statusText);
        }
    });

}

function PreSelect_FeatureModes(pageKey, pageId) {
    $.ajax({
        type: 'GET',
        url: $("#dvLoadPageFeatureModeByKey").data('request-url'),
        data: { pageKey: pageKey, pageId: pageId },
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
                $('#lstPageFeature').multiselect('select', value);
            }
        }
    });

}

