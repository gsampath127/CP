app.controller('CUSIPReportCtrl', ['$rootScope', '$scope', '$state', '$http', 'CUSIPReportService', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, CUSIPReportService, API_ENDPOINT) {
    ///
    var initialLoad = true;
    TriggerPopOver($('#dvCUSIPReportPopOver').html(),'CUSIP Reports'); // Popover for Info
    CUSIPReportService.GetReportTypes().then(function (output) {
        $scope.ReportTypeOptions = {
                placeholder: "Select Report Type",
                autobind: true,
                dataSource: {
                    data: output.data
                 }
        };
    });
    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.DisplayGrid = true;
            $scope.gridOptions = {
                columns: [
                    { "title": "CUSIP_WL", "field": "CUSIP_WL" },
                    { "title": "CUSIP_RP", "field": "CUSIP_RP", "hidden": $scope.HideByReportType('CUSIP_RP') },
                    { "title": "FundName_WL", "field": "FundName_WL", "width": "200px" },

                    { "title": "FundName_RP", "field": "FundName_RP", "width": "200px", "hidden": $scope.HideByReportType('FundName_RP') },
                    { "title": "Class_WL", "field": "Class_WL" },
                    { "title": "Class_RP", "field": "Class_RP", "hidden": $scope.HideByReportType('Class_RP') },
                    {
                        "title": "DateModified", "field": "DateModified",
                        template: "#= kendo.toString(kendo.parseDate(DateModified, 'yyyy-MM-dd'), 'MM/dd/yyyy') #",
                        "hidden": $scope.HideByReportType('DateModified')
                    },
                    { "title": "Status", "field": "Status", "hidden": $scope.HideByReportType('Status') }
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
                        var url = API_ENDPOINT.url + '/CUSIP/Report';
                        var data = {
                            startDate: $scope.StartDate,
                            endDate: $scope.EndDate,
                            reportType: $scope.SelectedReport,
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
                        kendo.ui.progress($("#cusipGrid"), true);
                    } else {
                        kendo.ui.progress($("#cusipGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#cusipGrid"), false);
                        initialLoad = false;
                    }
                }
            };

        }
    };


    $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;
        $scope.SelectedReport = null;
        $scope.StartDate = null;
        $scope.EndDate = null;
    };


    $scope.HideByReportType = function (column) {
        switch ($scope.SelectedReport) {
            case "Added Report":
                if (column == "Status")
                    return true;
                break;
            case "Removed Report":
                if (column == "Status")
                    return true;

                break;
            case "CUSIP Not Present in RP":
                if (column == "Status" || column == "DateModified" || column == "Class_RP" || column == "CUSIP_RP" || column == "FundName_RP")
                    return true;
                break;
            default:
                return false;
        }
    }



    //Validate
    $scope.Validate = function () {
            var isSuccess = true;
      $scope.CUSIPReportValidations='';
          $scope.CUSIPReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
          $scope.CUSIPReportValidations += "<ul>"

    
    if (!$scope.StartDate) {
        $scope.CUSIPReportValidations += "<li class='message'>Start Date</li>";
        isSuccess = false;
     }

    if (!$scope.EndDate) {
        $scope.CUSIPReportValidations += "<li class='message'>End Date</li>";
        isSuccess = false;
    }
    if (!$scope.SelectedReport) {

        $scope.CUSIPReportValidations += "<li class='message'>Report Type</li>";
        isSuccess = false;
    }
   
    $scope.CUSIPReportValidations += "</ul>"

    if (!isSuccess) {
       KendoValidationWindow($scope.CUSIPReportValidations);
    }
    return isSuccess;
    

    };

}]);





                
