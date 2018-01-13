djApp.controller("djsController", ['djsService', '$scope', function djsController(djsService, $scope) {

    $scope.djsList = djsService.getDjs();

    $scope.displayDetails = function (djID) {
        djsService.currentDJID = djID;
    };
}]);