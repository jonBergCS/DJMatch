djApp.controller("djProfileController", ['djsService', '$scope', function djsController(djsService,$scope) {
    $scope.currentDJ = djsService.getCurrentDJDetails(djsService.currentDJID);
}]);