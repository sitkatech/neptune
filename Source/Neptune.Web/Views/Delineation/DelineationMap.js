﻿/* Extension of GeoServerMap with functionality for the Delineation Workflow
 * Leaflet controls (JS) in DelineationMapControls.js
 * Leaflet controls (HTML Templates) in DelineationMap.cshtml (TODO: move to DelineationMapTemplate)
 */

NeptuneMaps.DelineationMap = function (mapInitJson, initialBaseLayerShown, geoserverUrl) {
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl);

    this.treatmentBMPLayerLookup = new Map();

    // ensure that wms layers fetched through the GeoServerMap interface are always above all other layers
    var networkCatchmentPane = this.map.createPane("networkCatchmentPane");
    networkCatchmentPane.style.zIndex = 10000;
    this.map.getPane("markerPane").style.zIndex = 10001;

    window.stormwaterNetworkLayer = this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "Stormwater Network");

    window.networkCatchmentLayer =
        this.addWmsLayer("OCStormwater:NetworkCatchments",
            "Network Catchments",
            { pane: "networkCatchmentPane" }, function (evt) {

                this.selectFeatureByWfs({
                    cql_filter: "intersects(CatchmentGeometry, POINT(" +
                        evt.latlng.lat +
                        " " +
                        evt.latlng.lng +
                        "))"
                },
                    "OCStormwater:NetworkCatchments",
                    function (json) {
                        if (json.features[0]) {
                            this.setSelectedFeature(json.features[0]);
                            // ReSharper disable once MisuseOfOwnerFunctionThis
                            this.selectedAssetControl.networkCatchment(json.features[0]);
                        }
                    }.bind(this));
            }.bind(this)
        );

    this.addWmsLayerWithParams("OCStormwater:Parcels",
        "All Parcels",
        {
            styles: "parcel_alt",
            pane: "overlayPane"
        });
    this.initializeTreatmentBMPClusteredLayer(mapInitJson);
    window.networkCatchmentLayer.bringToFront();

    L.control.watermark({ position: 'bottomleft' }).addTo(this.map);
    this.selectedAssetControl = L.control.delineationSelectedAsset({ position: 'topleft' });
    this.selectedAssetControl.addTo(this.map);

    this.hookupDeselectOnClick();
};

NeptuneMaps.DelineationMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);


NeptuneMaps.DelineationMap.prototype.addBeginDelineationControl = function(treatmentBMPFeature) {
    this.beginDelineationControl = L.control.beginDelineation({ position: "bottomright" }, treatmentBMPFeature);
    this.beginDelineationControl.addTo(this.map);

    this.treatmentBMPLayer.off("click");
    this.map.off("click");
};

NeptuneMaps.DelineationMap.prototype.removeBeginDelineationControl = function() {
    if (!this.beginDelineationControl) {
        return; //misplaced call
    }

    this.beginDelineationControl.remove();
    this.beginDelineationControl = null;

    this.selectedAssetControl.enableDelineationButton();

    this.hookupDeselectOnClick();
    this.hookupSelectTreatmentBMPOnClick();

    // re-enable click to select network catchments
    this.map.on("click", this.wmsLayers["OCStormwater:NetworkCatchments"].click);
};

NeptuneMaps.DelineationMap.prototype.launchDrawCatchmentMode = function() {
    this.beginDelineationControl.remove();
    this.beginDelineationControl = null;

    this.selectedAssetControl.launchDrawCatchmentMode();

    if (this.selectedBMPDelineationLayer) {
        this.zoomAndPanToLayer(this.selectedBMPDelineationLayer);
    } else {
        this.zoomAndPanToLayer(this.lastSelected);
    }

    this.buildDrawControl();
};

