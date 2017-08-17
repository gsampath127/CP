app.controller('DailyUpdateReportCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, API_ENDPOINT) {

    var initialLoad = true;
    TriggerPopOver($('#dvDailyReportPopOver').html(), 'Daily Update Reports'); 
    
    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.DisplayGrid = true;
            $scope.gridOptions = {
                columns: [
                    { "title": "rpCUSIP", "field": "CUSIP"},
                    { "title": "rpCompanyName", "field": "CompanyName"},
                    { "title": "rpFundName", "field": "FundName"},
                    { "title": "rpCIK", "field": "CompanyCIK"},
                    { "title": "rpSeries", "field": "SeriesID" },
                    { "title": "rpClassId", "field": "Class" },
                    { "title": "rpTicker", "field": "Ticker" },
                    { "title": "OldSecurityType", "field": "OldSecurityType"},
                    { "title": "SecurityType", "field": "SecurityType"}
                ],
                pageable: {
                    refresh: false,
                    pageSize: 5,
                    pageSizes: [5, 10, 15]

                },

                sortable: true,
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
            };

            $scope.gridDataSource = {
                transport: {

                    read: function (e) {
                        var url = API_ENDPOINT.url + '/DailyUpdate/Report';
                        var data = {
                            SelectDate: $scope.SelectDate,
                            clientName: $rootScope.CurrentUser.SelectedClient,
                            options: e.data
                        }

                        $http.post(url, data).then(function (output) {
                            e.success(output.data);
                        });

                    }


                },
                pageSize: 10,
                serverPaging: true,
                serverSorting: true,
                schema: {
                    data: "data",
                    total: "total"
                },
                requestStart: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#DailyUpdateGrid"), true);
                    } else {
                        kendo.ui.progress($("#DailyUpdateGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#DailyUpdateGrid"), false);
                        initialLoad = false;
                    }
                }
            };

        }
    };


    $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;       
        $scope.SelectDate = null;
    };

    //Validate
    $scope.Validate = function () {
        var isSuccess = true;
        $scope.DailyUpdateReportValidations = '';
        $scope.DailyUpdateReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.DailyUpdateReportValidations += "<ul>"


        if (!$scope.SelectDate) {
            $scope.DailyUpdateReportValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }


        $scope.DailyUpdateReportValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.DailyUpdateReportValidations);
        }
        return isSuccess;


    };
}]);