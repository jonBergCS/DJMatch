djApp.controller("eventDetailsController", function eventDetailsController($scope, $q, $http, generalFactory) {
    
    var promises = [];

    var eventID = generalFactory.getEventData();

    promises.push($http({
        method: 'GET',
        url: url + '/api/Events/' + eventID
    }));

    //Resolve all promise into the promises array
    $q.all(promises).then(function (response) {
        $scope.currEvent = response[0].data;
        $scope.currPlaylist = $scope.currEvent.evnt.PlaylistId;
    });
});