app.service('ReportService', function ($rootScope, $http, API_ENDPOINT) {
    

    this.CDUReportStatus = function () {
        var url = API_ENDPOINT.url + '/CUSTOMERDOCUPDATE/GetReportStatus';
        return $http.get(url);
    };
    this.CDUReportData = function (data) {
        var url = API_ENDPOINT.url + '/CUSTOMERDOCUPDATE/Report';
        return $http.post(url, data);
    };
    this.CDUDReportData = function (data) {
        var url = API_ENDPOINT.url + '/CustomerDocUpdateDetail/Report';
        return $http.post(url, data);
    };
    this.GDUReportStatus = function () {
        var url = API_ENDPOINT.url + '/GATEWAYDOCUPDATE/GetReportStatus';
        return $http.get(url);
    };
    this.GDUReportData = function (data) {
        var url = API_ENDPOINT.url + '/GATEWAYDOCUPDATE/Report';
        return $http.post(url, data);
    };

  
  
});

