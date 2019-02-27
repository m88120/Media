var myApp = angular.module("User", ['angularUtils.directives.dirPagination']);
myApp.controller("UserRefferalReport", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList1 = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/User/UserRefferalReportAngularJs',
            method: "POST",
            data: { }
        }).success(function (response) {
            $scope.loader.loading = false;
            if (response != null || response != "undefined") {
                $scope.dataList1 = response;
                $('#tblData').css({
                    'display': ''
                });
            }
        })
    };

    $scope.dataList2 = [];   
    loadRecordsAll();

    function loadRecordsAll() {
        $http({
            url: '/User/UserRefferalReportAllAngularJs',
            method: "POST",
            data: {}
        }).success(function (response) {
            $scope.loader.loading = false;
            if (response != null || response != "undefined") {
                $scope.dataList2 = response;
                $('#tblDataAll').css({
                    'display': ''
                });
            }
        })
    };

    $scope.search = function () {
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