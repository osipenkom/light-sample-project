/// <reference path="../Scripts/angular.js" />

var app = angular.module('app', ['ngRoute', 'ngCookies', 'ui.utils', 'blockUI'])
                    .config(['$httpProvider', function ($httpProvider) {
                        $httpProvider.interceptors.push('authInterceptor');
                    }])
                    .run(['$rootScope', 'commonService', '$location', '$cookies', '$timeout', function ($rootScope, commonService, $location, $cookies, $timeout) {
                        $rootScope.vehiclesLimit = 50;
                    }]);

app.directive('myOnKeyDownCall', function () {
    return function (scope, element, attrs) {
        var numKeysPress = 0;
        element.bind("keydown keypress", function (event) {
            numKeysPress++;
            if (numKeysPress >= 3) {
                scope.$apply(function () {
                    scope.$eval(attrs.myOnKeyDownCall);
                });
                event.preventDefault();
            }
        });
    };
});