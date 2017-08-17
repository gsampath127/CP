app.service('SLINKService', function ($rootScope, $http, API_ENDPOINT) {
 
    this.ReportStatus = function () {
        var url = API_ENDPOINT.url + '/SLINK/GetReportStatus';
        return $http.get(url);
    };

    this.GenerateSLINKReport = function (data) {
        var url = API_ENDPOINT.url + '/SLINK/Report';
        return $http.post(url, data);
    };
})
