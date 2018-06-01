

djApp.directive("djPlaylistDirective", function ($http) {
    var link = function (scope) {
        $http({
            method: 'GET',
            url: url + '/api/Playlists/' + scope.playlist + '/Songs',
        }).then(function successCallback(response) {
            scope.songs = response.data;
        });
    };

    return {
        scope: {
            playlist: "=",
            editable: "=",
        },
        link: link,
        controller: ['$scope', '$http', function ($scope, $http) {
            $scope.removeSong = function (songId, playlistId) {
                console.log('yo')
                $http({
                    method: "DELETE",
                    url: '/api/Playlists/' + playlistId + '/songs/' + songId,
                }).then(function successCallback(response) {
                    $http({
                        method: 'GET',
                        url: url + '/api/Playlists/' + playlistId + '/Songs',
                    }).then(function successCallback(response) {
                        $scope.songs = response.data;
                    });
                });
            };
        }],
        template: "<span ng-if='songs.length == 0'>Add songs to your playlist!</span>"
        + "<table class='table dj-playlist-table' ng-if='songs.length > 0' >"
        + "<thead>"
        + "<tr>"
        + "<th>Song name</th>"
        + "<th>Artist</th>"
        + "<th ng-if='editable'>Remove</th>"
        + "</tr>"
        + "</thead>"
        + "<tbody>"
        + "<tr ng-repeat='song in songs'>"
        + "<td>{{ song.Name }}</td>"
        + "<td>{{ song.Artist }}</td>"
        + "<td ng-if='editable'><button class='remove-song-button' ng-click='removeSong(song.ID, playlist)'><span class='glyphicon glyphicon-remove'></span></button></td>"
        + "</tr>"
        + "</tbody>"
        + "</table>"
        + "<button class='addSong btn btn-success' ng-if='editable'>" 
        + "<span class='glyphicon glyphicon-plus'></span>add a song"
        + "</buton>"
    };
});