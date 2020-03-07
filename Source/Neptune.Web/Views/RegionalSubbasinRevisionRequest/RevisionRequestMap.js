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

var revisionRequestMap;
var drawControl;
var buildMapOnDocumentReady = function (mapInitJson, editableFeatureJsonObject, saveButtonID, geoServerUrl) {
    jQuery(document).ready(function () {

        revisionRequestMap = new NeptuneMaps.Map(mapInitJson, "Terrain", geoServerUrl, {});
        revisionRequestMap.map.setMaxZoom(24);

        addReferenceLayers(revisionRequestMap);

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
            "<span><img src='/Content/img/legendImages/workflowAssessmentArea.png' height='12px' style='margin-bottom:3px;'/> Requested Revision</span>");

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


        revisionRequestMap.tooltip =
            jQuery.parseHTML(
                '<ul class="leaflet-draw-actions leaflet-draw-actions-bottom" style="top: 31px; display: none;"><li class=""><a class="" href="#" title="Save changes.">Click a polygon to delete it.</a></li></ul>')
            [0];


        // then our own event handler lets us implement deletes in a way that makes literally any sense
        revisionRequestMap.deleting = false;
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

        jQuery(".leaflet-draw-edit-edit").get(0).click();
    });
};

var buildReadOnlyMapOnDocumentReady = function(mapInitJson, geoServerUrl) {
    revisionRequestMap = new NeptuneMaps.Map(mapInitJson, "Terrain", geoServerUrl, {});
    revisionRequestMap.map.setMaxZoom(24);

    addReferenceLayers(revisionRequestMap);

    var layerGroup = L.geoJson(mapInitJson.CentralizedDelineationLayerGeoJson.GeoJsonFeatureCollection,
        {
            
        });

    layerGroup.addTo(revisionRequestMap.map);
};

NeptuneMaps.Map.prototype.getTextAreaId = function (featureId) { return "textareaFor" + featureId; };


var createUpdateFeatureCollectionJsonFunctionAsClosure = function (nameForWkt, nameForAnnotation, mapFormID) {
    return function () {
        var geoJson = revisionRequestMap.editableFeatureGroup.toGeoJSON(18);
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

var addReferenceLayers = function(revisionMap) {
    addDelineationWmsLayers(revisionMap);

    // ensure that wms layers fetched through the Neptune.Map interface are always above all other layers
    var regionalSubbasinPane = revisionMap.map.createPane("regionalSubbasinPane");
    regionalSubbasinPane.style.zIndex = 10000;
    revisionMap.map.getPane("markerPane").style.zIndex = 10001;

    var regionalSubbasinLayer =
        revisionMap.addWmsLayer("OCStormwater:RegionalSubbasins",
            "<span><img src='/Content/img/legendImages/regionalSubbasin.png' height='12px' style='margin-bottom:3px;' /> Regional Subbasins</span>",
            { pane: "regionalSubbasinPane" }, false);

    var parcelsLegendUrl = "/Content/img/legendImages/parcel.png";
    var parcelsLabel = "<span><img src='" + parcelsLegendUrl + "' height='14px'/> Parcels</span>";
    revisionMap.addWmsLayer("OCStormwater:Parcels",
        parcelsLabel,
        {
            styles: "parcel"
        }, true);
    regionalSubbasinLayer.bringToFront();

    revisionMap.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>", false);

    L.control.watermark({ position: 'bottomleft' }).addTo(revisionMap.map);
};

var addDelineationWmsLayers = function(revisionMap) {

    var verifiedLegendUrl = '/Content/img/legendImages/delineationVerified.png';
    var verifiedLabel = "<span>Delineations (Verified) </br><img src='" + verifiedLegendUrl + "'/></span>";
    revisionMap.verifiedLayer = revisionMap.addWmsLayer("OCStormwater:Delineations",
        verifiedLabel,
        {
            styles: "delineation",
            cql_filter: "DelineationStatus = 'Verified'",
            maxZoom: 22
        },
        true);

    var provisionalLegendUrl = '/Content/img/legendImages/delineationProvisional.png';
    var provisionalLabel = "<span>Delineations (Provisional) </br><img src='" + provisionalLegendUrl + "'/></span>";
    this.provisionalLayer = revisionMap.addWmsLayer("OCStormwater:Delineations",
        provisionalLabel,
        {
            styles: "delineation",
            cql_filter: "DelineationStatus = 'Provisional'",
            maxZoom: 22
        },
        true);
};
