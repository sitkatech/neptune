﻿/* Extension of GeoServerMap with functionality for the Delineation Workflow
 * Leaflet controls (JS) in DelineationMapControls.js
 * Leaflet controls (HTML Templates) in DelineationMapTemplate.cshtml
 */

NeptuneMaps.DelineationMap = function (mapInitJson, initialBaseLayerShown, geoserverUrl, config) {
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl);

    this.treatmentBMPLayerLookup = new Map();
    this.config = config;

    this.initializeTreatmentBMPClusteredLayer(mapInitJson);

    this.addDelineationWmsLayers();

    // ensure that wms layers fetched through the GeoServerMap interface are always above all other layers
    var networkCatchmentPane = this.map.createPane("networkCatchmentPane");
    networkCatchmentPane.style.zIndex = 10000;
    this.map.getPane("markerPane").style.zIndex = 10001;


    var networkCatchmentLayer =
        this.addWmsLayer("OCStormwater:NetworkCatchments",
            "<span><img src='/Content/img/legendImages/networkCatchment.png' height='12px' style='margin-bottom:3px;' /> Network Catchments</span>",
            { pane: "networkCatchmentPane" }, true);


    var parcelsLegendUrl = geoserverUrl +
        "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3AParcels&style=parcel_alt&scale=5000&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
    var parcelsLabel = "<span>Parcels </br><img src='" + parcelsLegendUrl + "'/></span>";
    this.addWmsLayer("OCStormwater:Parcels",
        parcelsLabel,
        {
            styles: "parcel_alt"
        }, true);
    networkCatchmentLayer.bringToFront();

    this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>");



    L.control.watermark({ position: 'bottomleft' }).addTo(this.map);
    this.selectedAssetControl = L.control.delineationSelectedAsset({ position: 'topleft' });
    this.selectedAssetControl.addTo(this.map);

    this.hookupDeselectOnClick();
};

NeptuneMaps.DelineationMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.DelineationMap.prototype.addDelineationWmsLayers = function () {

    var jurisdictionCQLFilter = this.config.JurisdictionCQLFilter;
    if (!Sitka.Methods.isUndefinedNullOrEmpty(jurisdictionCQLFilter)) {
        jurisdictionCQLFilter = " AND " + jurisdictionCQLFilter;
    }

    this.distributedLayer = this.addWmsLayer("OCStormwater:Delineations",
        "<span><img class='mapLegendSquare' src='/Content/img/legendImages/delineationDistributed.PNG'/></span> Delineations (Distributed)",
        { cql_filter: "DelineationType = 'Distributed'" + jurisdictionCQLFilter, maxZoom: 22 });
    this.centralizedLayer = this.addWmsLayer("OCStormwater:Delineations",
        "<span><img class='mapLegendSquare' src='/Content/img/legendImages/delineationCentralized.PNG'/></span> Delineations (Centralized)",
        { cql_filter: "DelineationType = 'Centralized'" + jurisdictionCQLFilter, maxZoom: 22 });
};

NeptuneMaps.DelineationMap.prototype.cacheBustDelineationWmsLayers = function () {
    this.distributedLayer.setParams({ wmsParameterThatDoesNotExist: Date.now() }, false);
    this.centralizedLayer.setParams({ wmsParameterThatDoesNotExist: Date.now() }, false);
}

NeptuneMaps.DelineationMap.prototype.initializeTreatmentBMPClusteredLayer = function (mapInitJson) {
    this.treatmentBMPLayer = L.geoJson(
        mapInitJson.TreatmentBMPLayerGeoJson.GeoJsonFeatureCollection,
        {
            pointToLayer: NeptuneMaps.DefaultOptions.pointToLayer,
            onEachFeature: function (feature, layer) {
                this.treatmentBMPLayerLookup.set(feature.properties["TreatmentBMPID"], layer);
            }.bind(this)
        });
    if (this.markerClusterGroup) {
        this.map.removeLayer(markerClusterGroup);
    }

    this.markerClusterGroup = this.makeMarkerClusterGroup(this.treatmentBMPLayer);

    var legendSpan = "<span><img src='https://api.tiles.mapbox.com/v3/marker/pin-m-water+935F59@2x.png' height='30px' /> Treatment BMPs</span>";
    this.layerControl.addOverlay(this.markerClusterGroup, legendSpan);
    this.hookupSelectTreatmentBMPOnClick();
};

