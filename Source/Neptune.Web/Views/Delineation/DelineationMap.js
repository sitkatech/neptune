﻿// page-specific leaflet controls.
// todo: the use of window.delineationMap to bac-reference the map object is a little brittle

L.Control.Watermark = L.Control.extend({
    onAdd: function (map) {
        var img = L.DomUtil.create('img');

        img.src = '/Content/img/OCStormwater/banner_logo.png';
        img.style.width = '200px';

        return img;
    },

    onRemove: function (map) {
        // Nothing to do here
    }
});

L.control.watermark = function (opts) {
    return new L.Control.Watermark(opts);
}

L.Control.DelineationMapSelectedAsset = L.Control.extend({
    onAdd: function (map) {
        this._div = L.DomUtil.create('div', 'selectedAssetControl leaflet-bar');
        stopClickPropagation(this._div);
        this._div.innerHTML = '<h4>Selected Asset</h4>';

        this.reset();

        this._div.append(this._innerDiv);

        return this._div;
    },

    onRemove: function (map) {
        //perfunctory
    },

    treatmentBMP: function (treatmentBMPFeature) {
        var delineationText;
        var delineationButtonText;
        if (treatmentBMPFeature.properties["DelineationURL"]) {
            delineationText = "<p>This BMP's current recorded delineation is displayed in yellow on the map.</p>";
            delineationButtonText = "Redelineate Drainage Area";
        } else {
            delineationText = "<p>No catchment delineation has been performed for this BMP</p>";
            delineationButtonText = "Delineate Drainage Area";
        }

        this._innerDiv.innerHTML = "BMP: " +
            treatmentBMPFeature.properties["Name"] +
            "<hr/>" +
            "<strong>Drainage Area</strong></br>" +
            delineationText + "<br/>";

        this._delinBtn = L.DomUtil.create("button", "delinBtn btn btn-sm btn-neptune");
        this._delinBtn.type = "button";
        this._delinBtn.innerHTML = delineationButtonText;

        L.DomEvent.on(this._delinBtn,
            "click",
            function (e) {
                window.delineationMap.addBeginDelineationControl();
                this.disableDelineationButton();
                e.stopPropagation();
            }.bind(this));

        this._innerDiv.append(this._delinBtn);
    },

    networkCatchment: function (networkCatchmentFeature) {
        this._innerDiv.innerHTML = "";

        this._catchmentInfoDiv = L.DomUtil.create("div");

        this._catchmentInfoDiv.innerHTML = "Catchment ID: " +
            networkCatchmentFeature.properties["NetworkCatchmentID"] + "<hr/>";

        this._traverseBtn = L.DomUtil.create("button", "traverseBtn btn btn-sm btn-neptune");
        this._traverseBtn.type = "button";
        this._traverseBtn.innerHTML = "Show Upstream Catchments";

        L.DomEvent.on(this._traverseBtn,
            "click",
            function (e) {
                window.delineationMap.retrieveAndShowUpstreamCatchments(networkCatchmentFeature);
            });

        this._innerDiv.append(this._catchmentInfoDiv);
        this._innerDiv.append(this._traverseBtn);
    },

    reportUpstreamCatchments: function (count) {
        var br = L.DomUtil.create("br");
        this._catchmentInfoDiv.append("Found " + count + " upstream catchments.");
        this._catchmentInfoDiv.append(br);
    },

    reset: function () {
        if (!this._innerDiv) {
            this._innerDiv = L.DomUtil.create("div", "selectedAssetInfo");
        }
        this._innerDiv.innerHTML = "<p>Select a BMP on the map to see options.</p>";
    },

    disableDelineationButton() {
        if (!this._delinBtn) {
            return; //misplaced call
        }
        this._delinBtn.disabled = "disabled";
    },

    enableDelineationButton() {
        if (!this._delinBtn) {
            return; //misplaced call
        }
        this._delinBtn.removeAttribute("disabled");
    },

    disableUpstreamCatchmentsButton() {
        if (!this._traverseBtn) {
            return; //misplaced call
        }
        this._traverseBtn.innerHTML = "Loading...";
        this._traverseBtn.disabled = "disabled";
    },

    enableUpstreamCatchmentsButton() {
        if (!this._traverseBtn) {
            return; //misplaced call
        }
        this._traverseBtn.innerHTML = "Show Upstream Catchments";
        this._traverseBtn.removeAttribute("disabled");
    }
});

