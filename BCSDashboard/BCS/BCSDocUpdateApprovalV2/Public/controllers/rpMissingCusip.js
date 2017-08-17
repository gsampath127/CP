app.controller('RPMissingCusipCtrl', ['$rootScope', '$scope', '$state', 'rpMissingCusipService', '$http', 'API_ENDPOINT', function ($rootScope, $scope, $state, rpMissingCusipService, $http, API_ENDPOINT) {

    var initialLoad = true;
    TriggerPopOver($('#dvRPMissingCusipReportPopOver').html(), 'RP Missing Cusip Report');
    rpMissingCusipService.GetCompany().then(function (output) {

        var company = output.data.CompanyDetails; 
        $scope.CompanyOptions = {
            placeholder: "Select Company",
            autobind: true,
            dataSource: company,
            dataTextField: "ClientName",
            dataValueField: "ClientName"
        };
    });
    $scope.GenerateReport = function () {
        initialLoad = true;
        $scope.gridDataSource = {};
        $scope.gridOptions = {};
        $scope.DisplayGrid = true;
        $scope.gridOptions = {
            columns: [
                { "title": "Company Name", "field": "CompanyName" },
                { "title": "Fund Name", "field": "FundName" },
                { "title": "FLT CUSIP", "field": "FLTCUSIP" },
                { "title": "LIPPER CUSIP", "field": "CompanyCIK" },//data field ?
                { "title": "EOnline CUSIP", "field": "ShareClass" },//data field ?
                { "title": "Calculated CUSIP", "field": "CalculatedCUSIP" }
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
                    var url = API_ENDPOINT.url + '/RPMissingCusip/Report';
                    var data = {
                        Company: $scope.Company,
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
                    kendo.ui.progress($("#rpMissingCusipGrid"), true);
                } else {
                    kendo.ui.progress($("#rpMissingCusipGrid"), false);
                }
            },
            requestEnd: function () {
                if (initialLoad) {
                    kendo.ui.progress($("#rpMissingCusipGrid"), false);
                    initialLoad = false;
                }
            }
        };
    }
    $scope.GenerateReport();
    $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;
        $scope.Company = null;
    };
}]);