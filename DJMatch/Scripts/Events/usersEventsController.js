djApp.controller("usersEventsController", function djsController($scope, $q, $http) {

    if (($scope.eventsList == undefined) || ($scope.eventsList, length == 0)) {
        var promises = [];

        var defer = $q.defer();

        promises.push($http({
            method: 'GET',
            url: url + '/api/Users/' + userID + '/events'
        }));

        //Resolve all promise into the promises array
        $q.all(promises).then(function (response) {
            $scope.eventsList = response[0].data;
        });
    }


});