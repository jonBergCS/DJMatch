djApp.controller('eventDetailsController',
    function eventDetailsControoler($scope, $http) {
        $scope.eventId = (new URL(window.location.href )).searchParams.get("id");

        $http({
            method: 'GET',
            url: url + '/api/Events/' + $scope.eventId
        }).then(function successCallback(response) {
            $scope.currEvent = response.data;
        });
    });