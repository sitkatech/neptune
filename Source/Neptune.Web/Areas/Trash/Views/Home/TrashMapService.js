angular.module("NeptuneApp").factory("trashMapService", ['$window', function(win) {
        var trashMapServiceInstance = {};

        trashMapServiceInstance.test = function() {
            win.alert("Service is wired!");
        };

        return trashMapServiceInstance;
    }]);