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

    trashMapServiceInstance.saveStormwaterJurisdictionID = function(stormwaterJurisdictionID) {
        this.stormwaterJurisdictionID = stormwaterJurisdictionID;
    }

    return trashMapServiceInstance;
}]);