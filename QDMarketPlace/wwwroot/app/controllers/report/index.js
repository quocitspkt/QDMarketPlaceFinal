/// <reference path="../../../lib/angular/angular.js" />

angular.module("app", ["chart.js"]).controller("BarCtrl", function ($scope) {
    $scope.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    //$scope.series = ['Series A', 'Series B'];

    $scope.data = [
        [22, 33, 26, 23, 49, 45, 36, 34, 38, 45, 31, 58]
    ];

    var reports = [
        { Month: 1, Purchase: 1025, Revenue: 1035 },
        { Month: 2, Purchase: 2985, Revenue: 3028 },
        { Month: 3, Purchase: 1268, Revenue: 1563 },
        { Month: 4, Purchase: 1097, Revenue: 1245 },
        { Month: 5, Purchase: 2890, Revenue: 3021 },
        { Month: 6, Purchase: 2584, Revenue: 2686 },
        { Month: 7, Purchase: 1956, Revenue: 2763 },
        { Month: 8, Purchase: 2386, Revenue: 2683 },
        { Month: 9, Purchase: 2195, Revenue: 2325 },
        { Month: 10, Purchase: 2609, Revenue: 2861 },
        { Month: 11, Purchase: 1976, Revenue: 2103 },
        { Month: 12, Purchase: 2986, Revenue: 3043 }
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

//angular.module("report",[]).controller("doanhthu", function ($scope) {
//    var reports = [
//        { Month: 1, Purchase: 1025, Revenue: 1035 },
//        { Month: 2, Purchase: 2985, Revenue: 3028 },
//        { Month: 3, Purchase: 1268, Revenue: 1563 },
//        { Month: 4, Purchase: 1097, Revenue: 1245 },
//        { Month: 5, Purchase: 2890, Revenue: 3021 },
//        { Month: 6, Purchase: 2584, Revenue: 2686},
//        { Month: 7, Purchase: 1956, Revenue: 2763},
//        { Month: 8, Purchase: 2386, Revenue: 2683},
//        { Month: 9, Purchase: 2195, Revenue: 2325},
//        { Month: 10, Purchase: 2609, Revenue: 2861},
//        { Month: 11, Purchase: 1976, Revenue: 2103},
//        { Month: 12, Purchase: 2986, Revenue: 3043}
//    ];

//    $scope.reports = reports;
//});