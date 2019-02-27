/// <reference path="../angular.min.js" />
var myApp = angular.module('Admin', ['angularUtils.directives.dirPagination']);
myApp.controller('EventPerformer', function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/EventPerformerReportByAngularJS',
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
    //Working After Click
    $scope.Search = function () {
        loadRecords();
    };
    $scope.edit = function (UID) {
        var AAA = UID;
        var EventList = $('#EventList');
        var PerformerName = $('#PerformerName');
        var Category = $('#Category');
        var Fee = $('#Fee');
        var Description = $('#Description');
        var Date = $('#Date');        
        var TimeFrom = $('#TimeFrom');
        var TimeTo = $('#TimeTo');       
        var ImageUrl = $('#ImageUrl');
        var file = $('#file');
        $http({
            url: '/admin/admin/EventPerformerEdit',
            method: "POST",
            data: { UID: AAA }
        }).success(function (response) {           
            EventList.val(response.EventName).change();
            PerformerName.val(response.PerformerName);
            Category.val(response.Category);
            Fee.val(response.Fee);
            Description.val(response.Description);
            Date.val(response.Date);            
            TimeFrom.val(response.TimeFrom);
            TimeTo.val(response.TimeTo);          
            $('#DivImageurl').removeClass("hidden");
            ImageUrl.attr('src', response.ImageUrl);           
            $('#Save').val("Update");
            $('#Save').addClass("btn btn-success").removeClass("btn-primary");
        })
    }
    $scope.delete = function (id) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this Performer Detail!",
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
        swal("Cancelled", "Your Performer Detail is safe :)", "error");
    }
});

    }
    function deleteRecord(id) {
        $http({
            url: '/admin/admin/DeleteEventPerformerAngularJs',
            method: "POST",
            data: { UID: id }
        }).success(function (response) {
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