(function (app) {
    function productCreateController($scope, apiService, notificationService, $state, commonService, initJavascriptService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        initJavascriptService.iCheck();
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

        function createProduct() {
            apiService.post("api/product/create/", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
                $state.go('products');
            }, function () {
                notificationService.displayError("Thêm mới không thành công.");
            });
        }
        $scope.CreateProduct = createProduct;

        $scope.ckeditorOptions = {
            language: "vi",
            height: "200px"
        }

        $scope.chooseImage = chooseImage;
        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function(fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }
    }

    app.controller('productCreateController', productCreateController);

    productCreateController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "initJavascriptService"];

})(angular.module('minhvh.products'));