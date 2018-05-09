djApp.controller("usersEventsController", ['$scope', '$q', '$http', 'generalFactory',
    function userEventsController($scope, $q, $http, generalFactory) {

        if (($scope.eventsList == undefined) || ($scope.eventsList, length == 0)) {
            $scope.eventsList = [];

            var promises = [];

            var userID = generalFactory.getCookieData();

            promises.push($http({
                method: 'GET',
                url: url + '/api/Users/' + userID + '/events'
            }));

            //Resolve all promise into the promises array
            $q.all(promises).then(function(response) {
                var eventsList = response[0].data;

                for (var i = 0; i < eventsList.length; i++) {
                    var currEvent = eventsList[i];
                    var innerPromises = [];

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
                        currEvent.playlist = response[0].data;
                        currEvent.DJName = response[1].data.Name;

                        $scope.eventsList.push(currEvent);
                    });

                }

            });
        }

        $scope.displayPlaylist = function(playlist) {
            playlistFactory.seChosenPlaylist(playlist);
        }

        $scope.moveToEvent = function (eventId) {
            window.location = url + '/Events/Details?id=' + eventId;
        }
    }]);