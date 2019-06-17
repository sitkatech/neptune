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

var assessmentAreaMap;
var drawControl;
var buildMapOnDocumentReady = function (mapInitJson, editableFeatureJsonObject, saveButtonID, geoServerUrl) {
    jQuery(document).ready(function () {

        assessmentAreaMap = new NeptuneMaps.TrashAssessmentMap(mapInitJson, "Terrain", geoServerUrl);
        assessmentAreaMap.map.setMaxZoom(24);
        assessmentAreaMap.editableFeatureGroup = new L.FeatureGroup();

        var layerGroup = L.geoJson(editableFeatureJsonObject.GeoJsonFeatureCollection,
            {
                onEachFeature: function (feature, layer) {
                    if (layer.getLayers) {
                        layer.getLayers().forEach(function (l) {
                            assessmentAreaMap.editableFeatureGroup.addLayer(l);
                        });
                    } else {
                        assessmentAreaMap.editableFeatureGroup.addLayer(layer);
                    }
                }
            });

        var drawOptions = getDrawOptions(assessmentAreaMap.editableFeatureGroup);
        drawControl = new L.Control.Draw(drawOptions);
        assessmentAreaMap.map.addControl(drawControl);
        assessmentAreaMap.map.addLayer(assessmentAreaMap.editableFeatureGroup);
        assessmentAreaMap.layerControl.addOverlay(assessmentAreaMap.editableFeatureGroup,
            "<span><img src='/Content/img/legendImages/workflowAssessmentArea.png' height='12px' style='margin-bottom:3px;'/> Assessment Area</span>");

        assessmentAreaMap.map.on('draw:created',
            function (e) {
                debugger;
                var layer = e.layer;
                assessmentAreaMap.editableFeatureGroup.addLayer(layer);
                var leafletId = layer._leaflet_id;
                assessmentAreaMap.editableFeatureGroup._layers[leafletId].feature = new Object();
                assessmentAreaMap.editableFeatureGroup._layers[leafletId].feature.properties = new Object();
                assessmentAreaMap.editableFeatureGroup._layers[leafletId].feature.type = "Feature";
                var feature = assessmentAreaMap.editableFeatureGroup._layers[leafletId].feature;
                updateFeatureCollectionJson();
            });
        assessmentAreaMap.map.on('draw:edited',
            function (e) {
                updateFeatureCollectionJson();
            });
        assessmentAreaMap.map.on('draw:editstop',
            function (e) {
                updateFeatureCollectionJson();
            });

        assessmentAreaMap.map.on('draw:editvertex',
            function (e) {
                updateFeatureCollectionJson();
            });

        updateFeatureCollectionJson();

        var saveButton = jQuery("#" + saveButtonID);
        if (!Sitka.Methods.isUndefinedNullOrEmpty(saveButton)) {
            saveButton.text("Save");
        }

        var modalTitle = jQuery(".ui-dialog-title");
        if (!Sitka.Methods.isUndefinedNullOrEmpty(modalTitle)) {
            modalTitle.html("Edit ??? - Detail");
        }

        HookupCheckIfFormIsDirtyNoDisable(undefined);
        var zoom = Math.min(assessmentAreaMap.map.getZoom(), 18);
        assessmentAreaMap.map.setZoom(zoom);

        // leaflet.draw doesn't use a sane event scheme. It was necessary to completely rebuild deleting to make our form pattern work.
        // cloning and replacing the node kills all event handlers
        var el = jQuery(".leaflet-draw-edit-remove").get(0);
        var clone = el.cloneNode(true);
        el.parentNode.replaceChild(clone, el);

        assessmentAreaMap.tooltip =
            jQuery.parseHTML(
                '<ul class="leaflet-draw-actions leaflet-draw-actions-bottom" style="top: 31px; display: none;"><li class=""><a class="" href="#" title="Save changes.">Click a polygon to delete it.</a></li></ul>')
            [0];

        clone.parentNode.parentNode.append(assessmentAreaMap.tooltip);

        // then our own event handler lets us implement deletes in a way that makes literally any sense
        assessmentAreaMap.deleting = false;
        L.DomEvent.on(clone,
            "click",
            function () {
                assessmentAreaMap.deleting = true;
                assessmentAreaMap.tooltip.style.display = "block";
            });
        assessmentAreaMap.editableFeatureGroup.on("click",
            function (event) {
                if (!assessmentAreaMap.deleting) {
                    return;
                }
                assessmentAreaMap.editableFeatureGroup.removeLayer(event.layer);
                updateFeatureCollectionJson();
            });

        // and we also have to make sure we leave our new-and-improved delete mode if draw or create starts
        jQuery(".leaflet-draw-edit-edit, .leaflet-draw-draw-polygon").on("click",
            function () {
                assessmentAreaMap.tooltip.style.display = "none";
                assessmentAreaMap.deleting = false;
            });

    });
};

NeptuneMaps.Map.prototype.getTextAreaId = function (featureId) { return "textareaFor" + featureId; };


var createUpdateFeatureCollectionJsonFunctionAsClosure = function (nameForWkt, nameForAnnotation, mapFormID) {
    return function () {
        var geoJson = assessmentAreaMap.editableFeatureGroup.toGeoJSON();
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
        if (assessmentAreaMap.tooltip) {
            assessmentAreaMap.tooltip.style.display = "none";
        }
        assessmentAreaMap.deleting = false;
    };
};

var resetZoom = function () {
    assessmentAreaMap.map.fitBounds(assessmentAreaMap.editableFeatureGroup.getBounds());
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