app.service('AddEditUserService', function ($rootScope, $http, API_ENDPOINT) {
 
    this.GetRoles = function (LoggedInRole) {
        var url = API_ENDPOINT.url + '/Register/GetRoles/?Id=' + LoggedInRole;
        return $http.get(url);
    };

    this.AddEditUser = function (data) {
        var url = API_ENDPOINT.url + '/Users/AddEditUser';
        return $http.post(url, data);
    };

    this.GetUserDetail = function (userID) {
        var url = API_ENDPOINT.url + '/Users/GetUserData/?Id=' + userID;
        return $http.get(url);
    };

    this.UpdateUser = function (data) {
        var url = API_ENDPOINT.url + '/Users/UpdateUser';
        return $http.post(url, data);
    };

})
