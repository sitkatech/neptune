/* Extension of Neptune.Maps with functionality for changing WQMP boundary */

NeptuneMaps.WQMPBoundaryMap = function (mapInitJson, initialBaseLayerShown, geoserverUrl, options) {

    if (!options) {
        options = {};
    }
    jQuery.extend(this.options, options);
    NeptuneMaps.Map.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl, this.options);

    var parcelsLegendUrl = geoserverUrl +
        "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3AParcels&style=parcel_alt&scale=5000&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
    var parcelsLabel = "<span>Parcels </br><img src='" + parcelsLegendUrl + "'/></span>";

    this.addWmsLayer("OCStormwater:Parcels", parcelsLabel,
        {
            styles: "parcel_alt"
        });

    if (mapInitJson.wqmpBoundary) {
        this.CreateBoundaryLayer(mapInitJson.wqmpBoundary.GeoJsonFeatureCollection, {});
        this.boundaryLayer.bringToFront();
    };
};

NeptuneMaps.WQMPBoundaryMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.Map.prototype);

NeptuneMaps.WQMPBoundaryMap.prototype.CreateBoundaryLayer = function (geoJsonFeatureCollection, options) {
    var layerOptions = jQuery.extend({}, NeptuneMaps.WQMPBoundaryMap.BoundaryLayerDefaultOptions);
    L.Util.extend(layerOptions, options);

    this.boundaryLayer = L.geoJson(geoJsonFeatureCollection, layerOptions);

    this.boundaryLayer.addTo(this.map);

    return this.boundaryLayer;
};

NeptuneMaps.WQMPBoundaryMap.BoundaryLayerDefaultOptions = {
    style: function(feature) {
        return {
            fillColor: "#4782ff",
            fill: true,
            fillOpacity: 0.4,
            color: "#4782ff",
            weight: 5,
            stroke: true
        };
    }
}

// placeholder for when options eventually become useful?
NeptuneMaps.WQMPBoundaryMap.prototype.options = {
    collapseLayerControl: false
};
