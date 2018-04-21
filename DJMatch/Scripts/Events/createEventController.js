djApp.controller("createEventController", function createEventController($scope) {
    //var url = new URL(window.location.href);
    $scope.currUser = "Michal Teverovsky";
    $scope.DJ = { Name: "Skazi" };
    $scope.currEvent = { Date: "23/03/2019", EventType: "wedding" };
    $scope.currPlaylist = { Name: "Wedding" };

    $scope.createEvent = function () {
        window.location.href = '/Events/Details';
    };
});