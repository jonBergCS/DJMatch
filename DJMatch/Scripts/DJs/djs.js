﻿djApp.controller("djsController", function djsController($scope, $q, $http, generalFactory) {

    if (($scope.djsList == undefined) || ($scope.djsList.size() == 0)) {
        var promises = [];

        var defer = $q.defer();

        promises.push($http({
            method: 'GET',
            url: url + '/api/DJs'
        }));

        //Resolve all promise into the promises array
        $q.all(promises).then(function (response) {
            $scope.djsList = response[0].data;
        });
    }
    

    $scope.displayDetails = function (djID) {
        $scope.currentDJID = djID;

        if (($scope.currentDJ == undefined) || ($scope.currentDJ == {}))
        {
            var promises = [];

            var defer = $q.defer();

            // get basic DJ info
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID
            }));

            // get DJ's reviews
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/reviews'
            }));

            // get DJ's rate
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/rate'
            }));

            // get all DJ's playlists
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/playlists'
            }));

            // get DJ's default playlists
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/playlists/default'
            }));

            //Resolve all promise into the promises array
            $q.all(promises).then(function (response) {
                $scope.currentDJ = response[0].data;
                $scope.currentDJ.Reviews = response[1].data;
                $scope.currentDJ.Rate = response[2].data;
                $scope.currentDJ.Playlists = response[3].data;
                $scope.currentDJ.DefaultPlaylists = response[4].data;
            });
        }
    };

    $scope.backToList = function ()
    {
        $scope.currentDJ = undefined;
    };
});