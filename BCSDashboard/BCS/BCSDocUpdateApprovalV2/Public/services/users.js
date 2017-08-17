app.service('UsersService', function ($rootScope, $http, API_ENDPOINT) {
 
    this.GetUserDetails = function (data) {
        var url = API_ENDPOINT.url + '/Users/GetUserDetails/?UserId=' + $rootScope.CurrentUser.userId;
        return $http.get(url, data);
    };

})
