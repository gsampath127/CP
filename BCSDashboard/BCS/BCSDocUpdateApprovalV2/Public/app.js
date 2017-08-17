var app = angular.module('app', ['ui.router', 'kendo.directives', 'angular-jwt', 'ngAnimate', 'angular-loading-bar','ui.bootstrap']);

app.constant('AUTH_EVENTS', {
    loginSuccess: 'auth-login-success',
    loginFailed: 'auth-login-failed',
    logoutSuccess: 'auth-logout-success',
    sessionTimeout: 'auth-session-timeout',
    notAuthenticated: 'auth-not-authenticated',
    notAuthorized: 'auth-not-authorized',
    badRequest: 'bad-request',
    internalServerError: 'internal-server-error'
})

app.constant('API_ENDPOINT', {
    url: $("#dvAPIUUrl").data('request-url'),
    LOCAL_TOKEN_KEY: 'TokenKey',
    SELECTED_CLIENT: 'SelectedClient'
});

app.constant('USER_ROLES', {
    SuperAdmin: 'Super Admin',
    GIMUser: 'GIM User',
    GIMAdmin: 'GIM Admin',
    GMSUser: 'GMS User',
    GMSAdmin: 'GMS Admin',
    AllianceBernsteinUser: 'AllianceBernstein User',
    AllianceBernsteinAdmin: 'AllianceBernstein Admin',
    TransamericaUser: 'Transamerica User',
    TransamericaAdmin: 'Transamerica Admin',
    TRPUser: 'TRP User',
    TRPAdmin:'TRP Admin'

});

app.run(function ($rootScope, $state, AuthService, AUTH_EVENTS) {
    $rootScope.$on('$stateChangeStart', function (event, next, nextParams, from, fromState) {
        if (next.name == 'error')
            $rootScope.previousState = from.name;
        if ('data' in next && 'authorizedRoles' in next.data) {
            var authorizedRoles = next.data.authorizedRoles;
            if (AuthService.isAuthenticated()) {
                if (!AuthService.isAuthorized(authorizedRoles)) {
                    event.preventDefault();
                    // $state.go($state.current, {}, { reload: true });
                    $rootScope.$broadcast(AUTH_EVENTS.notAuthorized);
                }
            }
            else {
                event.preventDefault();
                $state.transitionTo('login', {}, { 'reload': true });
            }
            
        }

    });

});




