﻿(function (app) {
    function productCategoryCreateController($scope, apiService, notificationService, $state, commonService, initJavascriptService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        initJavascriptService.iCheck();
        function loadParentCategory() {
            apiService.get("api/productcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('canot get list parents');
            });
        }
        loadParentCategory();

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        $scope.GetSeoTitle = getSeoTitle;

        function createProductCategory() {
            apiService.post("api/productcategory/create/", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
                $state.go('product_categories');
            }, function () {
                notificationService.displayError("Thêm mới không thành công.");
            });
        }
        $scope.CreateProductCategory = createProductCategory;
    }

    app.controller('productCategoryCreateController', productCategoryCreateController);

    productCategoryCreateController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService","initJavascriptService"];

})(angular.module('minhvh.product_categories'));