djApp.controller("createEventController", function createEventController($scope, $q, $http, generalFactory) {

    var promises = [];
    var userID = generalFactory.getCookieData();
    var DJID = generalFactory.getDJData();
    $scope.currEvent = {"DJId":DJID, "UserId":userID};
    
    promises.push($http({
        method: 'GET',
        url: url + '/api/DJs/' + 1 //TODO!!!!
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
        debugger;
        var answers = response[2].data;
        $scope.currEvent.Date = answers.date;
        $scope.currEvent.EventType = answers.type;
        
    });


    $scope.currEvent = { Date: "23/03/2019", EventType: "wedding" };

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

        generalFactory.clearDJData();
    };
});