djApp.controller("djsController", ['djsService', '$scope', '$q', function djsController(djsService, $scope, $q) {

    if (djsService.djsList.length == 0) {
        var promises = [];

        var defer = $q.defer();

        promises.push(djsService.getDjs());

        //Resolve all promise into the promises array
        $q.all(promises).then(function (response) {
            djsService.djsList = response[0].data;
            $scope.djsList = djsService.djsList;
        });
    }
    
    $scope.djsList = djsService.djsList;
    

    $scope.displayDetails = function (djID) {
        djsService.currentDJID = djID;
    };
}]);