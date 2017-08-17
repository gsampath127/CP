app.controller('TRPCustomerFLTCtrl', ['$rootScope', '$scope', '$state', '$http', 'TRPReportService', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, TRPReportService, API_ENDPOINT) {
    var initialLoad = true;
    TriggerPopOver($('#dvCustomerFLTPopOver').html(), 'Customer FLT'); // Popover for Info

    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.DisplayGrid = true;
            $scope.gridOptions = {
                columns: [
                    {
                        "title": "FLT Name", "field": "FileName",
                        template: '#=DownloadFile(FileName,DirectoryName)#'
                    },
                    {
                        "title": "Date Received/Time", "field": "ReceivedTime",
                        template: '#=DateFormat(ReceivedTime)#'
                    },
                    { "title": "Directory Name", "field": "DirectoryName", "hidden": true }
                ],
                pageable: {
                    refresh: false,
                    pageSize: 5,
                    pageSizes: [5, 10, 15]

                },

                sortable: false,
                noRecords: {
                    template: "<b>There are no FLT records for selected date.<b> "
                }
            };

            $scope.gridDataSource = {
                transport: {

                    read: function (e) {
                        var url = API_ENDPOINT.url + '/TRP/CustomerFLT';
                        var data = {
                            FLTDate: $scope.FLTDate,
                            options: e.data
                        }
                        $http.post(url, data).then(function (output) {
                            e.success(output.data);
                        });

                    }


                },
                pageSize: 10,
                serverPaging: true,
                schema: {
                    data: "data",
                    total: "total"
                },
                requestStart: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#customerFLTGrid"), true);
                    } else {
                        kendo.ui.progress($("#customerFLTGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#customerFLTGrid"), false);
                        initialLoad = false;
                    }
                }
            };

        }
    };

    $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;
        $scope.FLTDate = null;
    };

    $scope.Validate = function () {
        var isSuccess = true;
        $scope.CustomerFLTValidations = '';
        $scope.CustomerFLTValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.CustomerFLTValidations += "<ul>"
        if (!$scope.FLTDate) {
            $scope.CustomerFLTValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }
        $scope.CustomerFLTValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.CustomerFLTValidations);
        }
        return isSuccess;
    };

}]);