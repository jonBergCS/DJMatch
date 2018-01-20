djApp.controller("djsController", function djsController($scope, $q, $http) {

    if (($scope.djsList == undefined) || ($scope.djsList,length == 0)) {
        var promises = [];

        var defer = $q.defer();

        promises.push($http({
            method: 'GET',
            url: url + '/api/DJs'
        }));

        //Resolve all promise into the promises array
        $q.all(promises).then(function (response) {
            $scope.djsList = response[0].data;
        });
    }
    

    $scope.displayDetails = function (djID) {
        $scope.currentDJID = djID;

        if (($scope.currentDJ == undefined) || ($scope.currentDJ == {}))
        {
            var promises = [];

            var defer = $q.defer();

            // get basic DJ info
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID
            }));

            // get DJ's reviews
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/reviews'
            }));

            // get DJ's rate
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/rate'
            }));

            //Resolve all promise into the promises array
            $q.all(promises).then(function (response) {
                debugger;
                $scope.currentDJ = response[0].data;
                $scope.currentDJ.Reviews = response[1].data;
                $scope.currentDJ.Rate = response[2].data;
            });
        }
    };

    $scope.backToList = function ()
    {
        $scope.currentDJ = undefined;
    };
});