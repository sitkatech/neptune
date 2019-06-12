getDrawOptions = function (editableFeatureGroup) {
    var options = {
        position: 'topleft',
        draw: {
            polyline: false,
            polygon: false,
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
            remove: false
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
        assessmentAreaMap.CreateObservationsLayer(mapInitJson.ObservationsLayerGeoJson.GeoJsonFeatureCollection);

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
        assessmentAreaMap.layerControl.addOverlay(assessmentAreaMap.editableFeatureGroup, "<span><img src='/Content/img/legendImages/workflowAssessmentArea.png' height='12px' style='margin-bottom:3px;'/> Assessment Area</span>");

        assessmentAreaMap.map.on('draw:created',
            function (e) {
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

        assessmentAreaMap.map.on('draw:editvertex',
            function (e) {
                updateFeatureCollectionJson();
            })

        assessmentAreaMap.map.on('draw:deleted',
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
    });
}

NeptuneMaps.Map.prototype.getTextAreaId = function (featureId) { return "textareaFor" + featureId; };


var createUpdateFeatureCollectionJsonFunctionAsClosure = function(nameForWkt, nameForAnnotation, mapFormID) {
    return function () {
        var geoJson = assessmentAreaMap.editableFeatureGroup.toGeoJSON();
        var mapForm = jQuery("#" + mapFormID);
        mapForm.html("");
        var hiddens = [];
        for (var i = 0; i < geoJson.features.length; ++i) {
            var currentWktName = "name=\"" + nameForWkt + "\"".replace("0", i);
            var currentWktAnnotation = "name=\"" + nameForAnnotation + "\"".replace("0",
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
    }
}

function resetZoom() {
    assessmentAreaMap.map.fitBounds(assessmentAreaMap.editableFeatureGroup.getBounds());
}