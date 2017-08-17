app.service('LiveUpdateService', function ($rootScope, $http, API_ENDPOINT) {
 
    this.ReportStatus = function () {
        var url = API_ENDPOINT.url + '/LiveUpdate/GetReportStatus';
        return $http.get(url);
    };

    //this.GenerateSLINKReport = function (data) {
    //    var url = API_ENDPOINT.url + '/LiveUpdate/Report';
    //    return $http.post(url, data);
    //};
})
