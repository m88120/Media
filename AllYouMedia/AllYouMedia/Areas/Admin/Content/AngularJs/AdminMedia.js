var myApp = angular.module("Admin", ['angularUtils.directives.dirPagination']);
myApp.controller("AdminMedia", function ($scope, $http, $window) {
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
            url: '/Admin/Admin/AdminMediaAngularJs',
            method: "POST",
            data: {}
        }).success(function (response) {
            $scope.loader.loading = false;
            if (response != null || response != "undefined") {
                $scope.dataList = response;
                for (var i = 0; i < $scope.dataList.length; i++) {                                    
                    if ($scope.dataList[i].Name != "") {
                        $scope.isdellist.push(false);
                    }
                    else {
                        $scope.isdellist.push(true);                       
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
    $scope.delete = function (id, url) {
        swal({
            title: "Are you sure?",
            text: "Delete this Product!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel Please!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
function (isConfirm) {
    if (isConfirm) {
        deleteRecord(id, url);
    } else {
        swal("Cancelled", "Your Product is safe :)", "error");
    }
});

    }
    function deleteRecord(id, url) {
        $http({
            url: '/Admin/Admin/DeleteAdminMediaAngularJs',
            method: "POST",
            data: { UID: id, MediaUrl: url }
        }).success(function (response) {
            $scope.loader.loading = false;
            swal("Deleted!", "Your Product has been Deleted.", "success");
            location.reload();
        })
    };
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