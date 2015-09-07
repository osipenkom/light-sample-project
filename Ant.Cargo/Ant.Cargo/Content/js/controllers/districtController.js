/// <reference path="../Scripts/angular.js" />
app.controller('DistrictController', ['$scope', 'commonService', '$rootScope', '$routeParams', '$cookies', '$location', '$timeout', function ($scope, commonService, $rootScope, $routeParams, $cookies, $location, $timeout) {

    $scope.selectedVehicles = [];

    $scope.init = function () {

        if (!$rootScope.districts || $rootScope.districts.length == 0) {
            commonService.getDistricts()
                .then(function (result) {
                    $rootScope.districts = result;
                    $scope.initDistrict();
                });
        }
        else
        {
            $scope.initDistrict();
        }        
    }

    $scope.initDistrict = function () {
        var found = Core.FindInArray($rootScope.districts, 'ID', $routeParams.districtId);

        if (!found) {
            $scope.getDistrictWithVehicles();
        }
        else if (found && (!found.Vehicles || found.Vehicles.length == 0)) {
            $scope.getVehiclesForDistrict(found);
        }
        else {
            $rootScope.currentDistrict = found;
        }
    }
    
    $scope.getDistrictWithVehicles = function () {
        commonService.getDistrictByID($routeParams.districtId, true)
                .then(function (result) {
                    $rootScope.currentDistrict = result;
                    $rootScope.districts.push($rootScope.currentDistrict);
                });
    }

    $scope.getVehiclesForDistrict = function (found) {
        commonService.getVehiclesByDistrictID($routeParams.districtId)
                .then(function (result) {
                    found.Vehicles = result;
                    $rootScope.currentDistrict = found;
                });
    }

    $scope.searchVehiclesKeyUp = function () {
        $timeout(function () {
            $scope.searchText = $scope.searchVehicleString;
        }, 300);
    }

    $scope.selectVehicleForDeleteClick = function (vehicle) {
        var found = Core.FindInArray($scope.selectedVehicles, 'ID', vehicle.ID);
        if (found) {
            Core.RemoveFromArray($scope.selectedVehicles, 'ID', vehicle.ID);
        }
        else {
            $scope.selectedVehicles.push(vehicle);
        }
    }

    $scope.checkIfVehicleSelected = function (vehicle) {
        var found = Core.FindInArray($scope.selectedVehicles, 'ID', vehicle.ID);
        if (found) {
            return 'selected-danger';
        }
        else {
            return '';
        }
    }

    $scope.selectVehicleForSmsSendClick = function (vehicle) {
        var found = Core.FindInArray($rootScope.VehiclesForSendSmsManually, 'ID', vehicle.ID);
        if (found) {
            Core.RemoveFromArray($rootScope.VehiclesForSendSmsManually, 'ID', vehicle.ID);
        }
        else {
            if (!$rootScope.VehiclesForSendSmsManually || $rootScope.VehiclesForSendSmsManually.length == 0) {
                $rootScope.VehiclesForSendSmsManually = [];
            }
            $rootScope.VehiclesForSendSmsManually.push(vehicle);
        }
    }

    $scope.checkIfVehicleSelectedForSMSSend = function (vehicle) {
        var found = Core.FindInArray($rootScope.VehiclesForSendSmsManually, 'ID', vehicle.ID);
        if (found) {
            return 'selected';
        }
        else {
            return '';
        }
    }

    $scope.closeDistrictSettingsClick = function () {
        $location.path("/home");
    }

    $scope.deleteVehiclesClick = function () {
        var selectedVehicleIDs = Core.SelectArrayProperties($scope.selectedVehicles, 'ID');
        commonService.deleteVehicles(selectedVehicleIDs)
               .then(function (result) {
                   var found = Core.FindInArray($rootScope.districts, 'ID', $rootScope.currentDistrict.ID);

                   selectedVehicleIDs.forEach(function (vehicleID) {
                       Core.RemoveFromArray($rootScope.currentDistrict.Vehicles, 'ID', vehicleID);
                   })
                   $scope.selectedVehicles = [];
               });
    }

    $scope.goToAddVehicleClick = function () {
        $location.path("/addvehicle/" + $routeParams.districtId);
    }

    $scope.init();

}]);

