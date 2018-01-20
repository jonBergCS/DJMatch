djApp.factory('djsService', function ($http) {

    var fac = {};

    fac.djsList = [];
    fac.currentDJ = {};
    fac.currentDJID = null;

    fac.getDjs = function ()
    {
        return(
        $http({
            method: 'GET',
            url: url + '/api/DJs'
        }));
    };

    fac.getCurrentDJDetails = function (djID) {
        return (
            $http({
                method: 'GET',
                url: url + '/api/DJs/' + djID
            }).then(function successCallback(response) {
                currentDJ = response.data;
                return currentDJ;
            }));

    };

    return fac;
});