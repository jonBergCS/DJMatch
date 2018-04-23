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
        + "<table class='table dj-playlist-table' ng-if='playlist.songs.length > 0' >"
        + "<thead>"
        + "<tr>"
        + "<th>Song name</th>"
        + "<th>Artist</th>"
        + "<th ng-if='editable'>Remove</th>"
        + "</tr>"
        + "</thead>"
        + "<tbody>"
        + "<tr ng-repeat='song in playlist.songs'>"
        + "<td>{{ song.Name }}</td>"
        + "<td>{{ song.Artist }}</td>"
        + "<td ng-if='editable'><span class='glyphicon glyphicon-remove'></span></td>"
        + "</tr>"
        + "</tbody>"
        + "</table>"
        + "<button class='addSong btn btn-success' ng-if='editable'>" 
        + "<span class='glyphicon glyphicon-plus'></span>add a song"
        + "</buton>"
    };
});