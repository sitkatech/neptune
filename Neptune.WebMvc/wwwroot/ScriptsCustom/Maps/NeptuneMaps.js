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
NeptuneMaps.Map = function(mapInitJson, initialBaseLayerShown, geoserverUrl, customOptions) {
    var self = this;

    this.geoserverUrlOWS = geoserverUrl;

    this.wmsLayers = {};

    // todo: rename to baseWmsParams to make intent explicit
    this.wmsParams = {
        service: "WMS",
        transparent: true,
        version: "1.1.1",
        format: "image/png",
        info_format: "application/json",
        tiled: true
        //        tilesorigin: [this.map.getBounds().getSouthWest().lng, this.map.getBounds().getSouthWest().lat]
    };

    // todo: rename to baseWfsParams to make intent explicit
    this.wfsParams = {
        service: "WFS",
        version: "2.0",
        request: "GetFeature",
        outputFormat: "application/json",
        SrsName: "EPSG:4326"
    };

    this.MapDivId = mapInitJson.MapDivID;
    var tileOptions = {
        maxNativeZoom: 18,
        maxZoom: 22
    };
    var esriAerialUrl = 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}';
    var esriAerial = new L.TileLayer(esriAerialUrl, tileOptions);

    var esriStreetUrl = 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}';
    var esriStreet = new L.TileLayer(esriStreetUrl, tileOptions);

    var esriTerrainUrl = 'https://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}';
    var esriTerrain = L.tileLayer.wms(esriTerrainUrl, tileOptions);

    var esriHillshadeUrl = 'https://wtb.maptiles.arcgis.com/arcgis/rest/services/World_Topo_Base/MapServer/tile/{z}/{y}/{x}';
    var esriHillshade = L.tileLayer.wms(esriHillshadeUrl, {
        maxNativeZoom: 15,
        maxZoom: 22
    });

    var streetLabelsLayer = new L.TileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Transportation/MapServer/tile/{z}/{y}/{x}', {});
    var topographyLayer = L.vectorGrid.protobuf("https://tiles.arcgis.com/tiles/UXmFoWC7yDHcDN5Q/arcgis/rest/services/Contours_2ft/VectorTileServer/tile/{z}/{y}/{x}.pbf",
        {});
    topographyLayer.id = "Topography";

    var baseLayers = { 'Aerial': esriAerial, 'Street': esriStreet, 'Terrain': esriTerrain, 'Hillshade': esriHillshade };
    var overlayLayers = { 'Street Labels': streetLabelsLayer, 'Topography': topographyLayer };

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

    if (!customOptions) {
        customOptions = {};
    }
    this.customOptions = customOptions;
    var options = {
        scrollWheelZoom:
            true, // If this is on (default) scrolling down the page will intercept and starting zooming the map
        layers: [baseLayers[initialBaseLayerShown]],
        attributionControl: false,
        fullscreenControl: mapInitJson.AllowFullScreen ? { pseudoFullscreen: true } : false
    };

    this.map = L.map(this.MapDivId, options);

    if (streetLayerGroup != null)
    {
        streetLayerGroup.addTo(this.map);
    }

    // initialize the layer control
    var collapsed = false;
    if (this.customOptions.collapseLayerControl) {
        collapsed = true;
    }

    var layerControlOptions = {
        collapsed: collapsed
    };
    this.layerControl = new BetterLayerControl(baseLayers, overlayLayers, layerControlOptions);
    this.layerControl.addTo(this.map);


    // add vector layers
    this.vectorLayers = [];
    this.vectorLayerGroups = [];
    for (var i = 0; i < mapInitJson.Layers.length; ++i) {
        var currentLayer = mapInitJson.Layers[i];
        switch (currentLayer.LayerType) {
            case "Vector":
                this.addVectorLayer(currentLayer, overlayLayers);
                break;
            case "Wms":
                this.addWmsLayer("OCStormwater:" + currentLayer.LayerName, currentLayer.LayerName);
                break;
            default:
                console.error("Invalid LayerType " + currentLayer.LayerType + " not added to map layers.");
        }
    }

    this.addLayersToMapLayersControl(baseLayers, overlayLayers, mapInitJson.LayerControlClass);

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

    //This needs to happen after the controls are added because jQuery is removing the disabled attributes after adding the control layers
    this.map.on('zoomend', NeptuneMaps.disableAppropriateControls);
};

//Function assumes there is a retina version of your image under the same name just with "-2x" appended
NeptuneMaps.Map.prototype.buildDefaultLeafletMarkerFromMarkerPath = function (iconUrl) {
    var retinaUrl = iconUrl.replace("marker-icon", "marker-icon-2x");
    return this.buildDefaultLeafletMarker(iconUrl, retinaUrl);
};