NeptuneMaps.DelineationMap.prototype.addBeginDelineationControl = function (treatmentBMPFeature) {
    this.beginDelineationControl = L.control.beginDelineation({ position: "bottomright" }, treatmentBMPFeature);
    this.beginDelineationControl.addTo(this.map);

    this.treatmentBMPLayer.off("click");
    this.map.off("click");
};

NeptuneMaps.DelineationMap.prototype.removeBeginDelineationControl = function () {
    if (!this.beginDelineationControl) {
        return; //misplaced call
    }

    this.beginDelineationControl.remove();
    this.beginDelineationControl = null;

    this.selectedAssetControl.enableDelineationButton();

    this.hookupDeselectOnClick();
    this.hookupSelectTreatmentBMPOnClick();
};

/* "Draw Catchment Mode"
 * This is the bread and butter of the delineation workflows.
 * When in this mode, the user is given access to a Leaflet.Draw control pointed at this.selectedBMPDelineationLayer.
 * This mode is activated when the user chooses the draw option from the Begin Delineation Control or as the terminus
 * of the other delineation paths.
 */

NeptuneMaps.DelineationMap.prototype.launchDrawCatchmentMode = function () {
    this.restoreZoom = this.map.getZoom();

    if (this.beginDelineationControl) {
        this.beginDelineationControl.remove();
        this.beginDelineationControl = null;
    }

    this.selectedAssetControl.launchDrawCatchmentMode();

    if (this.selectedBMPDelineationLayer) {
        this.map.fitBounds(this.selectedBMPDelineationLayer.getBounds());
    } else {
        this.map.fitBounds(this.lastSelected.getBounds());
    }

    this.buildDrawControl();
};

NeptuneMaps.DelineationMap.prototype.testSimplify = function (tolerance) {
    if (this.simplified) {
        this.map.removeLayer(this.simplified);
        this.simplified = null;
    }
    this.map.removeLayer(this.selectedBMPDelineationLayer);

    var simplify = turf.simplify(this.selectedBMPDelineationLayer.getLayers()[0].feature, { tolerance: tolerance });

    this.simplified = L.geoJson(simplify); this.simplified.addTo(this.map);
};

NeptuneMaps.DelineationMap.prototype.buildDrawControl = function () {
    this.editableFeatureGroup = new L.FeatureGroup();
    var editableFeatureGroup = this.editableFeatureGroup; // eliminates need to .bind(this) later

    var drawOptions = getDrawOptions(editableFeatureGroup);
    this.drawControl = new L.Control.Draw(drawOptions);
    this.map.addControl(this.drawControl);

    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);

        L.geoJSON(this.selectedBMPDelineationLayer.getLayers()[0].feature,
            {
                onEachFeature: function (feature, layer) {
                    if (layer.getLayers) {
                        layer.getLayers().forEach(function (l) { editableFeatureGroup.addLayer(l); });
                    } else {
                        editableFeatureGroup.addLayer(layer);
                    }
                }

            });
    }

    /* this is not the best way to prevent drawing multiple polygons, but the other options are:
     * 1. fork Leaflet.Draw and add the functionality or
     * 2.maintain two versions of the draw control that track the same feature group but with different options
     * and the answer to both of those is "no"
     */
    this.map.addLayer(this.editableFeatureGroup);
    if (this.editableFeatureGroup.getLayers().length > 0) {
        killPolygonDraw();
    } else {
        unKillPolygonDraw();
    }

    this.map.on('draw:created',
        function (e) {
            var layer = e.layer;
            editableFeatureGroup.addLayer(layer);
            var leafletId = layer._leaflet_id;
            editableFeatureGroup._layers[leafletId].feature = new Object();
            editableFeatureGroup._layers[leafletId].feature.properties = new Object();
            editableFeatureGroup._layers[leafletId].feature.type = "Feature";

            if (editableFeatureGroup.getLayers().length > 0) {
                killPolygonDraw();
            } else {
                unKillPolygonDraw();
            }
        });

    this.map.on('draw:deleted',
        function (e) {
            if (editableFeatureGroup.getLayers().length > 0) {
                killPolygonDraw();
            } else {
                unKillPolygonDraw();
            }
        });
};

