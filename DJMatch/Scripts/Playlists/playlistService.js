djApp.factory("playlistFactory", function () {

    var data = {
        chosenPlalist: {}
    };

    return {
        getChosenPlaylist: function () {
            return data.chosenPlalist;
        },
        seChosenPlaylist: function (playlist) {
            data.ChosenPlaylist = playlist;
        }
    };
});