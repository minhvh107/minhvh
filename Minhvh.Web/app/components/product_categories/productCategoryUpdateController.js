(function (app) {
    function productCategoryUpdateController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        function loadProductCategoryDetail() {
            apiService.get('api/productcategory/getbyid/' + $stateParams.id,
                function(result) {
                    $scope.productCategory = result.data;
                },function(error) {
                    notificationService.displayError(error.data);
                });
        }

        function updateProductCategory() {
            apiService.post('api/productcategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật.");
                $state.go('product_categories');
            }, function () {
                notificationService.displayError("Cập nhật không thành công.");
            });
        }

        $scope.UpdateProductCategory = updateProductCategory;

        //function loadParentCategory() {
        //    apiService.get('api/productcategory/getallparents', null, function (result) {
        //        $scope.parentCategories = result.data;
        //    }, function () {
        //        console.log('canot get list parents');
        //    });
        //}

        //loadParentCategory();
        loadProductCategoryDetail();
    }

    app.controller('productCategoryUpdateController', productCategoryUpdateController);

    productCategoryUpdateController.$inject = ['$scope', 'apiService', 'notificationService', '$state','$stateParams'];

})(angular.module('minhvh.product_categories'));