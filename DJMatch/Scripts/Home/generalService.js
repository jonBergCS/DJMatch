djApp.factory("generalFactory", function ($cookies) {
    var userID = "";
    var eventID = "";
    var DJID = "";

    return {
        setCookieData: function (_userID) {
            userID = _userID;
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
            $cookies.remove("userID", { path: '/' });
        },

        setEventData: function (_eventID) {
            eventID = _eventID;
            $cookies.put("eventID", eventID);
        },
        getEventData: function () {
            eventID = $cookies.get("eventID");
            return eventID;
        },
        clearEventData: function () {
            eventID = "";
            $cookies.remove("eventID");
        }
    };
});