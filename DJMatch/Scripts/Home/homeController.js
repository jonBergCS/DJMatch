djApp.controller("homeController", function ($scope, $http) {

    $scope.btntext = "Login";
    $scope.btnSigntext = "Sign Up";

    $scope.login = function() {

        $scope.btntext = "Please wait..!";
        $http({
            method: "POST",
            url: '/api/Users/login',
            data: $scope.user

        }).then(function (d) {
                debugger;
                $scope.btntext = 'Login';
                if (d != null) {
                    window.location.href = '/Users/UserProfile';
                } else {
                    alert("Email or Password is wrong");
                }

                $scope.user = null;
            },
            function() {
                alert('failed');
            });

    };

    /*$scope.signUp = function() {

        $scope.btnSigntext = "Please wait..!";
        $http({
            method: "POST",
            url: '/Home/userlogin',
            data: $scope.user

        }).then(function (d) {
                $scope.btntext = 'Login';
                if (d != null) {
                    window.location.href = '/Users/UserProfile';
                } else {
                    alert("Email or Password is wrong");
                }

                $scope.user = null;
            },
            function () {
                alert('failed');
            });
    }*/


})