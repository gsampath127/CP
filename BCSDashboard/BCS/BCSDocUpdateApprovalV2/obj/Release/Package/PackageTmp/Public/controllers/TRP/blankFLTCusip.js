app.controller('TRPBlankFLTCUSIPCtrl', ['$rootScope', '$scope', '$state', '$http', 'TRPReportService', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, TRPReportService, API_ENDPOINT) {
    var initialLoad = true;
    TriggerPopOver($('#dvBlankFLTCUSIPPopOver').html(), 'Blank FLT CUSIP Details'); // Popover for Info
    $scope.gridDataSource = {};
    $scope.gridOptions = {};
    $scope.DisplayGrid = true;
    $scope.gridOptions = {
        columns: [
            { "title": "FUND CODE", "field": "FUNDCODE" },
            { "title": "FUND NAME", "field": "FUNDNAME" },
            { "title": "FUND TYPE", "field": "FUNDTYPE" },
            { "title": "FUND TELE ACCESS CODE", "field": "FUNDTELEACCESSCODE" },
            { "title": "FUND CUSIP NUMBER", "field": "FUNDCUSIPNUMBER" },
            { "title": "FUND CHK HEADING CODE", "field": "FUNDCHKHEADINGCODE" },
            { "title": "FUND GROUP NUMBER", "field": "FUNDGROUPNUMBER" },
            { "title": "FUND PROSPECTUS INSERT", "field": "FUNDPROSPECTUSINSERT" },
            { "title": "FUND PROSPECTUS INSERT2", "field": "FUNDPROSPECTUSINSERT2" },
            { "title": "FUND TICKER SYMBOL", "field": "FUNDTICKERSYMBOL" },
            { "title": "FUND DocName", "field": "FUNDDocName" },
            { "title": "Date FLT Record HasChanged", "field": "DateFLTRecordHasChanged" }
        ],
        pageable: {
            refresh: false,
            pageSize: 5,
            pageSizes: [5, 10, 15]

        },

        sortable: false,
        noRecords: {
            template: "<b>There are no FLT Missing entries.<b> "
        }
    };

    $scope.gridDataSource = {
        transport: {

            read: function (e) {
                var url = API_ENDPOINT.url + '/TRP/BlankFLTCUSIP';
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
                kendo.ui.progress($("#blankFLTCUSIPGrid"), true);
            } else {
                kendo.ui.progress($("#blankFLTCUSIPGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#blankFLTCUSIPGrid"), false);
                initialLoad = false;
            }
        }
    };

}]);