(function (app) {
    function apiService($http, notificationService) {
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


        return {
            get: get,
            post:post
        }
    }

    app.service('apiService', apiService);

    apiService.$inject = ["$http","notificationService"];
})(angular.module("minhvh.common"));