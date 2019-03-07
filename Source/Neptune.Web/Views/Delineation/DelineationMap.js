NeptuneMaps.DelineationMap = function(mapInitJson, initialBaseLayerShown, geoserverUrl) {
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoserverUrl);
};

NeptuneMaps.DelineationMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);


NeptuneMaps.DelineationMap.prototype.addBeginDelineationControl = function() {
    this.beginDelineationControl = L.control.beginDelineation({ position: "bottomright" });
    this.beginDelineationControl.addTo(this.map);
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

    // re-enable click to select network catchments
    this.map.on("click", this.wmsLayers["OCStormwater:NetworkCatchments"].click);
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
    this.treatmentBMPLayer.on("click",
        function(e) {
            //this.zoomAndPanToLayer(e.layer);
            this.removeUpstreamCatchmentsLayer();
            this.setSelectedFeature(e.layer.feature);
            this.selectedAssetControl.treatmentBMP(e.layer.feature);
            this.retrieveAndShowBMPDelineation(e.layer.feature);
        }.bind(this));
};

NeptuneMaps.DelineationMap.prototype.retrieveAndShowBMPDelineation = function(bmpFeature) {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
        this.map.removeLayer(this.selectedBMPDelineationLayer);
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
    window.blockMapInput = true;
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
            window.blockMapInput = false;
            this.selectedAssetControl.enableUpstreamCatchmentsButton();
            delineationErrorAlert();
        }
    );
};

NeptuneMaps.DelineationMap.prototype.processUpstreamCatchmentIDResponse = function(response) {
    this.selectedAssetControl.reportUpstreamCatchments(response.networkCatchmentIDs.length);

    if (response.networkCatchmentIDs.length === 0) {
        window.blockMapInput = false;
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
            window.blockMapInput = false;
            this.selectedAssetControl.enableUpstreamCatchmentsButton();

        });
};

NeptuneMaps.DelineationMap.prototype.processUpstreamCatchmentGeoServerResponse = function(response) {
    window.blockMapInput = false;
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
                this.selectedAssetControl.reset.bind(this.selectedAssetControl)();
                this.removeBMPDelineationLayer();
                this.removeUpstreamCatchmentsLayer();
            }.bind(this));
        }.bind(this));
};


// helpers

var delineationErrorAlert = function() {
    alert(
        "There was an unexpected error retrieving the BMP Delineation. Please try again. If the problem persists, please contact Support.");
};


window.blockMapInput = false;

window.pauseable = function (handler) {
    return function (event) {
        if (window.blockMapInput) {
            return;
        }
        return handler(event);
    };
};
