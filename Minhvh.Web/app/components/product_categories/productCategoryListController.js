(function (app) {
    app.controller("productCategoryListController", productCategoryListController);

    productCategoryListController.$inject = ["$scope", "apiService"];
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.keyword = '';

        $scope.search = search;
       
        $scope.getProductCategories = getProductCategories;
        $scope.getProductCategories();

        function search() {
            getProductCategories();
        }

        function getProductCategories(pageIndex,pageSize) {
            pageIndex = pageIndex || 0;
            pageSize = pageSize || 20;
            var config = {
                params: {
                    keyword:$scope.keyword,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                }
            }
            apiService.get('/api/productcategory/getall',
                config,
                function (result) {
                    $scope.productCategories = result.data.Item;
                    $scope.pageIndex = result.data.PageIndex;
                    $scope.pageSize = result.data.TotalCount > pageSize ? pageSize : result.data.TotalCount;
                    $scope.totalPages = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log("load false");
                });
        }
        
    }
})(angular.module("minhvh.product_categories"));