app.service('GatewayDocUpdateService', function ($rootScope, $http, API_ENDPOINT) {

    this.ReportStatus = function () {
        var url = API_ENDPOINT.url + '/GATEWAYDOCUPDATE/GetReportStatus';
        return $http.get(url);
    };
    this.ReportData = function (data) {
        var url = API_ENDPOINT.url + '/GATEWAYDOCUPDATE/Report';
        return $http.post(url, data);
    };



})
