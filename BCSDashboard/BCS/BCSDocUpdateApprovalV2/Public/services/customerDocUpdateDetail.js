app.service('CustomerDocUpdateDetailService', function ($rootScope, $http, API_ENDPOINT) {

    this.ReportData = function (data) {
        var url = API_ENDPOINT.url + '/CustomerDocUpdateDetail/Report';
        return $http.post(url, data);
    };



})
