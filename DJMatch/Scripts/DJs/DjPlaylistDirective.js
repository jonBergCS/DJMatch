djApp.directive("djPlaylistDirective", function ($http) {
    var link = function (scope) {
        $http({
            method: 'GET',
            url: url + '/api/Playlists/' + scope.playlist.ID + '/Songs',
        }).then(function successCallback(response) {
            scope.playlist.songs = response.data;
        });
    };

    return {
        scope: {
            playlist: "=",
            editable: "="
        },
        link: link,
        template: "<h3>{{playlist.Name}}</h3>"
        + "<table class='table' ng-if='playlist.songs.length > 0' >"
        + "<thead>"
        + "<tr>"
        + "<th>Song name</th>"
        + "<th>Artist</th>"
        + "<th>Remove</th>"
        + "</tr>"
        + "</thead>"
        + "<tbody>"
        + "<tr ng-repeat='song in playlist.songs'>"
        + "<td>{{ song.Name }}</td>"
        + "<td>{{ song.Artist }}</td>"
        + "<td><span class='glyphicon glyphicon-remove'></span></td>"
        + "</tr>"
        + "</tbody>"
        + "</table>"
        + "<div class='addSong' ng-if={{editable}}>" 
        + "<span class='glyphicon glyphicon-plus'></span>add a song"
        + "</div>"
    };
});