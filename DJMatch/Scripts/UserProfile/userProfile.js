djApp.controller("userProfileController", function userProfileController($scope, $http) {
    $http({
        method: 'GET',
        url: url + '/Questions'
    }).then(function successCallback(response) {
        $scope.questions = response.data;
        $scope.currentIndex = 1;
        });

    $scope.next = function () {
        $scope.currentIndex++;
    };
});