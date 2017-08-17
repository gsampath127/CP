app.controller('RegisterCtrl', ['$scope', '$state', 'RegisterService', 'jwtHelper', '$window', '$stateParams', function ($scope, $state, RegisterService, jwtHelper, $window, $stateParams) {
    //REGISTER
    $scope.user = {};

    $scope.ShowEmailError = false;
    $scope.ShowSaveError = false;
    $scope.ShowRolesError = false;

    // COMPLETE REGISTRATION
    $scope.user.UserId = $stateParams.userid;

    $scope.AllSecurityQns = [];
    $scope.viewTemplate = 'Loading';

    RegisterService.ValidateUserProfile($scope.user.UserId).then(function (output) {
        var status = output.data;
        if (status == 'ProfileCompleted')
        {
            $scope.viewTemplate = 'profileAlreadyCompleted';
        }
        else if (status == 'InvalidUser') {
            $scope.viewTemplate = 'userNotExist';
        }
        else {
            $scope.viewTemplate = 'registration';
            RegisterService.GetSecurityQns().then(function (output) {
                $scope.AllSecurityQns = output.data;
                $scope.user.selectedSecurityQn = $scope.AllSecurityQns[0]
            }, function (res) {

            });
        }
    });   

    $scope.ComplteRegistration = function () {
        if ($scope.Validate()) {
            RegisterService.CompleteRegistration($scope.user).then(function (token) {
                if (token.data == 'Success') {
                    $scope.viewTemplate = 'profileCompleted';
                }
                else if (token.data == 'InvalidUser')
                {
                    $scope.viewTemplate = 'userNotExist';
                }
                else if(token.data =='ProfileCompleted')
                {
                    $scope.viewTemplate = 'profileAlreadyCompleted';
                }
                else if (token.data == 'Failed') {
                    $scope.ShowSaveError = true;
                }
            }, function (err) {
                //redirect to error page or state
            }
            );
        }
        else {
            $scope.form.submitted = true;
        }
    }

    $scope.Validate = function () {
        var isSuccess = true;
        $scope.AddUserValidations = '';
        $scope.AddUserValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.AddUserValidations += "<ul>"


        if (!$scope.user.selectedSecurityQn) {
            $scope.AddUserValidations += "<li class='message'>Security Question</li>";
            isSuccess = false;
        }

        if (!$scope.user.securityAns) {
            $scope.AddUserValidations += "<li class='message'>Security Answer</li>";
            isSuccess = false;
        }
        if (!$scope.user.password) {
            $scope.AddUserValidations += "<li class='message'>Password</li>";
            isSuccess = false;
        }
       
        if (!$scope.user.ReEnterpassword) {
            $scope.AddUserValidations += "<li class='message'>Confirm Password</li>";
            isSuccess = false;
        }

        $scope.AddUserValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.AddUserValidations);
        }
        return isSuccess;


    };

}]);
app.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('pwmatch', v);
                });
            });
        }
    }
}]);

function CheckPasswordStrength(password) {
    var password_strength = document.getElementById("password_strength");

    //TextBox left blank.
    if (password.length == 0) {
        password_strength.innerHTML = "";
        return;
    }

    //Regular Expressions.
    var regex = new Array();
    regex.push("[A-Z]"); //Uppercase Alphabet.
    regex.push("[a-z]"); //Lowercase Alphabet.
    regex.push("[0-9]"); //Digit.
    regex.push("[$@$!%*#?&]"); //Special Character.

    var passed = 0;

    //Validate for each Regular Expression.
    for (var i = 0; i < regex.length; i++) {
        if (new RegExp(regex[i]).test(password)) {
            passed++;
        }
    }

    //Validate for length of Password.
    if (passed > 2 && password.length > 8) {
        passed++;
    }

    //Display status.
    var color = "";
    var strength = "";
    switch (passed) {
        case 0:
        case 1:
            strength = "Weak";
            color = "red";
            break;
        case 2:
            strength = "Good";
            color = "darkorange";
            break;
        case 3:
        case 4:
            strength = "Strong";
            color = "green";
            break;
        case 5:
            strength = "Very Strong";
            color = "darkgreen";
            break;
    }
    password_strength.innerHTML = strength;
    password_strength.style.color = color;
}

function SetDualListBox() {
    var userClientDualLstBox = $('#optUserClients').bootstrapDualListbox({
        nonSelectedListLabel: 'Roles Not Associated',
        selectedListLabel: 'Associated Roles',
        preserveSelectionOnMove: 'moved',
        moveOnSelect: false
    });
}
