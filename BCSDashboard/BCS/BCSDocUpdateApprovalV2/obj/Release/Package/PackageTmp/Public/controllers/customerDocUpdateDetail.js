app.controller('CustomerDocUpdateDetailReportCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', 'ReportService', function ($rootScope, $scope, $state, $http, API_ENDPOINT, ReportService) {

    var initialLoad = false;
    TriggerPopOver($('#dvCustomerDocUpdateDetailReportPopOver').html(), 'Customer Doc Update Details Reports'); // Popover for Info
    $scope.Dropdwnvalues = {};
    $scope.showGrid = false;
    $scope.GenerateReport = function () {
        if ($scope.Validate()) {

            $scope.showGrid = true;
            $scope.gridOptionsHeader = {
                columns: [
                            {
                                "title": "Record Type", "width": "100px", "field": "RecordType"
                            },
                            {
                                "title": "Data Type", "width": "100px", "field": "DataType"
                            },
                            {
                                "title": "System", "width": "100px", "field": "System"
                            },
                            {
                                "title": "File Name", "width": "100px", "field": "FileName"
                            },
                            {
                                "title": "Date/Time", "width": "100px", "field": "DateTime"
                            },
                            {
                                "title": "Total Record Count", "width": "100px", "field": "TotalRecordCount"
                            },
                            {
                                "title": "FL Record Count", "width": "100px", "field": "FLRecordCount"
                            },
                            {
                                "title": "EX Record Count", "width": "100px", "field": "EXRecordCount"
                            },
                            {
                                "title": "AP Record Count", "width": "100px", "field": "ApRecordCount"
                            },
                            {
                                "title": "OP Record Count", "width": "100px", "field": "OPRecordCount"
                            },
                            {
                                "title": "APC Record Count", "width": "100px", "field": "APCRecordCount"
                            },
                            {
                                "title": "OPC Record Count", "width": "100px", "field": "OPCRecordCount"
                            },
                            {
                                "title": "Reserved", "width": "100px", "field": "Reserved"
                            }
                ],
                
                noRecords: {
                    template: "<b>No Records Found<b> "
                }

            };
            $scope.gridOptionsDetails = {
                columns: [
                          {
                               "title": "Record Type", "width": "100px", "field": "RecordType"
                           },
                            {
                                "title": "System", "width": "100px", "field": "System"
                            },
                            {
                                "title": "Client Id", "width": "100px", "field": "ClientId"
                            },
                            {
                                "title": "RP Process Step", "width": "100px", "field": "RPProcessStep"
                            },
                            {
                                "title": "CUSIP Id", "width": "100px", "field": "CusipId"
                            },
                            {
                                "title": "Fund Name", "width": "100px", "field": "FundName"
                            },
                            {
                                "title": "PDF Name", "width": "100px", "field": "PdfName"
                            },
                            {
                                "title": "RRD External DocId", "width": "100px", "field": "RrdExternalDocId"
                            },
                            {
                                "title": "Document Type", "width": "100px", "field": "DocumentType"
                            },
                            {
                                "title": "Effective Date", "width": "100px", "field": "EffectiveDate"
                            },
                            {
                                "title": "Document Date", "width": "100px", "field": "DocumentDate"
                            },
                            {
                                "title": "Page Count", "width": "100px", "field": "PageCount"
                            },
                            {
                                "title": "Page Size Height", "width": "100px", "field": "PageSizeHeight"
                            },
                            {
                                "title": "Reserved", "width": "100px", "field": "Reserved1"
                            },
                            {
                                "title": "RRD Internal DocID", "width": "100px", "field": "RRDInternalDocID"
                            },
                            {
                                "title": "Reserved", "width": "100px", "field": "Reserved2"
                            },
                            {
                                "title": "Accession", "width": "100px", "field": "Accession"
                            },
                            {
                                "title": "Effective Date", "width": "100px", "field": "EffectiveDate"
                            },
                            {
                                "title": "Filing Date", "width": "100px", "field": "FilingDate"
                            },
                            {
                                "title": "SEC Form Type", "width": "100px", "field": "SECFormType"
                            },
                ],
                
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
            };
            $scope.report = {};
            $scope.report.reportDate = $scope.CustomerDocUpdateDetailDate;
            $scope.report.ClientName = $rootScope.CurrentUser.SelectedClient,
            
            ReportService.CDUDReportData($scope.report).then(function (output) {
                $scope.showGrid = true;
                $scope.gridDataSourceHeader = output.data;
                $scope.gridDataSourceDetails = output.data.DetailRecords;
            }, function (res) {
            });
        }
    };
    $scope.btnClearCustomerDetail = function () {
        $scope.customerDocUpdateDetailForm.submitted = false;
        $scope.CustomerDocUpdateDetailDate = undefined;
        $scope.showGrid = false;
    };

    //Validate
    $scope.Validate = function () {
        var isSuccess = true;
        $scope.CustomerDetailsReportValidations = '';
        $scope.CustomerDetailsReportValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.CustomerDetailsReportValidations += "<ul>"
        if (!$scope.CustomerDocUpdateDetailDate) {
            $scope.CustomerDetailsReportValidations += "<li class='message'>Select Date</li>";
            isSuccess = false;
        }
       $scope.CustomerDetailsReportValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.CustomerDetailsReportValidations);
        }
        return isSuccess;
    };
}]);
