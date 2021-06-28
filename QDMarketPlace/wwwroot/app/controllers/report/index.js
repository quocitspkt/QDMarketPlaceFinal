/// <reference path="../../../lib/angular/angular.js" />

angular.module("app", ["chart.js"]).controller("BarCtrl", function ($scope) {
    $scope.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']; $scope.data = [[0,0,0,2,2,6,0,0,0,0,0,0]];
    //$.ajax({
    //    url: "/Admin/Home/GetCountInMont",
    //    dataType: "json",
    //    type: "GET",
    //    contentType: 'application/json; charset=utf-8',
    //    async: true,
    //    processData: false,
    //    cache: false,
    //    success: function (data) {
    //        $scope.data = data;
    //    },
    //    error: function (xhr) {
    //        //alert('error');
    //    }
    //});

});

//angular.module("pro", ["chart.js"]).controller("ProCtrl", function ($scope) {
//    $scope.label = [35, 30, 33, 32, 31, 29, 36, 23, 24, 25, 29]; $scope.datament = [[2,1,3,1,3,1,3,1,2,4,3]];

//});
