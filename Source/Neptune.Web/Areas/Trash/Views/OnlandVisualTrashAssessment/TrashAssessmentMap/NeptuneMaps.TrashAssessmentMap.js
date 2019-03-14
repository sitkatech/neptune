/* Extension of GeoServerMap with functionality for the Onland Visual Trash Assessment Workflow */

NeptuneMaps.TrashAssessmentMap = function (mapInitJson, initialBaseLayerShown, geoserverUrl) {
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl);
    this.addWmsLayer("OCStormwater:LandUseBlocks", "Land Use Blocks");
    this.addWmsLayer("OCStormwater:Parcels", "Parcels",
        {
            styles: "parcel_alt"
        });

    var legendOptions = {
        layer: "OCStormwater:LandUseBlocks",
        style: "",
        legend_options: "forceLabels:on:fontAntiAliasing:true:dpi:200",
        format: "image/png"
    };
    this.wmsLegend = L.wmsLegend("Land Use Types", geoserverUrl, legendOptions, this.map);
};

NeptuneMaps.TrashAssessmentMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.TrashAssessmentMap.prototype.CreateObservationsLayer = function(geoJsonFeatureCollection, options) {
    var layerOptions = Object.assign({}, NeptuneMaps.TrashAssessmentMap.ObservationLayerDefaultOptions);
    L.Util.extend(layerOptions, options);

    this.observationsLayer = L.geoJson(geoJsonFeatureCollection, layerOptions);
    this.observationsLayer.addTo(this.map);

    return this.observationsLayer;
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

NeptuneMaps.TrashAssessmentMap.prototype.SetActiveObservationByID = function (observationID) {
    if (!this.observationsLayer) {
        console.error("Tried to set active observation before initializing observation layer")
    }

    var layer = _.find(this.observationsLayer._layers,
        function (layer) { return observationID === layer.feature.properties.ObservationID; });
    this.setSelectedFeature(layer.feature);
};