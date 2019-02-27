/// <reference path="../angular.min.js" />
var myApp = angular.module('Admin', ['angularUtils.directives.dirPagination']);
myApp.controller('Category', function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/CategoryReportByAngularJS',
            method: "POST",
            data: {}
        }).then(function (response) {
            $scope.loader.loading = false;
            if (response.status == 200) {
                $scope.dataList = response.data;
                $('#tblData').css({
                    'display': ''
                });
            }
        })
    };
    //Working After Click
    $scope.Search = function () {
        loadRecords();
    };
    $scope.edit = function (ID) {
        var CategoryTypeID = $('#CategoryTypeID');
        var CategoryName = $('#CategoryName');
        $http({
            url: '/admin/admin/CategoryEdit',
            method: "POST",
            data: { ID: ID }
        }).then(function (response) {
            CategoryTypeID.val(response.data.CategoryTypeID);
            CategoryName.val(response.data.CategoryName);
            $('#Save').val("Update");
            $('#Save').addClass("btn btn-success").removeClass("btn-primary");
        })
    }

    //For Paging
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
});
myApp.controller('SubCategory', function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/SubCategoryReportByAngularJS',
            method: "POST",
            data: {}
        }).then(function (response) {
            $scope.loader.loading = false;
            if (response.status==200) {
                $scope.dataList = response.data;
                $('#tblData').css({
                    'display': ''
                });
            }
        })
    };
    //Working After Click
    $scope.Search = function () {
        loadRecords();
    };
    $scope.edit = function (UID) {
        var AAA = UID;       
        var MenuList = $('#MenuList');
        var SubMenuList = $('#SubMenuList');
        var SubCategoryName = $('#SubCategoryName');
        $http({
            url: '/admin/admin/SubCategoryEdit',
            method: "POST",
            data: { UID: AAA }
        }).then(function (response) {
            MenuList.val(response.data.Menu);
            loadSubMenu(MenuList.val());            
            SubCategoryName.val(response.data.SubCategoryName);
            $('#Save').val("Update");
            $('#Save').addClass("btn btn-success").removeClass("btn-primary");           
            SubMenuList.val(response.data.SubMenu);
        })
    }
    function loadSubMenu(Category) {
        $("#SubMenuList").empty();
        $http({
            url: '/admin/admin/GetTalentSubCategory',
            method: "POST",
            data: { TalentCategory: Category }
        }).then(function (response) {
            var state = response.data;
            for (var i = 0; i <= state.data.length; i++) {               
                $("#SubMenuList").append('<option value="' + state[i].Value + '">' +
               state[i].Text + '</option>');
            }            
        })
    };
    //For Paging
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
});
myApp.controller('SuggestedCategory', function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/SuggestCategoryReportByAngularJS',
            method: "POST",
            data: {}
        }).then(function (response) {
            $scope.loader.loading = false;
            if (response.status == 200) {
                $scope.dataList = response.data;
                $('#tblData').css({
                    'display': ''
                });
            }
        })
    };
    //Working After Click
    $scope.Search = function () {
        loadRecords();
    };
    $scope.edit = function (UID) {
        var AAA = UID;
        var MenuList = $('#MenuList');
        var CategoryName = $('#CategoryName');
        $http({
            url: '/admin/admin/SuggestCategoryEdit',
            method: "POST",
            data: { UID: AAA }
        }).then(function (response) {
            MenuList.val(response.data.Menu);
            CategoryName.val(response.data.CategoryName);
            $('#Save').addClass("btn btn-success").removeClass("btn-primary hidden");
        })
    }
    $scope.delete = function (id) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this!",
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
        deleteRecord(id);
    } else {
        swal("Cancelled", "Category is safe :)", "error");
    }
});

    }
    function deleteRecord(id) {
        $http({
            url: '/admin/admin/SuggestCategoryDelete',
            method: "POST",
            data: { UID: id }
        }).then(function (response) {
            $scope.loader.loading = false;
            location.reload();
        })
    };

    //For Paging
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
});