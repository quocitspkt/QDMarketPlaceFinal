/// <reference path="../../../lib/angular/angular.js" />
angular.module("pro", ["chart.js"]).controller("ProCtrl", function ($scope) {
    $scope.labels = [35, 30, 33, 32, 31, 29, 36, 23, 24, 25, 29];
    $scope.data = [[2, 1, 3, 1, 3, 1, 3, 1, 2, 4, 3]];

});