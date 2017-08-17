app.controller('CustomerDocUpdateReportCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', 'ReportService', function ($rootScope, $scope, $state, $http, API_ENDPOINT, ReportService) {
    var initialLoad = false;
    TriggerPopOver($('#dvCustomerDocUpdateReportPopOver').html(), 'Customer Doc Update Reports'); // Popover for Info
    $scope.Dropdwnvalues = {};
    $scope.showCombo = false;
    $scope.showGridHeader = false;
    $scope.showGridGms = false;
    $scope.showGrid = false;
    if ($rootScope.CurrentUser.SelectedClient == "Transamerica") {
        $scope.showCombo = false;
       
    }
    else if ($rootScope.CurrentUser.SelectedClient == "GMS") {
        $scope.showCombo = true;
        

        ReportService.CDUReportStatus().then(function (output) {
            $scope.CustomerDocUpdateOptions = {
                placeholder: "Select File",
                autobind: true,
                dataSource: {
                    data: output.data
                }
            }

        }, function (res) {

        });


    }
    else if ($rootScope.CurrentUser.SelectedClient == "AllianceBernstein") {
        $scope.showCombo = false;
        
    }
    $scope.GenerateReport = function () {
        if ($scope.ValidateIPNU() && $scope.showCombo == true) {
            initialLoad = true;
            $scope.showGridHeader = true;
            $scope.showGridGms = true;
            $scope.showGrid = false;
            $scope.gridOptionsHeaderGms = {
                columns: [
                            {
                                "title": "Record Type", "width": "200px", "field": "HeaderRecordType"
                            },
                            {
                                "title": "Data Type", "width": "200px", "field": "HeaderDataType"
                            },
                            {
                                "title": "System", "width": "200px", "field": "HeaderSystem"
                            },
                            {
                                "title": "File Name", "width": "200px", "field": "HeaderFileName"
                            },
                            {
                                "title": "Date/Time", "width": "200px", "field": "HeaderDateTime"
                            },
                            {
                                "title": "Total Record Count", "width": "200px", "field": "HeaderTotalRecordCount"
                            },
                            {
                                "title": "FL Record Count", "width": "200px", "field": "HeaderFLRecordCount"
                            },
                            {
                                "title": "EX Record Count", "width": "200px", "field": "HeaderEXRecordCount"
                            },
                            {
                                "title": "AP Record Count", "width": "200px", "field": "HeaderAPRecordCount"
                            },
                            {
                                "title": "OP Record Count", "width": "200px", "field": "HeaderOPRecordCount"
                            },
                            {
                                "title": "NU Record Count", "width": "200px", "field": "HeaderAPCRecordCount"
                            }

                ],
                scrollable: true,
                noRecords: {
                    template: "<b>No Records Found<b> "
                }

            };
            $scope.gridOptionsDetails = {
                columns: [
                            
                    {
                        "title": "Customer - Doc Update(NU/IP) File", "width": "200px", "field": "FileName",
                        template: '#=DownloadFile(FileName,DirectoryName)#'
                    },
                    {
                        "title": "File Sent Downstream", "width": "200px", "field": "ReceivedTime",
                        template: "#= kendo.toString(new Date(ReceivedTime), 'MM-dd-yyyy HH:mm:ss') #"

                    }
                ],
                scrollable: false,
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
            };
            $scope.report = {};
            $scope.report.reportDate = $scope.CustomerDocUpdateDate;
            $scope.report.clientName = $rootScope.CurrentUser.SelectedClient,
            $scope.report.SelectedValue = $scope.CustomerDocUpdate;
            ReportService.CDUReportData($scope.report).then(function (output) {
                $scope.showGrid = true;
                $scope.gridDataSourceHeaderGms = output.data.HeaderData;
                $scope.gridDataSourceDetails = output.data.CustDocUPDTDetails;
            }, function (res) {
            });
        }

        else if ($scope.Validate() && $scope.showCombo == false) {

            initialLoad = true;
            $scope.showGridHeader = false;
            $scope.showGridGms = false;
            $scope.showGrid = true;
            $scope.gridOptions = {
                columns: [                            
                            {
                                "title": "Customer - Doc Update File Name", "width": "200px", "field": "FileName",
                                template: '#=DownloadFile(FileName,DirectoryName)#'
                            },
                            {
                                "title": "File Sent To TA_ Time", "width": "200px", "field": "ReceivedTime",
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
                        var url = API_ENDPOINT.url + '/CUSTOMERDOCUPDATE/Report';
                        var data = {

                            options: e.data,
                            customerDocUpdateDate: $scope.CustomerDocUpdateDate,
                            clientName: $rootScope.CurrentUser.SelectedClient,

                        }

                        $http.post(url, data).then(function (output) {

                            e.success(output.data.CustDocUPDTDetails);
                        });
                    }

                },               
                requestStart: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#customerDocUpdateGrid"), true);
                    } else {
                        kendo.ui.progress($("#customerDocUpdateGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#customerDocUpdateGrid"), false);
                        initialLoad = false;
                    }
                }
            };
        }

    };
    $scope.btnClearCustomerDocUpdate = function () {
        $scope.customerDocUpdateForm.submitted = false;        
        $scope.CustomerDocUpdateDate = undefined;
        $scope.showGridHeader = false;
        $scope.showGridGms = false;
        $scope.showGrid = false;
    };
    //Validate
    $scope.Validate = function () {
        if ($scope.showCombo == false) {
            var isSuccess = true;
            $scope.CustomerDocUpdateReportValidations = '';
            $scope.CustomerDocUpdateReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
            $scope.CustomerDocUpdateReportValidations += "<ul>"


            if (!$scope.CustomerDocUpdateDate) {
                $scope.CustomerDocUpdateReportValidations += "<li class='message'>Select Date</li>";
                isSuccess = false;
            }
            $scope.CustomerDocUpdateReportValidations += "</ul>"

            if (!isSuccess) {
                KendoValidationWindow($scope.CustomerDocUpdateReportValidations);
            }
            return isSuccess;
        }
        else {
            isSuccess = false;
        }

    };
    //Validate
    $scope.ValidateIPNU = function () {
        if ($scope.showCombo == true) {
            var isSuccess = true;
            $scope.CustomerDocUpdateIPNUReportValidations = '';
            $scope.CustomerDocUpdateIPNUReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
            $scope.CustomerDocUpdateIPNUReportValidations += "<ul>"


            if (!$scope.CustomerDocUpdateDate) {
                $scope.CustomerDocUpdateIPNUReportValidations += "<li class='message'>Select Date</li>";
                isSuccess = false;
            }

            if (!$scope.CustomerDocUpdate) {
                $scope.CustomerDocUpdateIPNUReportValidations += "<li class='message'>Select Customer Doc Update</li>";
                isSuccess = false;
            }


            $scope.CustomerDocUpdateIPNUReportValidations += "</ul>"

            if (!isSuccess) {
                KendoValidationWindow($scope.CustomerDocUpdateIPNUReportValidations);
            }
            return isSuccess;
        }
        else
            isSuccess = false;

    };
}]);

