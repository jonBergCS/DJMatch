djApp.factory("generalFactory", function ($rootScope) {
    service = {
        getCurrentUser: function () {
            return $rootScope.currentUser;
        },
        setCurrentUser: function (user) {
            $rootScope.currentUser = user;
        }
    };
    return service;
});