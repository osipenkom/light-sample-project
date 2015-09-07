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
        .when('/district/:districtId',
            {
                controller: 'DistrictController',
                templateUrl: '/View/District'
            })
        .when('/addvehicle',
            {
                controller: 'AddVehicleController',
                templateUrl: '/View/AddVehicle'
            })
        .when('/addvehicle/:districtId',
            {
                controller: 'AddVehicleController',
                templateUrl: '/View/AddVehicle'
            })
        .otherwise({ redirectTo: '/home' });
}]);