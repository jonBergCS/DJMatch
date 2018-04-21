djApp
    .controller("djPlaylistController", ["$scope", function ($scope, $http) {
            //$http({
            //    method: 'GET',
            //    url: url + '/api/Playlists/' + $scope.$parent.eachPlaylist.ID + '/full',
            //}).then(function successCallback(response) {
            //    $scope.songs = response;
            //});
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