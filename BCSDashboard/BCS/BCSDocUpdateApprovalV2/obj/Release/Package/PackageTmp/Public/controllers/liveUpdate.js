app.controller('LiveUpdateReportCtrl', ['$rootScope', '$scope', '$state', 'LiveUpdateService', 'API_ENDPOINT', '$http', function ($rootScope, $scope, $state, LiveUpdateService, API_ENDPOINT, $http) {
    var initialLoad = false;
    TriggerPopOver($('#dvLiveUpdateReportPopOver').html(), 'Live Update Reports'); // Popover for Info
    $scope.liveUpdate = {};
    $scope.liveUpdate.clientName = $rootScope.CurrentUser.SelectedClient;
    $scope.liveUpdate.hdnDateOffSet = new Date().getTimezoneOffset();
    $scope.StatusData = {};



    LiveUpdateService.ReportStatus().then(function (output) {
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


    $scope.GenerateLiveUpdateReport = function () {

        $scope.Show_divCUSIP = false;
        $scope.ShowGrid_GridLiveUpdateCUSIPDetails = false;
        $scope.ShowGrid_GridLiveUpdateInvalidCUSIPs = false;

        $scope.Show_divDocId = false;
        $scope.Show_divDocIdsCount = false;
        $scope.ShowGrid_GridLiveUpdateDocIdDetails = false;
        $scope.ShowGrid_GridLiveUpdateInvalidDocIds = false;

        $scope.showGridLiveUpdate = false;


        //------------------------  GRID OPTIONS  -------------------------
        $scope.gridOptions_GridLiveUpdate = {
            columns: [
                { "title": "CUSIP", "field": "CUSIP" },
                { "title": "Edgar ID", "field": "EdgarID" },
                { "title": "Acc#", "field": "Accnumber" },
                { "title": "Fund Name", "field": "FundName" },
                { "title": "Document Type", "field": "DocumentType" },
                { "title": "Document Date", "field": "DocumentDate" },
                { "title": "Document ID", "field": "DocumentID", "width": "13%" },
                { "title": "Status", "field": "Status" },
                { "title": "Status Date", "field": "StatusDate" }
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
        $scope.gridOptions_GridLiveUpdateCUSIPDetails = {
            columns: [
               { "title": "Total CUSIPs", "field": "TotalCUSIPs" },
               { "title": "CUSIPs Found", "field": "CUSIPsFound" },
               { "title": "Missing CUSIPs", "field": "MissingCUSIPs" }
            ]
        };
        $scope.gridOptions_GridLiveUpdateInvalidCUSIPs = {
            columns: [
               { "title": "CUSIP", "field": "CUSIP" }
            ],
            noRecords: {
                template: "<b>No Records Found<b> "
            }
        };

        $scope.gridOptions_GridLiveUpdateDocIdDetails = {
            columns: [
                { "title": "Total DocIds", "field": "TotalDocIds" },
               { "title": "DocIds Found", "field": "DocIdsFound" },
               { "title": "Missing DocIds", "field": "MissingDocIds" }
            ]
        };

        $scope.gridOptions_GridLiveUpdateInvalidDocIds = {
            columns: [
               { "title": "Doc Id", "field": "DocId" }
            ],
            noRecords: {
                template: "<b>No Records Found<b> "
            }
        };

        //  -------------------------------  GRID OPTIONS  ----------------------

        $scope.gridDataSource_GridLiveUpdate = {
            transport: {
                read: function (e) {
                    var url = API_ENDPOINT.url + '/LiveUpdate/Report';
                    var data = {
                        liveUpdate: $scope.liveUpdate,
                        options: e.data
                    };
                    $http.post(url, data).then(function (output) {
                        $scope.showGridLiveUpdate = true;


                        // Show/Hide Div
                        if (output.data.LiveUpdateData.Show_divGridLiveUpdateInvalidCUSIPs == true) {
                            $scope.Show_divCUSIP = true;
                            $scope.ShowGrid_GridLiveUpdateInvalidCUSIPs = true;
                            $scope.gridDataSource_GridLiveUpdateInvalidCUSIPs = output.data.LiveUpdateData.lstInvalidCusips;
                        }
                        if (output.data.LiveUpdateData.Show_divCUSIPCount == true) {
                            $scope.Show_divCUSIP = true;
                            $scope.ShowGrid_GridLiveUpdateCUSIPDetails = true;
                            $scope.gridDataSource_GridLiveUpdateCUSIPDetails = output.data.LiveUpdateData.LiveUpdateCUSIPDetails;
                        }
                        if (output.data.LiveUpdateData.Show_divGridLiveUpdateInvalidDocIds == true) {
                            $scope.Show_divDocId = true;
                            $scope.Show_divGridLiveUpdateInvalidDocIds = true;
                            $scope.ShowGrid_GridLiveUpdateInvalidDocIds = true;
                            $scope.gridDataSource_GridLiveUpdateInvalidDocIds = output.data.LiveUpdateData.lstInvalidDocumentIds;
                        }
                        if (output.data.LiveUpdateData.Show_divDocIdsCount == true) {
                            $scope.Show_divDocId = true;
                            $scope.Show_divDocIdsCount = true;
                            $scope.showGrid_GridLiveUpdateDocIdDetails = true;
                            $scope.gridDataSource_GridLiveUpdateDocIdDetails = output.data.LiveUpdateData.LiveUpdateDocumentIdDetails;
                        }
                        e.success(output.data);
                    });
                }
            },
            pageSize: 10,
            serverPaging: true,
            serverSorting: false,
            schema: {
                data: "data",
                total: "total"
            },
            requestStart: function () {
                if (initialLoad) {
                    kendo.ui.progress($("#GridLiveUpdate"), true);
                } else {
                    kendo.ui.progress($("#GridLiveUpdate"), false);
                }
            },
            requestEnd: function () {
                if (initialLoad) {
                    kendo.ui.progress($("#GridLiveUpdate"), false);
                    initialLoad = false;
                }
            }
        };
    };

    $scope.ClearFilters = function () {
        $scope.liveUpdate.CUSIP = null;
        $scope.liveUpdate.Accnt = null;
        $scope.liveUpdate.DocId = null;
        $scope.liveUpdate.Status = null;

        $scope.Show_divCUSIP = false;
        $scope.Show_divDocId = false;
        $scope.showGridLiveUpdate = false;
    };

}]);