NeptuneMaps.DelineationMap.prototype.buildDrawControl = function() {
    this.editableFeatureGroup = new L.FeatureGroup();
    var editableFeatureGroup = this.editableFeatureGroup; // eliminates need to .bind(this) later

    // what is this doing and there is a better way to do it?
    L.geoJSON(window.delineationMap.selectedBMPDelineationLayer.getLayers()[0].feature,
        {
            onEachFeature: function(feature, layer) {
                if (layer.getLayers) {
                    layer.getLayers().forEach(function(l) { editableFeatureGroup.addLayer(l); });
                } else {
                    editableFeatureGroup.addLayer(layer);
                }
            }

        });

    var drawOptions = getDrawOptions(editableFeatureGroup);
    this.drawControl = new L.Control.Draw(drawOptions);
    this.map.addControl(this.drawControl);

    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
    }

    this.map.addLayer(this.editableFeatureGroup);
    this.map.on('draw:created',
        function(e) {
            var layer = e.layer;
            editableFeatureGroup.addLayer(layer);
            var leafletId = layer._leaflet_id;
            editableFeatureGroup._layers[leafletId].feature = new Object();
            editableFeatureGroup._layers[leafletId].feature.properties = new Object();
            editableFeatureGroup._layers[leafletId].feature.type = "Feature";
            var feature = editableFeatureGroup._layers[leafletId].feature;
        });
};

NeptuneMaps.DelineationMap.prototype.exitDrawCatchmentMode = function (save) {
    if (save) {
        // todo: persist catchment and resolve back to appropriate state, including re-wiring handlers
    }
    this.drawControl.remove();
    this.map.removeLayer(this.editableFeatureGroup);
    this.drawControl = null;
    this.editableFeatureGroup = null;

    if (!save && this.selectedBMPDelineationLayer) {
        this.map.addLayer(this.selectedBMPDelineationLayer);
    }

    this.hookupSelectTreatmentBMPOnClick();
    this.hookupDeselectOnClick();
};

NeptuneMaps.DelineationMap.prototype.initializeTreatmentBMPClusteredLayer = function(mapInitJson) {
    this.treatmentBMPLayer = L.geoJson(
        mapInitJson.TreatmentBMPLayerGeoJson.GeoJsonFeatureCollection,
        {
            pointToLayer: NeptuneMaps.DefaultOptions.pointToLayer,
            onEachFeature: function(feature, layer) {
                this.treatmentBMPLayerLookup.set(feature.properties["TreatmentBMPID"], layer);
            }.bind(this)
        });
    if (this.markerClusterGroup) {
        this.map.removeLayer(markerClusterGroup);
    }

    this.markerClusterGroup = this.makeMarkerClusterGroup(this.treatmentBMPLayer);
    this.hookupSelectTreatmentBMPOnClick();
};

NeptuneMaps.DelineationMap.prototype.retrieveAndShowBMPDelineation = function(bmpFeature) {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }

    if (!bmpFeature.properties["DelineationURL"]) {
        return;
    }

    var url = bmpFeature.properties["DelineationURL"];
    SitkaAjax.ajax({
            url: url,
            dataType: "json",
            jsonpCallback: "getJson"
        },
        function(response) {
            if (response.type !== "Feature") {
                delineationErrorAlert();
            }
            this.addBMPDelineationLayer(response);
        }.bind(this),
        function(error) {
            delineationErrorAlert();
        }
    );
};

NeptuneMaps.DelineationMap.prototype.retrieveAndShowUpstreamCatchments = function(networkCatchmentFeature) {
    this.selectedAssetControl.disableUpstreamCatchmentsButton();
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.upstreamCatchmentsLayer)) {
        this.map.removeLayer(this.upstreamCatchmentsLayer);
    }

    if (!networkCatchmentFeature.properties["NetworkCatchmentID"]) {
        return;
    }

    var url = "/NetworkCatchment/UpstreamCatchments/" + networkCatchmentFeature.properties["NetworkCatchmentID"];

    SitkaAjax.ajax({
            url: url,
            dataType: "json",
            jsonpCallback: "getJson"
        },
        this.processUpstreamCatchmentIDResponse.bind(this),
        function(error) {
            this.selectedAssetControl.enableUpstreamCatchmentsButton();
            upstreamCatchmentErrorAlert();
        }.bind(this)
    );
};

