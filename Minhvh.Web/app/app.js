
(function () {
    angular.module('minhvh',
           ['minhvh.products',
            'minhvh.product_categories',
            'minhvh.common'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/homes/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise("/admin");
    }

})();