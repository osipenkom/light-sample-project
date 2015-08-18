app.factory('authInterceptor', ['$rootScope', '$q', '$location', function ($rootScope, $q, $location) {
    return {
        // Intercept 401s and redirect you to login
        responseError: function (response) {
            if (response.status === 401) {
                $location.path('/login');
                $rootScope.user = null;
                return $q.reject(response);
            }
            else {
                return $q.reject(response);
            }
        }
    };
}])