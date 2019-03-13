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

NeptuneMaps.TrashAssessmentMap.prototype.CreateObservationsLayer = function(geoJsonFeatureCollection, options) {
    var layerOptions = Object.assign({}, NeptuneMaps.TrashAssessmentMap.ObservationLayerDefaultOptions);
    L.Util.extend(layerOptions, options);

    return L.geoJson(geoJsonFeatureCollection, layerOptions);
};


NeptuneMaps.TrashAssessmentMap.ObservationLayerDefaultOptions = {
    pointToLayer: function(feature, latlng) {
        var icon = L.MakiMarkers.icon({
            icon: feature.properties.FeatureGlyph,
            color: feature.properties.FeatureColor,
            size: "m"
        });

        return L.marker(latlng,
            {
                icon: icon,
                title: feature.properties.Name,
                alt: feature.properties.Name
            });
    }
};
