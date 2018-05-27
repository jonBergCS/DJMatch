var djApp = angular.module("FinDJApp", ["ngRoute", "ngCookies"]);
djApp.controller('layoutController', function ($scope, generalFactory) {
    $scope.logout = function () {
        generalFactory.clearCookieData();
    }

    $scope.userId = generalFactory.getCookieData();
});
var url = "http://localhost:2155";
//var url = "http://findj.azurewebsites.net/"