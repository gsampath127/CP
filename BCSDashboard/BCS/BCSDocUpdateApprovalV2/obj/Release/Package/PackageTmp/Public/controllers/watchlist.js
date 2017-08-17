app.controller('WatchlistReportCtrl', ['$rootScope', '$scope', '$state','$http', 'API_ENDPOINT', function ($rootScope, $scope, $state,$http, API_ENDPOINT) {
    ///
    var initialLoad = false;
    TriggerPopOver($('#dvWatchlistReportPopOver').html(), 'Watchlist Reports'); // Popover for Info
    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            initialLoad = true;
            $scope.showGrid = true;
            $scope.gridOptions = {
                columns: [
                            
                    {
                        "title": "Watchlist File Name", "width": "200px", "field": "FileName",
                        template: '#=DownloadFile(FileName,DirectoryName)#'
                    },

                    {
                        "title": "WatchList Received Date/Time", "width": "200px", "field": "ReceivedTime",
                        template: "#= kendo.toString(new Date(ReceivedTime), 'MM-dd-yyyy HH:mm:ss') #"
                    }
            
                ],
                scrollable: false,
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
              
            };

            $scope.gridDataSource = {
                transport: {
                    read: function (e) {
                        var url = API_ENDPOINT.url + '/WATCHLIST/Report';
                        var data = {
                            watchlistDate: $scope.WatchListDate,
                            clientName: $rootScope.CurrentUser.SelectedClient,
                            options: e.data

                        }

                        $http.post(url, data).then(function (output) {
                          
                            e.success(output.data);
                        });
                    }
                  
                },
               
                requestStart: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#watchlistGrid"), true);
                    } else {
                        kendo.ui.progress($("#watchlistGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#watchlistGrid"), false);
                        initialLoad = false;
                    }
                }
            };
        }

    };
    $scope.btnClearWatchlist = function () {
        $scope.watchlistForm.submitted = false;
        $scope.WatchListDate = undefined;
        $scope.showGrid = false;
    };
    $scope.Validate = function () {
        var isSuccess = true;
        $scope.WatchlistReportValidations = '';
        $scope.WatchlistReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.WatchlistReportValidations += "<ul>"


        if (!$scope.WatchListDate) {
            $scope.WatchlistReportValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }

        $scope.WatchlistReportValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.WatchlistReportValidations);
        }
        return isSuccess;


    };
    
}]);
