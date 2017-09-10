(function (app) {
    'use strict';

    app.directive('pagerDirective', pagerDirective);

    function pagerDirective() {
        return {
            scope: {
                pageIndex: '@',
                pageSize: '@',
                totalPages: '@',
                totalCount: '@',
                searchFunc: '&',
                customPath: '@'
            },
            replace: true,
            restrict: 'E',
            templateUrl: '/app/shared/directives/pagerDirective.html',
            controller: [
                '$scope', function ($scope) {
                    $scope.search = function (i) {
                        if ($scope.searchFunc) {
                            $scope.searchFunc({ pageIndex: i });
                        }
                    };

                    $scope.range = function () {
                        if (!$scope.totalPages) { return []; }
                        var step = 2;
                        var doubleStep = step * 2;
                        var start = Math.max(0, $scope.pageIndex - step);
                        var end = start + 1 + doubleStep;
                        if (end > $scope.totalPages) { end = $scope.totalPages; }

                        var ret = [];
                        for (var i = start; i != end; ++i) {
                            ret.push(i);
                        }

                        return ret;
                    };

                    $scope.pagePlus = function (count) {
                        return +$scope.pageIndex + count;
                    }

                }]
        }
    }

})(angular.module('minhvh.common'));