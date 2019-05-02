angular.module("NeptuneApp")
    .controller("AreaBasedMapController", function ($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.selectedTrashCaptureStatusIDsForParcelLayer = [1, 2];
        $scope.treatmentBMPLayerLookup = new Map();

        $scope.neptuneMap = new NeptuneMaps.GeoServerMap($scope.AngularViewData.AreaBasedMapInitJson,
            "Terrain",
            $scope.AngularViewData.GeoServerUrl);

        var landUseBlocksLegendUrl = $scope.AngularViewData.GeoServerUrl +
            "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ATrashGeneratingUnits&style=tgu_style&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
        var landUseBlocksLabel = "<span>Trash Capture Status </br><img src='" + landUseBlocksLegendUrl + "'/></span>";
        $scope.neptuneMap.addWmsLayer("OCStormwater:TrashGeneratingUnits", landUseBlocksLabel);

        var areaBasedCalculationControl = L.control.areaBasedCalculationControl({
            position: 'topleft',
            areaCalculationsUrlTemplate: $scope.AngularViewData.AreaBasedCalculationsUrlTemplate,
            showDropdown: $scope.AngularViewData.ShowDropdown
        });

        areaBasedCalculationControl.addTo($scope.neptuneMap.map);

        areaBasedCalculationControl.zoomToJurisdictionOnLoad($scope.AngularViewData.MapInitJson.Layers[0].GeoJsonFeatureCollection.features);
        areaBasedCalculationControl.loadAreaBasedCalculationOnLoad();
        areaBasedCalculationControl.registerZoomToJurisdictionHandler($scope.AngularViewData.MapInitJson.Layers[0].GeoJsonFeatureCollection.features);


        $scope.initializeTreatmentBMPClusteredLayer = function () {

            $scope.treatmentBMPLayers = {};


            _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
                function (tcs) {
                    if (tcs.TrashCaptureStatusTypeID === 3 || tcs.TrashCaptureStatusTypeID === 4) {
                        return;
                    }
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
        };



        $scope.initializeTreatmentBMPClusteredLayer();

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
            $scope.areaSummaryPanel(e.layer.feature.properties.MapSummaryUrl);
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

        jQuery("#areaResultsTab").on("shown.bs.tab", function () {
            $scope.neptuneMap.map.invalidateSize(false);
            if (!$scope.areaBasedMapExtentSet) {
                $scope.areaBasedMapExtentSet = true;
                $scope.neptuneMap.setMapBounds($scope.AngularViewData.AreaBasedMapInitJson);
            }
        });

        jQuery("#areaResults .leaflet-top.leaflet-left").append(jQuery("#areaResults .leaflet-control-zoom"));
        jQuery("#areaResults .leaflet-top.leaflet-left").append(jQuery("#areaResults .leaflet-control-fullscreen"));
    });
