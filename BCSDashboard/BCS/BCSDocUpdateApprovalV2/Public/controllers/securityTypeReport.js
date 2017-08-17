app.controller('SecurityTypeReportCtrl', ['$rootScope', '$scope', '$state', 'SecurityTypesReportService','$http', 'API_ENDPOINT', function ($rootScope, $scope, $state,SecurityTypesReportService, $http, API_ENDPOINT) {

    var initialLoad = true;
    var summaryInitialLoad = true;
    TriggerPopOver($('#dvSecurityTypeReportPopOver').html(), 'Security Type Reports');
    SecurityTypesReportService.GetCompanyandSecurityType().then(function (output) {
     
        var company =  output.data.CompanyDetails ; var securtity = output.data.SecurityTypeDetails;
            
        $scope.CompanyOptions = {
            placeholder: "Select Company",
            autobind: true,
            dataSource: company,
            dataTextField: "CompanyName",
            dataValueField:"CompanyId"
               

        };
        $scope.SecurityTypeOptions = {
            placeholder: "Select Security Type",
            autobind: true,
            dataSource: securtity,
            dataTextField: "SecurityTypeCode",
            dataValueField: "SecurityTypeID"

        };
       
   
    });
   
    $scope.GenerateReport = function () {
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.DisplayGrid = true;
            $scope.gridOptions = {
                columns: [
                    { "title": "CUSIP", "field": "CUSIP" },
                    { "title": "CompanyName", "field": "CompanyName" },
                    { "title": "FundName", "field": "FundName" },
                    { "title": "CompanyCIK", "field": "CompanyCIK" },
                    { "title": "Share Class", "field": "ShareClass" },
                    { "title": "Ticker", "field": "Ticker" },
                    { "title": "SecurityTypes", "field": "SecurityType"},
                    { "title": "LoadType", "field": "Loadtype" }
                    
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
                        var url = API_ENDPOINT.url + '/SecurityTypeReport/SecurityReport';
                        var data = {
                          CUSIP:$scope.CUSIP,
                          Company:$scope.Company,
                          SecurityType: $scope.SecurityType,
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
                        kendo.ui.progress($("#SecurityTypeGrid"), true);
                    } else {
                        kendo.ui.progress($("#SecurityTypeGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#SecurityTypeGrid"), false);
                        initialLoad = false;
                    }
                }
            };
            
            
    }
    $scope.loadSummary = function () {
        $scope.sgridDataSource = {};
        $scope.sgridOptions = {};
        $scope.sDisplayGrid = true;
        $scope.sgridOptions = {
            columns: [
                { "title": "LoadType", "field": "LoadType" },
                { "title": "ETF", "field": "ETF" },
                { "title": "ETN", "field": "ETN" },
                { "title": "MF", "field": "MF" },
                { "title": "NA", "field": "NA" },
                { "title": "UIT", "field": "UIT" }

            ],
            noRecords: {
                template: "<b>No Records Found<b> "
            }
        };
        $scope.sgridDataSource = {
            transport: {
                read: function (e) {
                    var url = API_ENDPOINT.url + '/SecurityTypeReport/SummaryData';
                    $http.post(url).then(function (output) {
                        e.success(output.data);
                    });
                }
            },

            schema: {
                data: "data"

            },
            requestStart: function () {
                if (summaryInitialLoad) {
                    kendo.ui.progress($("#GridSummarizedData"), true);
                } else {
                    kendo.ui.progress($("#GridSummarizedData"), false);
                }
            },
            requestEnd: function () {
                if (summaryInitialLoad) {
                    kendo.ui.progress($("#GridSummarizedData"), false);
                    summaryInitialLoad = false;
                }
            }
        };

    }
    $scope.loadSummary();
    $scope.GenerateReport();
    $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;
        $scope.CUSIP = null;
        $scope.Company = null;
        $scope.SecurityType = null;
       };
}]);