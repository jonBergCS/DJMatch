djApp.controller("playlistController", function ($scope, playlistFactory) {

    $scope.chosenPlalist = playlistFactory.getChosenPlaylist();
});