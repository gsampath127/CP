app.controller('ForgotPasswordCtrl', ['$scope', '$state', 'jwtHelper', '$window', 'ForgotPasswordService', function ($scope, $state, jwtHelper, $window, ForgotPasswordService) {
    $scope.user = {};
    $scope.Question = "";
    $scope.Answer = "";
    $scope.Email = "";
    $scope.viewTemplate = "forgotPassword";
    $scope.ShowSaveError = false;
    $scope.ErrorMessage = "";
    $scope.ForgotPassword = function () {
        if (validateForm($scope.user.email)) {
            ForgotPasswordService.VerifyEmail($scope.user).then(function (output) {
                $scope.Email = $scope.user.email;
                if (output.data.SecurityQuestion == null) {
                    $scope.ErrorMessage = "";
                    $scope.ErrorMessage = "email does not exist !";
                }
                else {
                    $scope.user = output.data;
                    $scope.viewTemplate = "securityQuestion";
                    $scope.ErrorMessage = "";
                }

            });
        }
        else {
            $scope.ErrorMessage = "incorrect email ";
        }
    };
    $scope.CheckAnswer = function () {
        var textVal = $('#answer').val();
        if (textVal != '') {
            $scope.user.email = $scope.Email;
            ForgotPasswordService.CheckAnswer($scope.user).then(function (output) {
                if (output.data == "Ok") {
                    $scope.viewTemplate = "changePassword";
                    $scope.ErrorMessage = "";
                }
                else {
                    $scope.ErrorMessage = "Enter correct password";
                }
            });
        }
        else {
            $scope.ErrorMessage = "";
            $scope.ErrorMessage = "enter the fields";
        }
    };
    $scope.SetPassword = function () {
       
        if ($('#password').val() != '' && $('#reEnterPassword').val() != '') {
            if ($('#password').val() == $('#reEnterPassword').val()) {
                $scope.user.email = $scope.Email;
                ForgotPasswordService.SetPassword($scope.user).then(function (output) {
                    if (output.data == "Ok") {
                        $scope.viewTemplate = "passwordSuccess";

                    }
                    else {
                        $scope.ErrorMessage = "";
                        $scope.ErrorMessage = "Something went wrong !";
                    }
                });
            }
            else {
                $scope.ErrorMessage = "";
                $scope.ErrorMessage = "Passwords dont match !";
            }
        }
        else {
            $scope.ErrorMessage = "";
            $scope.ErrorMessage = "Enter all fields !";
        }
    };
}]);

function validateForm(x) {
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");
    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
        
        return false;
    }
    else
        return true;
}
