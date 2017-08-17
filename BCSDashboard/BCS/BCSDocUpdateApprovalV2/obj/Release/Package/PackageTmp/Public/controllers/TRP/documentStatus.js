app.controller('TRPDocumentStatusCtrl', ['$rootScope', '$scope', '$state', '$http', 'TRPReportService', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, TRPReportService, API_ENDPOINT) {
    var initialLoad = true;
    TriggerPopOver($('#dvDocumentCUSIPPopOver').html(), 'Document(s) Status'); // Popover for Info
    $scope.CUSIP = null;
    $scope.SelectedPDFStatus = null;
    var pdfStatus =[{ Value:"0", Text:""},{Value:"1" ,Text:"PDF received on FTP"},{Value:"2" ,Text:"PDF is not yet received on FTP"}];
   
    $scope.comboPDFStatus = {
            placeholder: "Select PDF Status",
            autobind: true,
            dataValueField: 'Value',
            dataTextField:'Text',
            dataSource: {
                data: pdfStatus
            }
    };
    
    $scope.GenerateReport = function () {
        
            initialLoad = true;
            $scope.gridDataSource = {};
            $scope.gridOptions = {};
            $scope.DisplayGrid = true;
            $scope.gridOptions = {
                columns: [
                    { "title": "Company Name", "field": "CompanyName" },
                    { "title": "Fund Name", "field": "FundName", },
                    { "title": "FLT CUSIP", "field": "FLTCUSIP" },

                    { "title": "RP CUSIP", "field": "RPCUSIP" },
                    {
                        "title": "PDF Received On FTP", "field": "DatePDFReceivedonFTP",
                        
                        template: function (dataItem) {
                            var date = new Date(dataItem.DatePDFReceivedonFTP);
                           
                            if(date=="Invalid Date")
                                return "PDF is not yet received on FTP";
                            else
                                return "Received on " + dataItem.DatePDFReceivedonFTP;
                        }
                          
                    }
                    
                ],
                pageable: {
                    refresh: false,
                    pageSize: 5,
                    pageSizes: [5, 10, 15]

                },

                sortable: false,
                noRecords: {
                    template: "<b>No Records Found<b> "
                }
            };

            $scope.gridDataSource = {
                transport: {

                    read: function (e) {
                        var url = API_ENDPOINT.url + '/TRP/DocumentStatus';
                        var data = {
                            CUSIP: $scope.CUSIP,
                            SelectedPDFStatus: $scope.SelectedPDFStatus,
                            
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
                        kendo.ui.progress($("#documentStatusCusipGrid"), true);
                    } else {
                        kendo.ui.progress($("#documentStatusCusipGrid"), false);
                    }
                },
                requestEnd: function () {
                    if (initialLoad) {
                        kendo.ui.progress($("#documentStatusCusipGrid"), false);
                        initialLoad = false;
                    }
                }
            };

        
    };

    $scope.GenerateReport();// Bind Grid on Load

    $scope.ClearFilters = function () {
        $scope.DisplayGrid = false;
        $scope.CUSIP = null;
        $scope.SelectedPDFStatus = null;
    };
}]);

