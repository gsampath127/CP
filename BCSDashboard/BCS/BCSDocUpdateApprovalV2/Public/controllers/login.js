app.controller('AuthCtrl', ['$scope', '$stateParams', '$rootScope', '$state', 'AuthService', 'AUTH_EVENTS', 'API_ENDPOINT', 'jwtHelper', function ($scope, $stateParams, $rootScope, $state, AuthService, AUTH_EVENTS, API_ENDPOINT, jwtHelper) {
    $scope.user = {};
    $scope.ErrorMessage = '';
    var token = window.localStorage.getItem(API_ENDPOINT.LOCAL_TOKEN_KEY);
    if (token) {
        if ($stateParams.logout)
            AuthService.logout(token).then(function (token) {
                // $scope.setCurrentUsername(data.username);
            }, function (err) {
                //redirect to error page or state
            });
        else {

            $state.go('selectClient', {}, { reload: true });
        }
    }
    $scope.Login = function () {

        if ($scope.loginform.$valid) {
            AuthService.login($scope.user).then(function (token) {

                $state.go('selectClient', {}, { reload: true });

                // $scope.setCurrentUsername(data.username);
            }, function (err) {
                $scope.ErrorMessage = err.Message;
                //redirect to error page or state
            });
        }
        else if (!$scope.loginform.userEmail.$valid && !$scope.loginform.userPassword.$valid)
            $scope.ErrorMessage = 'Fields are empty';
        else if (!$scope.loginform.userEmail.$valid)
            $scope.ErrorMessage = 'Email is empty';
        else
            $scope.ErrorMessage = 'Password is empty';
    };

    $rootScope.$on(AUTH_EVENTS.badRequest, function (event, args) {
        AuthService.destroyUserCredentials();
        Error(AUTH_EVENTS.badRequest,'Session expired');
        // code for not notAuthenticated and redirect to error page or show alert
    });

    $rootScope.$on(AUTH_EVENTS.notAuthorized, function (event) {
        //alert(AUTH_EVENTS.notAuthorized);
        Error(AUTH_EVENTS.notAuthorized,'Not authorized');
        //code for not authorized and redirect to error page or show alert
    });

    $rootScope.$on(AUTH_EVENTS.notAuthenticated, function (event) {
        //alert(AUTH_EVENTS.notAuthenticated);
        AuthService.destroyUserCredentials();
        Error(AUTH_EVENTS.notAuthenticated,'Not authenticated');
        // code for not notAuthenticated and redirect to error page or show alert
    });

    $rootScope.$on(AUTH_EVENTS.internalServerError, function (event, args) {
        //alert(AUTH_EVENTS.notAuthenticated);
        //    AuthService.logout();
        Error(AUTH_EVENTS.internalServerError,args.data.Message + ' ' + args.data.ExceptionMessage);
        // code for not notAuthenticated and redirect to error page or show alert
    });

    Error = function (code, description) {
        $state.transitionTo('error', { errorCode:code, errorDescription: description }, { 'reload': true });
    };

}]);


