app.controller('ErrorCtrl', ['$scope', '$rootScope', '$state', '$stateParams', 'AUTH_EVENTS', function ($scope, $rootScope, $state, $stateParams, AUTH_EVENTS) {
    $scope.errorDescription = $stateParams.errorDescription;
    if ($stateParams.errorCode == AUTH_EVENTS.sessionTimeout || $stateParams.errorCode == AUTH_EVENTS.notAuthenticated)
        $scope.loginButton = true;
    else
        $scope.loginButton = false;
    $scope.GoBack = function () {
        $state.transitionTo($rootScope.previousState, { 'reload': true });
    };    
}]);