app.service('CustomerDocUpdateService', function ($rootScope, $http, API_ENDPOINT) {

    this.ReportStatus = function () {
        var url = API_ENDPOINT.url +  '/CUSTOMERDOCUPDATE/GetReportStatus';
        console.log("url    " + url);
        return $http.get(url);
    };
    this.ReportData = function (data) {
        var url = API_ENDPOINT.url + '/CUSTOMERDOCUPDATE/Report';
        return $http.post(url,data);
    };



})
