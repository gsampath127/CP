app.service('HomeService', function ($rootScope, $http, API_ENDPOINT) {
    this.GetClients = function () {
        var url = API_ENDPOINT.url + '/Home/GetClients';
        return $http.post(url, {roles:$rootScope.CurrentUser.roles});
    };
});