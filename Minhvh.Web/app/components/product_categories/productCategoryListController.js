(function (app) {
    app.controller("productCategoryListController", productCategoryListController);

    productCategoryListController.$inject = ["$scope", "apiService","notificationService"];
    function productCategoryListController($scope, apiService, notificationService) {
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
                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                    }
                    else {
                        notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                    }
                    $scope.productCategories = result.data.Item;
                    $scope.pageIndex = result.data.PageIndex;
                    $scope.pageSize = result.data.TotalCount > pageSize ? pageSize : result.data.TotalCount;
                    $scope.totalPages = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                    
                },
                function () {
                    console.log("load false");
                });
            if ($scope.$last === true) {
                console.log("11");
            }
        }
        
    }
})(angular.module("minhvh.product_categories"));