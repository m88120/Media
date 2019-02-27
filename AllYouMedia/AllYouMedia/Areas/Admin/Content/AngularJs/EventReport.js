/// <reference path="../angular.min.js" />
var myApp = angular.module('Admin', ['angularUtils.directives.dirPagination']);
myApp.controller('Event', function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/EventReportByAngularJS',
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
        var EventName = $('#EventName');
        var CountryList = $('#CountryList');
        var StateList = $('#StateList');
        var City = $('#City');
        var Venue = $('#Venue');
        var Host = $('#Host');
        var Performance = $('#Performance');
        var SeatCapacity = $('#SeatCapacity');
        var Fee = $('#Fee');
        var DateFrom = $('#DateFrom');
        var DateTo = $('#DateTo');
        var TimeFrom = $('#TimeFrom');
        var TimeTo = $('#TimeTo');
        var Remark = $('#Remark');
        var MapUrl = $('#MapUrl');
        var file = $('#file');
        var Status = $('#Status');
        var ImageUrl = $('#ImageUrl');
        $http({
            url: '/admin/admin/EventEdit',
            method: "POST",
            data: { UID: AAA }
        }).success(function (response) {           
            EventName.val(response.EventName);
            CountryList.val(response.Country).change();           
            StateList.val(response.State);
            City.val(response.City);
            Venue.val(response.Venue);
            Host.val(response.Host);
            Performance.val(response.Performance);
            SeatCapacity.val(response.SeatCapacity);
            Fee.val(response.Fee);
            DateFrom.val(response.DateFrom);
            DateTo.val(response.DateTo);
            TimeFrom.val(response.TimeFrom);
            TimeTo.val(response.TimeTo);
            Remark.val(response.Remark);
            MapUrl.val(response.MapUrl);            
            $('#DivImageurl').removeClass("hidden");
            ImageUrl.attr('src', response.ImageUrl);
            Status.attr('checked', response.Status).change();
            $('#Save').val("Update");
            $('#Save').addClass("btn btn-success").removeClass("btn-primary");           
        })
    }   
    $scope.delete = function (id) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this Event Detail!",
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
        swal("Cancelled", "Your Event Detail is safe :)", "error");
    }
});

    }
    function deleteRecord(id) {
        $http({
            url: '/admin/admin/DeleteEventAngularJs',
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