app.controller('FullfillmentInfoReportCtrl', ['$rootScope', '$scope', '$state', 'FullfillmentInfoReportService', 'API_ENDPOINT', function ($rootScope, $scope, $state, FullfillmentInfoReportService, API_ENDPOINT) {
    TriggerPopOver($('#dvFullfilmentReportPopOver').html(), 'Fulfilment Info Reports'); // Popover for Info
    $scope.GenerateFullfillmentReport = function () {
        if ($scope.Validate()) {
            $scope.gridOptionsHeader = {
                columns: [
                    { "title": "Total Requests",  "field": "TotalRequests" },
                    { "title": "Completed",  "field": "Completed" },
                    { "title": "Not Available", "field": "NotAvailable" },
                ],
                scrollable: false,
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
            };

            $scope.gridOptionsDetails = {
                columns: [
                    { "title": "Trans Id",  "field": "TransID" },
                    { "title": "CUSIP",  "field": "CUSIP" },
                    { "title": "Status", "field": "Status" },
                ],
                scrollable: false,
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
            };

            $scope.rpt = {};
            $scope.rpt.reportDate = $scope.reportDate;
            $scope.rpt.ClientName = $rootScope.CurrentUser.SelectedClient;

            FullfillmentInfoReportService.ReportData($scope.rpt).then(function (output) {
                $scope.showGrid = true;
                $scope.gridDataSourceHeader = output.data;
                $scope.gridDataSourceDetails = output.data[0].FullfillmentDetails;
            }, function (res) {
            });
        }
    };

    $scope.ClearFilters = function () {
        $scope.form.submitted = false;
        $scope.reportDate = undefined;
        $scope.showGrid = false;
    };
    //Validate
    $scope.Validate = function () {
        var isSuccess = true;
        $scope.FullfillmentReportValidations = '';
        $scope.FullfillmentReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.FullfillmentReportValidations += "<ul>"


        if (!$scope.reportDate) {
            $scope.FullfillmentReportValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }

        $scope.CUSIPReportValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.FullfillmentReportValidations);
        }
        return isSuccess;


    };
}]);