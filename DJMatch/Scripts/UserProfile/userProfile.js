﻿djApp.controller("userProfileController", function userProfileController($rootScope, $scope, $http, generalFactory) {
    var userID = generalFactory.getCookieData();
    $scope.loading = true;

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
                url: url + '/api/UserAnswers/' + userID
            }).then(function successCallback(response) {
                var currentAnswers = response.data; 
                $scope.currentIndex = 0;
                $scope.userAnswers = {};
                $scope.dateAnswers = {};
                $scope.toSend = [];

                for (var i = 0; i < $scope.questions.length; i++) {
                    $scope.questions[i].Answers = [];

                    if ($scope.questions[i].IsSingleAnswer) {
                        $scope.questions[i].type = 'radio';
                    }
                    else {
                        $scope.questions[i].type = 'checkbox';
                    }
                }

                // Get the previous answers and match answers to question
                for (var i = 0; i < $scope.answers.length; i++) {
                    var result = currentAnswers.filter(function (obj) {
                        return obj.AnswerID == $scope.answers[i].ID;
                    });

                    if (result.length != 0) {
                        $scope.userAnswers[$scope.answers[i].ID] = true;
                    }
                    else {
                        $scope.userAnswers[$scope.answers[i].ID] = false;
                    }

                    var curQues = $scope.questions.filter(x => x.ID === $scope.answers[i].QuestionID)[0];
            
                    curQues.Answers.push({ ID: $scope.answers[i].ID, Text: $scope.answers[i].Text });
                }

                // Get the date answers
                var result = currentAnswers.filter(function (obj) {
                    return obj.AnswerID == -1;
                });

                for (var i = 0; i < result.length; i++) {
                    var question = $scope.questions.filter(x => x.ID == result[i].QuestionID);
                    if (question[0].IsSingleAnswer) {
                        $scope.dateAnswers[result[i].QuestionID] = new Date(result[i].Text);
                    } else {
                        $scope.dateAnswers[result[i].QuestionID] = parseInt(result[i].Text);
                    }
                }

                $scope.loading = false;
            });
        });
    });

    $scope.save = function () {
        $scope.loading = true;
        $scope.next();

        // Save the date answers
        var dateQuestions = Object.getOwnPropertyNames($scope.dateAnswers);

        for (var i = 0; i < dateQuestions.length; i++) {
            $scope.toSend.push({
                UserID: userID,
                QuestionID: parseInt(dateQuestions[i]),
                AnswerID: -1,
                Text: $scope.dateAnswers[dateQuestions[i]]
            });
        }

        var toSend = $scope.toSend;

        $http({
            method: 'POST',
            url: url + '/api/UserAnswers/array',
            data: toSend
        }).then(function successCallback(response) {
            window.location = url + '/DJs/Index'
        });
    };

    $scope.next = function () {
        // Save the current question's answers
        var currentAnswers = $scope.questions[$scope.currentIndex].Answers;

        if (currentAnswers.length != 0) {
            if ($scope.questions[$scope.currentIndex].type == 'radio') {
                var index = $scope.toSend.indexOf($scope.toSend.filter(x => x.QuestionID == $scope.questions[$scope.currentIndex].ID)[0]);
                delete $scope.toSend[index];

                for (var i = 0; i < currentAnswers.length; i++) {
                    $scope.userAnswers[currentAnswers[i].ID] = false;
                }

                var checkedAnswer =
                    $('#answers' + $scope.questions[$scope.currentIndex].ID + " input[type = 'radio']:checked").val();

                $scope.userAnswers[checkedAnswer] = true;

                $scope.toSend.push({
                    UserID: userID,
                    QuestionID: $scope.questions[$scope.currentIndex].ID,
                    AnswerID: checkedAnswer,
                    Text: ""
                });
            } else {
                for (var i = 0; i < currentAnswers.length; i++) {
                    // Check if the answer is checked
                    if ($scope.userAnswers[currentAnswers[i].ID] == true) {
                        $scope.toSend.push({
                            UserID: userID,
                            QuestionID: $scope.questions[$scope.currentIndex].ID,
                            AnswerID: currentAnswers[i].ID,
                            Text: ""
                        });
                    }
                }
            }
        }

        $scope.currentIndex++;
    };

    $scope.previous = function () {
        $scope.currentIndex--;
    }
});