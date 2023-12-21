angular.module("NeptuneApp").factory("trashMapService", ['$window', function (win) {
    var trashMapServiceInstance = {};

    trashMapServiceInstance.saveBounds = function (bounds) {
        this.bounds = bounds;
    };

    trashMapServiceInstance.saveCenter = function(center) {
        this.center = center;
    };

    trashMapServiceInstance.saveZoom = function(zoom) {
        this.zoom = zoom;
    };

    trashMapServiceInstance.saveStormwaterJurisdictionID = function(stormwaterJurisdictionID) {
        this.stormwaterJurisdictionID = stormwaterJurisdictionID;
    };

    trashMapServiceInstance.getMapState = function() {
        return {
            center: this.center,
            zoom: this.zoom,
            stormwaterJurisdictionID: this.stormwaterJurisdictionID
        };
    };

    window.test = function() {
        console.log(trashMapServiceInstance);
    };

    return trashMapServiceInstance;
}]);

NeptuneMaps.initTrashMapController = function ($scope, angularModelAndViewData, trashMapService, mapInitJson, resultsControl, options) {
    if (!options) {
        options = {};
    }

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.selectedTrashCaptureStatusIDsForParcelLayer = [1, 2];
    $scope.treatmentBMPLayerLookup = new Map();

    $scope.neptuneMap = new NeptuneMaps.Map(mapInitJson,
        "Terrain",
        $scope.AngularViewData.GeoServerUrl);

    // initialize reference layers
    $scope.neptuneMap.vectorLayerGroups[0].addTo($scope.neptuneMap.map);

    if (options.showTrashGeneratingUnitLoads) {
        var currentLoadLegendUrl = $scope.AngularViewData.GeoServerUrl +
            "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ATrashGeneratingUnitLoads&style=current_load&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
        var currentLoadLegendlabel =
            "<span>Current Loading </br><img src='" + currentLoadLegendUrl + "'/></span>";

        $scope.neptuneMap.currentLoadLegendUrl = currentLoadLegendUrl;

        var currentLoadLayer = $scope.neptuneMap.addWmsLayer(
            "OCStormwater:TrashGeneratingUnitLoads",
            currentLoadLegendlabel,
            { styles: "current_load" });

        var deltaLoadLegendUrl = $scope.AngularViewData.GeoServerUrl +
            "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ATrashGeneratingUnitLoads&style=delta_load&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
        var deltaLoadLegendlabel =
            "<span>Net Change in Loading </br><img src='" + deltaLoadLegendUrl + "'/></span>";

        $scope.neptuneMap.deltaLoadLegendUrl = deltaLoadLegendUrl;


        var deltaLoadLayer = $scope.neptuneMap.addWmsLayer(
            "OCStormwater:TrashGeneratingUnitLoads",
            deltaLoadLegendlabel,
            { styles: "delta_load" });
        
        $scope.neptuneMap.layerControl.removeLayer(deltaLoadLayer);
        $scope.neptuneMap.layerControl.removeLayer(currentLoadLayer);
        $scope.neptuneMap.map.removeLayer(deltaLoadLayer);
        
        $scope.neptuneMap.currentLoadLayer = currentLoadLayer;
        $scope.neptuneMap.deltaLoadLayer = deltaLoadLayer;
    }

    if (options.showTrashGeneratingUnits) {
        var trashGeneratingUnitsLegendUrl = $scope.AngularViewData.GeoServerUrl +
            "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ATrashGeneratingUnits&style=tgu_style&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
        var trashGeneratingUnitsLabel =
            "<span>Trash Capture Status </br><img src='" + trashGeneratingUnitsLegendUrl + "'/></span>";
        $scope.neptuneMap.addWmsLayer("OCStormwater:TrashGeneratingUnits", trashGeneratingUnitsLabel);
    }

    if (options.showLandUseBlocks) {
        var landUseBlocksLegendUrl = $scope.AngularViewData.GeoServerUrl +
            "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ALandUseBlocks&style=&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
        var landUseBlocksLabel = "<span>Land Use Blocks </br><img src='" + landUseBlocksLegendUrl + "'/></span>";
        $scope.neptuneMap.addWmsLayer("OCStormwater:LandUseBlocks", landUseBlocksLabel);
    }

    var wmsParamsForBackgroundLayer = $scope.neptuneMap.createWmsParamsWithLayerName("OCStormwater:MaskLayers");
    var backgroundLayer = L.tileLayer.wms($scope.neptuneMap.geoserverUrlOWS, wmsParamsForBackgroundLayer);
    backgroundLayer.addTo($scope.neptuneMap.map);
    backgroundLayer.bringToFront();
    
    $scope.applyJurisdictionMask = function (stormwaterJurisdictionID) {
        if ($scope.maskLayer) {
            $scope.neptuneMap.map.removeLayer($scope.maskLayer);
            $scope.maskLayer = null;
        }

        var wmsParams = $scope.neptuneMap.createWmsParamsWithLayerName("OCStormwater:Jurisdictions");
        if (stormwaterJurisdictionID) {
            wmsParams.cql_filter = "StormwaterJurisdictionID <> " + stormwaterJurisdictionID;
        } else {
            wmsParams.cql_filter = $scope.AngularViewData.NegativeStormwaterJurisdictionCqlFilter;
        }
        $scope.maskLayer = L.tileLayer.wms($scope.neptuneMap.geoserverUrlOWS, wmsParams);
        $scope.maskLayer.addTo($scope.neptuneMap.map);
        $scope.maskLayer.bringToFront();
    };

    // initialize BMPs
    if (!options.disallowedTrashCaptureStatusTypeIDs) {
        options.disallowedTrashCaptureStatusTypeIDs = [];
    }

    $scope.initializeTreatmentBMPClusteredLayer = function (stormwaterJurisdictionID) {
        $scope.treatmentBMPLayers = {};
        $scope.stormwaterJurisdictionID = stormwaterJurisdictionID;
        _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
            function (tcs) {
                if (_.includes(options.disallowedTrashCaptureStatusTypeIDs, tcs.TrashCaptureStatusTypeID)) {
                    return;
                }

                var layer = L.geoJson(
                    $scope.AngularViewData.MapInitJson.TreatmentBMPLayerGeoJson.GeoJsonFeatureCollection,
                    {
                        filter: function (feature, layer) {
                            return feature.properties.TrashCaptureStatusTypeID === tcs.TrashCaptureStatusTypeID && ($scope.stormwaterJurisdictionID === null || $scope.stormwaterJurisdictionID === undefined || feature.properties.StormwaterJurisdictionID.toString() === $scope.stormwaterJurisdictionID);
                        },
                        pointToLayer: function (feature, latlng) {
                            var icon = $scope.neptuneMap.buildDefaultLeafletMarkerFromMarkerPath('/Content/leaflet/images/marker-icon-blue.png');

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

    $scope.applyJurisdictionMaskAndRefreshTreatmentBMPClusteredLayer = function (stormwaterJurisdictionID) {
        $scope.applyJurisdictionMask(stormwaterJurisdictionID);
        $scope.initializeTreatmentBMPClusteredLayer(stormwaterJurisdictionID);

        if ($scope.filterBMPsByTrashCaptureStatusType !== null &&
            $scope.filterBMPsByTrashCaptureStatusType !== undefined) {
            _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
                function (tcs) {
                    $scope.filterBMPsByTrashCaptureStatusType(tcs.TrashCaptureStatusTypeID, $scope.bmpTrashCaptureStatusTypesOn[tcs.TrashCaptureStatusTypeID], true);
                });
            $scope.rebuildMarkerClusterGroup();
        }
    }

    // initialize results control
    if (resultsControl) {
        resultsControl.addTo($scope.neptuneMap.map);

        // this is brittle--it expects the 0th layer in the Layers object to be the Stormwater Jurisdictions
        resultsControl.zoomToJurisdictionOnLoad($scope.AngularViewData.JurisdictionsGeoJson.features,
            $scope.applyJurisdictionMaskAndRefreshTreatmentBMPClusteredLayer);
        resultsControl.loadAreaBasedCalculationOnLoad();
        resultsControl.registerZoomToJurisdictionHandler($scope.AngularViewData.JurisdictionsGeoJson.features);

        resultsControl.registerAdditionalHandler($scope.applyJurisdictionMaskAndRefreshTreatmentBMPClusteredLayer);

        resultsControl.registerAdditionalHandler(function (stormwaterJurisdictionID) {
            trashMapService.saveStormwaterJurisdictionID(stormwaterJurisdictionID);
        });
    }

    $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color

    // initialize map sync
    $scope.neptuneMap.map.on('zoomend', function () {
        $scope.$apply();
        trashMapService.saveZoom($scope.neptuneMap.map.getZoom());
    });
    $scope.neptuneMap.map.on('animationend', function () { $scope.$apply(); });
    $scope.neptuneMap.map.on('moveend', function () {
        $scope.$apply();
        trashMapService.saveCenter($scope.neptuneMap.map.getCenter());
    });
    $scope.neptuneMap.map.on('viewreset', function () { $scope.$apply(); });

    // filtering logic
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

    // utility methods of the controller
    $scope.setSelectedMarker = function (layer) {
        if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
            $scope.neptuneMap.map.removeLayer($scope.lastSelected);
        }

        $scope.lastSelected = L.geoJson(layer.toGeoJSON(),
            {
                pointToLayer: function (feature, latlng) {
                    var icon = $scope.neptuneMap.buildDefaultLeafletMarkerFromMarkerPath('/Content/leaflet/images/marker-icon-selected.png');

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

    $scope.zoomMapToCurrentLocation = function () {
        $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
    };

    // handle switching between tabs (i.e. sync)
    jQuery(options.tabSelector).on("shown.bs.tab", function () {
        var mapState = trashMapService.getMapState();
        $scope.neptuneMap.map.invalidateSize(false);

        $scope.applyJurisdictionMaskAndRefreshTreatmentBMPClusteredLayer(mapState.stormwaterJurisdictionID);

        if (resultsControl) {
            resultsControl.selectJurisdiction(mapState.stormwaterJurisdictionID);
        }

        $scope.neptuneMap.map.setView(mapState.center, mapState.zoom, { animate: false });
    });

    if (options.resultsSelector) {
        jQuery(options.resultsSelector + " .leaflet-top.leaflet-left")
            .append(jQuery(options.resultsSelector + " .leaflet-control-zoom"));
        jQuery(options.resultsSelector + " .leaflet-top.leaflet-left")
            .append(jQuery(options.resultsSelector + " .leaflet-control-fullscreen"));
    }

    // final map init
    $scope.bmpTrashCaptureStatusTypesOn = [];
    $scope.parcelTrashCaptureStatusTypesOn = [];
    _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
        function (tcs) {
            $scope.bmpTrashCaptureStatusTypesOn.push(false);
            $scope.parcelTrashCaptureStatusTypesOn.push(false);
            $scope.filterBMPsByTrashCaptureStatusType(tcs.TrashCaptureStatusTypeID, false, true);
        });
    $scope.rebuildMarkerClusterGroup();
};

window.stopClickPropagation = function (parentElement) {
    L.DomEvent.on(parentElement, "mouseover", function (e) {
        window.freeze = true;
    });

    L.DomEvent.on(parentElement,
        "mouseout",
        function (e) {
            window.freeze = false;
        });
};

function sigFigs(number) {
    if (number < 1000) {
        return number;
    } else {
        return Math.round(number / 1000);
    }
}