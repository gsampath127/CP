app.service('SecurityTypesReportService', function ($rootScope, $http, API_ENDPOINT) { 
    this.GetCompanyandSecurityType = function () {
        var url = API_ENDPOINT.url + '/SecurityTypeReport/GetCompanyandSecurityType'; 
        return $http.get(url);
    };    
})