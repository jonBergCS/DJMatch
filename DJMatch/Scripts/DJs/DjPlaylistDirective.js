djApp.directive("djPlaylistDirective", function ($http) {
    var link = function (scope) {

        $http({
            method: 'GET',
            url: url + '/api/Playlists/' + scope.playlist + '/Songs',
        }).then(function successCallback(response) {
            scope.songs = response.data;
        });

        $http({
            method: 'GET',
            url: url + '/api/Songs',
        }).then(function successCallback(response) {
            scope.allSongs = response.data;
            scope.selectedSong = {ID:0};
        });

        scope.addSong = function() {
            var songToAdd = {};
            songToAdd.SongID = scope.selectedSong.ID;
            songToAdd.PlaylistID = scope.playlist;

            $http({
                method: 'POST',
                url: url + '/api/SongsToPlaylists',
                data: songToAdd
            }).then(function successCallback(response) {
                scope.songs.push(scope.selectedSong);
            });
        };
    };

    return {
        scope: {
            playlist: "=",
            editable: "=",
            removeSong: "="
        },
        link: link,
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
        + "<td ng-if='editable'><button ngclick='removeSong(song.id)'><span class='glyphicon glyphicon-remove'></span></button></td>"
        + "</tr>"
        + "</tbody>"
        + "</table>"
        + "<button class='addSong btn btn-success' data-toggle='modal' data-target='#myModal' ng-if='editable'>"
        + "<span class='glyphicon glyphicon-plus'></span>add a song"
        + "</button>"
		+ "<div id='myModal' class='modal fade' role='dialog'>"
        + "<div class='modal-dialog'>"
        + "<!-- Modal content-->"
        + "<div class='modal-content'>"
        + "<div class='modal-header'>"
        + "<button type='button' class='close' data-dismiss='modal'>&times;</button>"
        + "<h4 class='modal-title'>add a song : </h4>"
        + "</div>"
        + "<div class='modal-body'>"
        + "<select class='selectpicker' data-live-search='true' ng-model='selectedSong' ng-options='item as item.Name for item in allSongs'>"
        + "</select>"
        //+ "<input type='text' data-ng-model='selectedSong' list='songs'>"
        //+ "<datalist id='songs'>"
        //+ "<select ng-model='selectedSong' ng-options='item as item.Name for item in allSongs'></select>"
        //+ "<option  data-ng-repeat='item in allSongs'>{{item.Name}} - {{item.Artist}} </option>"
        //+ "</datalist>"
        + "</div>"
        + "<div class='modal-footer'>"
        + "<button type='button' class='btn btn-default' ng-click='addSong()' data-dismiss='modal'>Add</button>"
        + "</div>"
        + "</div>"
        + "</div>"
        + "</div>"


    };
});