/*-----------------------------------------------------------------------
<copyright file="NeptuneMaps.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
var NeptuneMaps = {};

/* ====== Main Map ====== */
NeptuneMaps.Map = function (mapInitJson, initialBaseLayerShown)
{
    var self = this;
    this.MapDivId = mapInitJson.MapDivID;
    var tileOptions = {
        maxNativeZoom: 18,
        maxZoom:22
    };
    var esriAerialUrl = 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}';
    var esriAerial = new L.TileLayer(esriAerialUrl, tileOptions);

    var esriStreetUrl = 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}';
    var esriStreet = new L.TileLayer(esriStreetUrl, tileOptions);

    var esriTerrainUrl = 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}';
    var esriTerrain = L.tileLayer.wms(esriTerrainUrl, tileOptions);

    var streetLabelsLayer = new L.TileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Transportation/MapServer/tile/{z}/{y}/{x}', {});

    var baseLayers = { 'Aerial': esriAerial, 'Street': esriStreet, 'Terrain': esriTerrain };
    var overlayLayers = { 'Street Labels': streetLabelsLayer };

    var streetLayerGroup;
    if (initialBaseLayerShown === "Hybrid")
    {
         streetLayerGroup = new L.LayerGroup();
        initialBaseLayerShown = "Aerial"; //Since switching to ArcGIS maps, there is no "Hybrid", so use "Aerial" instead.
        streetLabelsLayer.addTo(streetLayerGroup);
    }
    else if (Sitka.Methods.isUndefinedNullOrEmpty(initialBaseLayerShown) || baseLayers[initialBaseLayerShown] == null)
    {
        initialBaseLayerShown = "Terrain";
    }    

    var options = {
        scrollWheelZoom: true, // If this is on (default) scrolling down the page will intercept and starting zooming the map
        layers: [baseLayers[initialBaseLayerShown]],
        attributionControl: false,
        fullscreenControl: mapInitJson.AllowFullScreen ? { pseudoFullscreen: true } : false
    };
    this.map = L.map(this.MapDivId, options);

    if (streetLayerGroup != null)
    {
        streetLayerGroup.addTo(this.map);
    }

    // add vector layers
    this.vectorLayers = [];

    for (var i = 0; i < mapInitJson.Layers.length; ++i) {
        var currentLayer = mapInitJson.Layers[i];
        switch (currentLayer.LayerType) {
            case "Vector":
                this.addVectorLayer(currentLayer, overlayLayers);
                break;
            case "Wms":
                this.addWmsLayer(currentLayer, overlayLayers);
                break;
            default:
                console.error("Invalid LayerType " + currentLayer.LayerType + " not added to map layers.");
        }
    }

    this.addLayersToMapLayersControl(baseLayers, overlayLayers);

    var modalDialog = jQuery(".modal");
    if (!Sitka.Methods.isUndefinedNullOrEmpty(modalDialog))
    {
        modalDialog.on("shown.bs.modal", function()
        {
            self.map.invalidateSize();
            self.setMapBounds(mapInitJson);
        });
    }
    self.setMapBounds(mapInitJson);
};

NeptuneMaps.Map.prototype.addVectorLayer = function (currentLayer, overlayLayers) {
    var self = this;

    var layerGroup = new L.LayerGroup();
    var layerGeoJson = L.geoJson(currentLayer.GeoJsonFeatureCollection, {
        pointToLayer: function (feature, latlng) {
            var featureColor = feature.properties.FeatureColor == null ? currentLayer.LayerColor : feature.properties.FeatureColor;
            var marker = L.marker(latlng, { icon: L.MakiMarkers.icon({ icon: "marker", color: featureColor, size: "s" }) });
            return marker;
        },
        style: function (feature) {
            var fillPolygonByDefault = _.includes(["Polygon", "MultiPolygon"], feature.geometry.type);
            return {
                color: feature.properties.FeatureColor == null ? currentLayer.LayerColor : feature.properties.FeatureColor,
                weight: feature.properties.FeatureWeight == null ? 2 : feature.properties.FeatureWeight,
                fill: feature.properties.FillPolygon == null ? fillPolygonByDefault : feature.properties.FillPolygon,
                fillOpacity: feature.properties.FillOpacity == null ? 0.0 : feature.properties.FillOpacity
            };
        },
        onEachFeature: function(feature, layer) {
            self.bindPopupToFeature(layer, feature);
        }
    }).addTo(layerGroup);

    if (currentLayer.LayerInitialVisibility === 1) {
        layerGroup.addTo(this.map);
    }

    if (currentLayer.HasClickThrough) {
        layerGeoJson.on("click", function (e) { self.clickThroughFeature(e); });
    }
    else if (!currentLayer.HasCustomPopups) {
        layerGeoJson.on("click", function(e) { self.getFeatureInfo(e); });
    }

    overlayLayers[currentLayer.LayerName] = layerGroup;
    this.vectorLayers.push(layerGeoJson);
};

