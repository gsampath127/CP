app.controller('MissingReportCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', '$uibModal', 'ReportService', function ($rootScope, $scope, $state, $http, API_ENDPOINT, $uibModal, ReportService) {
    TriggerPopOver($('#dvMissingReportPopOver').html(), 'Missing Report'); // Popover for Info
    $scope.showGrid = false;
    var typesOfReport = [ { Value: "0", Text: "CUSIP" }, { Value: "1", Text: "Security Types" }];
    $scope.SelectedMissingReport = null;

    $scope.comboMissingReportOptions = {
        placeholder: "Select Report Type",
        autobind: true,
        dataValueField: 'Text',
        dataTextField: 'Text',
        dataSource: {
            data: typesOfReport
        }
    };
       
    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.showGrid = true;
            $scope.gridOptions = {
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


            if ($scope.SelectedMissingReport == 'CUSIP')
                $scope.gridOptions.columns = CusipColumns;
            else if ($scope.SelectedMissingReport == 'Security Types')
                $scope.gridOptions.columns = SecurityColumns;

            $scope.gridDataSource = {
                transport: {

                    read: function (e) {
                        var url = API_ENDPOINT.url + '/SecurityTypes/Report';
                        var data = {
                            reportType: $scope.SelectedMissingReport,
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
                        kendo.ui.progress($("#missingReportGrid"), true);
                    } else {
                        kendo.ui.progress($("#missingReportGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#missingReportGrid"), false);
                        initialLoad = false;
                    }
                }
            };
        }
    };

    $scope.OpenAddWindow = function (CUSIP) {
        var action = "";
        
        var data = {  cusip: CUSIP };

        $scope.parentData = data;
        var modalInstanceAdd = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'Public/views/login/addEditMissingReport.html',
            controller: 'EditMissingReportCtrl',
            backdrop: 'static',
            keayboard: 'false',
           
            width: 400,
            height:300,
            scope: $scope
        });
        modalInstanceAdd.result.then(function (resultTaskObject) {
            debugger;
            $scope.GenerateReport();
        });
       
    };

    $scope.Validate = function () {
        var isSuccess = true;
        $scope.MissingReportValidations = '';
        $scope.MissingReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.MissingReportValidations += "<ul>"


        if (!$scope.SelectedMissingReport) {
            $scope.MissingReportValidations += "<li class='message'>Select Report</li>";
            isSuccess = false;
        }

        $scope.MissingReportValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.MissingReportValidations);
        }
        return isSuccess;
    };

    $scope.btnClearMissingReport = function () {
        $scope.showGrid = false;
        $scope.SelectedMissingReport = null;

    };

    var CusipColumns = [
                       { "title": "CUSIP", "field": "CUSIP", "width": "100px" },
                       { "title": "CompanyName", "field": "CompanyName", "width": "150px" },
                       { "title": "FundName", "field": "FundName", "width": "300px" },
                       { "title": "CIK", "field": "CIK", "width": "100px" },
                       { "title": "SeriesID", "field": "SeriesID", "width": "100px" },
                       { "title": "ClassContractID", "field": "ClassContractID", "width": "100px" },
                       { "title": "Ticker", "field": "Ticker", "width": "70px" },
                       {
                           "title": "Action", "field": "ADD", "width": "60px",
                           template: function () {
                               return "<a href='http://rightprospectus-stage.rightprospectus.com/' target='_blank'>LINK</a>"
                           }
                       }
    ];
    var SecurityColumns = [
                      { "title": "CUSIP", "field": "CUSIP", "width": "100px" },
                      { "title": "CompanyName", "field": "CompanyName", "width": "150px" },
                      { "title": "FundName", "field": "FundName", "width": "300px" },
                      { "title": "CIK", "field": "CIK", "width": "100px" },
                      { "title": "SeriesID", "field": "SeriesID", "width": "100px" },
                      { "title": "ClassContractID", "field": "ClassContractID", "width": "100px" },
                      { "title": "Ticker", "field": "Ticker", "width": "70px" },
                      { "title": "Edit", "field": "ADD", "width": "60px", template: "<a   data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' ng-click=\'OpenAddWindow(dataItem.CUSIP)\'  ></span></a>" }

    ];

}]);
