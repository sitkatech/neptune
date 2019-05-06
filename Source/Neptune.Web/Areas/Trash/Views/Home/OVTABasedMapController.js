angular.module("NeptuneApp")
    .controller("OVTABasedMapController", function ($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.selectedTrashCaptureStatusIDsForParcelLayer = [1, 2];
        $scope.treatmentBMPLayerLookup = new Map();

        $scope.neptuneMap = new NeptuneMaps.GeoServerMap($scope.AngularViewData.MapInitJson,
            "Terrain",
            $scope.AngularViewData.GeoServerUrl);

        var landUseBlocksLegendUrl = $scope.AngularViewData.GeoServerUrl +
            "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ALandUseBlocks&style=&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
        var landUseBlocksLabel = "<span>Land Use Blocks </br><img src='" + landUseBlocksLegendUrl + "'/></span>";
        $scope.neptuneMap.addWmsLayer("OCStormwater:LandUseBlocks", landUseBlocksLabel);

        var ovtaBasedResultsControl = L.control.ovtaBasedResultsControl({
            position: 'topleft',
            OVTABasedResultsUrlTemplate: $scope.AngularViewData.OVTABasedResultsUrlTemplate,
            showDropdown: $scope.AngularViewData.ShowDropdown
        });

        ovtaBasedResultsControl.addTo($scope.neptuneMap.map);

        var wmsParamsForBackgroundLayer = $scope.neptuneMap.createWmsParamsWithLayerName("OCStormwater:MaskLayers");
        var backgroundLayer = L.tileLayer.wms($scope.neptuneMap.geoserverUrlOWS, wmsParamsForBackgroundLayer);
        backgroundLayer.addTo($scope.neptuneMap.map);
        backgroundLayer.bringToFront();

        var applyJurisdictionMask = function (stormwaterJurisdictionID) {
            if ($scope.maskLayer) {
                $scope.neptuneMap.map.removeLayer($scope.maskLayer);
                $scope.maskLayer = null;
            }

            var wmsParams = $scope.neptuneMap.createWmsParamsWithLayerName("OCStormwater:Jurisdictions");
            wmsParams.cql_filter = "StormwaterJurisdictionID <> " + stormwaterJurisdictionID;
            $scope.maskLayer = L.tileLayer.wms($scope.neptuneMap.geoserverUrlOWS, wmsParams);
            $scope.maskLayer.addTo($scope.neptuneMap.map);
            $scope.maskLayer.bringToFront();

        };

        ovtaBasedResultsControl.zoomToJurisdictionOnLoad($scope.AngularViewData.MapInitJson.Layers[0].GeoJsonFeatureCollection.features, applyJurisdictionMask);
        ovtaBasedResultsControl.loadAreaBasedCalculationOnLoad();
        ovtaBasedResultsControl.registerZoomToJurisdictionHandler($scope.AngularViewData.MapInitJson.Layers[0].GeoJsonFeatureCollection.features);

        ovtaBasedResultsControl.registerAdditionalHandler(applyJurisdictionMask);

        if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.AngularViewData.StormwaterJurisdictionCqlFilter)) {
            $scope.AngularViewData.StormwaterJurisdictionCqlFilter =
                $scope.AngularViewData.StormwaterJurisdictionCqlFilter + " AND ";
        }

        $scope.ovtaLayers = {
            4: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "<span><img src='/Content/img/legendImages/ovtaGreen.png' height='12px' style='margin-bottom:3px;'/> OVTA Areas - Score A</span>",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 4
                }),
            3: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "<span><img src='/Content/img/legendImages/ovtaYellow.png' height='12px' style='margin-bottom:3px;'/> OVTA Areas - Score B</span>",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 3
                }),
            2: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "<span><img src='/Content/img/legendImages/ovtaSalmon.png' height='12px' style='margin-bottom:3px;'/> OVTA Areas - Score C</span>",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 2
                }),
            1: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "<span><img src='/Content/img/legendImages/ovtaMagenta.png' height='12px' style='margin-bottom:3px;'/> OVTA Areas - Score D</span>",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 1
                }),
            0: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "<span><img src='/Content/img/legendImages/ovtaGrey.png' height='12px' style='margin-bottom:3px;'/> OVTA Areas - Not Yet Assessed</span>",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=0"
                })
        };

        $scope.selectedOVTAScores = function () {
            var scores = [];
            for (var i = 0; i <= 5; i++) {
                if ($scope.neptuneMap.map.hasLayer($scope.ovtaLayers[i])) {
                    scores.push(i);
                }
            }
            return scores;
        };

        $scope.buildSelectOVTAAreaParameters = function (latlng) {

        };

        $scope.neptuneMap.map.on("click", function (event) {
            var scores = $scope.selectedOVTAScores();

            if (scores.length === 0) {
                return;
            }

            var scoresClause = "Score in (" + scores.join(',') + ")";

            var intersectionClause = "contains(OnlandVisualTrashAssessmentAreaGeometry, POINT(" + event.latlng.lat + " " + event.latlng.lng + "))";

            var cql_filter = scoresClause + " and " + intersectionClause;

            var customParams = {
                "cql_filter": cql_filter,
                "typeName": "OnlandVisualTrashAssessmentAreas"
            };

            $scope.selectOVTAArea(customParams);

        });



        $scope.selectOVTAArea = function (customParams) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            var parameters = L.Util.extend($scope.neptuneMap.wfsParams, customParams);


            SitkaAjax.ajax({
                url: $scope.neptuneMap.geoserverUrlOWS + L.Util.getParamString(parameters),
                dataType: 'json',
                jsonpCallback: 'getJson'
            },
                function (response) {
                    if (!response.features || !response.features[0]) {
                        return;
                    }
                    $scope.lastSelected = L.geoJson(response, {
                        style: function (feature) {
                            return {
                                stroke: true,
                                strokeColor: "#ffff00",
                                fillColor: '#ffff00',
                                fillOpacity: 0
                            };
                        },
                    }).addTo($scope.neptuneMap.map);

                    var bounds = $scope.lastSelected.getBounds();
                    if (bounds.isValid()) {
                        $scope.neptuneMap.map.fitBounds(bounds);
                    }

                    var assessmentAreaID = response.features[0].properties["OnlandVisualTrashAssessmentAreaID"];
                    var url = "/OnlandVisualTrashAssessmentArea/TrashMapAssetPanel/" + assessmentAreaID;
                    $scope.loadSummaryPanel(url);
                });
        };


        $scope.initializeTreatmentBMPClusteredLayer = function () {

            $scope.treatmentBMPLayers = {};


            _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
                function (tcs) {

                    var layer = L.geoJson(
                        $scope.AngularViewData.MapInitJson.TreatmentBMPLayerGeoJson.GeoJsonFeatureCollection,
                        {
                            filter: function (feature, layer) {
                                return feature.properties.TrashCaptureStatusTypeID === tcs.TrashCaptureStatusTypeID;
                            },
                            pointToLayer: function (feature, latlng) {
                                var icon = L.MakiMarkers.icon({
                                    icon: feature.properties.FeatureGlyph,
                                    color: feature.properties.FeatureColor,
                                    size: "m"
                                });

                                return L.marker(latlng,
                                    {
                                        icon: icon,
                                        title: feature.properties.Name,
                                        alt: feature.properties.Name
                                    });
                            },
                            onEachFeature: function (feature, layer) {
                                $scope.treatmentBMPLayerLookup.set(feature.properties["TreatmentBMPID"], layer);
                            }.bind(this)
                        });
                    $scope.treatmentBMPLayers[tcs.TrashCaptureStatusTypeID] = layer;
                    layer.on('click',
                        function (e) {
                            $scope.setActiveBMPByID(e.layer.feature.properties.TreatmentBMPID);
                            $scope.$apply();
                        });
                });

            $scope.treatmentBMPLayerGroup = L.layerGroup(Object.values($scope.treatmentBMPLayers));

            $scope.rebuildMarkerClusterGroup();
        };

        $scope.rebuildMarkerClusterGroup = function () {

            if ($scope.markerClusterGroup) {
                $scope.neptuneMap.map.removeLayer($scope.markerClusterGroup);
            }

            $scope.markerClusterGroup = L.markerClusterGroup({
                maxClusterRadius: 40,
                showCoverageOnHover: false,
                iconCreateFunction: function (cluster) {
                    return new L.DivIcon({
                        html: '<div><span>' + cluster.getChildCount() + '</span></div>',
                        className: 'treatmentBMPCluster',
                        iconSize: new L.Point(40, 40)
                    });
                }
            });
            $scope.treatmentBMPLayerGroup.addTo($scope.markerClusterGroup);
            $scope.markerClusterGroup.addTo($scope.neptuneMap.map);
        }

        $scope.initializeParcelLayer = function () {
            if ($scope.parcelLayerGeoJson) {
                $scope.neptuneMap.map.removeLayer($scope.parcelLayerGeoJson);
            }
            $scope.parcelLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.ParcelLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {
                        return _.includes($scope.selectedTrashCaptureStatusIDsForParcelLayer,
                            feature.properties.TrashCaptureStatusTypeID);
                    },
                    style: function (feature) {
                        return {
                            color: feature.properties.FeatureColor,
                            weight: .5,
                            fill: true,
                            fillOpacity: .35
                        };
                    }
                });

            $scope.parcelLayerGeoJson.addTo($scope.neptuneMap.map);
            $scope.parcelLayerGeoJson.on('click',
                function (e) {
                    $scope.setActiveParcelByID(e.layer.feature.properties.ParcelID);
                    $scope.$apply();
                });
        };

        $scope.initializeTreatmentBMPClusteredLayer();
        $scope.initializeParcelLayer();

        $scope.neptuneMap.map.on('zoomend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('animationend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('moveend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('viewreset', function () { $scope.$apply(); });
        $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color

        $scope.applyMap = function (marker, treatmentBMPID) {
            $scope.setSelectedMarker(marker);
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function (t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            $scope.activeTreatmentBMP = treatmentBMP;
            $scope.$apply();
        };

        $scope.$watch(function () {
            var foundIDs = [];
            var map = $scope.neptuneMap.map;
            map.eachLayer(function (layer) {
                if (layer instanceof L.Marker && !(layer instanceof L.MarkerCluster)) {
                    if (map.getBounds().contains(layer.getLatLng())) {
                        foundIDs.push(layer.feature.properties.TreatmentBMPID);
                    }
                }
                if (layer instanceof L.MarkerCluster) {
                    if (map.getBounds().contains(layer.getLatLng())) {
                        var markers = layer.getAllChildMarkers();
                        for (var i = 0; i < markers.length; i++) {
                            foundIDs.push(markers[i].feature.properties.TreatmentBMPID);
                        }
                    }
                }
            });
            // clusters get multicounted, so we need to use this function to pick out the unique IDs only
            $scope.visibleBMPIDs = foundIDs.filter(function (element, index, array) {
                return array.indexOf(element) === index;
            });
        });



        $scope.filterBMPsByTrashCaptureStatusType = function (trashCaptureStatusTypeID, isOn, skipRebuild) {
            if (isOn) {
                if (!$scope.treatmentBMPLayerGroup.hasLayer(
                    $scope.treatmentBMPLayers[trashCaptureStatusTypeID])) {
                    $scope.treatmentBMPLayerGroup.addLayer(
                        $scope.treatmentBMPLayers[trashCaptureStatusTypeID]);
                }
            } else {
                if ($scope.treatmentBMPLayerGroup.hasLayer(
                    $scope.treatmentBMPLayers[trashCaptureStatusTypeID])
                ) {
                    $scope.treatmentBMPLayerGroup.removeLayer(
                        $scope.treatmentBMPLayers[trashCaptureStatusTypeID]);
                }
            }
            if (!skipRebuild) {
                $scope.rebuildMarkerClusterGroup();
            }
        };

        $scope.filterParcelsByTrashCaptureStatusType = function (trashCaptureStatusTypeID, isOn) {

            // if the trash capture status is selected, be sure to display on the map. else, be sure it's not displayed
            if (isOn) {
                if (!_.includes($scope.selectedTrashCaptureStatusIDsForParcelLayer, trashCaptureStatusTypeID)) {
                    $scope.selectedTrashCaptureStatusIDsForParcelLayer.push(trashCaptureStatusTypeID);
                }
            } else {

                if (_.includes($scope.selectedTrashCaptureStatusIDsForParcelLayer, trashCaptureStatusTypeID)) {
                    Sitka.Methods.removeFromJsonArray($scope.selectedTrashCaptureStatusIDsForParcelLayer,
                        trashCaptureStatusTypeID);
                }
            }
            $scope.initializeParcelLayer();
        };

        $scope.setSelectedMarker = function (layer) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            $scope.lastSelected = L.geoJson(layer.toGeoJSON(),
                {
                    pointToLayer: function (feature, latlng) {
                        var icon = L.MakiMarkers.icon({
                            icon: "marker",
                            color: "#FFFF00",
                            size: "m"
                        });

                        return L.marker(latlng,
                            {
                                icon: icon,
                                riseOnHover: true
                            });
                    },
                    style: function (feature) {
                        return {
                            fillColor: "#FFFF00",
                            fill: true,
                            fillOpacity: 0.5,
                            color: "#FFFF00",
                            weight: 5,
                            stroke: true
                        };
                    }
                });

            $scope.lastSelected.addTo($scope.neptuneMap.map);
        };

        $scope.markerClicked = function (self, e) {
            $scope.setSelectedMarker(e.layer);
            $scope.loadSummaryPanel(e.layer.feature.properties.MapSummaryUrl);
        };

        $scope.setActiveBMPByID = function (treatmentBMPID) {
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function (t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            //var layer = _.find($scope.treatmentBMPLayerLookup.get(treatmentBMPID),
            //    function (layer) { return treatmentBMPID === layer.properties.TreatmentBMPID; });
            var layer = $scope.treatmentBMPLayerLookup.get(treatmentBMPID);
            setActiveImpl(layer, true);
        };

        $scope.setActiveParcelByID = function (parcelID) {
            var parcel = _.find($scope.AngularViewData.Parcels,
                function (t) {
                    return t.ParcelID == parcelID;
                });
            var layer = _.find($scope.parcelLayerGeoJson._layers,
                function (layer) { return parcelID === layer.feature.properties.ParcelID; });
            setActiveImpl(layer, true);
        };

        function setActiveImpl(layer, updateMap) {
            if (updateMap) {
                if (layer.getLatLng) {
                    $scope.neptuneMap.map.panTo(layer.getLatLng());
                } else {
                    $scope.neptuneMap.map.panTo(layer.getCenter());
                }
            }

            // multi-way binding
            $scope.setSelectedMarker(layer);
        }


        $scope.visibleBMPCount = function () {
            return $scope.visibleBMPIDs.length;
        };

        $scope.zoomMapToCurrentLocation = function () {
            $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
        };

        // ReSharper disable InconsistentNaming
        var FULL_TC = 1;
        var PARTIAL_TC = 2;
        $scope.fullBmpOn = false;
        $scope.partialBmpOn = false;
        $scope.fullParcelOn = false;
        $scope.partialParcelOn = false;
        _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
            function (tcs) {
                $scope.filterBMPsByTrashCaptureStatusType(tcs.TrashCaptureStatusTypeID, false, true);
            });
        $scope.rebuildMarkerClusterGroup();

        jQuery("#ovtaResultsTab").on("shown.bs.tab", function () {
            $scope.neptuneMap.map.invalidateSize(false);
            if (!$scope.ovtaBasedMapExtentSet) {
                $scope.ovtaBasedMapExtentSet = true;
                $scope.neptuneMap.setMapBounds($scope.AngularViewData.MapInitJson);
            }
        });

        jQuery("#ovtaResults .leaflet-top.leaflet-left").append(jQuery("#ovtaResults .leaflet-control-zoom"));
        jQuery("#ovtaResults .leaflet-top.leaflet-left").append(jQuery("#ovtaResults .leaflet-control-fullscreen"));
    });
