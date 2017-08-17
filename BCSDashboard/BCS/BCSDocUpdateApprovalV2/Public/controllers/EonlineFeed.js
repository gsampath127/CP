app.controller('EonlineFeedReportCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, API_ENDPOINT) {
    ///
    var initialLoad = false;
    TriggerPopOver($('#dvEonlineReportPopOver').html(), 'EonlineFeed Reports'); // Popover for Info
    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            initialLoad = true;
            $scope.showGrid = true;
            $scope.gridOptions = {
                columns: [                    
                     {
                         "title": "Edgar Online File Name", "width": "200px", "field": "FileName",
                         template: '#=DownloadFile(FileName,DirectoryName)#'
                     },                  
                     {
                         "title": "Received Date/Time", "width": "200px", "field": "DateReceived",
                         template: "#= kendo.toString(new Date(DateReceived), 'MM-dd-yyyy HH:mm:ss') #"
                     }
                   
                ],            
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
            };
            $scope.gridDataSource = {
                transport: {
                    read: function (e) {
                        var url = API_ENDPOINT.url + '/EdgarOnline/Report';
                        var data = {
                            EonlineDate: $scope.EonlineDate,
                            options: e.data

                        };

                        $http.post(url, data).then(function (output) {

                            e.success(output.data);
                        });
                    }

                },
                schema: {
                    data: "data"
                   
                },
                requestStart: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#EonlineFeedGrid"), true);
                    } else {
                        kendo.ui.progress($("#EonlineFeedGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#EonlineFeedGrid"), false);
                        initialLoad = false;
                    }
                }
            };
        }

    };
    $scope.ClearFilter = function () {
        $scope.showGrid = false;
        $scope.EonlineDate = null;
    };
    $scope.Validate = function () {
        var isSuccess = true;
        $scope.EonlineFeedReportValidations = '';
        $scope.EonlineFeedReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.EonlineFeedReportValidations += "<ul>"


        if (!$scope.EonlineDate) {
            $scope.EonlineFeedReportValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }

        $scope.EonlineFeedReportValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.EonlineFeedReportValidations);
        }
        return isSuccess;


    };

}]);
