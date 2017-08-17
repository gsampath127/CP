var SaveAllProofingChanges = function () {  
    

    var SiteId = $("#comboSite").val();   
        $.ajax({
            url: $("#dvApproveProofing").data('request-url'),
            data: { siteId: Number(SiteId) },
            type: "POST",
            success: function (data) {
                if (data == "Success") {
                    popupNotification.show("All Proofing Changes saved successfully", "success");
                }
                else {
                    popupNotification.show("Unable to Approve Changes", "error");
                }
            },
            error: function () {
                popupNotification.show("Unable to Approve Changes", "error");//Handle the server errors using the approach from the previous example
            }

        }); 
};

$("#btnApproveAllProofing").click(function () {
    SaveAllProofingChanges(); // Save All Production Changes to Server
});


var NavigateToHostedEngine = function (proofing) {

    $.ajax({
        url: $("#dvShowProofing").data('request-url'),
        data: {
            siteId: Number(SiteData.Id),

        },
        type: "POST",
        success: function (data) {
            if (data != "") {
                $('#lnkShowProduction').attr('href', data.production);
                $('#lnkShowProofing').attr('href', data.proofing);
                if (proofing) {                  
                    window.open(data.proofing, "_blank");
                }
                else {                  
                    window.open(data.production, "_blank");
                }
            }
        }
    });
};

var ApproveProofingSettings =
    {
        SaveAllProofingChanges: SaveAllProofingChanges,
        NavigateToHostedEngine: NavigateToHostedEngine
    };