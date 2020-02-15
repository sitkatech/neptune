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

var revisionRequestMap;
var drawControl;
var buildMapOnDocumentReady = function (mapInitJson, editableFeatureJsonObject, saveButtonID, geoServerUrl) {
    jQuery(document).ready(function () {

        revisionRequestMap = new NeptuneMaps.Map(mapInitJson, "Terrain", geoServerUrl, {});
        revisionRequestMap.map.setMaxZoom(24);
        revisionRequestMap.editableFeatureGroup = new L.FeatureGroup();

        var layerGroup = L.geoJson(editableFeatureJsonObject.GeoJsonFeatureCollection,
            {
                onEachFeature: function (feature, layer) {
                    if (layer.getLayers) {
                        layer.getLayers().forEach(function (l) {
                            revisionRequestMap.editableFeatureGroup.addLayer(l);
                        });
                    } else {
                        revisionRequestMap.editableFeatureGroup.addLayer(layer);
                    }
                }
            });

        var drawOptions = getDrawOptions(revisionRequestMap.editableFeatureGroup);
        drawControl = new L.Control.Draw(drawOptions);
        revisionRequestMap.map.addControl(drawControl);
        revisionRequestMap.map.addLayer(revisionRequestMap.editableFeatureGroup);
        revisionRequestMap.layerControl.addOverlay(revisionRequestMap.editableFeatureGroup,
            "<span><img src='/Content/img/legendImages/workflowAssessmentArea.png' height='12px' style='margin-bottom:3px;'/> Assessment Area</span>");

        revisionRequestMap.map.on('draw:created',
            function (e) {
                var layer = e.layer;
                revisionRequestMap.editableFeatureGroup.addLayer(layer);
                var leafletId = layer._leaflet_id;
                revisionRequestMap.editableFeatureGroup._layers[leafletId].feature = new Object();
                revisionRequestMap.editableFeatureGroup._layers[leafletId].feature.properties = new Object();
                revisionRequestMap.editableFeatureGroup._layers[leafletId].feature.type = "Feature";
                var feature = revisionRequestMap.editableFeatureGroup._layers[leafletId].feature;
                updateFeatureCollectionJson();
            });
        revisionRequestMap.map.on('draw:edited',
            function (e) {
                updateFeatureCollectionJson();
            });
        revisionRequestMap.map.on('draw:editstop',
            function (e) {
                updateFeatureCollectionJson();
            });

        revisionRequestMap.map.on('draw:editvertex',
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
        var zoom = Math.min(revisionRequestMap.map.getZoom(), 18);
        revisionRequestMap.map.setZoom(zoom);

        // leaflet.draw doesn't use a sane event scheme. It was necessary to completely rebuild deleting to make our form pattern work.
        // cloning and replacing the node kills all event handlers
        var el = jQuery(".leaflet-draw-edit-remove").get(0);
        var clone = el.cloneNode(true);
        el.parentNode.replaceChild(clone, el);

        revisionRequestMap.tooltip =
            jQuery.parseHTML(
                '<ul class="leaflet-draw-actions leaflet-draw-actions-bottom" style="top: 31px; display: none;"><li class=""><a class="" href="#" title="Save changes.">Click a polygon to delete it.</a></li></ul>')
            [0];

        clone.parentNode.parentNode.append(revisionRequestMap.tooltip);

        // then our own event handler lets us implement deletes in a way that makes literally any sense
        revisionRequestMap.deleting = false;
        L.DomEvent.on(clone,
            "click",
            function () {
                revisionRequestMap.deleting = true;
                revisionRequestMap.tooltip.style.display = "block";
            });
        revisionRequestMap.editableFeatureGroup.on("click",
            function (event) {
                if (!revisionRequestMap.deleting) {
                    return;
                }
                revisionRequestMap.editableFeatureGroup.removeLayer(event.layer);
                updateFeatureCollectionJson();
            });

        // and we also have to make sure we leave our new-and-improved delete mode if draw or create starts
        jQuery(".leaflet-draw-edit-edit, .leaflet-draw-draw-polygon").on("click",
            function () {
                revisionRequestMap.tooltip.style.display = "none";
                revisionRequestMap.deleting = false;
            });

    });
};

NeptuneMaps.Map.prototype.getTextAreaId = function (featureId) { return "textareaFor" + featureId; };


var createUpdateFeatureCollectionJsonFunctionAsClosure = function (nameForWkt, nameForAnnotation, mapFormID) {
    return function () {
        var geoJson = revisionRequestMap.editableFeatureGroup.toGeoJSON();
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
        if (revisionRequestMap.tooltip) {
            revisionRequestMap.tooltip.style.display = "none";
        }
        revisionRequestMap.deleting = false;
    };
};

var resetZoom = function () {
    revisionRequestMap.map.fitBounds(revisionRequestMap.editableFeatureGroup.getBounds());
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