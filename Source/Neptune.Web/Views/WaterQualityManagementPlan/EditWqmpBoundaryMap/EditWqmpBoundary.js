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
            },
            remove: true
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

NeptuneMaps.WQMPBoundaryAreaMap.prototype.prepFeatureGroupAndDrawControl = function () {
    /* this is not the best way to prevent drawing multiple polygons, but the other options are:
     * 1. fork Leaflet.Draw and add the functionality or
     * 2.maintain two versions of the draw control that track the same feature group but with different options
     * and the answer to both of those is "no"
     */

    var editableFeatureGroup = this.editableFeatureGroup;
    if (editableFeatureGroup.getLayers().length > 0) {
        killPolygonDraw();
    } else {
        unKillPolygonDraw();
    }
};

NeptuneMaps.WQMPBoundaryAreaMap.prototype.onClickPickParcel = function (event) {
    var latlng = event.latlng;

    var self = this;
    self.getFeatureInfo("OCStormwater:Parcels", [latlng.lng, latlng.lat]).then(function (response) {
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
    this.prepFeatureGroupAndDrawControl();

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
            self.prepFeatureGroupAndDrawControl();
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
            self.prepFeatureGroupAndDrawControl();
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

    //this.setUpBetterDeleteControl();
};

NeptuneMaps.WQMPBoundaryAreaMap.prototype.setUpBetterDeleteControl = function() {
    // leaflet.draw doesn't use a sane event scheme. It was necessary to completely rebuild deleting to make our form pattern work.
    // cloning and replacing the node kills all event handlers
    var el = jQuery(".leaflet-draw-edit-remove").get(0);
    var clone = el.cloneNode(true);
    el.parentNode.replaceChild(clone, el);

    var wqmpBoundaryMap = this;

    wqmpBoundaryMap.tooltip =
        jQuery.parseHTML(
            '<ul class="leaflet-draw-actions leaflet-draw-actions-bottom" style="top: 31px; display: none;"><li class=""><a class="" href="#" title="Save changes.">Click a polygon to delete it.</a></li></ul>')
        [0];

    clone.parentNode.parentNode.append(wqmpBoundaryMap.tooltip);

    // then our own event handler lets us implement deletes in a way that makes literally any sense
    wqmpBoundaryMap.deleting = false;
    L.DomEvent.on(clone,
        "click",
        function() {
            wqmpBoundaryMap.deleting = true;
            wqmpBoundaryMap.tooltip.style.display = "block";
        });
    wqmpBoundaryMap.editableFeatureGroup.on("click",
        function(event) {
            if (!wqmpBoundaryMap.deleting) {
                return;
            }
            wqmpBoundaryMap.editableFeatureGroup.removeLayer(event.layer);
            wqmpBoundaryMap.updateFeatureCollectionJson();
            wqmpBoundaryMap.prepFeatureGroupAndDrawControl();
        });

    // and we also have to make sure we leave our new-and-improved delete mode if draw or create starts
    jQuery(".leaflet-draw-edit-edit, .leaflet-draw-draw-polygon").on("click",
        function() {
            wqmpBoundaryMap.tooltip.style.display = "none";
            wqmpBoundaryMap.deleting = false;
        });
};

NeptuneMaps.WQMPBoundaryMap.prototype.updateSelectedParcelLayer = function () {
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

NeptuneMaps.WQMPBoundaryAreaMap.prototype.createUpdateFeatureCollectionJsonFunctionAsClosure = function (nameForWkt, nameForAnnotation, mapFormID) {
    this.updateFeatureCollectionJson = function () {
        var hiddens = [];
        var mapForm = jQuery("#" + mapFormID);
        mapForm.html("");

        if (this.editableFeatureGroup) {
            var geoJson = this.editableFeatureGroup.toGeoJSON(18);
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

        if (this.tooltip) {
            this.tooltip.style.display = "none";
        }
        this.deleting = false;

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



var killPolygonDraw = function () {
    jQuery(".leaflet-draw-toolbar-top").hide();
};

var unKillPolygonDraw = function () {
    jQuery(".leaflet-draw-toolbar-top").show();
};