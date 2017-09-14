(function (app) {
    function productCategoryCreateController($scope, apiService, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        function CreateProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
                $state.go('product_categories');
            }, function () {
                notificationService.displayError("Thêm mới không thành công.");
            });
        }

        $scope.CreateProductCategory = CreateProductCategory;

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('canot get list parents');
            });
        }

        loadParentCategory();
    }

    app.controller('productCategoryCreateController', productCategoryCreateController);

    productCategoryCreateController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

})(angular.module('minhvh.product_categories'));