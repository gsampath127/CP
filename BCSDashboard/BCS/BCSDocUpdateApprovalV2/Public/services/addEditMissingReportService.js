app.service('AddEditMissingReportService', function ($rootScope, $http, API_ENDPOINT) {


    this.AddEditLevel = function (data) {
        var url = API_ENDPOINT.url + '/SecurityTypes/AddEditMissingReport';
        return $http.post(url, data);
    };



})
