djApp.factory("generalFactory", function () {


    service = {
        currentUser: {},

        getCurrentUser: function () {
            return service.currentUser;
        },
        setCurrentUser: function (user) {
            service.currentUser = user;
        }
    };
    return service;
});