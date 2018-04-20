djApp.service("generalFactory", function ($rootScope) {

    this.currentUser = {};

    this.getCurrentUser = function () {
        return service.currentUser;
    };

    this.setCurrentUser = function (user) {
        service.currentUser = user;
    };
});