L.control.delineationSelectedAsset = function (opts) {
    return new L.Control.DelineationMapSelectedAsset(opts);
}

L.Control.BeginDelineation = L.Control.extend({
    onAdd: function (map) {
        // todo: is there a cleaner way to generate this stuff than L.DomUtil
        this._div = L.DomUtil.create("div", "beginDelineationControl leaflet-bar");
        stopClickPropagation(this._div);

        var titleBar = L.DomUtil.create("div", "row");

        var title = L.DomUtil.create("div", "col-sm-10");
        title.innerHTML = "<h4>Delineate Drainage Area</h4>";

        var closeButtonWrapper = L.DomUtil.create("div", "col-sm-2 text-right");

        var closeButton = L.DomUtil.create("button", "btn btn-sm btn-neptune");
        closeButton.innerHTML = "x";
        L.DomEvent.on(closeButton,
            "click",
            function (e) {
                window.delineationMap.removeBeginDelineationControl();
                e.stopPropagation();
            });

        closeButtonWrapper.append(closeButton);
        titleBar.append(title);
        titleBar.append(closeButtonWrapper);

        this._div.append(titleBar);

        var main = L.DomUtil.create("div", "beginDelineationOptions");
        // todo: add values to these radios when we start building out this step
        main.innerHTML = "<label>1. Select the type of flow this BMP will receive</label><br/>" +
            "<label class='group'><input type='radio' name='typeOfFlow'> Receives local surface flow only</label><br/>" +
            "<label class='group'><input type='radio' name='typeOfFlow'> Receives piped flow only</label><br/>" +
            "<label class='group'><input type='radio' name='typeOfFlow'> Receives both piped flow and surface flow</label><hr/>" +
            "<label>2. Choose a delineation option</label></br>" +
            "<label class='group'><input type='radio' name='delineationOption'> Delineate Automatically from DEM</label><br/>" +

            "<label class='group'><input type='radio' name='delineationOption'> Draw the Catchment Area</label><br/>" +
            "<label class='group'><input type='radio' name='delineationOption'> Upload a GIS file</label></hr>" +
            //todo get rid of this
            "<p>(Content under development)</p>";

        this._div.append(main);

        // todo: add a handler to goBtn and undisable it
        var formBtnWrapper = L.DomUtil.create("div", "text-right");
        var goBtn = L.DomUtil.create("button", "continueDelineate btn btn-sm btn-neptune");
        goBtn.type = "button";
        goBtn.innerHTML = "Delineate";
        goBtn.disabled = "disabled";
        var stopBtn = L.DomUtil.create("button", "cancelDelineate btn btn-sm btn-neptune");
        stopBtn.type = "button";
        stopBtn.innerHTML = "Cancel";
        L.DomEvent.on(stopBtn,
            "click",
            function (e) {
                window.delineationMap.removeBeginDelineationControl();
                e.stopPropagation();
            });
        formBtnWrapper.append(goBtn);
        formBtnWrapper.append(stopBtn);

        this._div.append(formBtnWrapper);


        return this._div;
    },
    onRemove: function () {
    }
});

L.control.beginDelineation = function (opts) {
    return new L.Control.BeginDelineation(opts);
}

