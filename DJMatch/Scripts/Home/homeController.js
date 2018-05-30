djApp.controller("homeController", function ($scope, $http, generalFactory) {
    $scope.loading = false;
    $scope.user = {};
    $scope.newUser = {};
    $scope.register = false;

    var userData = generalFactory.getCookieData(); 

    if (userData) {
        window.location.href = '/Users/UserProfile';
    }

    $scope.registerFunc = function () {
        $scope.register = true;
    }

    $scope.login = function () {
        $scope.loading = true;
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
            $scope.loading = false;
            },

            function () {
                $scope.loading = false;
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
                generalFactory.setCookieData(d.data.ID);
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