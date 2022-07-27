/* Extension of Neptune.Map with functionality for the Delineation Workflow
 * Leaflet controls (JS) in DelineationMapControls.js
 * Leaflet controls (HTML Templates) in DelineationMapTemplate.cshtml
 */

NeptuneMaps.DelineationMap = function (mapInitJson, initialBaseLayerShown, geoserverUrl, config, delineationMapService) {
    NeptuneMaps.Map.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl);
    configureProj4Defs();
    this.treatmentBMPLayerLookup = new Map();
    this.config = config;
    this.mapInitJson = mapInitJson;
    this.delineationMapService = delineationMapService;
    this.initializeTreatmentBMPClusteredLayer();

    this.addDelineationWmsLayers();

    // ensure that wms layers fetched through the Neptune.Map interface are always above all other layers
    var regionalSubbasinPane = this.map.createPane("regionalSubbasinPane");
    regionalSubbasinPane.style.zIndex = 10000;
    this.map.getPane("markerPane").style.zIndex = 10001;

    var regionalSubbasinLayer =
        this.addWmsLayer("OCStormwater:RegionalSubbasins",
            "<span><img src='/Content/img/legendImages/regionalSubbasin.png' height='12px' style='margin-bottom:3px;' /> Regional Subbasins</span>",
            { pane: "regionalSubbasinPane" }, true);

    var parcelsLegendUrl = "/Content/img/legendImages/parcel.png";
    var parcelsLabel = "<span><img src='" + parcelsLegendUrl + "' height='14px'/> Parcels</span>";
    this.addWmsLayer("OCStormwater:Parcels",
        parcelsLabel,
        {
            styles: "parcel"
        }, true);
    regionalSubbasinLayer.bringToFront();

    this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>", false);

    L.control.watermark({ position: 'bottomleft' }).addTo(this.map);
    this.initializationsOnMapRefresh();
};

NeptuneMaps.DelineationMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.Map.prototype);

/* Constants */

var TOLERANCE_CENTRALIZED = 0.0001;
var TOLERANCE_DISTRIBUTED = 0.000015;
var TOLERANCE_STEP_INCREMENT = 0.000001;

var DELINEATION_DISTRIBUTED = "Distributed";
var DELINEATION_CENTRALIZED = "Centralized";

var DELINEATION_VERIFIED = "Verified";
var DELINEATION_PROVISIONAL = "Provisional";

var STRATEGY_AUTODEM = "AutoDEM";
var STRATEGY_NETWORK_TRACE = "NetworkTrace";
var STRATEGY_MANUAL = "Manual";

var LEAFLET_TO_GEO_JSON_PRECISION = 18;

/* Prototype members */

/* Initialization methods and assorted convenience methods 
 */

NeptuneMaps.DelineationMap.prototype.initializationsOnMapRefresh = function () {
    this.hookupEditLocationOnclick();
    this.hookupSelectTreatmentBMPOnClick();
    this.hookupDeselectOnClick();
    this.hookupSelectTreatmentBMPByDelineation();
    this.hookupBringMarkerToFrontOnZoomEnd();
};

NeptuneMaps.DelineationMap.prototype.buildSelectedAssetControl = function () {
    this.selectedAssetControl = L.control.delineationSelectedAsset({ position: 'topleft' });
    this.selectedAssetControl.addTo(this.map);

    var selectedAssetControlElement = document.querySelector("#selectedAssetControl");

    this.selectedAssetControl.parentElement.append(selectedAssetControlElement);
};

NeptuneMaps.DelineationMap.prototype.addDelineationWmsLayers = function () {

    var jurisdictionCQLFilter = this.config.JurisdictionCQLFilter;
    if (!Sitka.Methods.isUndefinedNullOrEmpty(jurisdictionCQLFilter)) {
        jurisdictionCQLFilter = " AND " + jurisdictionCQLFilter;
    }
    // delete this line when the analysts realize that they actually do want the delineations hidden by jurisdiction
    jurisdictionCQLFilter = "";

    var verifiedLegendUrl = '/Content/img/legendImages/delineationVerified.png';
    var verifiedLabel = "<span>Delineations (Verified) </br><img src='" + verifiedLegendUrl + "'/></span>";
    this.verifiedLayer = this.addWmsLayer("OCStormwater:Delineations",
        verifiedLabel,
        {
            styles: "delineation",
            cql_filter: "DelineationStatus = 'Verified'" + jurisdictionCQLFilter,
            maxZoom: 22
        },
        false);

    var provisionalLegendUrl = '/Content/img/legendImages/delineationProvisional.png';
    var provisionalLabel = "<span>Delineations (Provisional) </br><img src='" + provisionalLegendUrl + "'/></span>";
    this.provisionalLayer = this.addWmsLayer("OCStormwater:Delineations",
        provisionalLabel,
        {
            styles: "delineation",
            cql_filter: "DelineationStatus = 'Provisional'" + jurisdictionCQLFilter,
            maxZoom: 22
        },
        true);
};

NeptuneMaps.DelineationMap.prototype.cacheBustDelineationWmsLayers = function () {
    this.verifiedLayer.setParams({ wmsParameterThatDoesNotExist: Date.now() }, false);
    this.provisionalLayer.setParams({ wmsParameterThatDoesNotExist: Date.now() }, false);
};

