app.controller('AddEditUserCtrl', ['$rootScope', '$scope', '$http', '$state','$uibModalInstance', 'AddEditUserService', 'API_ENDPOINT', function ($rootScope, $scope, $http, $state, $uibModalInstance, AddEditUserService, API_ENDPOINT) {

    $scope.UserId = $scope.parentData.userId;
    $scope.ShowSaveError = false;
    $scope.ShowEmailError = false;
    $scope.ShowRolesError = false;
    $scope.ShowEditArea = false;
    $scope.ShowAddArea = true;

    $scope.ManageUsers = {};
    // EDIT
    if ($scope.UserId > 0) {
        $scope.ShowEditArea = true;
        $scope.ShowAddArea = false;

        AddEditUserService.GetUserDetail($scope.UserId).then(function (output) {
            var Roles = [];
            $.each(output.data.RolesData, function (i, item) {
                Roles.push(item.RoleId);
            });
           
            $scope.ManageUsers.selectedRoles = Roles;
            $scope.SelectedRoles = output.data.RolesData;

            AddEditUserService.GetRoles($rootScope.CurrentUser.userId).then(function (output) {
                $scope.DefaultListItems = [];
                $('#optUserClients').empty();
                var i;
                for (i = 0; i < output.data.length; i++) {
                    var flag = 0;
                    for (j = 0; j < $scope.SelectedRoles.length; j++) {
                        if ($scope.SelectedRoles[j]["RoleId"] == output.data[i]["RoleId"]) {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0) {
                        $('#optUserClients').append($('<option />').val(output.data[i]["RoleId"]).text(output.data[i]["RoleName"]).prop('selected', false));  //this.Selected
                    }
                    else {
                        $('#optUserClients').append($('<option />').val(output.data[i]["RoleId"]).text(output.data[i]["RoleName"]).prop('selected', true));  //this.Selected
                    }
                }
                SetDualListBox();

            }, function (res) {
               
            });

            $scope.ManageUsers.LockOutEnabled = output.data.UserData.LockoutEnabled;
        }, function (res) {
          
        });
    }
    else {

        AddEditUserService.GetRoles($rootScope.CurrentUser.userId).then(function (output) {
            $scope.DefaultListItems = [];
            $('#optUserClients').empty();
            $.each(output.data, function (i, item) {
                $('#optUserClients').append($('<option />').val(this.RoleId).text(this.RoleName).prop('selected', this.Selected));
            });
            SetDualListBox();

        }, function (res) {
            
        });
    }
    $scope.AddEditUser = function () {
        if ($scope.UserId == 0)   // ADD
        {
            if ($scope.AddUserValidate()) {
                AddEditUserService.AddEditUser($scope.ManageUsers).then(function (output) {
                    var isSuccess = true;
                    $scope.DataValidations = '';
                    if (output.data == 'Inserted') {
                        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
                            title: "Success",
                            resizable: false,
                            modal: true,
                            draggable: false,
                            actions: []
                        });

                        kendoWindow.data("kendoWindow")
                            .content($("#divSuccessOrFailedMessage").html())
                            .center()
                            .open();

                        kendoWindow
                       .find(".confirm")
                       .click(function () {
                           kendoWindow.data("kendoWindow").close();
                           $uibModalInstance.close('Inserted');
                       });
                     
                        }
                    else if (output.data.indexOf('Duplicate') != -1) {
                       
                            if (output.data.indexOf('Email') != -1)
                            {
                                $scope.DataValidations = "<ul><li class='message'>Email already exists.</li></ul>";
                                isSuccess = false;
                            }
                        }
                        else if (output.data.indexOf('Failed') != -1) {
                            isSuccess = false;
                            $scope.DataValidations = "<ul><li class='message'>Manage User Failed.</li></ul>";
                        }

                        
                    if (!isSuccess && $scope.DataValidations) {
                            KendoValidationWindow($scope.DataValidations);
                        }

                    }, function (err) {
                        //redirect to error page or state
                    }
                    );
                }
        }
        else  //EDIT
        {
            $scope.ManageUsers.UserId = $scope.UserId;
            if ($scope.EditUserValidate()) {
                AddEditUserService.UpdateUser($scope.ManageUsers).then(function (output) {
                    if (output.data == 'Updated') {
                        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
                            title: "Success",
                            resizable: false,
                            modal: true,
                            draggable: false,
                            actions: []
                        });

                        kendoWindow.data("kendoWindow")
                            .content($("#divSuccessOrFailedMessage").html())
                            .center()
                            .open();

                        kendoWindow
                       .find(".confirm")
                       .click(function () {
                           kendoWindow.data("kendoWindow").close();
                           $uibModalInstance.close('Updated');
                       });
                       // $uibModalInstance.close('Updated');
                    }
                    else if (output.data.indexOf('Failed') != -1) {
                        $scope.DataValidations = "<li class='message'>Manage User Failed.</li>";
                        KendoValidationWindow($scope.DataValidations);
                    }
                }, function (err) {
                    //redirect to error page or state
                }
                );
            }
        }
    }


    //Validate
    $scope.AddUserValidate = function () {
        var isSuccess = true;
        $scope.AddUserValidations = '';
        $scope.AddUserValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.AddUserValidations += "<ul>"


        if (!$scope.ManageUsers.FirstName) {
            $scope.AddUserValidations += "<li class='message'>First Name</li>";
            isSuccess = false;
        }

        if (!$scope.ManageUsers.LastName) {
            $scope.AddUserValidations += "<li class='message'>Last Name</li>";
            isSuccess = false;
        }
        if (!$scope.ManageUsers.Email) {
            $scope.AddUserValidations += "<li class='message'>Email</li>";
            isSuccess = false;
        }
        else {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (!filter.test($scope.ManageUsers.Email)) {
                $scope.AddUserValidations += "<li class='message'>Valid E-mail Id.</li>";
                isSuccess = false;
            }
        }
        if (!$scope.ManageUsers.selectedRoles) {
            $scope.AddUserValidations += "<li class='message'>Roles</li>";
            isSuccess = false;
        }

        $scope.AddUserValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.AddUserValidations);
        }
        return isSuccess;


    };

    $scope.EditUserValidate = function () {
        var isSuccess = true;
        $scope.EditUserValidations = '';
        $scope.EditUserValidations += "<p class='message'>Please Enter/Select the below fields</p>";
        $scope.EditUserValidations += "<ul>"

        if (!$scope.ManageUsers.selectedRoles) {
            $scope.EditUserValidations += "<li class='message'>Roles</li>";
            isSuccess = false;
        }

        $scope.EditUserValidations += "</ul>"

        if (!isSuccess) {
            KendoValidationWindow($scope.EditUserValidations);
        }
        return isSuccess;


    };
    $scope.CloseWindow = function () {
        $uibModalInstance.close('Cancel');
    };
}]);

function SetDualListBox() {
    var userClientDualLstBox = $('#optUserClients').bootstrapDualListbox({
        nonSelectedListLabel: 'Roles Not Associated',
        selectedListLabel: 'Associated Roles',
        preserveSelectionOnMove: 'moved',
        moveOnSelect: false
    });
}
