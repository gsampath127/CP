app.controller('TRPCustomerDocsCtrl', ['$rootScope', '$scope', '$state', '$http', 'TRPReportService', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, TRPReportService, API_ENDPOINT) {
    var initialLoad = true;
    TriggerPopOver($('#dvCustomerDocsPopOver').html(), 'Customer Doc(s)'); // Popover for Info

    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.DisplayGrid = true;
            $scope.gridOptions = {
                columns: [
                    {
                        "title": "Docs Name", "field": "FileName",
                        template: function (dataItem) {
                            return "<a href='" + dataItem.DirectoryName + "' target='_blank'>" + dataItem.FileName + "</a>"
                        }
                    },
                    {
                        "title": "Date Received/Time", "field": "DateReceived",
                        template: '#=DateFormat(DateReceived)#'
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
                    template: "<b>There are no Docs for selected date.<b> "
                }
            };

            $scope.gridDataSource = {
                transport: {

                    read: function (e) {
                        var url = API_ENDPOINT.url + '/TRP/CustomerDocs';
                        var data = {
                            DocDate: $scope.DocDate,
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
                        kendo.ui.progress($("#customerDocsGrid"), true);
                    } else {
                        kendo.ui.progress($("#customerDocsGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#customerDocsGrid"), false);
                        initialLoad = false;
                    }
                }
            };

        }
    };

    $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;
        $scope.DocDate = null;
    };

    $scope.Validate = function () {
        var isSuccess = true;
        $scope.CustomerDocsValidations = '';
        $scope.CustomerDocsValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.CustomerDocsValidations += "<ul>"
        if (!$scope.DocDate) {
            $scope.CustomerDocsValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }
        $scope.CustomerDocsValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.CustomerDocsValidations);
        }
        return isSuccess;
    };

}]);