var myApp = angular.module("Admin", ['angularUtils.directives.dirPagination', 'ngSanitize']);
myApp.controller("Distributor", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    $scope.MajorList = [];
    $scope.StatusList = [];
    //$scope.UserList = [];
    $scope.UserData = {
        UserList: []
    };
    FillMajor();
    function FillMajor() {
        $http({
            method: 'POST',
            url: '/Admin/Admin/PopulateMajorAngularJs',
            data: {}
        }).then(function (response1) {
            if (response1.status == 200) {
                $scope.MajorList = response1.data;
            }
        })
    };
    FillStatus();
    function FillStatus() {
        $http({
            method: 'POST',
            url: '/Admin/Admin/PopulateStatusAngularJs',
            data: {}
        }).then(function (response1) {
            if (response1.status == 200) {
                $scope.StatusList = response1.data;
            }
        })
    };
    FillUser();
    function FillUser() {
        $http({
            method: 'POST',
            url: '/Admin/Admin/PopulateUserAngularJs',
            data: {}
        }).then(function (response1) {
            if (response1.status == 200) {
                $scope.UserData.UserList = response1.data;
                $scope.UserData.UserID = $scope.UserData.UserList[0].Value;
                loadRecords();
            }
        })
    };

    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;

    function loadRecords() {
        debugger;
        $http({
            url: '/admin/admin/DistributorReportAngularJs',
            method: "POST",
            data: { DateFrom: $scope.From, DateTo: $scope.To, UserID: $scope.UserData.UserID, City: $scope.City, Major: $scope.Major, Country: $scope.Country, College: $scope.College, Status: $scope.Status }
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

    $scope.Search = function () {
        //$scope.MajorList = [];
        //$scope.StatusList = [];
        //$scope.UserList = [];
        //FillMajor();
        //FillStatus();
        //FillUser();
        loadRecords();
    }

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
myApp.controller("Membership", function ($scope, $http) {
    $scope.loader = {
        loading: false,
    };
    $scope.TalentData = { TalentList: [] };
    $scope.UserData = { UserList: [] };
    FillTalent();
    function FillTalent() {
        $http({
            method: 'POST',
            url: '/Admin/Admin/PopulateTalentAngularJs',
            data: {}
        }).then(function (response1) {
            if (response1.stauts == 200) {
                $scope.TalentData.TalentList = response1.data;
                $scope.TalentData.Talent = $scope.TalentData.TalentList[0].Value;
            }
        })
    };
    FillUser();
    function FillUser() {
        $http({
            method: 'POST',
            url: '/Admin/Admin/PopulateUserAngularJs',
            data: {}
        }).then(function (response1) {
            if (response1.status == 200) {
                $scope.UserData.UserList = response1.data;
                $scope.UserData.UserID = $scope.UserData.UserList[0].Value;
            }
        })
    };

    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();

    function loadRecords() {
        $http({
            url: '/admin/admin/MembershipReportAngularJs',
            method: "POST",
            data: { DateFrom: $scope.From, DateTo: $scope.To, UserID: $scope.UserData.UserID, Talent: $scope.TalentData.Talent }
        }).then(function (response) {
            $scope.loader.loading = false;
            if (response.stauts == 200) {
                $scope.dataList = response.data;
                $('#tblData').css({
                    'display': ''
                });
            }
        })
    };

    $scope.Search = function () {
        $scope.TalentList = [];
        FillTalent();
        loadRecords();
    }

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