NeptuneMaps.DelineationMap.prototype.tearDownDrawControl = function () {
    this.drawControl.remove();
    this.map.removeLayer(this.editableFeatureGroup);
    this.drawControl = null;
    this.editableFeatureGroup = null;
};

NeptuneMaps.DelineationMap.prototype.exitDrawCatchmentMode = function (save) {
    if (!save) {
        if (this.selectedBMPDelineationLayer && !this.selectedBMPDelineationLayer.isUnsavedDelineation) {
            this.map.addLayer(this.selectedBMPDelineationLayer);
        } else {
            // either the selected BMP's delineation didn't exist or it should no longer
            this.selectedBMPDelineationLayer = null;
        }
        this.retrieveAndShowBMPDelineation(this.lastSelected.toGeoJSON().features[0]);
    } else {
        this.persistDrawnCatchment();
    }

    this.tearDownDrawControl();

    this.hookupSelectTreatmentBMPOnClick();
    this.hookupDeselectOnClick();

    this.map.setZoom(this.restoreZoom);
};

NeptuneMaps.DelineationMap.prototype.persistDrawnCatchment = function () {
    // had better be only one feature
    var editableFeatureJson = this.editableFeatureGroup.toGeoJSON();

    var wkt;
    if (editableFeatureJson.features.length == 1) {
        wkt = Terraformer.WKT.convert(editableFeatureJson.features[0].geometry);
    } else {
        wkt = Terraformer.WKT.convert({ type: "Polygon" });
    }

    var treatmentBMPID = this.lastSelected.toGeoJSON().features[0].properties.TreatmentBMPID;
    var delineationUrl = "/Delineation/ForTreatmentBMP/" + treatmentBMPID;

    var self = this;
    return jQuery.ajax({
        url: delineationUrl,
        data: { "WellKnownText": wkt, "DelineationType": this.delineationType },
        type: 'POST'
    }).then(function (response) {
        self.treatmentBMPLayerLookup.get(treatmentBMPID).feature.properties.DelineationURL = delineationUrl;
        self.retrieveAndShowBMPDelineation(self.lastSelected.toGeoJSON().features[0]);
        self.cacheBustDelineationWmsLayers();
    }).fail(function () {
        alert(
            "There was an error saving the delineation. Please try again. If the problem persists, please contact Support.");
    });
};

NeptuneMaps.DelineationMap.prototype.downsampleSelectedDelineation = function (tolerance) {
    if (!this.selectedBMPDelineationLayer) {
        return; //misplaced call
    }

    this.map.removeLayer(this.selectedBMPDelineationLayer);

    var feature = this.selectedBMPDelineationLayer.getLayers()[0].feature;
    this.selectedBMPDelineationLayer = null;

    var simplified = turf.simplify(feature, { tolerance: tolerance });
    this.addBMPDelineationLayer(simplified);
};

/* "Auto-Delineate Mode"
 * In this UI "mode", the map is locked down to all user interactions while waiting for the DEM service to return.
 * After a failed return, the failure is reported and the UI unblocked.
 * After a successful return, the map is put into Draw Catchment Mode for the user to revise or accept the auto-delineation.
 */

