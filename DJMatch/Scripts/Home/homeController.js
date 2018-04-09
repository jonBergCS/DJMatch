djApp.controller("homeController", function ($scope, $http) {

    $scope.btntext = "Login";

    $scope.login = function () {

        $scope.btntext = "Please wait..!";
        $http({
            method: "POST",
            url: '/Home/userlogin',
            data: $scope.user

        }).then(function (d) {
            debugger;
            $scope.btntext = 'Login';
            if (d.data == "True") {
                window.location.href = '/Users/UserProfile';
            } else {
                alert(d.data);
            }
            
            $scope.user = null;
        }, function () {
            alert('failed');
        })

    }



})