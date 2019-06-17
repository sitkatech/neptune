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


NeptuneMaps.AssessmentAreaMap = function (mapInitJson, initialBaseLayerShown, geoServerUrl, originalAssessmentAreaGeoJson, assessmentAreaID) {
    NeptuneMaps.TrashAssessmentMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, {});
    this.originalAssessmentAreaGeoJson = originalAssessmentAreaGeoJson;
    this.assessmentAreaID = assessmentAreaID;
    this.map.setMaxZoom(24);
    this.setUpDraw();
    var self = this;
    this.map.on("click",
        function (event) {
            if (self.parcelPickerModeActive) {
                self.onClickPickParcel(event);
            }
        });

    jQuery("#saveButton").on("click",
        function() {

            self.updateFeatureCollectionJson();
        });
};

NeptuneMaps.AssessmentAreaMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.TrashAssessmentMap.prototype);

NeptuneMaps.AssessmentAreaMap.prototype.onClickPickParcel = function (event) {
    var parcelMapSericeLayerName = "OCStormwater:Parcels";

    var latlng = event.latlng;
    var latlngWrapped = latlng.wrap();

    var wfsParametersExtended = {
        typeName: parcelMapSericeLayerName,
        cql_filter: "intersects(ParcelGeometry, POINT(" + latlngWrapped.lat + " " + latlngWrapped.lng + "))"
    };

    wfsParametersExtended = L.Util.extend(wfsParametersExtended, this.wfsParams);

    var self = this;
    jQuery.ajax({
        url: this.geoserverUrlOWS + L.Util.getParamString(wfsParametersExtended),

    }).then(function (response) {
        if (response.features.length === 0)
            return;

        var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));

        var parcelId = mergedProperties.ParcelID;

        if (_.includes(self.ParcelIDs, parcelId)) {
            _.pull(self.ParcelIDs, parcelId);
        } else {
            self.ParcelIDs.push(parcelId);
        }

        self.updateSelectedParcelLayer();

    }).fail(
        function () {
            console.error("There was an error selecting the " + $scope.AngularViewData.ParcelFieldDefinitionLabel + " from list");
        });
}

NeptuneMaps.AssessmentAreaMap.prototype.getTextAreaId = function (featureId) { return "textareaFor" + featureId; };

NeptuneMaps.AssessmentAreaMap.prototype.setUpDraw = function (geoJson) {
    if (!geoJson) {
        geoJson = this.originalAssessmentAreaGeoJson.GeoJsonFeatureCollection;
    }
    this.editableFeatureGroup = new L.FeatureGroup();
    var self = this;

    this.parcelPickerModeActive = false;
    var editableFeatureGroup = this.editableFeatureGroup;
    var layerGroup = L.geoJson(geoJson,
        {
            onEachFeature: function (feature, layer) {
                if (layer.getLayers) {
                    layer.getLayers().forEach(function (l) {
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
        function (e) {
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
        function (e) {
            self.updateFeatureCollectionJson();
        });
    this.map.on('draw:editstop',
        function (e) {
            self.updateFeatureCollectionJson();
        });

    this.map.on('draw:editvertex',
        function (e) {
            self.updateFeatureCollectionJson();
        });

    this.map.on('draw:deleted',
        function (e) {
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

NeptuneMaps.AssessmentAreaMap.prototype.acceptParcelsAndRefine = function () {

    if (this.ParcelIDs === null) {
        this.ParcelIDs = [];
    }

    if (this.selectedParcelLayer) {
        this.layerControl.removeLayer(this.selectedParcelLayer);
        this.map.removeLayer(this.selectedParcelLayer);
    }

    var wfsParametersExtended = {
        typeName: "OCStormwater:Parcels",
        cql_filter: "ParcelID in (" + this.ParcelIDs.join(",") + ")",
    };

    wfsParametersExtended = L.Util.extend(wfsParametersExtended, this.wfsParams);

    var self = this;
    jQuery.ajax({
        // todo:
        url: "https://localhost-trash.ocstormwatertools.org/Parcel/Union/",
        data: { ParcelIDs: this.ParcelIDs },
        type: "POST"

    }).then(function (response) {
        var geoJson = JSON.parse(response);
        self.setUpDraw(geoJson);
        self.updateFeatureCollectionJson();
    });

};

NeptuneMaps.AssessmentAreaMap.prototype.getParcelsAndPick = function () {
    this.drawControl.remove();
    this.map.removeLayer(this.editableFeatureGroup);
    this.drawControl = null;
    this.editableFeatureGroup = null;
    this.parcelPickerModeActive = true;

    this.map.off("draw:created");
    this.map.off("draw:edited");
    this.map.off("draw:editstop");
    this.map.off("draw:editvertex");
    this.map.off("draw:deleted");

    var self = this;
    jQuery.ajax({
        // todo
        url: "https://localhost-trash.ocstormwatertools.org/OnlandVisualTrashAssessmentArea/ParcelsViaTransect/" + self.assessmentAreaID //this.ParcelsUrl
    }).then(function (response) {
        self.ParcelIDs = response.ParcelIDs;
        self.updateSelectedParcelLayer();
    });
};

NeptuneMaps.AssessmentAreaMap.prototype.updateSelectedParcelLayer = function () {
    if (this.ParcelIDs === null) {
        this.ParcelIDs = [];
    }

    if (this.selectedParcelLayer) {
        this.layerControl.removeLayer(this.selectedParcelLayer);
        this.map.removeLayer(this.selectedParcelLayer);
    }

    var wmsParametersExtended = {
        layers: "OCStormwater:Parcels",
        cql_filter: "ParcelID in (" + this.ParcelIDs.join(",") + ")",
        styles: "parcel_yellow"
    };

    L.Util.extend(wmsParametersExtended, this.wmsParams);

    this.selectedParcelLayer = L.tileLayer.wms(this.geoserverUrlOWS, wmsParametersExtended);
    this.layerControl.addOverlay(this.selectedParcelLayer,
        "<span><img src='/Content/img/legendImages/selectedGeometry.png' height='12px' style='margin-bottom:3px;'/> Selected Parcels</span>");
    this.map.addLayer(this.selectedParcelLayer);
};

NeptuneMaps.AssessmentAreaMap.prototype.createUpdateFeatureCollectionJsonFunctionAsClosure = function (nameForWkt, nameForAnnotation, mapFormID) {
    this.updateFeatureCollectionJson = function () {
        var hiddens = [];
        var mapForm = jQuery("#" + mapFormID);
        mapForm.html("");

        if (this.editableFeatureGroup) {
            var geoJson = this.editableFeatureGroup.toGeoJSON();
            detectKinksAndReject(geoJson);
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
        }

        if (this.ParcelIDs) {

            for (var i = 0; i < this.ParcelIDs.length; i++) {
                hiddens.push("<input type='hidden' name='ParcelIDs[" + i + "]' value='" + this.ParcelIDs[i] + "' />");
            }
        }

        hiddens.push("<input type='hidden' name='IsParcelPicker' value='" + this.parcelPickerModeActive + "' />");

        mapForm.html(hiddens.join("\r\n"));


    }.bind(this);
};

var detectKinksAndReject = function (geoJson) {
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