NeptuneMaps.DelineationMap.prototype.launchAutoDelineateMode = function () {
    this.beginDelineationControl.remove();
    this.beginDelineationControl = null;

    var latLng = this.lastSelected.getLayers()[0].getLatLng();

    this.disableUserInteraction();
    this.treatmentBMPLayer.off("click");
    this.displayLoading();

    var self = this;
    var autoDelineate = new NeptuneMaps.DelineationMap.AutoDelineate(this.config.AutoDelineateBaseUrl);


    var promise = autoDelineate.MakeDelineationRequestNew(latLng);


    promise.then(function(featureCollection) {
        self.addBMPDelineationLayerFromDEM(featureCollection);

        self.removeLoading();
        self.enableUserInteraction();

        self.downsampleSelectedDelineation(0.0001);
        self.launchDrawCatchmentMode();
    }).fail(function(error) {

        if (!error) {
            // generic message
            window.alert(
                "There was an error retrieving the delineation from the remote service. If the issue persists, please contact Support.");
        }

        if (error.messages) {
            // look for the error message indicating that the service has no data to work with for the given location
            var unsupportedAreaMessage = _.find(error.messages,
                function (m) {
                    return m.description === "Number of intersecting catalog unit(s): 0";
                });
            if (unsupportedAreaMessage) {
                window.alert(
                    "The DEM service does not currently have data available near the selected Treatment BMP. If you would like to help expand the service to include your jurisdiction please contact the administrators to learn more.");
            }

        } else if (error.serviceUnavailable) {
            window.alert(
                "The DEM service is currently unavailable. Please try again later.");
        } else {
            // generic message
            window.alert(
                "There was an error retrieving the delineation from the remote service. If the issue persists, please contact Support.");
        }


        self.selectedAssetControl.enableDelineationButton();
        self.removeLoading();
        self.enableUserInteraction();
        self.hookupSelectTreatmentBMPOnClick();
        self.hookupDeselectOnClick();
    });
};

/* "Trace-Delineate Mode"
 * Map is locked down while the ajax calls for the network catchment trace are run.
 *
 * After a failed return, the failure is reported and the UI unblocked.
 * After a successful return, the map is put into Draw Catchment Mode for the user to revise or accept the trace-delineation.
 */

NeptuneMaps.DelineationMap.prototype.launchTraceDelineateMode = function () {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }

    this.beginDelineationControl.remove();
    this.beginDelineationControl = null;

    var latLng = this.lastSelected.getLayers()[0].getLatLng();

    this.disableUserInteraction();
    this.treatmentBMPLayer.off("click");
    this.displayLoading();

    var self = this;
    this.selectFeatureByWfs(
        "OCStormwater:NetworkCatchments",
        {
            cql_filter: "intersects(CatchmentGeometry, POINT(" +
                latLng.lat +
                " " +
                latLng.lng +
                "))"
        }).then(function (response) {
            if (response.features[0]) {
                return self.retrieveDelineationFromNetworkTrace(response.features[0].properties.NetworkCatchmentID);
            } else {
                return jQuery.Deferred();
            }
        }).then(function (response) {
            var geoJson = JSON.parse(response);
            self.processAndShowTraceDelineation(geoJson);
            self.removeLoading();
            self.enableUserInteraction();
            self.downsampleSelectedDelineation(.001);
            self.launchDrawCatchmentMode();
        }).fail(function () {
            self.selectedAssetControl.enableDelineationButton();
            self.removeLoading();
            self.enableUserInteraction();
            self.hookupSelectTreatmentBMPOnClick();
            self.hookupDeselectOnClick();
            window.alert(
                "There was an error retrieving the delineation from the Network Catchment Trace. Please try again. If the issue persists, please contact Support.");
        }).always(function () {
            self.selectedAssetControl.enableDelineationButton();
            self.removeLoading();
            self.enableUserInteraction();
        });
};

NeptuneMaps.DelineationMap.prototype.retrieveDelineationFromNetworkTrace = function (networkCatchmentID) {
    return jQuery.ajax({
        url: "/NetworkCatchment/UpstreamDelineation/" + networkCatchmentID,
        type: "GET"
    });
}

