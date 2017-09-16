
(function () {
    angular.module('minhvh.product_categories', ['minhvh.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories',
            {
                url: "/product_categories",
                templateUrl: "/app/components/product_categories/productCategoryListView.html",
                controller: "productCategoryListController"
            })
            .state('create_product_category',
            {
                url: "/create_product_category",
                templateUrl: "/app/components/product_categories/productCategoryCreateView.html",
                controller: "productCategoryCreateController"
            })
            .state('update_product_category',
            {
                url: "/update_product_category/:id",
                templateUrl: "/app/components/product_categories/productCategoryUpdateView.html",
                controller: "productCategoryUpdateController"
            });
    }

})();