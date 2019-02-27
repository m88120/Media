var myApp = angular.module("User", ['angularUtils.directives.dirPagination', 'ngSanitize']);
myApp.controller("CustomerHistory", function ($scope, $http, $window) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();
    $scope.isdellist = [];

    function loadRecords() {
        $http({
            url: '/User/CustomerHistoryAngularJs',
            method: "POST",
            data: { }
        }).success(function (response) {
            $scope.loader.loading = false;
            if (response != null || response != "undefined") {
                $scope.dataList = response;
                for (var i = 0; i < $scope.dataList.length; i++) {
                    if ($scope.dataList[i].ShipCount != "") {
                        $scope.isdellist.push(true);
                    }
                    else {
                        $scope.isdellist.push(false);
                    }
                }
                $('#tblData').css({
                    'display': ''
                });
            }
        })
    };

    $scope.search = function () {
        $scope.isdellist = [];
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