
(function () {
    angular.module('minhvh.products', ['minhvh.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/product",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('product_create', {
            url: "/product_create",
            templateUrl: "/app/components/products/productCreateView.html",
            controller: "productCreateController"
        })
    }

})();