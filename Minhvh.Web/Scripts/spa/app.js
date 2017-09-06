

var myApp = angular.module('myModule', []);

myApp.controller("schoolController", schoolController);

function schoolController($scope) {
    $scope.message = "Ahihi";
}