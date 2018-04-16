djApp
    .controller("DjPlaylistController", ['$scope', function ($scope) {
        
    }])
    .directive("djPlaylistDirective", function () {
    return {
        scope: {
            playlist: "@",
            editable: "="
        },
        template: '<h1>yo dude! {{playlist}} </h1>'
    };
});