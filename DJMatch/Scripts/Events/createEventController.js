djApp.controller("createEventController", function createEventController($scope, $q, $http, generalFactory) {

    var promises = [];
    var DJID = location.search.split('dj=')[1].split('&')[0];
    var PlaylistID = location.search.split('playlist=')[1];
    var userID = generalFactory.getCookieData();

    $scope.currEvent = { "DJId": DJID, "UserId": userID, "PlaylistId": PlaylistID };
    
    promises.push($http({
        method: 'GET',
        url: url + '/api/DJs/' + DJID
    }));

    promises.push($http({
        method: 'GET',
        url: url + '/api/Users/' + userID
    }));

    promises.push($http({
        method: 'GET',
        url: url + '/api/UserAnswers/eventData/' + userID
    }));

    //Resolve all promise into the promises array
    $q.all(promises).then(function (response) {
        $scope.currDJ = response[0].data;
        $scope.currUser = response[1].data;
        var answers = response[2].data;
        $scope.currEvent.Date = answers.date;
        $scope.currEvent.EventType = answers.type;
        
    });

    $scope.createEvent = function () {
        $http({
            method: "POST",
            url: '/api/Events',
            data: $scope.currEvent

        }).then(function (d) {
            if (d.data != null) {
                generalFactory.setEventData(d.data.ID);
                window.location.href = '/Events/Details?id=' + d.data.ID;
            } else {
                alert("The event has a problem");
            }
        },
            function () {
                alert('failed');
            });
    };
});