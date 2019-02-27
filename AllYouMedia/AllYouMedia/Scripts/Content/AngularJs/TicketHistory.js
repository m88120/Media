var myApp = angular.module("User", ['angularUtils.directives.dirPagination']);
myApp.controller("Ticket", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/User/TicketHistoryAngularJs',
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