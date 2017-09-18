
(function () {
    angular.module('minhvh.products', ['minhvh.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products',
            {
                url: "/products",
                templateUrl: "/app/components/products/productListView.html",
                controller: "productListController"
            }).state('create_product',
            {
                url: "/create_product",
                templateUrl: "/app/components/products/productCreateView.html",
                controller: "productCreateController"
            });
    }

})();