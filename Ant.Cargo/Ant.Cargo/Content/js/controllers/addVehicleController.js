/// <reference path="../Scripts/angular.js" />
app.controller('AddVehicleController', ['$scope', 'commonService', '$rootScope', '$routeParams', '$cookies', '$location', '$timeout', function ($scope, commonService, $rootScope, $routeParams, $cookies, $location, $timeout) {

    $scope.vehicle = {};

    $scope.init = function () {
        if ($routeParams.districtId) {
            $scope.vehicle.DistrictID = Number($routeParams.districtId);
        }
    }

    $scope.backButtonClick = function () {
        history.back();
    }

    $scope.addNewVihicle = function () {
        commonService.addNewVihicle($scope.vehicle)
               .then(function (result) {
                   if (!result.ErrorMessage) {
                       $scope.vehicle.PhoneNumber = '7' + $scope.vehicle.PhoneNumber;
                       $scope.vehicle.ID = result.VehicleID;
                       $rootScope.currentDistrict.Vehicles.push($scope.vehicle);
                       $scope.vehicle = {};
                       $scope.backButtonClick();
                   }
                   else {
                       alert(result.ErrorMessage);
                   }
               });
    }

    $scope.init();

}]);

