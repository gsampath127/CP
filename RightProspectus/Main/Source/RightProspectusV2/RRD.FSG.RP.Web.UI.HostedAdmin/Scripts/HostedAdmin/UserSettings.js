$(document).ready(function () {
    $("#GridContainer").hide();
    ClearFilters();
    Bind_btnSearchUser();
    Bind_Event_btnAddNewUser();
    LoadSearchParameters();
    //$("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#userGrid").find(".k-pager-wrap").insertBefore(".k-grid-header")
    $("#userGrid").find(".k-pager-wrap").css("padding-left", "62%");
    $("#userGrid").find(".k-pager-info").css("display", "none");

    $('body').keypress(function (e) {
        if (e.which == 13) {
            jQuery(this).blur();
            jQuery('#searchUsers').focus().click();
        }
    });

    $('#usersPopOver').popover({
        container: 'body',
        placement: 'right',
        html: 'true',
        title: '<span class="text-info"><strong>Users</strong></span> <button type="button" id="close" class="close" onclick="ClosePopOver(&apos;usersPopOver&apos;)">&times;</button></span>',
        content: $("#dvUsersPopOver").html(),
        trigger: 'click'
    });
    LoadDatabySearchParameters();
});

function LoadSearchParameters() {
    LoadUserNameCombo();
    LoadFirstNameCombo();
    LoadLastNameCombo();
    LoadEmailCombo();
}

function LoadUserNameCombo() {
    $('#UserNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvUserNameComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select User Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 && !viewModel.allowCustomValues) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#UserNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadFirstNameCombo() {
    $('#FirstNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvFirstNameComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select First Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            //// selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0 ) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#FirstNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadLastNameCombo() {
    $('#LastNameCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvLastNameComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select Last Name',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#LastNameCombo").data("kendoComboBox").dataSource.read();
}

function LoadEmailCombo() {
    $('#EmailCombo').kendoComboBox({
        dataSource: {
            transport: {
                read: {
                    url: $("#dvEmailComboLoad").data('request-url'),
                    cache: false
                },
                type: "GET",
                dataType: "json"
            }
        },
        autoBind: false,
        filter: 'startswith',
        dataTextField: 'Display',
        dataValueField: 'Value',
        suggest: true,
        placeholder: 'Select Email',
        cache: false,
        change: function (e) {

            var cmb = this;
            //If the dropdownlist want to be cleared for the invalid entries uncomment the below code
            // selectedIndex of -1 indicates custom value
            //if (cmb.selectedIndex < 0) {
            //    cmb.value(null); // or set to the first item in combobox
            //}
        }
    });

    $("#EmailCombo").data("kendoComboBox").dataSource.read();
}
function Bind_btnSearchUser() {
    $("#searchUsers").click(function (e) {
        initialLoad = true;
        $("#GridContainer").show();
        LoadDatabySearchParameters();
    });
}

function ClearFilters() {
    $('#clearUserSearch').click(function () {
        $("#GridContainer").hide();
        $('#UserNameCombo').data("kendoComboBox").text('');
        $('#FirstNameCombo').data("kendoComboBox").text('');
        $('#LastNameCombo').data("kendoComboBox").text('');
        $('#EmailCombo').data("kendoComboBox").text('');
        //$("#userGridwithSortingButtons").css('display', 'none');
        //$('#clientcontainerDiv').empty();
    });

}

function LoadDatabySearchParameters() {
    var userName = $('#UserNameCombo').data("kendoComboBox").text();
    var firstName = $('#FirstNameCombo').data("kendoComboBox").text();
    var lastName = $('#LastNameCombo').data("kendoComboBox").text();
    var email = $('#EmailCombo').data("kendoComboBox").text();
    //need to uncomment for sorting $("#siteGridwithSortingButtons").show();
    LoadData(userName, firstName, lastName, email);
}

var initialLoad = true;
function LoadData(searchUserName, searchFirstName, searchLastName, searchEmail) {
    var pageSize = 10;
    if (typeof $("#userGrid").data("kendoGrid") != 'undefined') // if grid exists 
    {
        pageSize = $("#userGrid").data("kendoGrid").dataSource.pageSize();
        $('#userGrid').empty();   // to clear the previous data of Grid.
    }



    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: $("#dvLoadAllUsers").data('request-url'),
                data: {
                    userName: searchUserName,
                    firstName: searchFirstName,
                    lastName: searchLastName,
                    email: searchEmail
                },
                dataType: "json",
                type: "POST"
            }

        },
        serverSorting: true,
        serverPaging: true,
        pageSize: pageSize,
        schema: {
            data: "data",
            total: "total"
        }
        ,
        requestStart: function () {
            if (initialLoad) {
                kendo.ui.progress($("#userGrid"), true);
            } else {
                kendo.ui.progress($("#userGrid"), false);
            }
        },
        requestEnd: function () {
            if (initialLoad) {
                kendo.ui.progress($("#userGrid"), false);
                initialLoad = false;
            }
        }
    });


    $("#GridContainer").show();
    $("#userGrid").kendoGrid({
        dataSource: dataSource,
        dataBound: gridDataBound,
        sortable: {
            mode: "single",
            allowUnsort: false

        },
        pageable: {
            buttonCount: 5,
            pageSize: pageSize,
            pageSizes: [5, 10, 15]

        },

        columns: [

                  { field: "UserName", title: "User Name", width: 150 },
                  { field: "FirstName", title: "First Name" },
                  { field: "LastName", title: "Last Name" },
                  { field: "Email", title: "Email" },
                  {
                      title: "",
                      width: 50,
                      template: "<a class='text-info' data-toggle='tooltip' data-original-title='Edit' data-placement='top' data-container='body'><span class='glyphicon glyphicon-edit editIcon' onclick='ShowEditUserPopUp(event,#= data.UserID #);'></span></a>",
                      attributes: { "class": "templateElements" }

                  },
                   {

                       title: "",
                       width: 50,
                       template: "<a class='text-info' data-toggle='tooltip' data-original-title='Remove' data-placement='top' data-container='body'><span class='glyphicon glyphicon-trash deleteIcon' onclick='ShowDeleteUserPopUp(#= data.UserID #);'></span></a>",
                       attributes: { "class": "templateElements" }
                   }
        ],
        editable: "popup"
    });
    $("#userGrid").data("kendoGrid").bind("dataBound", BindTopPager);
}
function Bind_Event_btnAddNewUser() {
    $("#btnAddNewUser").click(function (e) {
        ShowEditUserPopUp(e, 0, 0);
    });
}

