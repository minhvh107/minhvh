(function (app) {
    function statusFilter() {
        return function (input) {
            if (input == true) {
                return "kích hoạt";
            } else
                return "khóa";
        }
    };
    app.filter('statusFilter', statusFilter);
})(angular.module('minhvh.common'));