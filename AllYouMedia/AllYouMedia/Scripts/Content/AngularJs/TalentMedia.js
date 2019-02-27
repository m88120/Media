var myApp = angular.module("User", ['angularUtils.directives.dirPagination']);
myApp.controller("TalentMedia", function ($scope, $http, $window) {
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
            url: '/User/TalentMediaAngularJs',
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
            text: "Delete this Media!",
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
        swal("Cancelled", "Your Media is safe :)", "error");
    }
});
    }
    function deleteRecord(id, url) {
        $http({
            url: '/User/DeleteTalentMediaAngularJs',
            method: "POST",
            data: { UID: id, MediaUrl: url }
        }).success(function (response) {
            $scope.loader.loading = false;
            swal("Deleted!", "Your Image has been Deleted.", "success");
            location.reload();
        })
    };

    $scope.edit = function (id, url, Type) {
        var Category = $('.Category');
        var SubCategory = $('.SubCategory');
        var ImageCategory = $('.ImageCategory');
        var ImageSubCategory = $('.ImageSubCategory');
        var FileName = $('.FileName');
        var Description = $('.Description');
        var Genre = $('.Genre');
        var Price = $('.Price');
        var PriceRate = $('.PriceRate');
        var files = $('.files');
        var ImageUrl = $('.ImageUrl');
        var AlbumName = $('.AlbumName');
        var AlbumDescription = $('.AlbumDescription');
        var AlbumImageUrl = $('.AlbumImageUrl');
        $http({
            url: '/User/EditTalentMediaAngularJs',
            method: "POST",
            data: { UID: id, MediaUrl: url, MediaType: Type }
        }).success(function (response) {
            $scope.loader.loading = false;
            Category.val(response.Category);
            SubCategory.val(response.SubCategory);
            ImageCategory.val(response.Category);
            ImageSubCategory.val(response.ImageSubCategory);
            FileName.val(response.FileName);
            Description.val(response.Description);
            Genre.val(response.Genre);
            Price.val(response.Price);
            PriceRate.val(response.Price);
            AlbumName.val(response.AlbumName);
            AlbumDescription.val(response.AlbumDescription);
            $('.DivImageurl').removeClass("hidden");
            if (response.CoverImageUrl == null) {
                ImageUrl.attr('src', response.MediaFileUrl);
            }
            else {
                ImageUrl.attr('src', response.CoverImageUrl);
            }
            if (Type == "Audio") {
                $("#Audio").removeClass("hidden");
                if (response.AlbumName == '') {
                    $("#divSingleInfo").removeClass("hidden");
                    $("#divAlbums").addClass("hidden");
                    $('#single').attr('checked', true);
                    $("#divSingles").css("display", "block");
                    $("#divAlbums").css("display", "none");
                    $('#btnSubmit').val("Update");
                }
                else if (response.AlbumName != '') {
                    $("#divAlbums").removeClass("hidden");
                    $("#divSingleInfo").addClass("hidden");
                    $('#album').attr('checked', true);
                    $("#divAlbums").css("display", "block");
                    $("#divSingles").css("display", "none");
                    $('.DivAlbumImageurl').removeClass("hidden");
                    if (response.AlbumCoverImage != null) {
                        AlbumImageUrl.attr('src', response.AlbumCoverImage);
                    }
                    $('#btnAlbumSubmit').val("Update Album");
                    $('#btnAlbumSubmit').addClass("btn btn-success").removeClass("btn-primary");
                }

                $("#Film").addClass("hidden");
                $("#Ebook").addClass("hidden");
                $("#Image").addClass("hidden");
            }
            else if (Type == "Video") {
                $("#Film").removeClass("hidden");
                $("#Audio").addClass("hidden");
                $("#Ebook").addClass("hidden");
                $("#Image").addClass("hidden");
                $('#btnSubmit').val("Update");
            }
            else if (Type == "Document") {
                $("#Ebook").removeClass("hidden");
                $("#Audio").addClass("hidden");
                $("#Film").addClass("hidden");
                $("#Image").addClass("hidden");
                $('#btnSubmit').val("Update");
            }
            else if (Type == "Image") {
                $("#Image").removeClass("hidden");
                $("#Audio").addClass("hidden");
                $("#Film").addClass("hidden");
                $("#Ebook").addClass("hidden");
                ImageUrl.attr('src', response.MediaFileUrl);
                $('#btnSubmit').val("Update");
            }
            $('#btnSubmit').addClass("btn btn-success").removeClass("btn-primary");
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