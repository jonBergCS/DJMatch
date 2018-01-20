djApp.controller("userProfileController", function userProfileController($scope, $http) {
    $http({
        method: 'GET',
        url: url + '/api/Questions'
    }).then(function successCallback(response) {
        $scope.questions = response.data;

        $http({
            method: 'GET',
            url: url + '/api/Answers'
        }).then(function successCallback(response) {
            $scope.answers = response.data;

            $http({
                method: 'GET',
                //TODO: change the user id
                url: url + '/api/UserAnswers/1'
            }).then(function successCallback(response) {
                var currentAnswers = response.data; 
                $scope.currentIndex = 1;
                $scope.userAnswers = {};

                for (var i = 0; i < $scope.questions.length; i++) {
                    $scope.questions[i].Answers = [];
                }

                for (var i = 0; i < $scope.answers.length; i++) {
                    // Check the previous answers
                    var result = currentAnswers.filter(function (obj) {
                        return obj.AnswerID == $scope.answers[i].ID;
                    });

                    if (result.length != 0) {
                        if (result[0].Text != null) {
                            $scope.userAnswers[$scope.answers[i].ID] = result[0].Text;
                        }
                        else {
                            $scope.userAnswers[$scope.answers[i].ID] = true;
                        }
                    }
                    else {
                        $scope.userAnswers[$scope.answers[i].ID] = false;
                    }
                    
                    $scope.questions[$scope.answers[i].QuestionID - 1].Answers.
                        push({ ID: $scope.answers[i].ID, Text: $scope.answers[i].Text });
                }
            });
        });
    });

    $scope.save = function () {
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
      //  $scope.toSend.push({ question: questionID, answer:  })
        $scope.currentIndex++;
    };
});