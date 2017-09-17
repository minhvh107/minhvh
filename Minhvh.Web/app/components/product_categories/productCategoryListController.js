(function (app) {
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.productCategories = [];
        $scope.keyword = '';

        /**
         * Load danh sách
         * @param {any} pageIndex
         * @param {any} pageSize
         */
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

                    $scope.tdSelected = result.data.Item[0];
                    $scope.setSelected = function (item) {
                        $scope.tdSelected = item;
                    }
                },
                function () {
                    console.log("load false");
                });
        }
        $scope.getProductCategories = getProductCategories;
        $scope.getProductCategories();

        /**
         * tìm kiếm
         */
        function search() {
            getProductCategories();
        }
        $scope.search = search;

        /*Xóa*/
        //$scope.deleteProductCategory = deleteProductCategory;
        //function deleteProductCategory(id) {
        //    $ngBootbox.confirm("Bạn có chắc muốn xóa ?").then(function () {
        //        var config = {
        //            params: {
        //                id: id
        //            }
        //        };
        //        apiService.post("api/productcategory/delete/", config, function () {
        //            notificationService.displaySuccess("Xóa thành công");
        //            search();
        //        }, function () {
        //            notificationService.displayError("Xóa không thành công");
        //        });
        //    });
        //}
    }

    app.controller("productCategoryListController", productCategoryListController);

    productCategoryListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox"];
})(angular.module("minhvh.product_categories"));