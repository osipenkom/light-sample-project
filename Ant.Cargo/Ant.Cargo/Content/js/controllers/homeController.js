/// <reference path="../Scripts/angular.js" />
app.controller('HomeController', ['$scope', 'commonService', '$rootScope', '$routeParams', '$cookies', '$location', '$timeout', function ($scope, commonService, $rootScope, $routeParams, $cookies, $location, $timeout) {
    $rootScope.currentPath = 'home';
    $scope.vehicle = {};
    $scope.searchVehicleString = '';

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
        if (!$rootScope.districts || $rootScope.districts.length == 0) {
        commonService.getDistricts()
            .then(function (result) {
                $rootScope.districts = result;
            });
        }
    }

    $scope.changeDistrictStateClick = function (district) {
        var found = Core.FindInArray($rootScope.selectedDistricts, 'ID', district.ID);
        if (found) {
            Core.RemoveFromArray($rootScope.selectedDistricts, 'ID', district.ID);
        }
        else {
            var found = Core.FindInArray($scope.districts, 'ID', district.ID);
            if (found && found.Vehicles.length == 0) {
                commonService.getVehiclesByDistrictID(district.ID)
                    .then(function (result) {
                        found.Vehicles = result;
                        $scope.pushSelectedDistrict(district);
                    });
            }
            else {
                $scope.pushSelectedDistrict(district);
            }
        }
        $rootScope.VehiclesForSendSmsManually = [];
    }

    $scope.pushSelectedDistrict = function (district) {
        if (!$rootScope.selectedDistricts || $rootScope.selectedDistricts.length == 0) {
            $rootScope.selectedDistricts = [];
        }
        $rootScope.selectedDistricts.push(district);
    }

    $scope.checkIfDistrictSelected = function (district) {
        var found = Core.FindInArray($rootScope.selectedDistricts, 'ID', district.ID);
        if (found) {
            return 'selected';
        }
        else {
            return '';
        }
    }

    $scope.changeDistrictSettingsClick = function (district) {
        $location.path("district/" + district.ID);
    }

    $scope.sendMessageClick = function () {
        if ($rootScope.selectedDistricts && $rootScope.selectedDistricts.length > 0) {
            var SmsMessageModel = { Text: $scope.SmsMessage, Districts: $rootScope.selectedDistricts };
            commonService.sendSmsMessageForDistricts(SmsMessageModel)
                   .then(function (result) {
                       if (!result) {
                           $scope.SmsMessage = '';
                           $rootScope.selectedDistricts = [];
                           alert('Смс отправлены успешно');
                       }
                       else {
                           alert(result);
                       }
                   });
        }
        else if ($rootScope.VehiclesForSendSmsManually.length > 0) {
            var SmsMessageModel = { Text: $scope.SmsMessage, Vehicles: $rootScope.VehiclesForSendSmsManually };
            commonService.sendSmsMessageForVehicles(SmsMessageModel)
                   .then(function (result) {
                       if (!result) {
                           $scope.SmsMessage = '';
                           $rootScope.selectedDistricts = [];
                           $rootScope.VehiclesForSendSmsManually = [];
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

    $scope.goToAddVehicleClick = function () {
        $location.path("/addvehicle");
    }

    $scope.init();

    $scope.$watchCollection('selectedDistricts', function (newValue) {
        if (newValue) {

            $rootScope.VehiclesForSendSms = [];
            angular.forEach($rootScope.selectedDistricts, function (selectedDistrict) {
                angular.forEach(selectedDistrict.Vehicles, function (vehicle) {
                    $rootScope.VehiclesForSendSms.push(vehicle);
                });
            });
        }
    });

}]);

