djApp.controller("homeController", function ($scope, $http, generalFactory) {
    $scope.user = {};
    $scope.newUser = {};

    $scope.login = function () {
        $http({
            method: "POST",
            url: '/api/Users/login',
            data: $scope.user

        }).then(function (d) {
            if (d.data != null) {
                generalFactory.setCookieData(d.data.ID);
                window.location.href = '/Users/UserProfile';
            } else {
                alert("Email or Password is wrong");
            }

            $scope.user = null;
        },
            function () {
                alert('failed');
            });

    };

    $scope.signUp = function () {

        $http({
            method: "POST",
            url: '/api/Users',
            data: $scope.newUser

        }).then(function (d) {
            if (d.data != null) {
                window.location.href = '/Users/UserProfile';
            } else {
                alert("The user has a problem");
            }

            $scope.newUser = null;
        },
             function () {
                 alert('failed');
             });
    }


});