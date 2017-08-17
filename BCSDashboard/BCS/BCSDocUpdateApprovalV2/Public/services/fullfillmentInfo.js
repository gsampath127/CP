app.service('FullfillmentInfoReportService', function ($rootScope, $http,  API_ENDPOINT) {
 
    this.ReportData = function (data) {
        var url = API_ENDPOINT.url + '/FullfillmentInfo/Report';
        return $http.post(url, data);
    };

 

})
