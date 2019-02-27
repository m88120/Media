var myApp = angular.module("User", ['angularUtils.directives.dirPagination']);
myApp.controller("Event", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    $scope.CountryList;
    FillCountry();
    function FillCountry() {
        $http({
            method: 'POST',
            url: '/User/PopulateCountryAngularJs',
            data: {}
        }).success(function (response) {            
            if (response != null || response != "undefined") {
                $scope.CountryList = response;
            }
        })
    };

    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/User/EventAngularJs',
            method: "POST",
            data: { DateFrom: $scope.From, DateTo: $scope.To, Country: $scope.Country }
        }).success(function (response) {
            $scope.loader.loading = false;
            if (response != null || response != "undefined") {
                $scope.dataList = response;
                $('#tblData').css({
                    'display': ''
                });
            }
        })
    };

    $scope.Search = function () {
        loadRecords();
    }

    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };
    $(document).ready(function () {
        $scope.pageChangeHandler = function (num) {
            console.log('going to page ' + num);
        };
    });
})