/// <reference path="../Scripts/angular.js" />
app.controller('HomeController', ['$scope', 'commonService', '$rootScope', '$routeParams', '$cookies', '$location', '$timeout', function ($scope, commonService, $rootScope, $routeParams, $cookies, $location, $timeout) {
    $rootScope.currentPath = 'home';
    $scope.selectedDistricts = [];
    $scope.selectedVehicles = [];
    $scope.isDistrictSettingsVisible = false;
    $scope.isAddVehicleVisible = false;
    $scope.vehicle = {};
    $scope.vehiclesLimit = 50;
    $scope.searchVehicleString = '';
    $scope.VehiclesForSendSmsManually = [];

    $scope.searchVehiclesKeyUp = function () {
        $timeout(function () {
            $scope.searchText = $scope.searchVehicleString;
        }, 300);
    }

    $scope.getVehiclesForDistrict = function (districtID) {
        var found = Core.FindInArray($scope.districts, 'ID', districtID);
        if (found && found.Vehicles.length == 0) {
            commonService.getVehiclesByDistrictID(districtID)
                .then(function (result) {
                    found.Vehicles = result;
                });
        }
    }

    $scope.init = function () {
        $scope.getDistricts();
    }

    $scope.getDistricts = function () {
        commonService.getDistricts()
                .then(function (result) {
                    $scope.districts = result;
                });
    }

    $scope.updateDistrict = function (districtID) {
        commonService.getDistrictsByID(districtID)
                .then(function (result) {
                    var found = Core.FindInArray($scope.districts, 'ID', result.ID);
                    var idx = $scope.districts.indexOf(found);
                    $scope.districts.splice(idx, 1);
                    $scope.districts.push(result);
                });
    }

    $scope.changeDistrictStateClick = function (district) {
        var found = Core.FindInArray($scope.selectedDistricts, 'ID', district.ID);
        if (found) {
            Core.RemoveFromArray($scope.selectedDistricts, 'ID', district.ID);
        }
        else {
            var found = Core.FindInArray($scope.districts, 'ID', district.ID);
            if (found && found.Vehicles.length == 0) {
                commonService.getVehiclesByDistrictID(district.ID)
                    .then(function (result) {
                        found.Vehicles = result;
                        $scope.selectedDistricts.push(district);
                    });
            }
            else {
                $scope.selectedDistricts.push(district);
            }
        }
        $scope.VehiclesForSendSmsManually = [];
    }

    $scope.checkIfDistrictSelected = function (district) {
        var found = Core.FindInArray($scope.selectedDistricts, 'ID', district.ID);
        if (found) {
            return 'selected';
        }
        else {
            return '';
        }
    }

    $scope.changeDistrictSettingsClick = function (district) {
        $scope.currentDistrict = district;
        $scope.getVehiclesForDistrict(district.ID);
        $scope.isDistrictSettingsVisible = true;
    }

    $scope.closeDistrictSettingsClick = function () {
        $scope.isDistrictSettingsVisible = false;
        $scope.currentDistrict = null;
    }

    $scope.checkBackgroundStyle = function () {
        if ($scope.isDistrictSettingsVisible || $scope.isAddVehicleVisible) {
            return 'modal-backdrop in';
        }
        else {
            return '';
        }
    }

    $scope.toggleVehiclePopup = function () {
        if ($scope.currentDistrict && $scope.isAddVehicleVisible) {
            $scope.isDistrictSettingsVisible = true;
        }
        else {
            $scope.isDistrictSettingsVisible = false;
        }
        if ($scope.currentDistrict) {
            $scope.vehicle.DistrictID = $scope.currentDistrict.ID;
        }
        $scope.isAddVehicleVisible = !$scope.isAddVehicleVisible;
    }

    $scope.addNewVihicle = function () {
        commonService.addNewVihicle($scope.vehicle)
               .then(function (result) {
                   if (!result) {
                       $scope.isAddVehicleVisible = false;
                       $scope.vehicle.PhoneNumber = '7' + $scope.vehicle.PhoneNumber;
                       $scope.currentDistrict.Vehicles.push($scope.vehicle);
                       $scope.vehicle = {};
                       if ($scope.currentDistrict) {
                           $scope.isDistrictSettingsVisible = true;
                       }
                   }
                   else {
                       alert(result);
                   }
               });
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

    $scope.deleteVehiclesClick = function () {
        var selectedVehicleIDs = Core.SelectArrayProperties($scope.selectedVehicles, 'ID');
        commonService.deleteVehicles(selectedVehicleIDs)
               .then(function (result) {
                   var found = Core.FindInArray($scope.districts, 'ID', $scope.currentDistrict.ID);

                   $scope.currentDistrict.Vehicles.forEach(function (vehicle) {
                       if (selectedVehicleIDs.indexOf(vehicle.ID) >= 0) {
                           Core.RemoveFromArray($scope.currentDistrict.Vehicles, 'ID', vehicle.ID);
                       }
                   });
                   $scope.selectedVehicles = [];
               });
    }

    $scope.sendMessageClick = function () {
        if ($scope.selectedDistricts.length > 0) {
            var SmsMessageModel = { Text: $scope.SmsMessage, Districts: $scope.selectedDistricts };
            commonService.sendSmsMessageForDistricts(SmsMessageModel)
                   .then(function (result) {
                       if (!result) {
                           $scope.SmsMessage = '';
                           $scope.selectedDistricts = [];
                           alert('Смс отправлены успешно');
                       }
                       else {
                           alert(result);
                       }
                   });
        }
        else if ($scope.VehiclesForSendSmsManually.length > 0) {
            var SmsMessageModel = { Text: $scope.SmsMessage, Vehicles: $scope.VehiclesForSendSmsManually };
            commonService.sendSmsMessageForVehicles(SmsMessageModel)
                   .then(function (result) {
                       if (!result) {
                           $scope.SmsMessage = '';
                           $scope.selectedDistricts = [];
                           $scope.VehiclesForSendSmsManually = [];
                           alert('Смс отправлены успешно');
                       }
                       else {
                           alert(result);
                       }
                   });
        }
    }

    $scope.addNewDistrictClick = function () {
        commonService.addNewDistrict($scope.newDistrictName)
               .then(function (result) {
                   if (!result) {
                       $scope.newDistrictName = '';
                       $scope.getDistricts();
                   }
                   else {
                       alert(result);
                   }
               });
    }

    $scope.selectVehicleForSmsSendClick = function (vehicle) {
        var found = Core.FindInArray($scope.VehiclesForSendSmsManually, 'ID', vehicle.ID);
        if (found) {
            Core.RemoveFromArray($scope.VehiclesForSendSmsManually, 'ID', vehicle.ID);
        }
        else {
            $scope.VehiclesForSendSmsManually.push(vehicle);
        }
    }

    $scope.checkIfVehicleSelectedForSMSSend = function (vehicle) {
        var found = Core.FindInArray($scope.VehiclesForSendSmsManually, 'ID', vehicle.ID);
        if (found) {
            return 'selected';
        }
        else {
            return '';
        }
    }

    $scope.init();

    $scope.$watchCollection('selectedDistricts', function (newValue) {
        if (newValue) {

            $scope.VehiclesForSendSms = [];
            angular.forEach($scope.selectedDistricts, function (selectedDistrict) {
                angular.forEach(selectedDistrict.Vehicles, function (vehicle) {
                    $scope.VehiclesForSendSms.push(vehicle);
                });
            });
        }
    });
}]);

