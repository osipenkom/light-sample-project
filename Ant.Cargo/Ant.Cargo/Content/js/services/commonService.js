/// <reference path="../core.js" />
/// <reference path="../Scripts/angular.js" />
app.service('commonService', ['$http', '$rootScope', '$timeout', '$q', function ($http, $rootScope, $timeout, $q) {
    $http.defaults.headers.put["Content-Type"] = "application/x-www-form-urlencoded";

    var commonService = this;
    this._timestamp = null;

    this.init = function () {

    }

    this.getDistricts = function () {
        return $http.get('api/district/getdistricts')
            .then(function (response) {
                return response.data;
            });
    }

    this.authorize = function (user) {
        return $http.post('api/authorization/login', user)
           .success(function (data, status, headers, config) {
               return data;
           })
           .error(function (data, status, headers, config) {
               return null;
           });
    }

    this.addNewVihicle = function (model) {
        return $http.post('api/vehicle/addvehicle', model)
                .then(function (response) {
                    return response.data;
                });
    }

    this.deleteVehicles = function (vehicleIDs) {
        return $http.post('api/vehicle/deletevehicles', vehicleIDs)
                .then(function (response) {
                    return response.data;
                });
    }

    this.sendSmsMessageForDistricts = function (model) {
        return $http.post('api/home/sendsmsmessagefordistricts', model)
                .then(function (response) {
                    return response.data;
                });
    }

    this.sendSmsMessageForVehicles = function (model) {
        return $http.post('api/home/sendsmsmessageforvehicles', model)
                .then(function (response) {
                    return response.data;
                });
    }

    this.addNewDistrict = function (districtName) {
        return $http.get('api/district/addnewdistrict', { params: { districtName: districtName } })
                .then(function (response) {
                    return response.data;
                });
    }

    this.getDistrictsByID = function (districtID) {
        return $http.get('api/district/getdistrictbyid', { params: { districtID: districtID } })
            .then(function (response) {
                return response.data;
            });
    }

    this.getVehiclesByDistrictID = function (districtID) {
        return $http.get('api/vehicle/getvehiclesbydistrictid', { params: { districtID: districtID } })
            .then(function (response) {
                return response.data;
            });
    }

    this.init();
}]);