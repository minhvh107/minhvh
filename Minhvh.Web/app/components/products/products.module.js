
(function () {
    angular.module('minhvh.products', ['minhvh.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products',
            {
                url: "/products",
                parent: "base",
                templateUrl: "/app/components/products/productListView.html",
                controller: "productListController"
            })
            .state("create_product",
            {
                url: "/create_product",
                parent: "base",
                templateUrl: "/app/components/products/productCreateView.html",
                controller: "productCreateController"
            })
            .state("update_product",
                {
                    url: "/update_product/:id",
                    parent: "base",
                    templateUrl: "/app/components/products/productUpdateView.html",
                    controller: "productUpdateController"
                });
    }

})();