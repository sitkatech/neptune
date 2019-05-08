angular.module("NeptuneApp").factory("trashMapService", ['$window', function (win) {
    var trashMapServiceInstance = {};

    trashMapServiceInstance.saveBounds = function (bounds) {
        this.bounds = bounds;
    };

    trashMapServiceInstance.saveCenter = function(center) {
        this.center = center;
    };

    trashMapServiceInstance.saveZoom = function(zoom) {
        this.zoom = zoom;
    };

    return trashMapServiceInstance;
}]);