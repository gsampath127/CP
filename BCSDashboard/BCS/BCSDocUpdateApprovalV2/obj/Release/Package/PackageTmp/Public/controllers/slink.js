app.controller('SlinkReportCtrl', ['$rootScope', '$scope', '$state', 'SLINKService', 'API_ENDPOINT', '$http', function ($rootScope, $scope, $state, SLINKService, API_ENDPOINT, $http) {
    var initialLoad = false;
    TriggerPopOver($('#dvSlinkReportPopOver').html(), 'SLINK Reports'); // Popover for Info
    $scope.slinkData = {};
    //$scope.GridCount = {};
    $scope.slinkData.clientName = $rootScope.CurrentUser.SelectedClient;
    $scope.StatusData = {};
    SLINKService.ReportStatus().then(function (output) {
        $scope.StatusData = output.data;

        $scope.ReportStatusOptions = {
            placeholder: "Select Report Status",
            autobind: true,
            dataSource: {
                data: output.data
            }
        };

    }, function (res) {

    });

   
    $scope.slinkData.hdnDateOffSet = new Date().getTimezoneOffset();

    $scope.GenerateSLINKReport = function () {
     
            initialLoad = true;
            $scope.gridDataSourceHeader = {};
            $scope.showGrid = true;
            $scope.gridOptionsHeader = {
                columns: [
                    { "title": "Total Count", "field": "TotalCount" },
                    { "title": "EX Count", "field": "ExCount" },
                    { "title": "AP Count", "field": "APCount" },
                    { "title": "OP Count", "field": "OPCount" },
                    { "title": "APC Count", "field": "APCCount" },
                    { "title": "OPC Count", "field": "OPCCount" }
                ]
            };
            $scope.gridOptionsDetails = {
                columns: [
                    { "title": "SLINK File Name", "field": "SLINKFileName" },
                    {
                        "title": "ZIP File Name", "field": "ZipFileName",
                        template: '#=DownloadFile(ZipFileName,ZipFilePath)#'
                    },
                    { "title": "Status", "field": "Status" },
                    { "title": "Status Date", "field": "ReceivedDate" }
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
            //SLINKService.GenerateSLINKReport($scope.rpt).then(function (output) {
            //    $scope.gridDataSourceHeader = output.data.HeaderData;
            //  $scope.gridDataSourceDetails = output.data.DetailData;

            $scope.gridDataSourceDetails = {
                transport: {
                    read: function (e) {
                        var url = API_ENDPOINT.url + '/SLINK/Report';
                        var data = {
                            slinkData: $scope.slinkData,
                            options: e.data
                        };
                        $http.post(url, data).then(function (output) {
                            $scope.gridDataSourceHeader = output.data.HeaderData;
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
                        kendo.ui.progress($("#gridSLINKDetail"), true);
                    } else {
                        kendo.ui.progress($("#gridSLINKDetail"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#gridSLINKDetail"), false);
                        initialLoad = false;
                    }
                }
            };
           
        
    };

    $scope.ClearFilters = function () {
        $scope.slinkData.reportDate = undefined;
        $scope.slinkData.Status = null;
        $scope.slinkData.DocId = null;
        $scope.showGrid = false;
        $scope.ShowDateRequired = false;
    };

    
}]);