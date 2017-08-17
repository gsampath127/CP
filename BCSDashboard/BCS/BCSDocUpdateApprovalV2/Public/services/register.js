app.service('RegisterService', function ($rootScope, $http, API_ENDPOINT) {
 
    //REGISTER
    this.Register = function (data) {
        var url = API_ENDPOINT.url + '/Register';
        return $http.post(url, data);
    };

    this.GetRoles = function (LoggedInRole) {
        var url = API_ENDPOINT.url + '/Register/GetRoles?Id=' + LoggedInRole;
        return $http.get(url);
    };

    // COMPLETE REGISTRATION

    this.ValidateUserProfile = function (data) {
        var url = API_ENDPOINT.url + '/CompleteRegistration/ValidateUserProfile?Id=' + data;
        return $http.get(url, data);
    };

    this.CompleteRegistration = function (data) {
        var url = API_ENDPOINT.url + '/CompleteRegistration';
        return $http.post(url, data);
    };

    this.GetSecurityQns = function () {
        var url = API_ENDPOINT.url + '/CompleteRegistration/GetSecQns';
        return $http.get(url);
    };

})
