(function (app) {
    function productUpdateController($scope, apiService, notificationService, $state, $stateParams, commonService, initJavascriptService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        initJavascriptService.iCheck();
        function loadProductDetail() {
            apiService.get('api/product/getbyid/' + $stateParams.id,
                null,
                function (result) {
                    $scope.product = result.data;
                },
                function (error) {
                    notificationService.displayError(error.data);
                });
        }
        loadProductDetail();

        function updateProduct() {
            apiService.put('api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        $scope.UpdateProduct = updateProduct;

        $scope.ckeditorOptions = {
            language: "vi",
            height: "200px"
        }

        $scope.chooseImage = chooseImage;
        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }

        function loadProductCategories() {
            apiService.get("api/productcategory/GetAllParents", null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('canot get list product category');
            });
        }
        loadProductCategories();

        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        $scope.GetSeoTitle = getSeoTitle;
    }

    app.controller('productUpdateController', productUpdateController);

    productUpdateController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "initJavascriptService"];

})(angular.module("minhvh.products"));