NeptuneMaps.Map.prototype.addWmsLayer = function (currentLayer, overlayLayers) {
    var layerGroup = new L.LayerGroup(),
        wmsParams = L.Util.extend(this.wmsParams, { layers: currentLayer.MapServerLayerName }),
        wmsLayer = L.tileLayer.wms(currentLayer.MapServerUrl, wmsParams).addTo(layerGroup);

    if (currentLayer.LayerInitialVisibility === 1) {
        layerGroup.addTo(this.map);
    }

    overlayLayers[currentLayer.LayerName] = layerGroup;
    this.vectorLayers.push(wmsLayer);
};

NeptuneMaps.Map.prototype.wmsParams = {
    service: "WMS",
    transparent: true,
    version: "1.1.1",
    format: "image/png",
    info_format: "application/json",
    tiled: true
};

NeptuneMaps.Map.prototype.wfsParams = {
    service: "WFS",
    version: "2.0",
    request: "GetFeature",
    outputFormat: "application/json",
    SrsName: "EPSG:4326"
};

NeptuneMaps.Map.prototype.addLayersToMapLayersControl = function(baseLayers, overlayLayers) {
    var options = {
        collapsed: false
    };
    this.layerControl = L.control.layers(baseLayers, overlayLayers, options);
    this.layerControl.addTo(this.map);

    var closem = L.DomUtil.create("a", "leaflet-control-layers-close");
    closem.innerHTML = "Close";
    L.DomEvent.on(closem,
        "click",
        function(e) {
            jQuery(".leaflet-control-layers").removeClass("leaflet-control-layers-expanded");
            jQuery(".leaflet-control-layers-close").toggle();
        });

    jQuery(".leaflet-control-layers-toggle").on("click",
        function() {
            jQuery(".leaflet-control-layers-close").toggle();
        });

    jQuery(".leaflet-control-layers").append(closem);
};

NeptuneMaps.Map.prototype.setMapBounds = function(mapInitJson) {
    this.map.fitBounds([
        [mapInitJson.BoundingBox.Northeast.Latitude, mapInitJson.BoundingBox.Northeast.Longitude],
        [mapInitJson.BoundingBox.Southwest.Latitude, mapInitJson.BoundingBox.Southwest.Longitude]
    ]);
};

NeptuneMaps.Map.prototype.bindPopupToFeature = function(layer, feature) {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(feature.properties.PopupUrl)) {
        layer.bindPopup("Loading...", { maxWidth: 600 });
        layer.on("click",
            function() {
                //var popup = e.target.getPopup();
                jQuery.get(feature.properties.PopupUrl).done(function(data) {
                    layer.bindPopup(data).openPopup();
                });
            });
    }
};

NeptuneMaps.Map.prototype.assignClickEventHandler = function(clickEventFunction) {
    var self = this;
    for (var i = 0; i < this.vectorLayers.length; ++i) {
        var currentLayer = this.vectorLayers[i];
        currentLayer.on("click", function(e) { clickEventFunction(self, e); });
    }
    this.map.on("click", function(e) { clickEventFunction(self, e); });
};

NeptuneMaps.Map.prototype.removeClickEventHandler = function() {
    for (var i = 0; i < this.vectorLayers.length; ++i) {
        var currentLayer = this.vectorLayers[i];
        currentLayer.off("click");
    }
    this.map.off("click");
};

