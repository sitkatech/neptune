getDrawOptions = function (editableFeatureGroup) {
    var options = {
        position: 'topleft',
        draw: {
            polyline: false,
            polygon: true,
            circle: false,
            rectangle: false,
            marker: false
        },
        edit: {
            featureGroup: editableFeatureGroup, //REQUIRED!!
            edit: {
                maintainColor: true,
                opacity: 0.3
            }
        }
    };
    return options;
};

NeptuneMaps.WQMPBoundaryAreaMap = function (mapInitJson, initialBaseLayerShown, geoServerUrl, originalBoundaryAreaGeoJson, options) {
    NeptuneMaps.WQMPBoundaryMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, {});
    L.Util.extend(this.options, options);

    this.originalBoundaryAreaGeoJson = originalBoundaryAreaGeoJson;
    this.WaterQualityManagementPlanID = options.WaterQualityManagementPlanID;
    
    this.map.setMaxZoom(24);
    this.setUpDraw();
    var self = this;

    jQuery("#saveButton").on("click",
        function() {

            self.updateFeatureCollectionJson();
        });
};

NeptuneMaps.WQMPBoundaryAreaMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.WQMPBoundaryMap.prototype);

NeptuneMaps.WQMPBoundaryAreaMap.prototype.getTextAreaId = function (featureId) { return "textareaFor" + featureId; };

NeptuneMaps.WQMPBoundaryAreaMap.prototype.setUpDraw = function (geoJson) {
    if (!geoJson) {
        geoJson = this.originalBoundaryAreaGeoJson.GeoJsonFeatureCollection;
    }
    this.editableFeatureGroup = new L.FeatureGroup();
    var self = this;

    //this.parcelPickerModeActive = false;
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

NeptuneMaps.WQMPBoundaryAreaMap.prototype.createUpdateFeatureCollectionJsonFunctionAsClosure = function (nameForWkt, nameForAnnotation, mapFormID) {
    this.updateFeatureCollectionJson = function () {
        var hiddens = [];
        var mapForm = jQuery("#" + mapFormID);
        mapForm.html("");

        if (this.editableFeatureGroup) {
            var geoJson = this.editableFeatureGroup.toGeoJSON(18);
            //detectKinksAndReject(geoJson);
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

        if (this.tooltip) {
            this.tooltip.style.display = "none";
        }
        this.deleting = false;

    }.bind(this);
};

//var detectKinksAndReject = function (geoJson) {
//    if (!geoJson.features) {
//        return;
//    }

//    var len = geoJson.features.length;
//    for (var i = 0; i < len; i++) {
//        var geom = turf.getGeom(geoJson.features[i]);
//        var kinksFeatureCollection = turf.kinks(geom);
//        if (kinksFeatureCollection.features && kinksFeatureCollection.features.length > 0) {
//            jQuery("button[type='submit']").attr("disabled", true);
//            jQuery("#kinkDanger").css("display", "inline");
//            return;
//        }
//    }

//    // if they make it out here, their geometry is vanilla
//    jQuery("button[type='submit']").removeAttr("disabled");
//    jQuery("#kinkDanger").css("display", "none");
//};
