app.service('rpMissingCusipService', function ($rootScope, $http, API_ENDPOINT) {    
    this.GetCompany = function () {
        var url = API_ENDPOINT.url + '/RPMissingCusip/GetCompany';
        return $http.get(url);
    };
})