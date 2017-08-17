app.controller('TRPMissingDocumentCtrl', ['$rootScope', '$scope', '$state', '$http', 'API_ENDPOINT', function ($rootScope, $scope, $state, $http, API_ENDPOINT) {
    var initialLoad = true;
    TriggerPopOver($('#dvMissingDocumentPopOver').html(), 'Missing - Doc Info'); // Popover for Info
    
    $scope.GenerateReport = function () {

        initialLoad = true;
        $scope.gridDataSource = {};
        $scope.gridOptions = {};
        $scope.DisplayGrid = true;
        $scope.gridOptions = {
            columns: [
                { "title": "Company Name", "field": "CompanyName" },
                {
                    "title": "File Name", "field": "FileName",
                    template:function(dataItem)
                    {
                        return "<a target='_blank' href='" + dataItem.Path + "' >" + dataItem.FileName + "</a>";
                    }
                },
                {
                    "title": "PDF Received On FTP", "field": "DatePDFReceivedonFTP",

                    template: function (dataItem) {
                        var date = new Date(dataItem.DatePDFReceivedonFTP);

                        if (date == "Invalid Date")
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
                template: "<b>There are no FLT Missing entries.<b> "
            }
        };

        $scope.gridDataSource = {
            transport: {

                read: function (e) {
                    var url = API_ENDPOINT.url + '/TRP/MissingDocs';
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
                    kendo.ui.progress($("#missingDocumentGrid"), true);
                } else {
                    kendo.ui.progress($("#missingDocumentGrid"), false);
                }
            },
            requestEnd: function () {
                if (initialLoad) {
                    kendo.ui.progress($("#missingDocumentGrid"), false);
                    initialLoad = false;
                }
            }
        };


    };

    $scope.GenerateReport();// Bind Grid on Load

    
}]);

