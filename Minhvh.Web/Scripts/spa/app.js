var myApp = angular.module('myModule', []);

myApp.controller("schoolController", schoolController);
myApp.service("validator", validator);

schoolController.$inject =['$scope', 'validator'];

function schoolController($scope, validator) {
    validator.checkNumber(1);
    //$scope.message = "Ahihi";
}


function validator($window) {
    function checkNumber(input) {
        if (input % 2 == 0) {
            $window.alert("day la so chan");
        } else {
            $window.alert("day la so le");
        }
    }

    return {
        checkNumber: checkNumber
    }
}