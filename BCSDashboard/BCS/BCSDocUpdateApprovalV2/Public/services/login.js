app.service('AuthService', function ($q, $rootScope, $http, jwtHelper, API_ENDPOINT) {
    var isAuthenticated = false;
    var authToken;

    function loadUserCredentials() {
        var token = window.localStorage.getItem(API_ENDPOINT.LOCAL_TOKEN_KEY);
        if (token) {
            useCredentials(token);
        }
    }

    function storeUserCredentials(token) {
        window.localStorage.setItem(API_ENDPOINT.LOCAL_TOKEN_KEY, token.access_token);
        useCredentials(token.access_token);
    }

    function useCredentials(token) {
        isAuthenticated = true;
        authToken = token;
        var tokenPayload = jwtHelper.decodeToken(authToken);
        var date = jwtHelper.getTokenExpirationDate(authToken);
        $rootScope.CurrentUser = {};
        $rootScope.CurrentUser.userId = tokenPayload.UserId;
        $rootScope.CurrentUser.roles = [];
        $rootScope.CurrentUser.username = tokenPayload.unique_name;
        $rootScope.CurrentUser.roles = tokenPayload.role;
        var selectedClient=window.localStorage.getItem(API_ENDPOINT.SELECTED_CLIENT);
        if (selectedClient)
        {
            $rootScope.CurrentUser.SelectedClient = selectedClient;
        }
        
       
        // Set the token as header for your requests!
        $http.defaults.headers.common.Authorization = "Token " + authToken;
    }

    var destroyUserCredentials = function () {
        authToken = undefined;
        isAuthenticated = false;
        $http.defaults.headers.common.Authorization = undefined;
        window.localStorage.removeItem(API_ENDPOINT.LOCAL_TOKEN_KEY);
        window.localStorage.removeItem(API_ENDPOINT.SELECTED_CLIENT);
    }

    var register = function (user) {
        return $q(function (resolve, reject) {
            $http.post(API_ENDPOINT.url + '/signup', user).then(function (result) {
                if (result.data.success) {
                    resolve(result.data.msg);
                } else {
                    reject(result.data.msg);
                }
            });
        });
    };

    var login = function (user) {
        return $q(function (resolve, reject) {
            $http.post(API_ENDPOINT.url + '/audience/User', user).then(function (result) {
                if (result.data.Success) {
                    var Token = getToken(result);
                    Token.then(function (token) { 
                        storeUserCredentials(token);
                        resolve(token);
                    },
                    function (error) { });
                } else {
                    reject(result.data);
                }
            });
        });
    };

    var logout = function (token) {
        return $q(function (resolve, reject) {
            var tokenObject = { token: token, userId: $rootScope.CurrentUser.userId };
            $http.post(API_ENDPOINT.url + '/audience/Logout', tokenObject).then(function (result) {
                destroyUserCredentials();
            }, function (err) {
            });
        });

    };

    var isAuthorized = function (authorizedRoles) {
        if (!angular.isArray(authorizedRoles)) {
            authorizedRoles = [authorizedRoles];
        }
        var isValid = false;
        angular.forEach($rootScope.CurrentUser.roles, function (obj, key) {
            if (authorizedRoles.indexOf(obj) > -1) {
                isValid = true; return false;
            }

        });
        return (isAuthenticated && isValid);
    };



    function getToken(result) {
        // perform some asynchronous operation, resolve or reject the promise when appropriate.
        return $q(function (resolve, reject) {

            $.ajax({
                type: 'POST',
                url: API_ENDPOINT.url + '/oauth2/token',
                data: { username: result.data.Name, grant_type: 'password', client_id: result.data.ClientId },
                dataType: "json",
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                xhrFields: {
                    withCredentials: true
                },
                headers: {
                    'Content-Type': 'application/json'
                },
                success: function (response) { resolve(response); },
                error: function (req, status, error) {
                    if (req.responseJSON && req.responseJSON.error_description) {
                        var error = $.parseJSON(req.responseJSON.error_description);
                        reject(error.message);
                    }
                }
            });

        });
    }



    loadUserCredentials();

    return {
        login: login,
        register: register,
        isAuthorized: isAuthorized,
        logout: logout,
        isAuthenticated: function () { return isAuthenticated; },
        destroyUserCredentials: destroyUserCredentials
    };
})

app.factory('AuthInterceptor', function ($rootScope, $q, AUTH_EVENTS) {
    return {
        responseError: function (response) {
            $rootScope.$broadcast({
                400: AUTH_EVENTS.badRequest,
                401: AUTH_EVENTS.notAuthenticated,
                403: AUTH_EVENTS.notAuthorized,
                419: AUTH_EVENTS.sessionTimeout,
                440: AUTH_EVENTS.sessionTimeout,
                500: AUTH_EVENTS.internalServerError
            }[response.status], response);
            return $q.reject(response);
        }
    };
})

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('AuthInterceptor');
});

