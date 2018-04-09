djApp.directive("djPlaylistDirective", function() {
    return {
        scope: {
            playlist: '=playlistId',
            editable: '='
        },
        templateUrl: '~/Views/DJs/dj-playlist.html'
    };
});