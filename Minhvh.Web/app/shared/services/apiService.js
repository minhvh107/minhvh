(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ["$http"];
    function apiService($http) {
        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) {
                success(result);
            },
                function (error) {
                    failure(error);
                });
        }

        return {
            get: get
        }
    }
})(angular.module("minhvh.common"));