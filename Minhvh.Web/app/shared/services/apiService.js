(function (app) {
    function apiService(initJavascriptService, $http, notificationService) {

        initJavascriptService.init();

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) {
                success(result);
            },
                function (error) {
                    failure(error);
                });
        }

        function post(url, data, success, failure) {
            $http.post(url, data).then(function (result) {
                success(result);
            },
                function (error) {
                    if (error.status == '401') {
                        notificationService.displayError('Authendicate is required');
                    }
                    failure(error);
                });
        }

        function put(url, data, success, failure) {
            $http.put(url, data).then(function (result) {
                success(result);
            },
                function (error) {
                    if (error.status == '401') {
                        notificationService.displayError('Authendicate is required');
                    }
                    failure(error);
                });
        }

        return {
            get: get,
            post: post,
            put: put
        }
    }

    app.service('apiService', apiService);

    apiService.$inject = ["initJavascriptService", "$http", "notificationService"];
})(angular.module("minhvh.common"));