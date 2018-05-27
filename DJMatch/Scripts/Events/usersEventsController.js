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
                    $scope.eventsList.push(eventsList[i]);
                }
            });
        }

        $scope.displayPlaylist = function(playlist) {
            playlistFactory.seChosenPlaylist(playlist);
        }

        $scope.moveToEvent = function (eventId) {
            generalFactory.setEventData(eventId);
            window.location = url + '/Events/Details?id=' + eventId;
        }
    }]);