function ShowEditUserPopUp(event, userID) {
    var pageURL = $("#dvEditUser").data('request-url') + '/' + userID;
    var pageTitle = "Edit User";
    var width = "1000";
    var height = "800";
    PopupCenter(pageURL, pageTitle, width, height);
}

function PopupCenter(pageURL, pageTitle, width, height) {
    var left = (screen.width / 2) - (width / 2);
    var top = (screen.height / 2) - (height / 2);
    var targetWin = window.open(pageURL, pageTitle, 'scrollbars=yes, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left + ',resizable=yes');
    targetWin.focus();
}

function ShowDeleteUserPopUp(userID) {
    var kendoWindow = $('<div />').kendoWindow({
        title: "Confirm",
        resizable: false,
        modal: true,
        draggable: false
    });

    kendoWindow.data("kendoWindow")
        .content($("#alertDeleteUser").html())
        .center().open();

    kendoWindow
        .find(".confirm,.cancel")
        .click(function () {
            if ($(this).hasClass("confirm")) {
                DeleteUserDetails(userID);
                kendoWindow.data("kendoWindow").close();

            }
            if ($(this).hasClass("cancel")) {
                kendoWindow.data("kendoWindow").close();
            }
        });


}

function DeleteUserDetails(UserID) {
    $.ajax({
        url: $("#dvDeleteUser").data('request-url'),
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: { userID: UserID },
        async: false,
        success: function (data) {

            $('#UserNameCombo').data("kendoComboBox").text('');
            $('#FirstNameCombo').data("kendoComboBox").text('');
            $('#LastNameCombo').data("kendoComboBox").text('');
            $('#EmailCombo').data("kendoComboBox").text('');

            LoadSearchParameters();
            LoadDatabySearchParameters();


        },
        error: function (xhr, status, error) {

            alert(error);
        }
    });

}
function gridDataBound(e) {
    var grid = e.sender;
    if (grid.dataSource.total() == 0) {
        var colCount = grid.columns.length;
        $(e.sender.wrapper)
            .find("tbody")
.append('<tr class="kendo-data-row"><td colspan="' + colCount + '" class="no-data"> <b>No Records Found<b> </td></tr>');
        $(e.sender.wrapper)
            .find(".k-grid-content-expander").remove();

    }
}

function BindTopPager(e) {

    var gridView = $('#userGrid').data('kendoGrid');
    var pager = $('#div .k-pager-wrap');
    var id = pager.attr('id') + '_top';
    var $grid = $('#userGrid');
    var topPager;

    if (gridView.topPager == null) {
        // create top pager div
        topPager = $('<div/>', {
            'id': id,
            'class': 'k-pager-wrap pagerTop'
        }).insertBefore($grid.find('.k-grid-header'));

        // copy options for bottom pager to top pager
        gridView.topPager = new kendo.ui.Pager(topPager, $.extend({}, gridView.options.pageable, { dataSource: gridView.dataSource }));

        // cloning the pageable options will use the id from the bottom pager
        gridView.options.pagerId = id;

        // DataSource change event is not fired, so call this manually
        gridView.topPager.refresh();
        pagerPosition();

    }
}
function pagerPosition() {
    $("#userGrid").find(".k-pager-wrap").css("padding-left", "35%");

}



function ClosePopOver(popoverID) {
    $('#' + popoverID).trigger("click");
}

// hide any open popovers when the anywhere else in the body is clicked
$('body').on('click', function (e) {
    $('[data-toggle=popover]').each(function () {
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            if ($('body div').find('.popover-content').length > 0)
                $('[data-toggle=popover]').trigger("click");
        }
    });
});





