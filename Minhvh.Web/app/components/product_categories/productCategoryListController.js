(function (app) {
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
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
            apiService.get("api/productcategory/getall",
                config,
                function (result) {
                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning("Không có bản ghi nào được tìm thấy.");
                    }
                    else {
                        notificationService.displaySuccess("Đã tìm thấy " + result.data.TotalCount + " bản ghi.");
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
        $scope.deleteProductCategory = deleteProductCategory;
        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có chắc muốn xóa ?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del("api/productcategory/delete", config, function () {
                    notificationService.displaySuccess("Xóa thành công");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công");
                });
            });
        }
        /* Xoá nhiều */
        $scope.$watch("productCategories",
            function (n, o) {
                var checked = $filter("filter")(n, { checked: true });
                if (checked.length) {
                    $scope.selected = checked;
                    $("#btnDelete").removeClass("hide");
                } else {
                    $("#btnDelete").addClass("hide");
                }
            }, true);
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories,
                    function (item) {
                        item.checked = true;
                    });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories,
                    function (item) {
                        item.checked = false;
                    });
                $scope.isAll = false;
            }
        }

        $scope.deleteMultiple = deleteMultiple;
        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected,
                function(i, item) {
                    listId.push(item.ID);
                });
            var config = {
                params: {
                    listId: JSON.stringify(listId)
                }
            }
            apiService.del("api/productcategory/DeleteMulti",config,function(result) {
                notificationService.displaySuccess("Xóa thành công " + result.data + " bản ghi.");
                search();
            },function() {
                notificationService.displayError("Xóa không thành công");
            });
        }

    }

    app.controller("productCategoryListController", productCategoryListController);

    productCategoryListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];
})(angular.module("minhvh.product_categories"));