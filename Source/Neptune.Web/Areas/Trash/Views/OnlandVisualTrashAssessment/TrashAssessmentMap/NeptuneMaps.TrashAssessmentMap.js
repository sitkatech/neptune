/* Extension of GeoServerMap with functionality for the Onland Visual Trash Assessment Workflow */

NeptuneMaps.TrashAssessmentMap = function (mapInitJson, initialBaseLayerShown, geoserverUrl, options) {

    if (!options) {
        options = {};
    }
    jQuery.extend(this.options, options);
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl, this.options);

    var landUseBlocksLegendUrl = geoserverUrl +
        "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ALandUseBlocks&style=&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
    var landUseBlocksLabel = "<span>Land Use Blocks </br><img src='" + landUseBlocksLegendUrl + "'/></span>";

    var parcelsLegendUrl = geoserverUrl +
        "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3AParcels&style=parcel_alt&scale=5000&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
    var parcelsLabel = "<span>Parcels </br><img src='" + parcelsLegendUrl + "'/></span>";

    this.addWmsLayer("OCStormwater:LandUseBlocks", landUseBlocksLabel);
    this.addWmsLayer("OCStormwater:Parcels", parcelsLabel,
        {
            styles: "parcel_alt"
        });

    if (mapInitJson.TransectLineLayerGeoJson) {
        this.CreateTransectLineLayer(mapInitJson.TransectLineLayerGeoJson.GeoJsonFeatureCollection, {});
        this.transectLineLayer.bringToFront();
    };
};

NeptuneMaps.TrashAssessmentMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.TrashAssessmentMap.prototype.BuildTrashAssessmentMapLegend = function() {

};

NeptuneMaps.TrashAssessmentMap.prototype.CreateObservationsLayer = function(geoJsonFeatureCollection, options) {
    var layerOptions = jQuery.extend({}, NeptuneMaps.TrashAssessmentMap.ObservationLayerDefaultOptions);
    L.Util.extend(layerOptions, options);

    this.observationsLayer = L.geoJson(geoJsonFeatureCollection, layerOptions);
    this.observationsLayer.addTo(this.map);

    if (this.options.showObservationsOnLegend) {
        this.layerControl.addOverlay(this.observationsLayer,
            "<span><img src='https://api.tiles.mapbox.com/v3/marker/pin-m-water+FF00FF@2x.png' height='30px' /> Observations</span>");
    }
    return this.observationsLayer;
};

NeptuneMaps.TrashAssessmentMap.prototype.CreateTransectLineLayer = function(geoJsonFeatureCollection, options) {
    var layerOptions = jQuery.extend({}, NeptuneMaps.TrashAssessmentMap.TransectLineLayerDefaultOptions);
    L.Util.extend(layerOptions, options);

    this.transectLineLayer = L.geoJson(geoJsonFeatureCollection, layerOptions);
    this.transectLineLayer.addTo(this.map);

    if (this.options.showTransectOnLegend) {
        this.layerControl.addOverlay(this.transectLineLayer,
            "<span><img src='/Content/img/legendImages/transectLine.png' height='12px' /> Transect</span>");
    }

    return this.transectLineLayer;
};

NeptuneMaps.TrashAssessmentMap.TransectLineLayerDefaultOptions = {
    style: function (feature) {
        return {
            fillOpacity: 0.5,
            color: "#FF00FF",
            weight: 2,
            stroke: true
        };
    }
}

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

// placeholder for when options eventually become useful
NeptuneMaps.TrashAssessmentMap.prototype.options = {
    showTransectOnLegend: true,
    showObservationsOnLegend: true,
    collapseLayerControl: true
};
