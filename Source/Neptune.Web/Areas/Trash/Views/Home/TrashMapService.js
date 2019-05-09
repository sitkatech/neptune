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
    };

    trashMapServiceInstance.getMapState = function() {
        return {
            center: this.center,
            zoom: this.zoom,
            stormwaterJurisdictionID: this.stormwaterJurisdictionID
        };
    };

    window.test = function() {
        console.log(trashMapServiceInstance);
    };

    return trashMapServiceInstance;
}]);