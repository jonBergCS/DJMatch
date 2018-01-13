djApp.controller("djProfileController", ['djsService', '$scope', '$q', function djsController(djsService,$scope,$q) {

    if (djsService.currentDJ == {}) {
    var promises = [];

    var defer = $q.defer();

    promises.push(djsService.getCurrentDJDetails(djsService.currentDJID));

    //Resolve all promise into the promises array
    $q.all(promises).then(function (response) {
        djsService.currentDJ = response[0].data;
        $scope.currentDJ = djsService.currentDJ;
    });
    }

    $scope.currentDJ = djsService.currentDJ;




}]);