NeptuneMaps.DelineationMap.prototype.initializeTreatmentBMPClusteredLayer = function () {
    var mapInitJson = this.mapInitJson;
    this.treatmentBMPLayer = L.geoJson(
        mapInitJson.TreatmentBMPLayerGeoJson.GeoJsonFeatureCollection,
        {
            pointToLayer: NeptuneMaps.DefaultOptions.pointToLayer,
            onEachFeature: function (feature, layer) {
                this.treatmentBMPLayerLookup.set(feature.properties["TreatmentBMPID"], layer);
            }.bind(this)
        });
    if (this.markerClusterGroup) {
        this.map.removeLayer(this.markerClusterGroup);
    }

    this.markerClusterGroup = this.makeMarkerClusterGroup(this.treatmentBMPLayer);

    var legendSpan = "<span><img src='https://api.tiles.mapbox.com/v3/marker/pin-m-water+935F59@2x.png' height='30px' /> Treatment BMPs</span>";
    this.layerControl.addOverlay(this.markerClusterGroup, legendSpan);
};

NeptuneMaps.DelineationMap.prototype.preselectTreatmentBMP = function (treatmentBMPID) {
    if (!treatmentBMPID) {
        return; //misplaced call
    }
    var layer = this.treatmentBMPLayerLookup.get(treatmentBMPID);

    var promise = this.retrieveAndShowBMPDelineation(layer.feature);

    var self = this;
    promise.then(function () {
        var delineationStatus;
        if (self.selectedBMPDelineationLayer) {
            delineationStatus = self.selectedBMPDelineationLayer.getLayers()[0].feature.properties
                .DelineationStatus;
            self.delineationMapService.adjustZoom(self.selectedBMPDelineationLayer);
        } else {
            delineationStatus = "None";
            var coords = layer.feature.geometry.coordinates;
            self.map.setView([coords[1], coords[0]], 18);
        }
        self.delineationMapService.broadcastDelineationMapState({ selectedTreatmentBMPFeature: layer.feature });
        
        self.selectedAssetControl.treatmentBMP(layer.feature, delineationStatus);

        return jQuery.Deferred().resolve();

    }).always(function () {
        // don't set the selected layer until after the zoommies are done
        setTimeout(function () { self.setSelectedFeature(layer.feature); }, 500);
    });
};

NeptuneMaps.DelineationMap.prototype.adjustZoom = function (zoomData) {
    this.map.fitBounds(zoomData.getBounds());
};

NeptuneMaps.DelineationMap.prototype.hookupBringMarkerToFrontOnZoomEnd = function() {
    //var self = this;
    //this.map.on("zoomend",
    //    function () {
    //        if(self.lastSelectedMarker) {
    //            self.lastSelectedMarker._bringToFront();
    //        }
    //    });
    //this.markerClusterGroup.on("animationend",
    //    function () {
    //        if(self.lastSelectedMarker) {
    //            self.lastSelectedMarker._bringToFront();
    //        }
    //    });
};

NeptuneMaps.DelineationMap.prototype.getSelectedBMPFeature = function () {
    return this.lastSelected.toGeoJSON(LEAFLET_TO_GEO_JSON_PRECISION).features[0];
};

NeptuneMaps.DelineationMap.prototype.getSelectedBMPID = function() {
    // precision doesn't matter here since this method is only for getting the ID
    return this.lastSelected.toGeoJSON().features[0].properties.TreatmentBMPID;
};


/* Methods for turning on or off the pop-up that begins the delineation workflow
 */

NeptuneMaps.DelineationMap.prototype.addBeginDelineationControl = function () {
    var treatmentBMPFeature = this.getSelectedBMPFeature();

    if (this.beginDelineationControl) {
        return; // misplaced call
    }
    this.beginDelineationControl = L.control.beginDelineation({ position: "bottomright" }, treatmentBMPFeature);
    this.beginDelineationControl.addTo(this.map);
    this.beginDelineationControl.preselectDelineationType(treatmentBMPFeature.properties.DelineationType);
    this.beginDelineationControl.displayDelineationOptionsForFlowOption(treatmentBMPFeature.properties.DelineationType);

    this.disableSelectOnClick();
};

NeptuneMaps.DelineationMap.prototype.removeBeginDelineationControl = function () {
    if (!this.beginDelineationControl) {
        return; //misplaced call
    }

    this.beginDelineationControl.remove();
    this.beginDelineationControl = null;

    this.enableUserInteraction();
    this.enableSelectOnClick();

    this.delineationMapService.resetDelineationMapEditingState();
    window.freeze = false;
};

/* "Edit Location Mode"
 * This is an add-on feature that allows a user to move the BMP pin to change its location from the delineation map.
 */

NeptuneMaps.DelineationMap.prototype.launchEditLocationMode = function () {
    this.disableSelectOnClick();
    this.enableEditLocationOnClick();

    jQuery("#delineationMap").css("cursor", "crosshair");
    

};

