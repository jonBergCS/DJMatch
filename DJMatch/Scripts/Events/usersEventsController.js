djApp.controller("usersEventsController", function djsController($scope, $q, $http) {

    if (($scope.eventsList == undefined) || ($scope.eventsList, length == 0)) {
        $scope.eventsList = [];

        var promises = [];

        var defer = $q.defer();

        promises.push($http({
            method: 'GET',
            url: url + '/api/Users/' + '1'/*userID*/ + '/events'
        }));

        //Resolve all promise into the promises array
        $q.all(promises).then(function (response) {
            var eventsList = response[0].data;

            for (var i = 0; i < eventsList.length; i++) {
                debugger;
                var currEvent = eventsList[i];

                var innerPromises = [];

                var innerDefer = $q.defer();

                innerPromises.push($http({
                    method: 'GET',
                    url: url + '/api/playlists/' + currEvent.PlaylistId
                }));

                innerPromises.push($http({
                    method: 'GET',
                    url: url + '/api/DJs/' + currEvent.DJId
                }));

                //Resolve all promise into the promises array
                $q.all(innerPromises).then(function(response) {
                    currEvent.playlistName = response[0].data.Name;
                    currEvent.DJName = response[1].data.Name;

                    $scope.eventsList.push(currEvent);
                });
                
            }

        });
    }


});