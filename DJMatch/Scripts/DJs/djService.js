djApp.service('djsService', function ($http) {
    var djsList = [];
    var currentDJ = {};
    var currentDJID = null;

    var getDjs = function ()
    {
        return(
        $http({
            method: 'GET',
            url: url + '/api/DJs'
        }));
    };

    var getCurrentDJDetails = function (djID) {
        return (
            $http({
                method: 'GET',
                url: url + '/api/DJs/' + djID
            }).then(function successCallback(response) {
                currentDJ = response.data;
                return currentDJ;
            }));

    };

    return {
        djsList : djsList,
        currentDJ : currentDJ,
        currentDJID: currentDJID,
        getDjs: getDjs,
        getCurrentDJDetails: getCurrentDJDetails
    };
});