NeptuneMaps.DelineationMap.prototype.processAndShowTraceDelineation = function (geoJson) {
    if (this.selectedBMPDelineationLayer) {
        this.selectedBMPDelineationLayer.remove();
        this.selectedBMPDelineationLayer = null;
    }

    this.selectedBMPDelineationLayer = L.geoJSON(geoJson,
        {
            style: function (feature) {
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
    this.selectedBMPDelineationLayer.addTo(this.map);
    this.selectedBMPDelineationLayer.isUnsavedDelineation = true; // so we know to clear the delineation if they cancel later
};

/* Catchment trace requires two ajax calls
 * The Neptune Application provides the list of upstream catchments
 * and GeoServer provides the actual geometry.
 * It would be ideal to rewrite these routine to use promises.
 */

NeptuneMaps.DelineationMap.prototype.retrieveAndShowUpstreamCatchments = function (networkCatchmentFeature) {
    var self = this;

    this.retrieveUpstreamCatchmentIDs(networkCatchmentFeature).then(function (response) {
        return self.retrieveUpstreamCatchmentGeometry(response);
    }).then(function (response) {
        self.addUpstreamCatchmentLayer(response);
    }).fail(function (error) {
        self.selectedAssetControl.enableUpstreamCatchmentsButton();
        upstreamCatchmentErrorAlert();
    });
};

NeptuneMaps.DelineationMap.prototype.retrieveUpstreamCatchmentIDs = function (networkCatchmentFeature) {
    this.selectedAssetControl.disableUpstreamCatchmentsButton();
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.upstreamCatchmentsLayer)) {
        this.map.removeLayer(this.upstreamCatchmentsLayer);
    }

    if (!networkCatchmentFeature.properties["NetworkCatchmentID"]) {
        return jQuery.Deferred();
    }

    var url = "/NetworkCatchment/UpstreamCatchments/" + networkCatchmentFeature.properties["NetworkCatchmentID"];

    return jQuery.ajax({
        url: url,
        type: "GET"
    });
};

NeptuneMaps.DelineationMap.prototype.retrieveUpstreamCatchmentGeometry = function (response) {
    this.selectedAssetControl.reportUpstreamCatchments(response.networkCatchmentIDs.length);

    if (response.networkCatchmentIDs.length === 0) {
        this.selectedAssetControl.enableUpstreamCatchmentsButton();
        return jQuery.Deferred();
    }
    var parameters = L.Util.extend(this.createWfsParamsWithLayerName("OCStormwater:NetworkCatchments"),
        {
            cql_filter: "NetworkCatchmentID IN (" + response.networkCatchmentIDs.toString() + ")"
        });

    var self = this;
    return jQuery.ajax({
        url: this.geoserverUrlOWS + L.Util.getParamString(parameters),
        type: "GET"
    });
};

NeptuneMaps.DelineationMap.prototype.addUpstreamCatchmentLayer = function (geoJson) {
    this.selectedAssetControl.enableUpstreamCatchmentsButton();

    this.upstreamCatchmentsLayer = L.geoJSON(geoJson,
        {
            style: function (feature) {
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

NeptuneMaps.DelineationMap.prototype.removeUpstreamCatchmentsLayer = function () {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.upstreamCatchmentsLayer)) {
        this.map.removeLayer(this.upstreamCatchmentsLayer);
        this.upstreamCatchmentsLayer = null;
    }
};

NeptuneMaps.DelineationMap.prototype.addBMPDelineationLayer = function (geoJsonResponse) {
    if (this.selectedBMPDelineationLayer) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }


    this.selectedBMPDelineationLayer = L.geoJson(geoJsonResponse,
        {
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

    this.selectedBMPDelineationLayer.addTo(this.map);
};

NeptuneMaps.DelineationMap.prototype.addBMPDelineationLayerFromDEM = function (geoJsonResponse) {
    if (this.selectedBMPDelineationLayer) {
        this.selectedBMPDelineationLayer.remove();
        this.selectedBMPDelineationLayer = null;
    }

    // Justin's service is sending back a feature collection of multi polygons, instead of a polygon, which would be the appropriate thing to send.
    // they do always seem to be in a form that this code here can pull them from.
    var hacky;
    var totalUpstream = _.find(geoJsonResponse.features, function (f) { return f.properties.WshdType === "Total Upstream"; }).geometry;
    if (totalUpstream.type === "Polygon") {
        hacky = totalUpstream;
    } else {

        hacky = {
            type: "Polygon",
            coordinates: _
                .find(geoJsonResponse.features, function (f) { return f.properties.WshdType === "Total Upstream"; })
                .geometry
                .coordinates[1]
        };
    }

    this.selectedBMPDelineationLayer = L.geoJson(hacky,
        {
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

    this.selectedBMPDelineationLayer.addTo(this.map);
    this.selectedBMPDelineationLayer.isUnsavedDelineation = true; // so we know to clear the auto-delineation if they cancel without saving later.
};

NeptuneMaps.DelineationMap.prototype.removeBMPDelineationLayer = function () {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }
};

/* For getting the BMP delineation from the Neptune DB.
 Returns a promise so the caller can mess w/ the delineation layer later if they want.
 */
NeptuneMaps.DelineationMap.prototype.retrieveAndShowBMPDelineation = function (bmpFeature) {
    if (this.selectedBMPDelineationLayer) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }

    if (!bmpFeature.properties["DelineationURL"]) {
        
        return jQuery.Deferred().resolve();
    }

    var url = bmpFeature.properties["DelineationURL"];
    var self = this;

    var promise = jQuery.ajax({
        url: url,
        dataType: "json",
        jsonpCallback: "getJson",

    }).then(function (response) {
        if (response.noDelineation) {
            self.selectedAssetControl.reportDelineationArea({ Area: "-", DelineationType: "No delineation provided" });
            bmpFeature.properties.DelineationType = "No delineation provided";
            return;
        }
        if (response.type !== "Feature") {
            delineationErrorAlert();
        }
        self.addBMPDelineationLayer(response);
        
        self.selectedAssetControl.reportDelineationArea(response.properties);
    }).fail(delineationErrorAlert);

    return promise;
};

NeptuneMaps.DelineationMap.prototype.preselectTreatmentBMP = function (treatmentBMPID) {
    if (!treatmentBMPID) {
        return; //misplaced call
    }
    var layer = this.treatmentBMPLayerLookup.get(treatmentBMPID);

    this.selectedAssetControl.treatmentBMP(layer.feature);
    var promise = this.retrieveAndShowBMPDelineation(layer.feature);

    var self = this;
    promise.then(function () { 
        if (self.selectedBMPDelineationLayer) {
            self.map.fitBounds(self.selectedBMPDelineationLayer.getBounds(), { maxZoom: 18 });
        } else {
            self.map.fitBounds(L.latLngBounds([layer.getLatLng()]), { maxZoom: 18 });
        }
        return jQuery.Deferred().resolve();

    }).always(function () {
        // don't set the selected layer until after the zoommies are done
        setTimeout(function () { self.setSelectedFeature(layer.feature); }, 500);
    });
};

/* helper methods to restore UI interactions after a blocking mode returns */

NeptuneMaps.DelineationMap.prototype.hookupDeselectOnClick = function () {
    var self = this;

    this.map.on('click',
        function (e) {
            self.deselect();
            self.removeBMPDelineationLayer();
            self.removeUpstreamCatchmentsLayer();
            self.selectedAssetControl.reset();
        });
};

NeptuneMaps.DelineationMap.prototype.hookupSelectTreatmentBMPOnClick = function () {
    var self = this;

    this.treatmentBMPLayer.on("click",
        function (e) {

            self.setSelectedFeature(e.layer.feature);
            self.selectedAssetControl.treatmentBMP(e.layer.feature);
            self.retrieveAndShowBMPDelineation(e.layer.feature);
        });
};

NeptuneMaps.DelineationMap.prototype.displayLoading = function () {
    this.frosty = new L.LeafletLoading();
    this.frosty.addTo(this.map);
    this.map.spin(true);
};
NeptuneMaps.DelineationMap.prototype.removeLoading = function () {
    if (this.frosty) {
        this.frosty.remove();
        this.frosty = null;
    }
    this.map.spin(false);
};




/* assorted miscellaneous helper functions */

var delineationErrorAlert = function () {
    window.alert(
        "There was an error retrieving the BMP Delineation. Please try again. If the problem persists, please contact Support.");
};

var upstreamCatchmentErrorAlert = function () {
    window.alert(
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

var killPolygonDraw = function () {
    jQuery(".leaflet-draw-toolbar-top").hide();
};

var unKillPolygonDraw = function () {
    jQuery(".leaflet-draw-toolbar-top").show();
}