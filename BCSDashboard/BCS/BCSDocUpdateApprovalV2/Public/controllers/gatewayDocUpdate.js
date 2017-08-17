app.controller('GatewayDocUpdateReportCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT','ReportService', function ($rootScope, $scope, $state, $http, API_ENDPOINT,ReportService) {
    TriggerPopOver($('#dvGatewayDocUpdateReportPopOver').html(), 'Gateway Doc Update Reports'); // Popover for Info
    var initialLoad = false;
    $scope.Dropdwnvalues = {};
    $scope.showGrid = false;


    ReportService.GDUReportStatus().then(function (output) {
        $scope.GatewayDocUpdateOptions = {
            placeholder: "Select File",
            autobind: true,
            dataSource: {
                data: output.data
            }
        }
        }, function (res) {
           
        });


    $scope.GenerateReport = function () {
        if ($scope.Validate()) {
            $scope.showGrid = true;
            $scope.gridOptionsHeader = {
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
                
                noRecords: {
                    template: "<b>No Records Found<b> "
                }

            };
            $scope.gridOptionsDetails = {
                columns: [
                            
                    {
                        "title": "Gateway - Doc Update(NU/IP) File", "width": "200px", "field": "FileName",
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
            $scope.report.reportDate = $scope.GatewayDocUpdateDate;
            $scope.report.ClientName = $rootScope.CurrentUser.SelectedClient,
            $scope.report.SelectedValue = $scope.GatewayDocUpdate;
            ReportService.GDUReportData($scope.report).then(function (output) {
                $scope.showGrid = true;
                $scope.gridDataSourceHeader = output.data.HeaderData;
                $scope.gridDataSourceDetails = output.data.GatewayDocUPDTDetails;
            }, function (res) {
            });
        }
    };
    $scope.btnClearGateway = function () {
        $scope.gatewayDocUpdateForm.submitted = false;
        $scope.GatewayDocUpdateDate = undefined;
        $scope.showGrid = false;
    };

    //Validate
    $scope.Validate = function () {
        var isSuccess = true;
        $scope.GatewayReportValidations = '';
        $scope.GatewayReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.GatewayReportValidations += "<ul>"


        if (!$scope.GatewayDocUpdate) {
            $scope.GatewayReportValidations += "<li class='message'>Gateway Doc Update File</li>";
            isSuccess = false;
        }

        if (!$scope.GatewayDocUpdateDate) {
            $scope.GatewayReportValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }
       
        $scope.GatewayReportValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.GatewayReportValidations);
        }
        return isSuccess;
    };

}]);