NeptuneMaps.Map.prototype.getFeatureInfo = function (e)
{
    var self = this,
        latlng = e.latlng,
        html = "<div>";

    html += this.formatLayerProperty("Latitude", L.Util.formatNum(latlng.lat, 4));
    html += this.formatLayerProperty("Longitude", L.Util.formatNum(latlng.lng, 4));

    var match = _(this.vectorLayers)
        .filter(function (layer) {
            return typeof layer.eachLayer !== "undefined" && self.map.hasLayer(layer);
        })
        .map(function (currentLayer) {
            return leafletPip.pointInLayer(latlng, currentLayer, true);
        })
        .flatten()
        .value();

    var propertiesGroupedByKey = _(match)
        .map(function (x) {
            return _.keys(x.feature.properties);
        })
        .flatten()
        .uniq()
        .map(function (x) {
            return {
                key: x,
                values: _(match)
                    .map(function (y) {
                        return y.feature.properties[x];
                    })
                    .filter()
                    .value()
            };
        })
        .value();

    // if there's overlap, add some content to the popup: the layer name
    // and a table of attributes
    for (var i = 0; i < propertiesGroupedByKey.length; i++) {
        var group = propertiesGroupedByKey[i];
        if (group.key !== "Short Name") {
            var key = group.values.length > 1
                    ? group.key + "s" // pluralized
                    : group.key,
                value = group.values.join(", ");
            html += this.formatLayerProperty(key, value);
        }
    }

    html += "</div>";

    this.map.openPopup(L.popup({minWidth: 200, maxWidth: 350}).setLatLng(latlng).setContent(html).openOn(this.map));   
};

NeptuneMaps.Map.prototype.clickThroughFeature = function(e) {
    var self = this,
        latlng = e.latlng;

    var match = _(this.vectorLayers)
        .filter(function (layer) {
            return typeof layer.eachLayer !== "undefined" && self.map.hasLayer(layer);
        })
        .map(function (currentLayer) {
            return leafletPip.pointInLayer(latlng, currentLayer, true);
        })
        .flatten()
        .value();

    // Map should be such that no two features overlap, otherwise possible unexpected effects.
    var feature = match.pop().feature;
    var targetUrl = feature.properties["Target URL"];

    if (!Sitka.Methods.isUndefinedNullOrEmpty(targetUrl))
    {
        window.location.href = targetUrl;
    }
}

NeptuneMaps.Map.prototype.formatLayerProperty = function (propertyName, propertyValue)
{
    if (Sitka.Methods.isUndefinedNullOrEmpty(propertyValue))
    {
        propertyValue = "&nbsp";
    }
    return "<div class=\"row\"><div class=\"col-xs-4\"><strong>" + propertyName + "</strong></div><div class=\"col-xs-8\">" + propertyValue + "</div></div>";
};

NeptuneMaps.Map.prototype.removeLayerFromMap = function (layerToRemove) {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(layerToRemove)) {
        this.map.removeLayer(layerToRemove);
    }
};

NeptuneMaps.Map.prototype.addLayerToLayerControl = function (layer, layerName, hide) { 
    this.layerControl.addOverlay(layer, layerName);
    if (hide) {
        this.map.removeLayer(layer);
    }
};

NeptuneMaps.Map.prototype.setSelectedFeature = function (feature, callback) {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.lastSelected)) {
        this.map.removeLayer(this.lastSelected);
    }

    console.log(feature.properties);

    this.lastSelected = L.geoJson(feature,
        {
            pointToLayer: function (feature, latlng) {
                var icon = L.MakiMarkers.icon({
                    icon: "marker",
                    color: "#FFFF00",
                    size: "m"
                });

                return L.marker(latlng,
                    {
                        icon: icon,
                        riseOnHover: true
                    });
            },
            style: function (feature) {
                return {
                    fillColor: "#FFFF00",
                    fill: true,
                    fillOpacity: 0.4,
                    color: "#FFFF00",
                    weight: 5,
                    stroke: true
                };
            }
        });

    this.lastSelected.addTo(this.map);

    if (callback) {
        callback(feature);
    }
};