NeptuneMaps.DelineationMap.prototype.exitEditLocationMode = function (save) {
    var treatmentBMPID = this.getSelectedBMPFeature().properties.TreatmentBMPID;
    jQuery("#delineationMap").css("cursor", "grab");

    var self = this;
    if (save && this.treatmentBMPLocationModel) {
        this.displayLoading();

        var treatmentBMPLocationUrl = new Sitka.UrlTemplate(this.config.TreatmentBMPLocationUrlTemplate).ParameterReplace(treatmentBMPID);

        jQuery.ajax({
            url: treatmentBMPLocationUrl,
            data: self.treatmentBMPLocationModel,
            type: 'POST'
        }).then(function(response) {
            var mijfs = self.mapInitJson.TreatmentBMPLayerGeoJson.GeoJsonFeatureCollection.features;
            var a = _.find(mijfs, function(o) { return o.properties.TreatmentBMPID == treatmentBMPID });

            var coords = a.geometry.coordinates;

            coords[0] = self.treatmentBMPLocationModel.TreatmentBMPPointX;
            coords[1] = self.treatmentBMPLocationModel.TreatmentBMPPointY;

            self.initializeTreatmentBMPClusteredLayer();
            var movedLayer = self.treatmentBMPLayerLookup.get(treatmentBMPID);
            self.setSelectedFeature(movedLayer.feature);
            self.retrieveAndShowBMPDelineation(movedLayer.feature).then(function (response) {
                var delineationStatus;
                if (self.selectedBMPDelineationLayer) {
                    delineationStatus = self.selectedBMPDelineationLayer.getLayers()[0].feature.properties
                        .DelineationStatus;
                } else {
                    delineationStatus = "None";
                }
                self.selectedAssetControl.treatmentBMP(movedLayer.feature, delineationStatus);
                self.delineationMapService.broadcastDelineationMapState({
                    selectedTreatmentBMPFeature: movedLayer.feature
                });
            }).always(function () {
                self.removeLoading();
                self.initializationsOnMapRefresh();
                toast("Successfully updated Treatment BMP location.", "success");
            });
        }).fail(function() {
            self.initializeTreatmentBMPClusteredLayer();
            var movedLayer = self.treatmentBMPLayerLookup.get(treatmentBMPID);
            self.setSelectedFeature(movedLayer.feature);
            self.removeLoading();
            self.initializationsOnMapRefresh();
            toast("There was an error updating the Treatment BMP location.");
        });
    } else {
        // don't jump through the hoops of actually saving, nothing was changed.
        this.lastSelected.remove();
        var movedLayer = this.treatmentBMPLayerLookup.get(treatmentBMPID);
        this.setSelectedFeature(movedLayer.feature);
    }

    this.disableSelectOnClick();
    this.disableEditLocationOnClick();
    this.enableSelectOnClick();
};

/* "Draw Catchment Mode"
 * This is the bread and butter of the delineation workflows.
 * When in this mode, the user is given access to a Leaflet.Draw control pointed at this.selectedBMPDelineationLayer.
 * This mode is activated when the user chooses the draw option from the Begin Delineation Control or as the terminus
 * of most other delineation paths.
 * drawModeOptions is a
 * {
 *   tolerance: number,
 *   delineationType: string [DELINEATION_CENTRALIZED | DELINEATION_DISTRIBUTED],
 *   delineationStrategy: string [STRATEGY_AUTODEM | STRATEGY_NETWORK_TRACE]
 * }
 */

NeptuneMaps.DelineationMap.prototype.launchDrawCatchmentMode = function (drawModeOptions) {
    drawModeOptions.newDelineation = !this.selectedBMPDelineationLayer;
    this.disableEditLocationOnClick();
    if (this.beginDelineationControl) {
        this.beginDelineationControl.remove();
        this.beginDelineationControl = null;
    }

    if (this.selectedBMPDelineationLayer) {
        this.delineationMapService.adjustZoom(this.selectedBMPDelineationLayer);
    }

    this.selectedAssetControl.launchDrawCatchmentMode(drawModeOptions);

    this.delineationMapService.broadcastDelineationMapState({
        isInDelineationMode: true,
        isEditedDelineationPresent: true
    });

    this.buildDrawControl(drawModeOptions);
};

