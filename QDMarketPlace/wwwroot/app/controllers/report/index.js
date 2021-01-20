/// <reference path="../../../lib/angular/angular.js" />

angular.module("app", ["chart.js"]).controller("BarCtrl", function ($scope) {
    $scope.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    $scope.data = [
        [12, 6, 14, 7, 18, 0, 0, 0, 0, 18, 14, 9]
    ];

    var reports = [
        { Month: 1, Purchase: 1025, Revenue: 1035 },
        { Month: 2, Purchase: 500, Revenue: 604 },
        { Month: 3, Purchase: 1294, Revenue: 1390 },
        { Month: 4, Purchase: 856, Revenue: 914 },
        { Month: 5, Purchase: 2890, Revenue: 3021 },
        { Month: 6, Purchase: 0, Revenue: 0 },
        { Month: 7, Purchase: 0, Revenue: 0 },
        { Month: 8, Purchase: 0, Revenue: 0 },
        { Month: 9, Purchase: 0, Revenue: 0 },
        { Month: 10, Purchase: 2609, Revenue: 2861 },
        { Month: 11, Purchase: 1176, Revenue: 1403 },
        { Month: 12, Purchase: 902, Revenue: 1023 }
    ];

    var sumPur = 0;
    var sumRev = 0;
    for (var i = 0; i < reports.length; i++) {
        sumPur += reports[i].Purchase;
        sumRev += reports[i].Revenue;
    }
    $scope.reports = reports;
    $scope.sumPur = sumPur;
    $scope.sumRev = sumRev;
});
