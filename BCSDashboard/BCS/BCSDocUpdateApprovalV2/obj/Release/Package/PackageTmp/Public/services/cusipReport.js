app.service('CUSIPReportService', function ($q, $rootScope, $http, API_ENDPOINT) {
    this.GetReportTypes = function () {
        var url = API_ENDPOINT.url + '/CUSIP/ReportTypes';
        return $http.get(url);

    };
});