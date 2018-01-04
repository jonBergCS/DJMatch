djApp.controller("userProfileController", function userProfileController($scope) {
    $http({
        method: 'GET',
        url: url + '/Questions'
    }).then(function successCallback(response) {
        $scope.questions = response;
    });

    
    //$scope.answers = {};
});