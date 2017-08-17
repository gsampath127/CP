app.controller('TRPMissingRPCusipCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, API_ENDPOINT) {
    var initialLoad = true;
    TriggerPopOver($('#dvMissingRPCusipPopOver').html(), 'Missing - CUSIP(s) in RP'); // Popover for Info

    $scope.GenerateReport = function () {

        initialLoad = true;
        $scope.gridDataSource = {};
        $scope.gridOptions = {};
        $scope.DisplayGrid = true;
        $scope.gridOptions = {
            columns: [
                { "title": "Company Name", "field": "CompanyName" },
                {
                    "title": "Fund Name", "field": "FundName"
                },
                { "title": "FLT CUSIP", "field": "FLTCUSIP" },
                
                {
                    "title": "LIPPER CUSIP", "field": "LIPPERCUSIP",

                    template: function (dataItem) {
                      
                        if (dataItem.LIPPERCUSIP)
                            return dataItem.LIPPERCUSIP;
                        else
                            return "Not Found";
                    }

                }

            ],
            pageable: {
                refresh: false,
                pageSize: 5,
                pageSizes: [5, 10, 15]

            },

            sortable: false,
            noRecords: {
                template: "<b>There are no RP Missing CUSIP entries.<b> "
            }
        };

        $scope.gridDataSource = {
            transport: {

                read: function (e) {
                    var url = API_ENDPOINT.url + '/TRP/MissingRPCusip';
                    var data = {

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
                    kendo.ui.progress($("#missingRPCusipGrid"), true);
                } else {
                    kendo.ui.progress($("#missingRPCusipGrid"), false);
                }
            },
            requestEnd: function () {
                if (initialLoad) {
                    kendo.ui.progress($("#missingRPCusipGrid"), false);
                    initialLoad = false;
                }
            }
        };


    };

    $scope.GenerateReport();// Bind Grid on Load


}]);

