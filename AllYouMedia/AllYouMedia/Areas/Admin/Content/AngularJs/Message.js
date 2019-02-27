var myApp = angular.module("Admin", ['angularUtils.directives.dirPagination']);
myApp.controller("Inbox", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/InboxAngularJs',
            method: "POST",
            data: {}
        }).success(function (response) {
            $scope.loader.loading = false;
            if (response != null || response != "undefined") {
                
                $scope.dataList = response;
                $('#tblDataInbox').css({
                    'display': ''
                });
            }
        })       
    };

    $scope.Search = function () {
        loadRecords();
    }

    $scope.messageList = [];
    $scope.Read = function (id) {
        ReadRecord(id);
    }
    $scope.Cancel = function () {
        $('#ReadMessage1').addClass('hidden');
    }
    function ReadRecord(id) {
        var ID = id;
        var SentDate = $('#SentDate');
        var Subject = $('#Subject');
        var Message = $('#Message');
        $http({
            url: '/admin/admin/MessageReadInboxAngularJs',
            method: "POST",
            data: { UID: ID }
        }).success(function (response) {
            $scope.SentDate = response.SentDate;
            SentDate.val(response.SentDate);
            Subject.val(response.Subject);
            Message.val(response.Message);
            $('#ReadMessage1').removeClass('hidden');
        })
    };
    $scope.numLimit = 120;
    $scope.currentPage = 1;
    $scope.pageSize = 50;
    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };
    $(document).ready(function () {
        $scope.pageChangeHandler = function (num) {
            console.log('going to page ' + num);
        };
    });
})
myApp.controller("Outbox", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };

    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/OutboxAngularJs',
            method: "POST",
            data: {}
        }).success(function (response) {
            $scope.loader.loading = false;
            if (response != null || response != "undefined") {
                $scope.dataList = response;
                $('#tblDataOutbox').css({
                    'display': ''
                });
            }
        })
    };
    $scope.Search = function () {
        loadRecords();
    }
    $scope.delete = function (id) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this Message!",
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
        swal("Cancelled", "Your message is safe :)", "error");
    }
});

    }
    function deleteRecord(id) {
        $http({
            url: '/admin/admin/DeleteOutboxAngularJs',
            method: "POST",
            data: { UID: id }
        }).success(function (response) {
            $scope.loader.loading = false;
            location.reload();
        })
    };
    $scope.numLimit = 120;
    $scope.currentPage = 1;
    $scope.pageSize = 50;
    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };
    $(document).ready(function () {
        $scope.pageChangeHandler = function (num) {
            console.log('going to page ' + num);
        };
    });
})