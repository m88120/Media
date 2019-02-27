var myApp = angular.module("User", ['angularUtils.directives.dirPagination', 'ngSanitize']);
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
            url: '/User/InboxAngularJs',
            method: "POST",
            data: { }
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
    $scope.Reply = function (id) {
        ReplyRecord(id);
    }
    $scope.Send = function () {       
        var LoginName = $scope.Reply_LoginName;
        var Subject = $scope.Reply_Subject;
        var Message = $scope.Reply_Message;
        if (LoginName == "") {
            $("#E_LoginName").append("Please enter LoginName!");
            return false;
        }
        if (Subject == "") {
            $("#E_Subject").append("Please enter Subject!");
            return false;
        }
        if (Message == "") {
            $("#E_Message").append("Please enter Message!");
            return false;
        }
        SendRecord();
    }
    $scope.Cancel = function () {
        $('#ReadMessage1').addClass('hidden');
        $('#ReplyMessage').addClass('hidden');
    }   
    function ReadRecord(id) {
        var ID = id;
        var SentDate = $('#SentDate');
        var Subject = $('#Subject');
        var Message = $('#Message');
        $http({
            url: '/User/MessageReadInboxAngularJs',
            method: "POST",
            data: { UID: ID }
        }).success(function (response) {
            $scope.SentDate = response.SentDate;
            SentDate.html(response.SentDate);
            Subject.html(response.Subject);
            Message.html(response.Message);
            $('#ReadMessage1').removeClass('hidden');
        })
    };
    function ReplyRecord(id) {
        var ID = id;
        var Reply_LoginName = $('#Reply_LoginName');
        var Reply_Subject = $('#Reply_Subject');
        var Reply_Message = $('#Reply_Message');
        $http({
            url: '/User/MessageReadInboxAngularJs',
            method: "POST",
            data: { UID: ID }
        }).success(function (response) {
            $scope.Reply_LoginName = response.LoginName;
            Reply_LoginName.val(response.LoginName);
            Reply_Subject.val("Re:  " + response.Subject);
            Reply_Message.html(response.Message);
            $('#ReplyMessage').removeClass('hidden');
        })
    };
    function SendRecord() {
        $http({
            url: '/User/MessageAngularJs',
            method: "POST",
            data: { LoginName: $scope.Reply_LoginName, Subject: $scope.Reply_Subject, Message: $scope.Reply_Message }
        }).success(function (response) {
            if (response.flag == "1") {
                swal("Sent!", response.msg, "success");
                location.reload();
            }
            else if (response.flag == "0") {               
                swal("Error!", response.msg, "error");
            }
        })
    };
    $scope.numLimit = 300;
    $scope.currentPage = 1;
    $scope.pageSize = 100;
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
            url: '/User/OutboxAngularJs',
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
            text: "Delete this Message!",
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
            url: '/User/DeleteOutboxAngularJs',
            method: "POST",
            data: { UID: id }
        }).success(function (response) {
            $scope.loader.loading = false;
            swal("Deleted!", "Your message has been Deleted.", "success");
            loadRecords();
        })
    };
    $scope.numLimit = 300;
    $scope.currentPage = 1;
    $scope.pageSize = 100;
    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };
    $(document).ready(function () {
        $scope.pageChangeHandler = function (num) {
            console.log('going to page ' + num);
        };
    });
})
myApp.controller("ChatRoom", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/User/ChatRoomAngularJs',
            method: "GET",
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
    $scope.Reply = function (id) {
        ReplyRecord(id);
    }
    $scope.Send = function () {           
        var Message = $scope.SendMessage;
        if (Message == "" || Message == null) {
            swal("Error!", "Please enter Message!", "error");          
            return false;
        }
        else
        {
            swal("Sent!", "Congrats", "success");
        }       
    }
    $scope.Cancel = function () {
        $('#ReadMessage1').addClass('hidden');
        $('#ReplyMessage').addClass('hidden');
    }      
    function SendRecord() {
        $http({
            url: '/User/MessageAngularJs',
            method: "POST",
            data: { LoginName: $scope.Reply_LoginName, Subject: $scope.Reply_Subject, Message: $scope.Reply_Message }
        }).success(function (response) {
            if (response.flag == "1") {
                swal("Sent!", response.msg, "success");
                loadRecords();
            }
            else if (response.flag == "0") {
                swal("Error!", response.msg, "error");
            }
        })
    };
    $scope.numLimit = 120;
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