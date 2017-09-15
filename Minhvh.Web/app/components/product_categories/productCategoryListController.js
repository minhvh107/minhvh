(function (app) {
    function productCategoryListController($scope, apiService, notificationService, initJavascriptService, $timeout) {
        $scope.productCategories = [];
        $scope.keyword = '';
        initJavascriptService.init();
        $timeout(function() {
            $('#listContent').find('tbody tr:first').addClass('selected');
        }, 3000);
     
        function getProductCategories(pageIndex, pageSize) {
            pageIndex = pageIndex || 0;
            pageSize = pageSize || 20;
            var config = {
                params: {
                    keyword: $scope.keyword,
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
                    initJavascriptService.initTable();
                    
                },
                function () {
                    console.log("load false");
                });
        }

        function search() {
            getProductCategories();
        }


        $scope.search = search;
        $scope.getProductCategories = getProductCategories;
        $scope.getProductCategories();
        


    }

    app.controller("productCategoryListController", productCategoryListController);

    productCategoryListController.$inject = ["$scope", "apiService", "notificationService", "initJavascriptService", "$timeout"];
})(angular.module("minhvh.product_categories"));