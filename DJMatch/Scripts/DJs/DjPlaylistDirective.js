djApp
    .controller("DjPlaylistController", ['$scope', function ($scope) {
        
    }])
    .directive("djPlaylistDirective", function () {
    return {
        scope: {
            playlist: '=playlistId',
            editable: '='
        },
        template: '<h1>yo dude! </h1>'
    };
});