NeptuneMaps.DelineationMap.prototype.processUpstreamCatchmentIDResponse = function(response) {
    this.selectedAssetControl.reportUpstreamCatchments(response.networkCatchmentIDs.length);

    if (response.networkCatchmentIDs.length === 0) {
        this.selectedAssetControl.enableUpstreamCatchmentsButton();
        return;
    }
    var parameters = L.Util.extend(this.createWfsParamsWithLayerName("OCStormwater:NetworkCatchments"),
        {
            cql_filter: "NetworkCatchmentID IN (" + response.networkCatchmentIDs.toString() + ")"
        });

    SitkaAjax.ajax({
            url: this.geoserverUrlOWS + L.Util.getParamString(parameters),
            dataType: "json",
            jsonpCallback: "getJson"
        },
        this.processUpstreamCatchmentGeoServerResponse.bind(this),
        function(error) {
            this.selectedAssetControl.enableUpstreamCatchmentsButton();
            upstreamCatchmentErrorAlert();
        }.bind(this));
};

NeptuneMaps.DelineationMap.prototype.processUpstreamCatchmentGeoServerResponse = function(response) {
    this.selectedAssetControl.enableUpstreamCatchmentsButton();

    this.upstreamCatchmentsLayer = L.geoJSON(response,
        {
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
        });
    this.upstreamCatchmentsLayer.addTo(this.map);
};

NeptuneMaps.DelineationMap.prototype.addBMPDelineationLayer = function(geoJsonResponse) {
    this.selectedBMPDelineationLayer = L.geoJson(geoJsonResponse,
        {
            style: function(feature) {
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

    this.selectedBMPDelineationLayer.addTo(this.map);
};

NeptuneMaps.DelineationMap.prototype.removeBMPDelineationLayer = function() {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }
};

NeptuneMaps.DelineationMap.prototype.removeUpstreamCatchmentsLayer = function() {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.upstreamCatchmentsLayer)) {
        this.map.removeLayer(this.upstreamCatchmentsLayer);
        this.upstreamCatchmentsLayer = null;
    }
};

NeptuneMaps.DelineationMap.prototype.preselectTreatmentBMP = function(treatmentBMPID) {
    if (!treatmentBMPID) {
        return; //misplaced call
    }
    var layer = this.treatmentBMPLayerLookup.get(treatmentBMPID);
    this.zoomAndPanToLayer(layer);
    this.setSelectedFeature(layer.feature);
    this.selectedAssetControl.treatmentBMP(layer.feature);
    this.retrieveAndShowBMPDelineation(layer.feature);
};

NeptuneMaps.DelineationMap.prototype.hookupDeselectOnClick = function() {
    this.map.on('click',
        function(e) {
            this.deselect(function() {
                this.removeBMPDelineationLayer();
                this.removeUpstreamCatchmentsLayer();
                this.selectedAssetControl.reset();
            }.bind(this));
        }.bind(this));
};

NeptuneMaps.DelineationMap.prototype.hookupSelectTreatmentBMPOnClick = function () {
    this.treatmentBMPLayer.on("click",
        function (e) {
            //this.zoomAndPanToLayer(e.layer);
            this.removeUpstreamCatchmentsLayer();
            this.setSelectedFeature(e.layer.feature);
            this.selectedAssetControl.treatmentBMP(e.layer.feature);
            this.retrieveAndShowBMPDelineation(e.layer.feature);
        }.bind(this));
};


// helpers

var delineationErrorAlert = function() {
    alert(
        "There was an error retrieving the BMP Delineation. Please try again. If the problem persists, please contact Support.");
};

var upstreamCatchmentErrorAlert = function() {
    alert(
        "There was an error retrieving the upstream catchments. Please try again. If the problem persists, please contact Support.");
};

var getDrawOptions = function (editableFeatureGroup) {
    var options = {
        position: 'topleft',
        draw: {
            polyline: false,
            polygon: {
                allowIntersection: false, // Restricts shapes to simple polygons
                drawError: {
                    color: '#e1e100', // Color the shape will turn when intersects
                    message: 'Self-intersecting polygons are not allowed.' // Message that will show when intersect
                },
                shapeOptions: {
                    color: '#f357a1'
                }
            },
            circle: false, // Turns off this drawing tool
            rectangle: false,
            marker: false
        },
        edit: {
            featureGroup: editableFeatureGroup, //REQUIRED!!
            edit: {
                maintainColor: true,
                opacity: 0.3
            },
            remove: true
}
    };
    return options;
};
