﻿djApp.controller("djsController", function djsController($rootScope, $scope, $q, $http, generalFactory) {
    $scope.userID = generalFactory.getCookieData();
    $scope.loading = true;
    $scope.rate = 0;
    $scope.review = { reviewText: "" };

    if (($scope.djsList == undefined) || ($scope.djsList.size() == 0)) {
        var promises = [];

        var defer = $q.defer();

        promises.push($http({
            method: 'GET',
            url: url + '/api/Djs/filterForUser/' + generalFactory.getCookieData()
        }));

        //Resolve all promise into the promises array
        $q.all(promises).then(function (response) {
            $scope.djsList = response[0].data.result;
            var recommended = response[0].data.recommendedDJId;

            if (recommended !== "") {
                $scope.recommended = $scope.djsList.filter(dj => dj.ID == recommended)[0];
                var index = $scope.djsList.indexOf($scope.recommended);
                $scope.djsList.splice(index, 1);
            }

            $scope.loading = false;
        });
    }
    

    $scope.displayDetails = function (djID) {
        $scope.loading = true;
        $scope.currentDJID = djID;

        if (($scope.currentDJ == undefined) || ($scope.currentDJ == {}))
        {
            var promises = [];

            var defer = $q.defer();

            // get basic DJ info
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID
            }));

            // get DJ's reviews
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/reviews'
            }));

            // get DJ's rate
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/rate'
            }));

            // get all DJ's playlists
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/playlists'
            }));

            // get DJ's default playlists
            promises.push($http({
                method: 'GET',
                url: url + '/api/DJs/' + djID + '/playlists/default'
            }));

            promises.push($http({
                method: 'GET',
                url: url + '/api/Users'
            }));

            //Resolve all promise into the promises array
            $q.all(promises).then(function (response) {
                $scope.currentDJ = response[0].data;
                $scope.currentDJ.Reviews = response[1].data;
                $scope.currentDJ.Rate = response[2].data;
                $scope.currentDJ.Rate = Number($scope.currentDJ.Rate).toFixed(1);   
                $scope.currentDJ.Playlists = response[3].data;
                $scope.currentDJ.DefaultPlaylists = response[4].data;
                $scope.allUsers = response[5].data;
                $scope.loading = false;
            });
        }
    };

    $scope.backToList = function ()
    {
        $scope.currentDJ = undefined;
    };

    $scope.createEventPage = function (currPlaylist) {
        window.location.href = '/Events/Create?dj=' + currPlaylist.DJ_ID + '&playlist=' + currPlaylist.ID;
    };

    $scope.checkStar = function (event) {
        if (event.srcElement.className.includes('checked')) {
            event.srcElement.classList.remove("checked");
            $scope.rate--;
        } else {
            event.srcElement.classList.add("checked")
            $scope.rate++;
        }
    }

    $scope.saveReview = function () {
        $scope.loading = true;

        var reviewToSend = {
            DJ_ID: $scope.currentDJ.ID,
            UserID: $scope.userID,
            Text: $scope.review.reviewText,
            Score: $scope.rate,
            Date: new Date()
        }

        $scope.review.reviewText = "";
        $scope.rate = 0;
        var checkedStars = $(".checked");

        for (var i = 0; i < checkedStars.length; i++) {
            checkedStars[i].classList.remove("checked");
        }

        $http({
            method: 'POST',
            url: url + '/api/Reviews',
            data: reviewToSend
        }).then(function successCallback(response) {
            $scope.currentDJ.Reviews.push(reviewToSend);
            $scope.loading = false;
        });
    }

    $scope.getDate = function(date) {
        return new Date(date).toLocaleDateString();
    }

    $scope.getUserName = function (userId) {
        return $scope.allUsers.filter(user => user.ID == userId)[0].Name;
    }
});