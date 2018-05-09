djApp.factory("generalFactory", function ($cookies) {
    var userID = "";

    return {
        setCookieData: function (userID) {
            userID = userID;
            $cookies.put("userID", userID);
        },
        getCookieData: function () {
            userID = $cookies.get("userID");
            return userID;
        },
        clearCookieData: function () {
            userID = "";
            $cookies.remove("userID");
        }
    };
});