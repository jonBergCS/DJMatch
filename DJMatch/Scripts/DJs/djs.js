djApp.controller("djsController", function djsController($rootScope, $scope, $q, $http, generalFactory) {
    $scope.userID = generalFactory.getCookieData();
    $scope.loading = true;

    if (($scope.djsList == undefined) || ($scope.djsList.size() == 0)) {
        var promises = [];

        var defer = $q.defer();

        promises.push($http({
            method: 'GET',
            url: url + '/api/Djs/filterForUser/' + generalFactory.getCookieData()
        }));

        //Resolve all promise into the promises array
        $q.all(promises).then(function (response) {
            $scope.djsList = response[0].data.result;
            var recommended = response[0].data.recommendedDJId;

            if (recommended !== "") {
                $scope.recommended = $scope.djsList.filter(dj => dj.ID == recommended)[0];
                var index = $scope.djsList.indexOf($scope.recommended);
                $scope.djsList.splice(index, 1);
            }

            $scope.loading = false;
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

    $scope.createEventPage = function (currPlaylist) {
        window.location.href = '/Events/Create?dj=' + currPlaylist.DJ_ID + '&playlist=' + currPlaylist.ID;
    };
});