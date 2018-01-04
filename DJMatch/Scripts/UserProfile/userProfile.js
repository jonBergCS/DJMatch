djApp.controller("userProfileController", function userProfileController($scope, $http) {
    $http({
        method: 'GET',
        url: url + '/Questions'
    }).then(function successCallback(response) {
        $scope.questions = response.data;
    });
});