NeptuneMaps.DelineationMap.prototype.unthinDelineationVertices = function () {
    this.editableFeatureGroup.clearLayers();
    var editableFeatureGroup = this.editableFeatureGroup;

    L.geoJSON(this.delineationFeatureSavedForReset,
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

NeptuneMaps.DelineationMap.prototype.thinDelineationVertices = function (drawModeOptions, tolerance) {

    if (!this.delineationFeatureSavedForReset) {
        return;
    }

    var editableFeatureGroup = this.editableFeatureGroup;


    this.editableFeatureGroup.clearLayers();

    var downsampledDelineationFeature = turf.flatten(downsampleGeoJsonFeature(this.delineationFeatureSavedForReset, tolerance));

    L.geoJSON(downsampledDelineationFeature,
        {
            onEachFeature: function (feature, layer) {
                if (layer.getLayers) {
                    layer.getLayers().forEach(function (l) { editableFeatureGroup.addLayer(l); });
                } else {
                    editableFeatureGroup.addLayer(layer);
                }
            }
        });
};

NeptuneMaps.DelineationMap.prototype.buildDrawControl = function (drawModeOptions) {

    var self = this;
    this.editableFeatureGroup = new L.FeatureGroup();
    var editableFeatureGroup = this.editableFeatureGroup;

    var drawOptions = getDrawOptions(editableFeatureGroup);
    this.drawControl = new L.Control.Draw(drawOptions);
    this.map.addControl(this.drawControl);

    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {

        this.map.removeLayer(this.selectedBMPDelineationLayer);

        var selectedBMPDelineationLayers = this.selectedBMPDelineationLayer.getLayers();

        var geoJsonForEdit;
        if (selectedBMPDelineationLayers.length > 1) {
            geoJsonForEdit =
            {
                "type":
                    "FeatureCollection",
                "features":
                    _.map(this.selectedBMPDelineationLayer.getLayers(), function(layer) { return layer.feature; })
            };
        } else {
            geoJsonForEdit = selectedBMPDelineationLayers[0].feature;
        }

        this.delineationFeatureSavedForReset = geoJsonForEdit;

        if (geoJsonForEdit.geometry && geoJsonForEdit.geometry.type === "MultiPolygon") {
            geoJsonForEdit = turf.flatten(geoJsonForEdit);
        }

        L.geoJSON(geoJsonForEdit,
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
    this.map.addLayer(editableFeatureGroup);
    editableFeatureGroup.persist = true;
    if (drawModeOptions.delineationStrategy === STRATEGY_NETWORK_TRACE) {

        jQuery(".leaflet-draw-edit-edit").on("click",
            function (e) {
                if (!editableFeatureGroup.persist) {
                    editableFeatureGroup.persist = true;
                }
            });
    }

    this.prepFeatureGroupAndDrawControl(drawModeOptions, true);
    this.map.on('draw:created',
        function (e) {
            var layer = e.layer;
            editableFeatureGroup.addLayer(layer);
            var leafletId = layer._leaflet_id;
            editableFeatureGroup._layers[leafletId].feature = new Object();
            editableFeatureGroup._layers[leafletId].feature.properties = new Object();
            editableFeatureGroup._layers[leafletId].feature.type = "Feature";

            self.prepFeatureGroupAndDrawControl(drawModeOptions, false);
        });

    this.map.on('draw:deleted',
        function (e) {
            self.prepFeatureGroupAndDrawControl(drawModeOptions, false);
        });
};

NeptuneMaps.DelineationMap.prototype.prepFeatureGroupAndDrawControl = function (drawModeOptions, goDirectlyToVertexEditIfAllowed) {
    /* this is not the best way to prevent drawing multiple polygons, but the other options are:
     * 1. fork Leaflet.Draw and add the functionality or
     * 2.maintain two versions of the draw control that track the same feature group but with different options
     * and the answer to both of those is "no"
     */

    var editableFeatureGroup = this.editableFeatureGroup;
    if (editableFeatureGroup.getLayers().length > 0) {
        killPolygonDraw();
        if (goDirectlyToVertexEditIfAllowed && drawModeOptions.delineationStrategy !== STRATEGY_NETWORK_TRACE) {
            jQuery(".leaflet-draw-edit-edit").get(0).click();
        }
    } else {
        unKillPolygonDraw();
        if (goDirectlyToVertexEditIfAllowed && drawModeOptions.delineationStrategy !== STRATEGY_NETWORK_TRACE) {
            jQuery(".leaflet-draw-draw-polygon").get(0).click();
        }
    }
};

NeptuneMaps.DelineationMap.prototype.tearDownDrawControl = function () {
    this.drawControl.remove();
    this.map.removeLayer(this.editableFeatureGroup);
    this.drawControl = null;
    this.editableFeatureGroup = null;
    this.delineationFeatureSavedForReset = null;
    this.networkTraceDelineationFeatureCollection = null;
};

NeptuneMaps.DelineationMap.prototype.exitDrawCatchmentMode = function (save) {
    if (!save) {
        if (this.selectedBMPDelineationLayer && !this.selectedBMPDelineationLayer.isUnsavedDelineation) {
            this.map.addLayer(this.selectedBMPDelineationLayer);
        } else {
            // either the selected BMP's delineation didn't exist or it should no longer
            this.selectedBMPDelineationLayer = null;
        }
    } else {
        // this is where to set the UI back to showing "Provisional" instead of "Verified"
        this.selectedAssetControl.flipVerifyButton(false);
        this.selectedAssetControl.showVerifyButton();

        this.treatmentBMPLayerLookup.get(this.getSelectedBMPFeature().properties.TreatmentBMPID).feature.properties.DelineationType = this.delineationType;

        // returns a promise but there's no need to actually do anything with it
        this.persistDrawnCatchment();
    }

    this.tearDownDrawControl();
    this.retrieveAndShowBMPDelineation(this.getSelectedBMPFeature());

    this.enableUserInteraction();
    this.enableSelectOnClick();

    var noApply = true;
    this.delineationMapService.resetDelineationMapEditingState(noApply);
};

NeptuneMaps.DelineationMap.prototype.exitTraceMode = function (save) {
    if (!save) {
        // todo: the logic here can be cleaned up a bit.
        if (this.selectedBMPDelineationLayer && !this.selectedBMPDelineationLayer.isUnsavedDelineation) {
            this.map.addLayer(this.selectedBMPDelineationLayer);
        } else {
            if (this.selectedBMPDelineationLayer) {
                this.map.removeLayer(this.selectedBMPDelineationLayer);
                this.selectedBMPDelineationLayer = null;
            }
            // either the selected BMP's delineation didn't exist or it should no longer
            this.selectedBMPDelineationLayer = null;
        }
    } else {
        // this is where to set the UI back to showing "Provisional" instead of "Verified"
        this.selectedAssetControl.flipVerifyButton(false);
        this.selectedAssetControl.showVerifyButton();

        this.treatmentBMPLayerLookup.get(this.getSelectedBMPFeature().properties.TreatmentBMPID).feature.properties.DelineationType = this.delineationType;

        // returns a promise but there's no need to actually do anything with it
        this.persistTracedCatchment();
    }

    // todo: I think this is technically a race-condition.
    this.retrieveAndShowBMPDelineation(this.getSelectedBMPFeature());

    this.enableSelectOnClick();
};

NeptuneMaps.DelineationMap.prototype.persistDrawnCatchment = function () {
    // had better be only one feature
    var persistableFeatureJson;
    if (this.editableFeatureGroup.persist) {
        // using precision of 14 to avoid accidentally creating invalid geometries
        persistableFeatureJson = this.editableFeatureGroup.toGeoJSON(LEAFLET_TO_GEO_JSON_PRECISION);
    } else {
        persistableFeatureJson = this.selectedBMPDelineationLayer.getLayers()[0].feature;
    }

    var wkts;

    if (persistableFeatureJson.type === "Feature") {
        wkts = [Terraformer.WKT.convert(persistableFeatureJson.geometry)];
    } else if (persistableFeatureJson.features.length == 1) {
        wkts = [Terraformer.WKT.convert(persistableFeatureJson.features[0].geometry)];
    } else if (persistableFeatureJson.features.length > 1) {

        wkts = _.map(persistableFeatureJson.features,
            function(feature) {
                return Terraformer.WKT.convert(feature.geometry);
            });

    } else {
        wkts = [Terraformer.WKT.convert({ type: "Polygon" })];
    }

    var treatmentBMPID = this.getSelectedBMPFeature().properties.TreatmentBMPID;
    var delineationUrl =
        new Sitka.UrlTemplate(this.config.TreatmentBMPDelineationUrlTemplate).ParameterReplace(treatmentBMPID);

    var self = this;
    return jQuery.ajax({
        url: delineationUrl,
        data: { "WellKnownText": wkts, "DelineationType": this.delineationType },
        type: 'POST'
    }).then(function (response) {
        self.treatmentBMPLayerLookup.get(treatmentBMPID).feature.properties.DelineationURL = delineationUrl;
        self.treatmentBMPLayerLookup.get(treatmentBMPID).feature.properties.DelineationID = response.delineationID;
        self.retrieveAndShowBMPDelineation(self.getSelectedBMPFeature());
        self.cacheBustDelineationWmsLayers();
    }).fail(function () {
        alert(
            "There was an error saving the delineation. Please try again. If the problem persists, please contact Support.");
    });
};

NeptuneMaps.DelineationMap.prototype.persistTracedCatchment = function () {
    // had better be only one feature
    var persistableFeatureJson = this.selectedBMPDelineationLayer.toGeoJSON(LEAFLET_TO_GEO_JSON_PRECISION);

    var wkts;

    if (persistableFeatureJson.type === "Feature") {
        wkts = [Terraformer.WKT.convert(persistableFeatureJson.geometry)];
    } else if (persistableFeatureJson.features.length == 1) {
        wkts = [Terraformer.WKT.convert(persistableFeatureJson.features[0].geometry)];
    } else if (persistableFeatureJson.features.length > 1) {

        wkts = _.map(persistableFeatureJson.features,
            function(feature) {
                return Terraformer.WKT.convert(feature.geometry);
            });

    } else {
        wkts = [Terraformer.WKT.convert({ type: "Polygon" })];
    }

    var treatmentBMPID = this.getSelectedBMPFeature().properties.TreatmentBMPID;
    var delineationUrl =
        new Sitka.UrlTemplate(this.config.TreatmentBMPDelineationUrlTemplate).ParameterReplace(treatmentBMPID);

    var self = this;
    return jQuery.ajax({
        url: delineationUrl,
        data: { "WellKnownText": wkts, "DelineationType": this.delineationType },
        type: 'POST'
    }).then(function (response) {
        self.treatmentBMPLayerLookup.get(treatmentBMPID).feature.properties.DelineationURL = delineationUrl;
        self.treatmentBMPLayerLookup.get(treatmentBMPID).feature.properties.DelineationID = response.delineationID;
        self.retrieveAndShowBMPDelineation(self.getSelectedBMPFeature());
        self.cacheBustDelineationWmsLayers();
    }).fail(function () {
        alert(
            "There was an error saving the delineation. Please try again. If the problem persists, please contact Support.");
    });
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
    this.disableSelectOnClick();

    this.displayLoading();

    var self = this;
    var autoDelineate = new NeptuneMaps.DelineationMap.AutoDelineate(this.config.AutoDelineateBaseUrl);


    var promise = autoDelineate.MakeDelineationRequestNew(latLng);


    promise.then(function (featureCollection) {
        // RL 7/15/22 - Per Austin we only need to look for local upstream (not the total upstream) when we are grabbing the result. 
        // This would be a one part feature instead of a multi - part.
        var localUpstreamFeature = _.find(featureCollection.features, function (f) { return f.properties.WshdType === "Local Upstream"; });
        if (localUpstreamFeature == null) {
            window.alert("No local upstream returned from the remote service.  If the issue persists, please contact Support.");
            self.removeLoading();
            self.enableUserInteraction();

            self.enableSelectOnClick();
            self.delineationMapService.resetDelineationMapEditingState();
            return;
        }

        self.addBMPDelineationLayerFromDEM(localUpstreamFeature);

        self.removeLoading();
        self.enableUserInteraction();

        var drawModeOptions = { tolerance: TOLERANCE_DISTRIBUTED, delineationType: DELINEATION_DISTRIBUTED, delineationStrategy: STRATEGY_AUTODEM };
        self.delineationMapService.broadcastDelineationMapState({ isEditedDelineationPresent: true });
        self.launchDrawCatchmentMode(drawModeOptions);
    }).fail(function (error) {

        if (!error) {
            // generic message
            window.alert(
                "There was an error retrieving the delineation from the remote service. If the issue persists, please contact Support.");
        }

        if (error.messages && _.find(error.messages,
            function (m) {
                return m.description === "Number of intersecting catalog unit(s): 0";
            })) {

            // look for the error message indicating that the service has no data to work with for the given location
            window.alert(
                "The DEM service does not currently have data available near the selected Treatment BMP. If you would like to help expand the service to include your jurisdiction please contact the administrators to learn more.");
        }

        else if (error.serviceUnavailable) {
            window.alert(
                "The DEM service is currently unavailable. Please try again later.");
        } else {
            // generic message
            window.alert(
                "There was an error retrieving the delineation from the remote service. If the issue persists, please contact Support.");
        }

        
        self.removeLoading();
        self.enableUserInteraction();

        self.enableSelectOnClick();
        self.delineationMapService.resetDelineationMapEditingState();
    });
};

/* "Trace-Delineate Mode"
 * Map is locked down while the ajax calls for the regional subbasin trace are run.
 *
 * After a failed return, the failure is reported and the UI unblocked.
 * After a successful return, the map is put into Draw Catchment Mode for the user to revise or accept the trace-delineation.
 */

NeptuneMaps.DelineationMap.prototype.launchTraceDelineateMode = function () {
    this.delineationMapService.broadcastDelineationMapState({ isEditingCentralizedDelineation: true });

    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }

    this.beginDelineationControl.remove();
    this.beginDelineationControl = null;

    var selectedBMPID = this.getSelectedBMPID();

    this.disableUserInteraction();
    this.disableSelectOnClick();

    this.displayLoading();

    var self = this;

    this.retrieveDelineationFromNetworkTrace(selectedBMPID).then(function (response) {
        var geoJson = JSON.parse(response);
        self.processAndShowTraceDelineation(geoJson);
        self.removeLoading();
        self.enableUserInteraction();

    }).fail(function () {
        self.removeLoading();
        self.enableUserInteraction();
        this.enableSelectOnClick();

        window.alert(
            "There was an error retrieving the delineation from the Regional Subbasin Trace. Please try again. If the issue persists, please contact Support.");
    }).always(function () {
        self.removeLoading();
        self.enableUserInteraction();
    });
};

NeptuneMaps.DelineationMap.prototype.retrieveDelineationFromNetworkTrace = function (treatmentBMPID) {
    var url = new Sitka.UrlTemplate(this.config.CatchmentTraceUrlTemplate).ParameterReplace(treatmentBMPID);

    return jQuery.ajax({
        url: url,
        type: "GET"
    });
};

NeptuneMaps.DelineationMap.prototype.processAndShowTraceDelineation = function (geoJsonFeatureCollection) {
    if (this.selectedBMPDelineationLayer) {
        this.selectedBMPDelineationLayer.remove();
        this.selectedBMPDelineationLayer = null;
    }

    this.networkTraceDelineationFeatureCollection = geoJsonFeatureCollection;

    this.selectedBMPDelineationLayer = L.geoJSON(geoJsonFeatureCollection,
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
    this.delineationMapService.broadcastDelineationMapState({ isEditedDelineationPresent: true });
};

/* Methods for handling the visual presentation of the selected BMPs delineation
 */

NeptuneMaps.DelineationMap.prototype.addBMPDelineationLayer = function (geoJson) {
    if (this.selectedBMPDelineationLayer) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
        this.selectedBMPDelineationLayer = null;
    }

    this.selectedBMPDelineationLayer = L.geoJson(geoJson,
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

NeptuneMaps.DelineationMap.prototype.addBMPDelineationLayerFromDEM = function (localUpstreamFeature) {
    if (this.selectedBMPDelineationLayer) {
        this.selectedBMPDelineationLayer.remove();
        this.selectedBMPDelineationLayer = null;
    }

    // Justin's service is sending back a feature collection of multi polygons, instead of a polygon, which would be the appropriate thing to send.
    // they do always seem to be in a form that this code here can pull them from.
    var hacky;
    var localUpstreamGeometry = localUpstreamFeature.geometry;
    if (localUpstreamGeometry.type === "Polygon") {
        hacky = localUpstreamGeometry;
    } else {

        hacky = {
            type: "Polygon",
            coordinates: localUpstreamGeometry.coordinates[1]
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

/* Assorted ajax calls for delineation details/geometry etc
 * 
 * @param {any} treatmentBMPFeature
 */


// todo: instead of using a callback to $apply(), use the delineationMapService
NeptuneMaps.DelineationMap.prototype.deleteDelineation = function (afterDeleteCallback) {
    var treatmentBMPFeature = this.getSelectedBMPFeature();

    var deleteUrl =
        new Sitka.UrlTemplate(this.config.DeleteDelineationUrlTemplate).ParameterReplace(treatmentBMPFeature.properties
            .TreatmentBMPID);
    var self = this;

    this.postDelete = function () {
        jQuery.ajax({
            url: deleteUrl,
            data: {},
            method: "POST"
        }).then(function () {
            self.delineationMapService.setDelineation(null);
            self.removeBMPDelineationLayer();
            self.cacheBustDelineationWmsLayers();
            afterDeleteCallback();
        }).fail(function () {
            window.alert(
                "There was an error deleting the delineation. Please try again. If the issue persists, please contact support.");
        });
    };
    confirmDeleteDelineation(treatmentBMPFeature.properties.Name);
};

NeptuneMaps.DelineationMap.prototype.retrieveAndShowBMPDelineation = function (bmpFeature) {
    this.delineationMapService.setDelineation(null);
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
            return;
        }
        if (response.type !== "Feature") {
            delineationErrorAlert();
        }

        self.delineationMapService.setDelineation(response);

        self.addBMPDelineationLayer(response);

        self.selectedAssetControl.showDeleteButton();
        self.delineationMapService.broadcastDelineationMapState({});
    }).fail(delineationErrorAlert);

    return promise;
};

NeptuneMaps.DelineationMap.prototype.changeDelineationStatus = function (verified) {
    var delineationID = this.getSelectedBMPFeature().properties.DelineationID;

    var url = new Sitka.UrlTemplate(this.config.ChangeDelineationStatusUrlTemplate).ParameterReplace(delineationID);
    var self = this;

    jQuery.ajax({
        url: url,
        method: "POST",
        data: {
            IsVerified: verified
        }
    }).then(function (data) {
        if (!data.success) {

            toast(
                "There was an error changing the delineation status.",
                "error");

        } else {
            self.cacheBustDelineationWmsLayers();
            toast("The Delineation status was successfully changed.", "success");
        }
    });
};

NeptuneMaps.DelineationMap.prototype.selectBMPByDelineation = function(latlng) {
    var jurisdictionCQLFilter = this.config.JurisdictionCQLFilter;
    if (!Sitka.Methods.isUndefinedNullOrEmpty(jurisdictionCQLFilter)) {
        jurisdictionCQLFilter += " AND ";
    } else {
        jurisdictionCQLFilter = "";
    }

    var params = {
        cql_filter: jurisdictionCQLFilter +
            "INTERSECTS(DelineationGeometry, POINT(" +
            latlng.lat +
            " " +
            latlng.lng +
            ")) AND DelineationType <> 'WQMP'"
    };
    var self = this;
    this.getFeatureInfo("OCStormwater:Delineations", [latlng.lng, latlng.lat]).then(function (response) {
        if (response.totalFeatures === 0 || (response.features && response.features.length ===0)) {
            return; // no one cares
        }
        var delineation = response.features[0];

        if ((delineation.properties.DelineationStatus === DELINEATION_VERIFIED &&
            self.map.hasLayer(self.verifiedLayer))
            ||
            (delineation.properties.DelineationStatus === DELINEATION_PROVISIONAL &&
                self.map.hasLayer(self.provisionalLayer))

        ) {
            var treatmentBMPID = delineation.properties.TreatmentBMPID;
            var layer = self.treatmentBMPLayerLookup.get(treatmentBMPID);
            if (layer) {
                self.setSelectedFeature(layer.feature);
                self.retrieveAndShowBMPDelineation(layer.feature).then(function (response) {
                    var delineationStatus;
                    if (self.selectedBMPDelineationLayer) {
                        delineationStatus = self.selectedBMPDelineationLayer.getLayers()[0].feature.properties
                            .DelineationStatus;
                    } else {
                        delineationStatus = "None";
                    }
                    self.selectedAssetControl.treatmentBMP(layer.feature, delineationStatus);
                });
                self.delineationMapService.broadcastDelineationMapState({ selectedTreatmentBMPFeature: layer.feature });
            }
        }
    });
};

/* helper methods to restore UI interactions after a blocking mode returns */

NeptuneMaps.DelineationMap.prototype.disableSelectOnClick = function() {
    this.selectOnClickDisabled = true;
};

NeptuneMaps.DelineationMap.prototype.enableSelectOnClick = function() {
    this.selectOnClickDisabled = false;
};

NeptuneMaps.DelineationMap.prototype.hookupDeselectOnClick = function () {
    var self = this;
    this.map.on('click',
        function (e) {
            if (!(window.freeze || self.selectOnClickDisabled)) {
                self.deselect();
                self.removeBMPDelineationLayer();
                self.delineationMapService.broadcastDelineationMapState({
                    selectedTreatmentBMPFeature: null
                });
            }
        });
};

NeptuneMaps.DelineationMap.prototype.hookupSelectTreatmentBMPOnClick = function () {
    var self = this;

    this.treatmentBMPLayer.on("click",
        function (e) {
            if (!(window.freeze || self.selectOnClickDisabled)) {
                self.setSelectedFeature(e.layer.feature);

                self.retrieveAndShowBMPDelineation(e.layer.feature).then(function (response) {
                    var delineationStatus;
                    if (self.selectedBMPDelineationLayer) {
                        delineationStatus = self.selectedBMPDelineationLayer.getLayers()[0].feature.properties
                            .DelineationStatus;
                    } else {
                        delineationStatus = "None";
                    }
                    self.selectedAssetControl.treatmentBMP(e.layer.feature, delineationStatus);
                    self.delineationMapService.broadcastDelineationMapState({
                        selectedTreatmentBMPFeature: e.layer.feature
                    });
                });
            }
        });

};

NeptuneMaps.DelineationMap.prototype.hookupSelectTreatmentBMPByDelineation = function() {
    var self = this;
    this.map.on("click",
        function(e) {
            if (!(window.freeze || self.selectOnClickDisabled)) {
                self.selectBMPByDelineation(e.latlng);
            }
        });
};

NeptuneMaps.DelineationMap.prototype.disableEditLocationOnClick = function () {
    this.editLocationOnClickDisabled = true;
};

NeptuneMaps.DelineationMap.prototype.enableEditLocationOnClick = function () {
    this.editLocationOnClickDisabled = false;
};

NeptuneMaps.DelineationMap.prototype.hookupEditLocationOnclick = function() {
    var self = this;
    this.map.on("click",
        function(e) {
            if (!(window.freeze || self.editLocationOnClickDisabled)) {

                var latlng = e.latlng;

                self.lastSelectedMarker.setLatLng(latlng);
                self.treatmentBMPLocationModel = {
                    TreatmentBMPPointY: latlng.lat,
                    TreatmentBMPPointX: latlng.lng
                };
            }
        });
    this.disableEditLocationOnClick();
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

/* Utility functions. These should be free of side-effects */
var downsampleGeoJsonFeature = function (geoJson, tolerance) {
    return turf.simplify(geoJson, { tolerance: tolerance });
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
};

var confirmDeleteDelineation = function (treatmentBMPName) {
    var alertHtml =
        "<div class='modal neptune-modal' style='width: 500px; margin:auto;'>" +
        "<div class='modal-dialog neptune-modal-dialog'>" +
        "<div class='modal-content'>" +
        "<div class='modal-header'>" +
        "<button type='button' class='btn btn-xs btn-neptune modal-close-button' data-dismiss='modal'><span>&times</span></button>" +
        "<span class='modal-title'>Delete Delineation</span>" +
        "</div>" +
        "<div class='modal-body'><p>Are you sure you want to delete the delineation for BMP " + treatmentBMPName + "?</p></div>" +
        "<div class='modal-footer'>" +
        "<button type='button' class='btn btn-neptune pull-right' data-dismiss='modal'>Cancel</button>" +
        "<button type='button' class='btn btn-neptune pull-right' style='margin-right:5px;' onclick='window.delineationMap.postDelete()' data-dismiss='modal'>Continue</a>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";
    var alertDiv = jQuery(alertHtml);
    alertDiv.modal({ keyboard: true });
    alertDiv.draggable({ handle: ".modal-header" });
};

var configureProj4Defs = function () {
    proj4.defs([
        [
            "EPSG:4326",
            'GEOGCS["WGS 84",DATUM["WGS_1984",SPHEROID["WGS 84",6378137,298.257223563,AUTHORITY["EPSG","7030"]],AUTHORITY["EPSG","6326"]],PRIMEM["Greenwich",0,AUTHORITY["EPSG","8901"]],UNIT["degree",0.01745329251994328,AUTHORITY["EPSG","9122"]],AUTHORITY["EPSG","4326"]]'
        ],
        [
            "EPSG:2230",
            'PROJCS["NAD83 / California zone 6 (ftUS)",GEOGCS["NAD83",DATUM["North_American_Datum_1983",SPHEROID["GRS 1980",6378137,298.257222101,AUTHORITY["EPSG","7019"]],AUTHORITY["EPSG","6269"]],PRIMEM["Greenwich",0,AUTHORITY["EPSG","8901"]],UNIT["degree",0.01745329251994328,AUTHORITY["EPSG","9122"]],AUTHORITY["EPSG","4269"]],UNIT["US survey foot",0.3048006096012192,AUTHORITY["EPSG","9003"]],PROJECTION["Lambert_Conformal_Conic_2SP"],PARAMETER["standard_parallel_1",33.88333333333333],PARAMETER["standard_parallel_2",32.78333333333333],PARAMETER["latitude_of_origin",32.16666666666666],PARAMETER["central_meridian",-116.25],PARAMETER["false_easting",6561666.667],PARAMETER["false_northing",1640416.667],AUTHORITY["EPSG","2230"],AXIS["X",EAST],AXIS["Y",NORTH]]'
        ]
    ]);
};

function toast(toastText, level) {
    jQuery.toast({
        top: 8,
        text: toastText,
        hideAfter: 3500,
        stack: 1,
        icon: level,
        bgColor: "#707070",
        loaderBg: "#77cfdc"
    });
}