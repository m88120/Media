var myApp = angular.module("User", ['angularUtils.directives.dirPagination']);
myApp.controller("UserTalentMedia", function ($scope, $http, $window) {
    $scope.loader = {
        loading: false,
    };   
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();  
    function loadRecords() {
        $http({
            url: '/User/UserTalentMediaAngularJs',
            method: "POST",
            data: {}
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

    $scope.search = function () {       
        loadRecords();
    }   
    $scope.currentPage = 1;
    $scope.pageSize = 20;
    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };
    $(document).ready(function () {
        $scope.pageChangeHandler = function (num) {
            console.log('going to page ' + num);
        };
    });
})