angular.module("NeptuneApp")
    .controller("LoadBasedMapController", function ($scope, angularModelAndViewData, trashMapService) {
        NeptuneMaps.initTrashMapController($scope,
            angularModelAndViewData,
            trashMapService,
            angularModelAndViewData.AngularViewData.LoadBasedMapInitJson,
            null,
            {
                showTrashGeneratingUnits: true,
                disallowedTrashCaptureStatusTypeIDs: [3, 4]
            });
        $scope.applyJurisdictionMask();
        
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

        $scope.fullBmpOn = false;
        $scope.partialBmpOn = false;
        $scope.fullParcelOn = false;
        $scope.partialParcelOn = false;
        _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
            function (tcs) {
                $scope.filterBMPsByTrashCaptureStatusType(tcs.TrashCaptureStatusTypeID, false, true);
            });
        $scope.rebuildMarkerClusterGroup();

        jQuery("#loadResultsTab").on("shown.bs.tab", function () {
            var mapState = trashMapService.getMapState();
            $scope.neptuneMap.map.invalidateSize(false);

            $scope.applyJurisdictionMask(mapState.stormwaterJurisdictionID);
            $scope.neptuneMap.map.setView(mapState.center, mapState.zoom, { animate: false });
        });

        console.log("Load based results loaded successfully");
    });