var mapMethods = {
    addBeginDelineationControl: function () {
        this.beginDelineationControl = L.control.beginDelineation({ position: "bottomright" });
        this.beginDelineationControl.addTo(this.map);
        this.map.off("click");
    },

    removeBeginDelineationControl: function () {
        if (!this.beginDelineationControl) {
            return; //misplaced call
        }

        this.beginDelineationControl.remove();
        this.beginDelineationControl = null;

        this.selectedAssetControl.enableDelineationButton();
        this.hookupDeselectOnClick();

        // re-enable click to select network catchments
        this.map.on("click", this.wmsLayers["OCStormwater:NetworkCatchments"].click);
    },

    initializeTreatmentBMPClusteredLayer: function (mapInitJson) {
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
        this.treatmentBMPLayer.on("click",
            function (e) {
                //this.zoomAndPanToLayer(e.layer);
                this.removeUpstreamCatchmentsLayer();
                this.setSelectedFeature(e.layer.feature);
                this.selectedAssetControl.treatmentBMP(e.layer.feature);
                this.retrieveAndShowBMPDelineation(e.layer.feature);
            }.bind(this));
    },

    retrieveAndShowBMPDelineation: function (bmpFeature) {
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
            function (response) {
                if (response.type !== "Feature") {
                    delineationErrorAlert();
                }
                this.addBMPDelineationLayer(response);
            }.bind(this),
            function (error) {
                delineationErrorAlert();
            }
        );
    },

    retrieveAndShowUpstreamCatchments: function (networkCatchmentFeature) {
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
            }, this.processUpstreamCatchmentIDResponse.bind(this),
            function (error) {
                window.blockMapInput = false;
                this.selectedAssetControl.enableUpstreamCatchmentsButton();
                delineationErrorAlert();
            }
        );
    },

    processUpstreamCatchmentIDResponse: function (response) {
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
        }, this.processUpstreamCatchmentGeoServerResponse.bind(this), function(error) {
            window.blockMapInput = false;
            this.selectedAssetControl.enableUpstreamCatchmentsButton();

        });
    },

    processUpstreamCatchmentGeoServerResponse: function (response) {
        window.blockMapInput = false;
        this.selectedAssetControl.enableUpstreamCatchmentsButton();

        this.upstreamCatchmentsLayer = L.geoJSON(response,
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
    },

    addBMPDelineationLayer: function (geoJsonResponse) {
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
    },

    removeBMPDelineationLayer: function () {
        if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedBMPDelineationLayer)) {
            this.map.removeLayer(this.selectedBMPDelineationLayer);
            this.selectedBMPDelineationLayer = null;
        }
    },

    removeUpstreamCatchmentsLayer: function () {
        if (!Sitka.Methods.isUndefinedNullOrEmpty(this.upstreamCatchmentsLayer)) {
            this.map.removeLayer(this.upstreamCatchmentsLayer);
            this.upstreamCatchmentsLayer = null;
        }
    },

    preselectTreatmentBMP: function (treatmentBMPID) {
        if (!treatmentBMPID) {
            return; //misplaced call
        }
        var layer = this.treatmentBMPLayerLookup.get(treatmentBMPID);
        this.zoomAndPanToLayer(layer);
        this.setSelectedFeature(layer.feature);
        this.selectedAssetControl.treatmentBMP(layer.feature);
        this.retrieveAndShowBMPDelineation(layer.feature);
    },

    hookupDeselectOnClick: function() {
        this.map.on('click',
            function(e) {
                this.deselect(function() {
                    this.selectedAssetControl.reset.bind(this.selectedAssetControl)();
                    this.removeBMPDelineationLayer();
                    this.removeUpstreamCatchmentsLayer();
                }.bind(this));
            }.bind(this));
    }
};

// helpers

var delineationErrorAlert = function() {
    alert(
        "There was an unexpected error retrieving the BMP Delineation. Please try again. If the problem persists, please contact Support.");
};

var stopClickPropagation = function (el) {
    L.DomEvent.on(el, 'click', function (e) { e.stopPropagation(); });
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
