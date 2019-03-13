/* Extension of GeoServerMap with functionality for the Onland Visual Trash Assessment Workflow */

NeptuneMaps.TrashAssessmentMap = function (mapInitJson, initialBaseLayerShown, geoserverUrl) {
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl);
    this.addWmsLayer("OCStormwater:LandUseBlocks", "Land Use Blocks");
    this.addWmsLayer("OCStormwater:Parcels", "Parcels",
        {
            styles: "parcel_alt"
        });
};

NeptuneMaps.TrashAssessmentMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);