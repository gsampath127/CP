app.service('ForgotPasswordService', function ($q, $rootScope, $http, API_ENDPOINT) {

    this.VerifyEmail = function (data) {
        var url = API_ENDPOINT.url + '/ForgotPassword';
        var config = {
            headers: {
                'Content-Type': 'application/json',
                'Token': $rootScope.token
            }
        };
        return $http.post(url, data, config);
    };
    this.CheckAnswer = function (data) {
        var url = API_ENDPOINT.url + '/ForgotPassword/CheckAnswer';
        var config = {
            headers: {
                'Content-Type': 'application/json',
                'Token': $rootScope.token
            }
        };
        return $http.post(url, data, config);
    };
    this.SetPassword = function (data) {
        var url = API_ENDPOINT.url + '/ForgotPassword/SetPassword';
        var config = {
            headers: {
                'Content-Type': 'application/json',
                'Token': $rootScope.token
            }
        };
        return $http.post(url, data, config);
    };

});
