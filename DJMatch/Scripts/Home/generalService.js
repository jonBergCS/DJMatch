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
            return userID;
        },
        clearCookieData: function () {
            userID = "";
            $cookies.remove("userID");
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
        },

        setDJData: function (_DJID) {
            DJID = _DJID;
            $cookies.put("djID", DJID);
        },
        getDJData: function () {
            DJID = $cookies.get("djID");
            return DJID;
        },
        clearDJData: function () {
            DJID = "";
            $cookies.remove("djID");
        }
    };
});