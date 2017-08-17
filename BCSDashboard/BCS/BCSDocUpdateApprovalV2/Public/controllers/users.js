app.controller('UsersCtrl', ['$rootScope', '$scope', '$http', '$state', 'UsersService', '$uibModal', 'API_ENDPOINT', function ($rootScope, $scope, $http, $state, UsersService, $uibModal, API_ENDPOINT) {

    var initialLoad = true;
    TriggerPopOver($('#dvUsersPopOver').html(), 'Users'); // Popover for Info
    $scope.showGrid = false;
    $scope.FilterData = {};

    UsersService.GetUserDetails().then(function (output) {
        $scope.LoadComboBoxes(output); 
    });
    

    $scope.ManageUsers = {};
    $scope.ManageUsers.UserId = $rootScope.CurrentUser.userId;
    $scope.ManageUsers.EmailConfirmed = null;
    $scope.ManageUsers.LockoutEnabled = null;
    $scope.AddNewUser = function () {
        $scope.OpenAddWindow(0);
    };

    $scope.SearchUsers = function () {
        $scope.showGrid = true;
        $scope.gridOptionsUsers = {
            columns: [
                 { "title": "User Id", "field": "UserId" ,"hidden":true },
                { "title": "User Name", "field": "UserName" },
                { "title": "First Name", "field": "FirstName" },
                { "title": "Last Name", "field": "LastName" },
                { "title": "Email", "field": "Email" },
                { "title": "Lockout Enabled", "field": "LockoutEnabled" },
                { "title": "Email Confirmed", "field": "EmailConfirmed" },

                { "title": "", "width": "80px", template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' ng-click=\'OpenAddWindow(dataItem.UserId)\'  ></span></a>" }
               
            ],
            editable: "popup",
            pageable: {
                refresh: false,
                pageSize: 5,
                pageSizes: [5, 10, 15]
            },

            sortable: true,
            noRecords: {
                template: "<b>No Records Found<b> "
            }
        };
        $scope.gridDataSourceUsers = { };
        $scope.gridDataSourceUsers = {
            transport: {
                read: function (e) {
                    var url = API_ENDPOINT.url + '/Users/GetAllUsers';
                    var data = {
                        ManageUsers: $scope.ManageUsers,
                        options: e.data
                    }

                    $http.post(url, data).then(function (output) {
                        e.success(output.data);
                    });
                }
            },
            pageSize: 10,
            serverPaging: true,
            serverSorting: true,
            schema: {
                data: "data",
                total: "total"
            },
            requestStart: function () {
                if (initialLoad) {
                    kendo.ui.progress($("#gridUsers"), true);
                } else {
                    kendo.ui.progress($("#gridUsers"), false);
                }
            },
            requestEnd: function () {
                if (initialLoad) {
                    kendo.ui.progress($("#gridUsers"), false);
                    initialLoad = false;
                }
            }
        };
    };

    $scope.ClearFilters = function () {
        $scope.ManageUsers.UserName = null;
        $scope.ManageUsers.FirstName = null;
        $scope.ManageUsers.LastName = null;
        $scope.ManageUsers.Email = null;
        $scope.ManageUsers.LockoutEnabled = null;
        $scope.ManageUsers.EmailConfirmed = null;
        $("#comboUserName").data("kendoComboBox").value(null);
        $("#comboFirstName").data("kendoComboBox").value(null);
        $("#comboLastName").data("kendoComboBox").value(null);
        $("#comboEmail").data("kendoComboBox").value(null);
        $("#comboLockOut").data("kendoComboBox").value(null);
        $("#comboEmailConfirmed").data("kendoComboBox").value(null);
        

        $scope.showGrid = false;
    };

    $scope.OpenAddWindow = function (UserId) {
        var action = "";
        if (UserId == 0) action = 'Add User'; else action = 'Edit User';
        var data = { action: action, userId: UserId };

        $scope.parentData = data;
        var modalInstanceAdd = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'Public/views/login/addEditUser.html',
            controller: 'AddEditUserCtrl',
            backdrop: 'static',
            keyboard: 'false',
            size: 'lg',
            scope: $scope
        });
        modalInstanceAdd.result.then(function (resultTaskObject) {
            UsersService.GetUserDetails().then(function (output) {
                $scope.LoadComboBoxes(output);
            });
            $scope.SearchUsers();
        });
    };

    $scope.LoadComboBoxes = function (output) {

           
         $("#comboUserName").kendoComboBox({
                     placeholder: "Select User Name",
                      dataSource: output.data.UserNames,
                     filter: "contains",
                     suggest: true
                 });

           
            $("#comboFirstName").kendoComboBox({
                    placeholder: "Select First Name",
                    dataSource: output.data.FirstNames,
                    filter: "contains",
                    suggest: true
            });

             $("#comboLastName").kendoComboBox({
                placeholder: "Select Last Name",
                dataSource: output.data.LastNames,
                filter: "contains",
                suggest: true
               });


            
             $("#comboEmail").kendoComboBox({
                placeholder: "Select Email",
                dataSource: output.data.Emails,
                filter: "contains",
                suggest: true
              });
            
            

            $("#comboLockOut").kendoComboBox({
                placeholder: "Select Lockout",
                dataSource: output.data.LockoutEnabled,
                filter: "contains",
                suggest: true
            });

            $("#comboEmailConfirmed").kendoComboBox({
                placeholder: "Select Email Confirm",
                autobind: true,
                dataSource: output.data.EmailConfirmed,
                suggest: true
                
            });

        }, function (res) {

        
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





