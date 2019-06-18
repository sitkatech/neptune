getDrawOptions = function (editableFeatureGroup) {
    var options = {
        position: 'topleft',
        draw: {
            polyline: false,
            polygon: true,
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


NeptuneMaps.AssessmentAreaMap = function(mapInitJson, initialBaseLayerShown, geoServerUrl) {
    NeptuneMaps.TrashAssessmentMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, {});
    this.map.setMaxZoom(24);
    this.editableFeatureGroup = new L.FeatureGroup();
    var editableFeatureGroup = this.editableFeatureGroup;
    var self = this;

    this.parcelPickerModeActive = false;

    var layerGroup = L.geoJson(editableFeatureJsonObject.GeoJsonFeatureCollection,
        {
            onEachFeature: function(feature, layer) {
                if (layer.getLayers) {
                    layer.getLayers().forEach(function(l) {
                        editableFeatureGroup.addLayer(l);
                    });
                } else {
                    editableFeatureGroup.addLayer(layer);
                }
            }
        });

    var drawOptions = getDrawOptions(this.editableFeatureGroup);
    this.drawControl = new L.Control.Draw(drawOptions);
    var drawControl = this.drawControl;
    this.map.addControl(drawControl);
    this.map.addLayer(this.editableFeatureGroup);
    this.layerControl.addOverlay(this.editableFeatureGroup,
        "<span><img src='/Content/img/legendImages/workflowAssessmentArea.png' height='12px' style='margin-bottom:3px;'/> Assessment Area</span>");


    this.map.on('draw:created',
        function(e) {
            var layer = e.layer;
            editableFeatureGroup.addLayer(layer);
            var leafletId = layer._leaflet_id;
            editableFeatureGroup._layers[leafletId].feature = new Object();
            editableFeatureGroup._layers[leafletId].feature.properties = new Object();
            editableFeatureGroup._layers[leafletId].feature.type = "Feature";
            var feature = editableFeatureGroup._layers[leafletId].feature;
            self.updateFeatureCollectionJson();
        });
    this.map.on('draw:edited',
        function(e) {
            self.updateFeatureCollectionJson();
        });
    this.map.on('draw:editstop',
        function(e) {
            self.updateFeatureCollectionJson();
        });

    this.map.on('draw:editvertex',
        function(e) {
            self.updateFeatureCollectionJson();
        });

    this.map.on('draw:deleted',
        function(e) {
            self.updateFeatureCollectionJson();
        });

    var saveButton = jQuery("#" + saveButtonID);
    if (!Sitka.Methods.isUndefinedNullOrEmpty(saveButton)) {
        saveButton.text("Save");
    }

    var modalTitle = jQuery(".ui-dialog-title");
    if (!Sitka.Methods.isUndefinedNullOrEmpty(modalTitle)) {
        modalTitle.html("Edit ??? - Detail");
    }
    var zoom = Math.min(this.map.getZoom(), 18);
    this.map.setZoom(zoom);
};

NeptuneMaps.AssessmentAreaMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.TrashAssessmentMap.prototype);

NeptuneMaps.AssessmentAreaMap.prototype.getTextAreaId = function (featureId) { return "textareaFor" + featureId; };

NeptuneMaps.AssessmentAreaMap.prototype.getParcelsAndPick = function() {
    
}

NeptuneMaps.AssessmentAreaMap.prototype.updateSelectedParcelLayer = function() {

    if (this.ParcelIDs == null) {
        this.ParcelIDs = [];
    }

    if (this.selectedParcelLayer) {
        this.layerControl.removeLayer(this.selectedParcelLayer);
    this.map.removeLayer(this.selectedParcelLayer);
    }

    // FIXME: Using L.Util.extend here will overwrite the neptuneMap.wmsParams object with the result of the extend, which may not be ideal
    var wmsParameters = L.Util.extend(this.wmsParams, {
        layers: this.ParcelMapServiceLayerName,
        cql_filter: "ParcelID in (" + this.ParcelIDs.join(",") + ")",
        styles: "parcel_yellow"
    });

    this.selectedParcelLayer = L.tileLayer.wms(this.GeoServerUrl, wmsParameters);
    this.layerControl.addOverlay(this.selectedParcelLayer, "<span><img src='/Content/img/legendImages/selectedGeometry.png' height='12px' style='margin-bottom:3px;'/> Selected Parcels</span>");
    this.map.addLayer(this.selectedParcelLayer);

    // Update map extent to selected parcels
    if (_.any(this.ParcelIDs)) {
        var wfsParameters = L.Util.extend(this.wfsParams,
            {
                typeName: this.ParcelMapServiceLayerName,
                cql_filter: "ParcelID in (" + this.ParcelIDs.join(",") + ")"
            });
        SitkaAjax.ajax({
            url: this.GeoServerUrl + L.Util.getParamString(wfsParameters),
            dataType: "json",
            jsonpCallback: "getJson"
        },
            function (response) {
                if (response.features.length === 0)
                    return;

                $scope.$apply();
            },
            function () {
                console.error("There was an error setting map extent to the selected " + $scope.AngularViewData.ParcelFieldDefinitionLabel + "s");
            });
    }
}

NeptuneMaps.AssessmentAreaMap.prototype.createUpdateFeatureCollectionJsonFunctionAsClosure = function(nameForWkt, nameForAnnotation, mapFormID) {
    this.updateFeatureCollectionJson = function() {
        var geoJson = this.editableFeatureGroup.toGeoJSON();
        detectKinksAndReject(geoJson);
        var mapForm = jQuery("#" + mapFormID);
        mapForm.html("");
        var hiddens = [];
        for (var i = 0; i < geoJson.features.length; ++i) {
            var currentWktName = "name=\"" + nameForWkt + "\"";
            currentWktName = currentWktName.replace("0", i);

            var currentWktAnnotation = "name=\"" +
                nameForAnnotation +
                "\"";
            currentWktAnnotation = currentWktAnnotation.replace("0",
                i);

            hiddens.push("<input type=\"hidden\" " +
                currentWktName +
                " value=\"" +
                Terraformer.WKT.convert(geoJson.features[i].geometry) +
                "\" />");
            hiddens.push("<input type=\"hidden\" " +
                currentWktAnnotation +
                " value=\"" +
                Sitka.Methods.htmlEncode(geoJson.features[i].properties.Info) +
                "\" />");
        }
        mapForm.html(hiddens.join("\r\n"));
    }.bind(this);
};

var detectKinksAndReject = function(geoJson) {
    if (!geoJson.features) {
        return;
    }

    var len = geoJson.features.length;
    for (var i = 0; i < len; i++) {
        var geom = turf.getGeom(geoJson.features[i]);
        var kinksFeatureCollection = turf.kinks(geom);
        if (kinksFeatureCollection.features && kinksFeatureCollection.features.length > 0) {
            jQuery("button[type='submit']").attr("disabled", true);
            jQuery("#kinkDanger").css("display", "inline");
            return;
        }
    }

    // if they make it out here, their geometry is vanilla
    jQuery("button[type='submit']").removeAttr("disabled");
    jQuery("#kinkDanger").css("display", "none");
};