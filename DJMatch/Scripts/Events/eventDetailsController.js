djApp.controller("eventDetailsController", function eventDetailsController($scope) {
    $scope.currUser = "Michal Teverovsky";
    $scope.DJ = { Name: "Skazi" };
    $scope.currEvent = { Date: "23/03/2019", EventType: "wedding", Name: "Best wedding", Description: "Our day" };
    $scope.currPlaylist = { Name: "Wedding" };
});