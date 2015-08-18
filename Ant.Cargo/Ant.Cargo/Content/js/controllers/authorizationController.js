/// <reference path="../services/commonService.js" />
/// <reference path="../Scripts/angular.js" />
app.controller('AuthorizationController', ['$scope', 'commonService', '$http', '$rootScope', '$cookies', '$cookieStore', '$parse', '$location',
    function ($scope, commonService, $http, $rootScope, $cookies, $cookieStore, $parse, $location) {
    $scope.error = "";

    $scope.loginClick = function () {
        commonService.authorize($scope.loginModel)
            .then(function (result) {
                if (result.data) {
                    alert(result.data);
                } else {
                    $location.path('/home');
                }
            });
    };
}]);

