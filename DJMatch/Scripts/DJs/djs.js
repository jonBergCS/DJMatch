djApp.controller("djsController", function djsController($scope, $http) {
    $http({
        method: 'GET',
        url: url + '/DJs'
    }).then(function successCallback(response) {
        $scope.djsList = response.data;
    });

    $scope.getDetails = function (djID) {

    };
});