NeptuneMaps.Map.prototype.zoomAndPanToLayer = function(layer) {
    if (layer.getLatLng) {
        this.map.panTo(layer.getLatLng());
        this.map.fitBounds(L.latLngBounds([layer.getLatLng()]), {
            maxZoom: 18
        });
    } else {
        if (layer.getBounds().isValid()) {
            this.map.fitBounds(layer.getBounds(),
            {
                maxZoom:18
            });
        }
    }
};

NeptuneMaps.Map.prototype.deselect = function () {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.lastSelected)) {
        this.map.removeLayer(this.lastSelected);
    }
};

NeptuneMaps.Map.prototype.disableUserInteraction = function() {
    this.map.dragging.disable();
    this.map.touchZoom.disable();
    this.map.doubleClickZoom.disable();
    this.map.scrollWheelZoom.disable();
    this.map.boxZoom.disable();
    this.map.keyboard.disable();
};

NeptuneMaps.Map.prototype.enableUserInteraction = function() {
    this.map.dragging.enable();
    this.map.touchZoom.enable();
    this.map.doubleClickZoom.enable();
    this.map.scrollWheelZoom.enable();
    this.map.boxZoom.enable();
    this.map.keyboard.enable();
};

NeptuneMaps.Map.prototype.blockMapImpl = function() {
    this.map.dragging.disable();
    this.map.scrollWheelZoom.disable();
    jQuery("#" + this.MapDivId).block(
        {
            message: "<span style='font-weight: bold; font-size: 14px; margin: 0 2px'>Location Not Specified</span>",
            css: {
                border: "none",
                cursor: "default"
            },
            overlayCSS: {
                backgroundColor: "#D3D3D3",
                cursor: "default"
            },
            baseZ: 999
        });
};

NeptuneMaps.Map.prototype.blockMap = function() {
    var self = this;
    this.blockMapImpl();
    var modalDialog = jQuery(".modal");
    modalDialog.on("shown.bs.modal", function() { self.blockMapImpl(); });
};

NeptuneMaps.Map.prototype.unblockMap = function() {
    this.map.dragging.enable();
    jQuery("#" + this.MapDivId).unblock();
};

// helper functions for initializing layers

NeptuneMaps.Map.prototype.addEsriReferenceLayer = function(url, layerName, popup) {
    var features = L.esri.featureLayer({
            url: url
        }
    );

    if (popup) {
        features.bindPopup(popup);
    }
    this.addLayerToLayerControl(features, layerName);
};

NeptuneMaps.Map.prototype.addEsriDynamicLayer = function (url, layerName) {
    var pane = this.map.createPane("esriPane");
    pane.style.zIndex = 300;

    var tile = L.esri.dynamicMapLayer({
        url: url}
    );

    this.addLayerToLayerControl(tile, layerName);
    tile.addTo(this.map);
    return tile;
};

NeptuneMaps.Map.prototype.makeMarkerClusterGroup = function(layerToCluster) {
    var markerClusterGroup = L.markerClusterGroup({
        maxClusterRadius: 40,
        showCoverageOnHover: false,
        iconCreateFunction: function(cluster) {
            return new L.DivIcon({
                html: '<div><span>' + cluster.getChildCount() + '</span></div>',
                className: 'treatmentBMPCluster',
                iconSize: new L.Point(40, 40)
            });
        }
    });
    layerToCluster.addTo(markerClusterGroup);
    this.addLayerToLayerControl(markerClusterGroup, "Treatment BMPs");
    markerClusterGroup.addTo(this.map);

    return markerClusterGroup;
};

// constants for things like feature color that ought to be consistent across the site

NeptuneMaps.Constants = {
    defaultPolyColor: "#4242ff",
    defaultSelectedFeatureColor: "#ffff00",
    spatialReference: 4326
};

NeptuneMaps.DefaultOptions = {
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

L.Control.Watermark = L.Control.extend({
    onAdd: function (map) {

        var img = L.DomUtil.create("img");

        img.src = "/Content/img/OCStormwater/banner_logo.png";
        img.style.width = "200px";

        return img;
    },

    onRemove: function (map) {
        jQuery(this.parentElement).remove();
    }
});

L.control.watermark = function (opts) {
    return new L.Control.Watermark(opts);
};
