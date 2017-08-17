app.controller('EdgarOnlineDataCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, API_ENDPOINT) {

    var initialLoad = true;
    TriggerPopOver($('#dvEdgarOnlineDataGridPopOver').html(), 'Edgar Online Data');
    $scope.GenerateReport = function () {       
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.DisplayGrid = true;
            $scope.gridOptions = {
                columns: [
                    { "title": "eoCUSIP", "field": "ECUSIP" },
                    { "title": "eoCompanyName", "field": "ECompanyName" },
                    { "title": "eoFundName", "field": "EFundName" },
                    { "title": "eoCIK", "field": "ECIK" },
                    { "title": "eoSeries#", "field": "ESeriesID" },
                    { "title": "eoClass#", "field": "EClassContractID" },
                    { "title": "eoTicker", "field": "ETicker" }
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
                        var url = API_ENDPOINT.url + '/EdgarOnlineData/Data';
                        var data = {
                            eoCUSIP:$scope.eoCUSIP,
                            eoCIK: $scope.eoCIK,
                            eoSeries: $scope.eoSeries, 
                            eoClass: $scope.eoClass, 
                            eoTicker: $scope.eoTicker,                             
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
                        kendo.ui.progress($("#EdgarOnlineDataGrid"), true);
                    } else {
                        kendo.ui.progress($("#EdgarOnlineDataGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#EdgarOnlineDataGrid"), false);
                        initialLoad = false;
                    }
                }
            };        
    };
        $scope.GenerateReport();
        $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;
        $scope.eoCUSIP = '';
        $scope.eoCIK = '';
        $scope.eoSeries = '';
        $scope.eoClass = '';
        $scope.eoTicker = '';
    };
}]);