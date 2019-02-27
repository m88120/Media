var myApp = angular.module("User", ['angularUtils.directives.dirPagination', 'ngSanitize']);
myApp.controller("TalentSearch", function ($scope, $http) {   
    $scope.loader = {
        loading: false,
    };
    $scope.CountryList = [];
    $scope.TalentCategoryList = [];
    $scope.StateList = [];
    $scope.TalentSubCategoryList = [];

    FillCountry();
    function FillCountry() {
        $http({
            method: 'POST',
            url: '/User/PopulateCountryAngularJs',
            data: {}
        }).success(function (response1) {
            if (response1 != null || response1 != "undefined") {
                $scope.CountryList = response1;
            }
        })
    };

    FillState();
    function FillState() {
        $http({
            method: 'POST',
            url: '/User/PopulateStateAngularJs',
            data: { Country_UID: $scope.Country }
        }).success(function (response5) {
            if (response5 != null || response5 != "undefined") {
                $scope.StateList = response5;
            }
        })
    };

    FillTalentCategoryList();
    function FillTalentCategoryList() {
        $http({
            method: 'POST',
            url: '/User/PopulateTalentCategoryAngularJs',
            data: {}
        }).success(function (response2) {
            if (response2 != null || response2 != "undefined") {
                $scope.TalentCategoryList = response2;
            }
        })
    };

    FillTalentSubCategoryList();
    function FillTalentSubCategoryList() {
        $http({
            method: 'POST',
            url: '/User/PopulateTalentSubCategoryAngularJs',
            data: { TalentCategory: $scope.TalentCategory }
        }).success(function (response6) {
            if (response6 != null || response6 != "undefined") {
                $scope.TalentSubCategoryList = [];
                $scope.TalentSubCategoryList = response6;
            }
        })
    };


    //Working onLoad
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();
    function loadRecords() {
        $http({
            url: '/User/TalentSearchAngularJs',
            method: "POST",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: { TalentLoginName: $scope.TalentLoginName, Country: $("#Country option:selected").text(), State: $("#State option:selected").text(), TalentCategory: $scope.TalentCategory, TalentSubCategory: $scope.TalentSubCategory }
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

    $scope.Search = function () {
        loadRecords();
    }

    $scope.getstate = function () {
        FillState();
    }

    $scope.getsubcategory = function () {
        FillTalentSubCategoryList();
    }

    $scope.edit = function (LoginName) {
        var AAA = LoginName;
        var LoginName = $('#Contact_LoginName');
        LoginName.val(AAA);
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
