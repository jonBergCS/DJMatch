djApp.factory("generalFactory", function ($cookies) {
    var userID = "";

    return {
        setCookieData: function (userID) {
            userID = userID;
            $cookies.put("userID", userID);
        },
        getCookieData: function () {
            userID = $cookies.get("userID");
            if (!userID && window.location.href !== url + '/') {
                window.location.href = '/';
            }

            return userID;
        },
        clearCookieData: function () {
            userID = "";
            $cookies.remove("userID");
        },

        setEventData: function (eventID) {
            eventID = eventID;
            $cookies.put("eventID", eventID);
        },
        getEventData: function () {
            eventID = $cookies.get("eventID");
            return eventID;
        },
        clearEventData: function () {
            eventID = "";
            $cookies.remove("eventID");
            window.location.href = '/';
        }
    };
});