djApp
    .controller("DjPlaylistController", ["$scope", function ($scope, $http) {
            $http({
                method: 'GET',
                url: url + '/api/Playlists/' + $scope.playlist.ID + '/full',
            }).then(function successCallback(response) {
                $scope.playlist.songs = response;
            });
    }])
    .directive("djPlaylistDirective", function () {
    return {
        scope: {
            playlist: "@",
            editable: "="
        },
        template: "<h1>yo dude! {{playlist}} </h1>"
    };
});