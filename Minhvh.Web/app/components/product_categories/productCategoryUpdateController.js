(function (app) {
    function productCategoryUpdateController($scope, apiService, notificationService, $state, $stateParams, commonService, initJavascriptService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        initJavascriptService.iCheck();
        function loadProductCategoryDetail() {
            apiService.get('api/productcategory/getbyid/' + $stateParams.id,
                null,
                function (result) {
                    $scope.productCategory = result.data;
                },
                function (error) {
                    notificationService.displayError(error.data);
                });
        }
        loadProductCategoryDetail();

        function updateProductCategory() {
            apiService.put('api/productcategory/update', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        $scope.UpdateProductCategory = updateProductCategory;

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents',
                null,
                function (result) {
                    $scope.parentCategories = result.data;
                },
                function () {
                    console.log('canot get list parents');
                });
        }
        loadParentCategory();

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        $scope.GetSeoTitle = getSeoTitle;
    }

    app.controller('productCategoryUpdateController', productCategoryUpdateController);

    productCategoryUpdateController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService","initJavascriptService"];

})(angular.module("minhvh.product_categories"));