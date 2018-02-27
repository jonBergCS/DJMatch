﻿djApp.controller("usersEventsController",
    function djsController($scope, $q, $http, playlistFactory) {

        if (($scope.eventsList == undefined) || ($scope.eventsList, length == 0)) {
            $scope.eventsList = [];

            var promises = [];

            promises.push($http({
                method: 'GET',
                url: url + '/api/Users/' + '1' /*userID*/ + '/events'
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
    });