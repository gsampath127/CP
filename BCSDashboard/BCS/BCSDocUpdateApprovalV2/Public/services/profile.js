app.service('ProfileService',function ($rootScope, $http, API_ENDPOINT) {
    this.UpdateSecurityQuestion = function (data) {
        var url = API_ENDPOINT.url + '/profile/SecurityQuestion';
        return $http.post(url, data);
    };
    this.UpdateOldPassword = function (data) {
        var url = API_ENDPOINT.url + '/profile/NewPassword';
        return $http.post(url, data);
    };
});