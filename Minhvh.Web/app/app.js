﻿(function () {
    angular.module("minhvh",
        ["minhvh.products",
            "minhvh.product_categories",
            "minhvh.common"])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("base",
            {
                url: "",
                templateUrl: "/app/shared/views/baseView.html",
                abstract: true
            })
            .state("login", {
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController"
            })
            .state("home", {
                url: "/admin",
                parent: "base",
                templateUrl: "/app/components/homes/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise("/login");
    }
})();