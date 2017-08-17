app.controller('EditMissingReportCtrl', ['$rootScope', '$scope', '$http', '$state', '$uibModalInstance', 'AddEditMissingReportService', 'API_ENDPOINT', function ($rootScope, $scope, $http, $state, $uibModalInstance, AddEditMissingReportService, API_ENDPOINT) {
    var typesOfReport = [{ Value: "0", Text: "UIT" }, { Value: "1", Text: "ETN" }, { Value: "2", Text: "ETF" }, { Value: "3", Text: "MF" }, { Value: "4", Text: "NA" }];
    $scope.parentData.action = "Update Security Type";
    $scope.SecurityTypeFeedSourceName = "Manual";
    //alert("SHOW CUSIP"+$scope.CUSIP);
    $scope.comboLevel = {
        
            placeholder: "Select Level",
            autobind: true,
            dataValueField: 'Value',
            dataTextField: 'Text',
            dataSource: {
                data: typesOfReport
            }
    };
    $scope.ShowAddArea = true;
    $scope.btnClearEditMissingReport = function () {
        $scope.SelectedLevel = "";
    }
    $scope.CloseWindow = function () {
        $uibModalInstance.close('Cancel');
    };

    $scope.EditLevelValidate = function () {
        var isSuccess = true;
        $scope.EditLevelValidations = '';
        $scope.EditLevelValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.EditLevelValidations += "<ul>"

        if (!$scope.SelectedLevel) {
            $scope.EditLevelValidations += "<li class='message'>Level</li>";
            isSuccess = false;
        }

        $scope.EditLevelValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.EditLevelValidations);
        }
        return isSuccess;


    };

    $scope.AddEditLevel = function () {
        $scope.report = {};
        $scope.report.CUSIP = $scope.parentData.cusip;
        $scope.report.SelectedLevel = $scope.SelectedLevel;
        $scope.report.SecurityTypeFeedSourceName = $scope.SecurityTypeFeedSourceName;
        if ($scope.EditLevelValidate()) {

            AddEditMissingReportService.AddEditLevel($scope.report).then(function (output) {
                var isSuccess = true;
                $scope.DataValidations = '';
                if (output.data == 1) {
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
                       kendoWindow.data("kendoWindow").close();
                       $uibModalInstance.close('Inserted');
                   
                   });

                }
                
                else {
                    isSuccess = false;
                    $scope.DataValidations = "<ul><li class='message'>Edit Level Failed.</li></ul>";
                }


                if (!isSuccess && $scope.DataValidations) {
                    KendoValidationWindow($scope.DataValidations);
                }

            }, function (err) {
                //redirect to error page or state
            }
                );
        }
    }
}]);


