djApp.service('djsService', function ($http) {
    var djsList = [];
    var currentDJ = {};
    var currentDJID = null;

    var getDjs = function ()
    {
        if (djsList.length == 0)
        {
            $http({
                method: 'GET',
                url: url + '/DJs'
            }).then(function successCallback(response) {
                djsList = response.data;
                return djsList;
            });
        }
        return djsList;
    };

    var getCurrentDJDetails = function(djID)
    {
        if (currentDJ == {})
        {
            $http({
                method: 'GET',
                url: url + '/DJs/' + djID
            }).then(function successCallback(response) {
                currentDJ = response.data;
                return currentDJ;
            });
        }
        return currentDJ;
    }

    return {
        getDjs: getDjs,
        getCurrentDJDetails: getCurrentDJDetails
    };

});