app.controller('ProfileCtrl', ['$rootScope', '$scope', '$state', 'jwtHelper', '$window', 'RegisterService', 'ProfileService', function ($rootScope, $scope, $state, jwtHelper, $window, RegisterService, ProfileService) {
    $scope.user = {};

    $scope.viewTemplate = "changePassword";
    $scope.AllSecurityQns = [];
    $scope.ErrorMessage = "";
    $scope.user.CurrentUserId = $rootScope.CurrentUser.userId;

    $scope.SecurityQuestion = function () {

        $scope.ErrorMessage = "";
        $scope.user = {};
        $scope.user.CurrentUserId = $rootScope.CurrentUser.userId;
        $scope.user.selectedQuestion = $scope.AllSecurityQns[0];
        $scope.viewTemplate = "securityQuestion";
    }
    $scope.ChangePassword = function () {
        $scope.ErrorMessage = "";
        $scope.user = {};
        $scope.user.CurrentUserId = $rootScope.CurrentUser.userId;
        $scope.user.selectedQuestion = $scope.AllSecurityQns[0];
        $scope.viewTemplate = "changePassword";
    }
    RegisterService.GetSecurityQns().then(function (output) {
        $scope.AllSecurityQns = output.data;
        $scope.user.selectedQuestion = $scope.AllSecurityQns[0];
    }, function (res) {

    });
    $scope.UpdateSecurityQuestion = function () {
        if ($scope.ValidateSecurityQuestion()) {
            ProfileService.UpdateSecurityQuestion($scope.user).then(function (output) {
                if (output.data == 'Ok') {
                    $scope.viewTemplate = "success";
                }
                else
                    $scope.ErrorMessage = "something went wrong";

            });
        }
    };

    $scope.UpdateOldPassword = function () {
        if ($scope.ValidatePassword()) {
            if ($scope.user.oldPassword == $scope.user.newPassword) {
                ProfileService.UpdateOldPassword($scope.user).then(function (output) {
                    if (output.data) {
                        $scope.viewTemplate = "success";
                    }
                    else {
                        $scope.ErrorMessage - "";
                        $scope.ErrorMessage = "something went wrong";
                    }
                });
            }
            else {
                $scope.ErrorMessage = "";
                $scope.ErrorMessage = "Old Password is Incorrect ! ";
            }
        }
    };
    $scope.Clear = function () {
        $scope.user.securityAnswer = "";

    };
    $scope.ClearSecurityQuestion = function () {
        $scope.user.oldPassword = "";
        $scope.user.newPassword = "";
        $scope.user.ReEnterPassword = "";

    };
    //Validate
    $scope.ValidateSecurityQuestion = function () {
        var isSuccess = true;
        $scope.ProfileSecurityQuestionValidations = '';
        $scope.ProfileSecurityQuestionValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.ProfileSecurityQuestionValidations += "<ul>"


        if (!$scope.user.selectedQuestion) {
            $scope.ProfileSecurityQuestionValidations += "<li class='message'>Security Question</li>";
            isSuccess = false;
        }

        if (!$scope.user.securityAnswer) {
            $scope.ProfileSecurityQuestionValidations += "<li class='message'>Security Answer</li>";
            isSuccess = false;
        }

        $scope.ProfileSecurityQuestionValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.ProfileSecurityQuestionValidations);
        }
        return isSuccess;


    };
    $scope.ValidatePassword = function () {
        var isSuccess = true;
        $scope.ProfilePasswordValidations = '';
        $scope.ProfilePasswordValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.ProfilePasswordValidations += "<ul>"


        if (!$scope.user.oldPassword) {
            $scope.ProfilePasswordValidations += "<li class='message'>Old Password</li>";
            isSuccess = false;
        }

        if (!$scope.user.newPassword) {
            $scope.ProfilePasswordValidations += "<li class='message'>New Password</li>";
            isSuccess = false;
        }
        if (!$scope.user.ReEnterPassword) {
            $scope.ProfilePasswordValidations += "<li class='message'>Re-Enter Password</li>";
            isSuccess = false;
        }
        if ($scope.user.ReEnterPassword != $scope.user.newPassword) {
            $scope.ProfilePasswordValidations += "<li class='message'>Passwords Dont Match</li>";
            isSuccess = false;
        }

        $scope.ProfilePasswordValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.ProfilePasswordValidations);
        }
        return isSuccess;


    };
    $('#navigation a').click(function (e) {
        $('#navigation a').removeClass('current_page_item');
        $(this).addClass('current_page_item');
    });
}]);


