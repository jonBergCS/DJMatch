djApp.controller("userProfileController", function userProfileController($scope, $http) {
    $http({
        method: 'GET',
        url: url + '/api/Questions'
    }).then(function successCallback(response) {
        $scope.questions = response.data;
        $scope.currentIndex = 1;
        $scope.userAnswers = {};

        // TODO : change according to previous answers
        for (var i = 0; i < $scope.questions.length; i++) {
            for (var j = 0; j < $scope.questions[i].Answers.length; j++) {
                $scope.userAnswers[$scope.questions[i].Answers[j].ID] = false;
            }
        }
    });

    $scope.save = function (user, question, answer) {
        $http({
            method: 'POST',
            url: url + '/api/UserAnswers',
            data: { UserID: user, QuestionID: question, AnswerID: answer }
        }).then(function successCallback(response) {
            var a = response;
        }, function errorCallback(response) {
                var b = response;
        });
    };

    $scope.next = function () {
        for (var i = 0; i < $scope.questions[$scope.currentIndex - 1].Answers.length; i++) {
            if ($scope.userAnswers[$scope.questions[$scope.currentIndex - 1].Answers[i].ID] == true) {
                // TODO: get real user id
                $scope.save(1, $scope.currentIndex - 1, $scope.questions[$scope.currentIndex - 1].Answers[i].ID);
            }
        } 

        $scope.currentIndex++;
    };

    $scope.end = function () {
        $scope.next();
        window.location.href = url + '/DJs/Index'
    }
});