NeptuneMaps.Map.prototype.buildDefaultLeafletMarker = function (iconUrl, iconRetinaUrl) {
    let shadowUrl = '/Content/leaflet/images/marker-shadow.png';
    //todo: check into using L.divIcon in the future and not have to use images
//    return L.divIcon({
//        className: 'map-marker-icon',
//        html: "<div style='background-color:#c30b82;' class='map-marker-icon'></div><i class='glyphicon glyphicon-map-marker' style='font-size:36px; color: #935F59'></i>",
//        iconSize: [25, 41],
//        iconAnchor: [12, 41],
//        popupAnchor: [1, -34],
//        tooltipAnchor: [16, -28],
//    });
    return L.icon({
        iconRetinaUrl,
        iconUrl,
        shadowUrl,
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        tooltipAnchor: [16, -28],
        shadowSize: [41, 41]
    });
};

NeptuneMaps.Map.prototype.addVectorLayer = function (currentLayer, overlayLayers) {
    var self = this;
    var layerGroup = new L.LayerGroup();
    var layerGeoJson = L.geoJson(currentLayer.GeoJsonFeatureCollection, {
        pointToLayer: function (feature, latlng) {
            var featureColor = feature.properties.FeatureColor == null ? currentLayer.LayerColor : feature.properties.FeatureColor;
            var marker = L.marker(latlng, { icon: self.buildDefaultLeafletMarkerFromMarkerPath('/Content/leaflet/images/marker-icon-' + featureColor.replace("#", "") + '.png') });
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

    if (currentLayer.LayerInitialVisibility === "Show") {
        layerGroup.addTo(this.map);
    }

    if (currentLayer.HasClickThrough) {
        layerGeoJson.on("click", function (e) { self.clickThroughFeature(e); });
    }
    else if (!currentLayer.HasCustomPopups) {
        layerGeoJson.on("click", function(e) { /*self.getFeatureInfo(e);*/ });
    }

    overlayLayers[currentLayer.LayerName] = layerGroup;
    this.vectorLayers.push(layerGeoJson);
    this.vectorLayerGroups.push(layerGroup);
};

//NeptuneMaps.Map.prototype.addWmsLayer = function (currentLayer, overlayLayers) {
//    var layerGroup = new L.LayerGroup();
//    var wmsParams = L.Util.extend(this.wmsParams, { layers: currentLayer.MapServerLayerName });
//    var wmsLayer = L.tileLayer.wms(currentLayer.MapServerUrl, wmsParams).addTo(layerGroup);

//    if (currentLayer.LayerInitialVisibility === "Show") {
//        layerGroup.addTo(this.map);
//    }

//    overlayLayers[currentLayer.LayerName] = layerGroup;
//    this.vectorLayers.push(wmsLayer);
//};

NeptuneMaps.Map.prototype.addWmsLayer = function (layerName, layerControlLabelHtml, params, hide) {
    var wmsParams = this.createWmsParamsWithLayerName(layerName);

    if (params) { L.Util.extend(wmsParams, params); }

    var wmsLayer = L.tileLayer.wms(this.geoserverUrlOWS, wmsParams).addTo(this.map);

    this.addLayerToLayerControl(wmsLayer, layerControlLabelHtml, hide);

    return wmsLayer;
};

NeptuneMaps.Map.prototype.createWmsParamsWithLayerName = function (layerName) {
    var customParams = {
        layers: layerName
    };

    // deep-copy the base params and extend them by the customParams;
    // doing it this way ensures that the base params are left alone so future WMS layers aren't called with those params
    var wmsParams = jQuery.extend({}, this.wmsParams);
    L.Util.extend(wmsParams, customParams);

    return wmsParams;
};

NeptuneMaps.Map.prototype.createWfsParamsWithLayerName = function (layerName) {
    var customParams = {
        typeName: layerName
    };

    var wfsParams = jQuery.extend({}, this.wfsParams);
    L.Util.extend(wfsParams, customParams);
    return wfsParams;
};



NeptuneMaps.Map.prototype.selectFeatureByWfs = function (layerName, params) {
    var parameters = L.Util.extend(this.createWfsParamsWithLayerName(layerName), params);
    return jQuery.ajax({
        url: this.geoserverUrlOWS + L.Util.getParamString(parameters),
        type: "GET"
    });
};

NeptuneMaps.Map.prototype.getFeatureInfo = function (layerName, xy) {

    var x1 = xy[0];
    var y1 = xy[1];
    var x2 = x1 + 0.0001;
    var y2 = y1 + 0.0001;

    var bbox = [x1, y1, x2, y2].join(",");


    var params = {
        service: "WMS",
        transparent: true,
        version: "1.1.1",
        request: "GetFeatureInfo",
        info_format: "application/json",
        tiled: true,
        QUERY_LAYERS: layerName,
        layers: layerName,
        X: 50,
        Y: 50,
        SRS: 'EPSG:4326',
        WIDTH: 101,
        HEIGHT: 101,
        BBOX: bbox
    };

    return jQuery.ajax({
        url: this.geoserverUrlOWS + L.Util.getParamString(params),
        type: "GET"
    });
};

var BetterLayerControl = L.Control.Layers.extend(
    {
        fixClass: function (classNameToAdd) {
            this._container.className += " ";
            this._container.className += classNameToAdd;
        },

        //The following three functions allow for layer control rebuilding on zoom
        //This is beneficial for adding classes that will later disable a control based on zoom level
        onAdd: function (map) {
            this._map = map;
            map.on('zoomend', this._update, this);
            return L.Control.Layers.prototype.onAdd.call(this, map);
        },

        onRemove: function (map) {
            map.off('zoomend', this._update, this);
            L.Control.Layers.prototype.onRemove.call(this, map);
        },

        //Because of something jQuery is doing after we add our items, we can't just disable the control here
        //We'll add this class, which jQuery doesn't take away, and then call the NeptuneMaps.disableAppropriateControls function to disable and turn off the layer
        _addItem: function (obj) {
            var item = L.Control.Layers.prototype._addItem.call(this, obj);
            if (this._map.getZoom() < 14 && obj.name  === "Topography") {
                $(item).find('input').addClass('disabled-control');
            }
            return item;
        }
    });

NeptuneMaps.Map.prototype.addLayersToMapLayersControl = function(baseLayers, overlayLayers, layerControlClass) {
    var collapsed = false;
    if (this.customOptions.collapseLayerControl) {
        collapsed = true;
    }

    if (!collapsed)
    {
        var leafletControlLayersSelector = ".leaflet-control-layers";
        var closeButtonClass = "leaflet-control-layers-close";
        var closeButtonSelector = ".leaflet-control-layers-close";
        var toggleSelector = ".leaflet-control-layers-toggle";

        if (!Sitka.Methods.isUndefinedNullOrEmpty(layerControlClass)) {
            this.layerControl.fixClass(layerControlClass);
            leafletControlLayersSelector = "." + layerControlClass;
            closeButtonClass += " " + layerControlClass + "-close";
            closeButtonSelector = "." + layerControlClass + "-close";
            toggleSelector = "." + layerControlClass + " " + toggleSelector;
        }

        var closem = L.DomUtil.create("a", closeButtonClass);
        closem.innerHTML = "Close";
        L.DomEvent.on(closem,
            "click",
            function(e) {
                jQuery(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");
                jQuery(closeButtonSelector).toggle();
            });

        jQuery(toggleSelector).on("click",
            function() {
                jQuery(closeButtonSelector).toggle();
            });
        jQuery(leafletControlLayersSelector).append(closem);

        this.closeMapLayersControl = function() {
            jQuery(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");
            jQuery(closeButtonSelector).toggle();
        };
    }
};

NeptuneMaps.Map.prototype.setMapBounds = function(mapInitJson) {
    this.map.fitBounds([
        [mapInitJson.BoundingBox.Bottom, mapInitJson.BoundingBox.Left],
        [mapInitJson.BoundingBox.Top, mapInitJson.BoundingBox.Right]
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

    var self = this;

    this.lastSelected = L.geoJson(feature,
        {
            pointToLayer: function (feature, latlng) {
                var icon = self.buildDefaultLeafletMarkerFromMarkerPath('/Content/leaflet/images/marker-icon-ffff00.png');
                
                self.lastSelectedMarker = L.marker(latlng,
                    {
                        icon: icon,
                        riseOnHover: true
                    });

                return self.lastSelectedMarker;
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

NeptuneMaps.Map.prototype.zoomAndPanToLayer = function (layer) {
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

NeptuneMaps.Map.prototype.addEsriDynamicLayer = function (url, layerName, show) {
    var pane = this.map.createPane("esriPane");
    pane.style.zIndex = 300;

    var tile = L.esri.dynamicMapLayer({
        url: url}
    );

    this.addLayerToLayerControl(tile, layerName);
    if (show) {
        tile.addTo(this.map);
    }
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
    markerClusterGroup.addTo(this.map);

    return markerClusterGroup;
};

// constants for things like feature color that ought to be consistent across the site

NeptuneMaps.Constants = {
    defaultPolyColor: "#4242ff",
    defaultSelectedFeatureColor: "#ffff00",
    spatialReference: 4326
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

NeptuneMaps.epsg4326ToEpsg2771 = function(xy) {
    var projection =
        "+proj=lcc +lat_1=33.88333333333333 +lat_2=32.78333333333333 +lat_0=32.16666666666666 +lon_0=-116.25 +x_0=2000000 +y_0=500000 +ellps=GRS80 +units=m +no_defs";
    return proj4(projection, xy);
};

NeptuneMaps.disableAppropriateControls = function () {
    $(".disabled-control").each(function () {
        if ($(this).is(":checked")) {
            $(this).trigger("click");
        }
        $(this).prop("disabled", true);
    });
}
