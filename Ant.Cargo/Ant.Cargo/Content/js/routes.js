app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/home',
            {
                controller: 'HomeController',
                templateUrl: '/View/Home'
            })
        .when('/login',
            {
                controller: '',
                templateUrl: '/View/Login'
            })
        .otherwise({ redirectTo: '/home' });
}]);