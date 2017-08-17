app.controller('SelectClientCtrl', ['$rootScope', '$scope', '$state', 'HomeService', 'API_ENDPOINT', function ($rootScope, $scope, $state, HomeService, API_ENDPOINT) {
  
    $scope.hideDiv = true;
    
    HomeService.GetClients().then(function (output) {
        if (output.data.length == 1) {
            $scope.SelectedClient = output.data[0];
            SelectClient();            
        }
        else
        {
            $scope.ClientData = output.data;
            $scope.hideDiv = false;
        }        
    });
    
    $scope.Submit = function () {
        SelectClient();
    };

    var SelectClient = function () {
        if ($scope.Validate()) {
            window.localStorage.setItem(API_ENDPOINT.SELECTED_CLIENT, $scope.SelectedClient);
            $rootScope.CurrentUser.SelectedClient = $scope.SelectedClient;
            $state.transitionTo('home', {}, { 'reload': true });
        }
    };


    //Validate
    $scope.Validate = function () {
        var isSuccess = true;
        $scope.ClientValidations = '';
        $scope.ClientValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.ClientValidations += "<ul>"


        if (!$scope.SelectedClient) {
            $scope.ClientValidations += "<li class='message'>Client Name</li>";
            isSuccess = false;
        }

        

        $scope.ClientValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.CUSIPReportValidations);
        }
        return isSuccess;


    };
}]);