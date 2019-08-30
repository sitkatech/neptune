angular.module("NeptuneApp")
    .controller("ObservationsMapController", function ($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.neptuneMap = new NeptuneMaps.TrashAssessmentMap($scope.AngularViewData.MapInitJson, "Terrain", $scope.AngularViewData.GeoServerUrl);
        $scope.currentSelectedMarkerModel = null;
        $scope.currentFakeID = -1;
        $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color
        $scope.isClickToAddModeActive = false;
        $scope.isClickToMoveModeActive = false;

        $scope.activateClickToAddMode = function () {
            $scope.isClickToAddModeActive = true;
            $scope.isClickToMoveModeActive = false;
            if ($scope.neptuneMap.map.hasLayer($scope.neptuneMap.transectLineLayer)) {
                jQuery('.leaflet-container').css('cursor', 'pointer');

            } else {
                jQuery('.leaflet-container').css('cursor', 'crosshair');
            }

            jQuery('.leaflet-container path').css('cursor', 'crosshair');
        };

        $scope.activateClickToMoveMode = function () {
            $scope.isClickToMoveModeActive = true;
            jQuery('.leaflet-container').css('cursor', 'crosshair');
            jQuery('.leaflet-container path').css('cursor', 'crosshair');
        }

        function onMapClick(event) {
            var latlng = event.latlng;
            setPointOnMap(latlng);
            $scope.$apply();
            $scope.isClickToAddModeActive = false;
            jQuery('.leaflet-container').css('cursor', '');
            jQuery('.leaflet-container path').css('cursor', 'pointer');
        }

        function onMapClickToMove(event) {
            var latlng = event.latlng;

            $scope.currentSelectedMarkerModel.MapMarker.setLatLng(latlng);

            $scope.currentSelectedMarkerModel.MapMarker.feature.geometry.coordinates[0] = latlng.lng;
            $scope.currentSelectedMarkerModel.MapMarker.feature.geometry.coordinates[1] = latlng.lat;

            $scope.currentSelectedMarkerModel.LocationX = latlng.lng;
            $scope.currentSelectedMarkerModel.LocationY = latlng.lat;

            var lastSelectedKey = Object.keys($scope.lastSelected._layers)[0];
            var layer = $scope.lastSelected._layers[lastSelectedKey];
            layer.setLatLng(latlng);

            $scope.$apply();
            $scope.isClickToMoveModeActive = false;
            jQuery('.leaflet-container').css('cursor', '');
        }

        $scope.initializeMap = function () {

            if ($scope.AngularViewData.MapInitJson.AssessmentAreaLayerGeoJson) {

                $scope.areaGeoJson = L.geoJson(
                    $scope.AngularViewData.MapInitJson.AssessmentAreaLayerGeoJson.GeoJsonFeatureCollection,
                    {
                        style: function (feature) {
                            return {
                                fillColor: NeptuneMaps.Constants.defaultPolyColor,
                                fill: true,
                                fillOpacity: 0.5,
                                color: NeptuneMaps.Constants.defaultPolyColor,
                                weight: 2,
                                stroke: true
                            };
                        }
                    });

                $scope.areaGeoJson.addTo($scope.neptuneMap.map);
            }

            $scope.observationsLayerGeoJson = $scope.neptuneMap.CreateObservationsLayer(
                $scope.AngularViewData.MapInitJson.ObservationsLayerGeoJson.GeoJsonFeatureCollection,
                {
                    onEachFeature: function (feature, layer) {
                        var modelID = feature.properties.ObservationID;
                        var observationModel = _($scope.AngularModel.Observations).find(function (f) {
                            return f.OnlandVisualTrashAssessmentObservationID == modelID;
                        });
                        observationModel.MapMarker = layer;
                    }
                });

            $scope.observationsLayerGeoJson.on('click',
                function (e) {
                    $scope.setSelectedMarker(e.layer.feature);
                    $scope.$apply();
                    $scope.isClickToAddModeActive = false;
                    $scope.isClickToMoveModeActive = false;
                    jQuery('.leaflet-container').css('cursor', '');
                });

            $scope.neptuneMap.map.on("click",
                function (event) {
                    if ($scope.isClickToAddModeActive) {
                        onMapClick(event);
                    }
                    if ($scope.isClickToMoveModeActive) {
                        onMapClickToMove(event);
                    }
                }
            );

            if ($scope.AngularModel.Observations.length > 0) {
                var zoom = Math.min($scope.neptuneMap.map.getZoom(), 18);
                $scope.neptuneMap.map.setZoom(zoom);
            } else if (!$scope.AngularViewData.MapInitJson.AssessmentAreaLayerGeoJson.GeoJsonFeatureCollection.features.length) {
                var bounds = L
                    .geoJson(_.find($scope.AngularViewData.MapInitJson.Layers[0].GeoJsonFeatureCollection.features,
                        function (f) {
                            return f.properties.StormwaterJurisdictionID ==
                                $scope.AngularViewData.OVTAStormwaterJurisdictionID;
                        })).getBounds();
                $scope.neptuneMap.map.fitBounds(bounds);
            }
        };

        $scope.initializeMap();

        function setPointOnMap(latlng) {

            if ($scope.lastSelected) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            var feature = {
                "type": "Feature",
                "properties": {
                    "ObservationID": $scope.currentFakeID
                },
                "geometry": {
                    "type": "Point",
                    "coordinates": [latlng.lng, latlng.lat]
                }
            };

            var featurePlaceholder;

            var newMarkerLayer = L.geoJson(feature, {
                onEachFeature: function (feature, layer) {
                    var observation = {
                        ObservationDateTime: (new Date()).toISOString(),
                        LocationX: L.Util.formatNum(latlng.lng),
                        LocationY: L.Util.formatNum(latlng.lat),
                        MapMarker: layer,
                        OnlandVisualTrashAssessmentID: $scope.AngularModel.OVTAID,
                        OnlandVisualTrashAssessmentObservationID: $scope.currentFakeID
                    };

                    $scope.AngularModel.Observations.push(observation);

                    $scope.currentSelectedMarkerModel = observation;
                },

                filter: function (feature, layer) {
                    return true;
                },
                pointToLayer: function (feature, latlng) {
                    // this works because there's always only one feature here
                    featurePlaceholder = feature;

                    return L.marker(latlng,
                        {
                            icon: L.MakiMarkers.icon({
                                icon: "marker",
                                color: "#FF00FF",
                                size: "m"
                            })
                        });
                }
            });

            newMarkerLayer.addTo($scope.observationsLayerGeoJson);
            $scope.setSelectedMarker(featurePlaceholder);

            $scope.neptuneMap.map.panTo(latlng);
            $scope.currentFakeID--;
        }

        function showUserLocationOnMap(latlng) {

            // not necessary to recreate this layer if exists, just update
            if ($scope.userLocationLayer) {
                $scope.userLocationMarker.setLatLng(latlng);
                return;
            }

            var feature = {
                "type": "Feature",
                "properties": {
                    "IsUserLocation": true
                },
                "geometry": {
                    "type": "Point",
                    "coordinates": [latlng.lng, latlng.lat]
                }
            };

            $scope.userLocationLayer = L.geoJson(feature, {
                pointToLayer: function (feature, latlng) {

                    $scope.userLocationMarker = L.marker(latlng,
                        {
                            zIndexOffset: -300,

                            icon: L.MakiMarkers.icon({
                                icon: "marker",
                                color: "#919191",
                                size: "m"
                            })
                        });

                    return $scope.userLocationMarker;
                }
            });

            $scope.neptuneMap.layerControl.addOverlay($scope.userLocationLayer,
                "<span><img src='https://api.tiles.mapbox.com/v3/marker/pin-m-water+919191@2x.png' height='30px' /> Current Location</span>");
            $scope.userLocationLayer.addTo($scope.neptuneMap.map);
        }

        $scope.setSelectedMarker = function (feature) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            if (feature.properties["ObservationID"]) {
                $scope.currentSelectedMarkerModel = _($scope.AngularModel.Observations).find(function (f) {
                    return f.OnlandVisualTrashAssessmentObservationID == feature.properties["ObservationID"];
                });
            }

            $scope.lastSelected = L.geoJson(feature,
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
        };

        $scope.deleteObservation = function () {
            $scope.currentSelectedMarkerModel.PhotoStagingID = null;
            $scope.currentSelectedMarkerModel.PhotoID = null;
            $scope.currentSelectedMarkerModel.PhotoUrl = null;
            $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            $scope.lastSelected = null;
            $scope.neptuneMap.map.removeLayer($scope.currentSelectedMarkerModel.MapMarker);
            $scope.currentSelectedMarkerModel.MapMarker = null;
            Sitka.Methods.removeFromJsonArray($scope.AngularModel.Observations, $scope.currentSelectedMarkerModel);
            $scope.currentSelectedMarkerModel = null;
        };

        $scope.addObservationAtCurrentLocation = function () {
            $scope.dropPinOnLocate = true;
            $scope.neptuneMap.map.locate({ setView: false, enableHighAccuracy: true });
        };

        $scope.neptuneMap.map.locate({ setView: false, enableHighAccuracy: true, watch:true });

        $scope.neptuneMap.map.on("locationfound", function (event) {
            if ($scope.dropPinOnLocate) {
                var latlng = event.latlng;
                setPointOnMap(latlng);
                $scope.$apply();
                $scope.isClickToAddModeActive = false;
                $scope.dropPinOnLocate = false;
                jQuery('.leaflet-container').css('cursor', '');
            }

            showUserLocationOnMap(event.latlng);
        });

        // photo handling
        $scope.photoFileTypeError = false;

        $scope.stagePhoto = function () {
            var file = jQuery("#photoUpload")[0].files[0];
            //if (!file.name.trim()) {
            //    var blob = file.slice(0, file.size, file.type);
            //    var newFile = new File([blob], 'image.jpg', { type: file.type });
            //    file = newFile;
            //}

            var blob = file.slice(0, file.size, file.type);

            var formData = new FormData();
            formData.append("Photo", blob, 'image.jpg');
            //formData.boundary = "----------opu" + new Date().getTime();

            if (file.type.split('/')[0] !== "image") {
                $scope.photoFileTypeError = true;
                $scope.$apply();
                jQuery("#photoUpload").fileinput('reset');
                return;
            }

            $.ajax({
                url: "/OnlandVisualTrashAssessmentPhoto/StageObservationPhoto/" + $scope.AngularViewData.ovtaID,
                data: formData,
                processData: false,
                contentType: false,
                type: 'POST',
                beforeSend: function (request) {
                    request.setRequestHeader("Connection", "keep-alive");
                },
                success: function (data) {
                    $scope.photoFileTypeError = false;

                    if (data.Error) {
                        window.alert(data.Error);
                        return;
                    }

                    $scope.currentSelectedMarkerModel.PhotoUrl = data.PhotoStagingUrl;
                    $scope.currentSelectedMarkerModel.PhotoStagingID = data.PhotoStagingID;
                    $scope.$apply();
                    jQuery("#photoUpload").fileinput('reset');
                },
                error: function (jq, ts, et) {
                    window.alert(
                        "There was an error uploading the image. Please try again. If you are using Safari, please switch to Google Chrome as Safari does not support key features of this page.");
                }
            });
        };

        jQuery("#photoUpload").on("change", $scope.stagePhoto);

        $scope.deletePhoto = function () {

            var formData = new FormData();

            if ($scope.currentSelectedMarkerModel.PhotoStagingID) {
                formData.append("ID", $scope.currentSelectedMarkerModel.PhotoStagingID);
                formData.append("IsStagedPhoto", true);
            } else if ($scope.currentSelectedMarkerModel.PhotoID) {
                formData.append("ID", $scope.currentSelectedMarkerModel.PhotoID);
                formData.append("IsStagedPhoto", false);
            } else {
                return;
            }

            $.ajax({
                url: "/OnlandVisualTrashAssessmentPhoto/DeleteObservationPhoto/",
                data: formData,
                processData: false,
                contentType: false,
                type: 'POST',
                success: function (data) {
                    $scope.currentSelectedMarkerModel.PhotoUrl = null;
                    $scope.currentSelectedMarkerModel.PhotoStagingID = null;
                    $scope.$apply();
                },
                error: function (jq, ts, et) {
                    window.alert(
                        "There was an error deleting the image. Please try again. If the issue persists, please contact Support.");
                }
            });
        };

        $scope.currentPhotoUrl = function () {
            if ($scope.currentSelectedMarkerModel) {
                return $scope.currentSelectedMarkerModel.PhotoUrl;
            }
            return null;
        };

        $scope.showUploader = function () {
            if (!$scope.currentSelectedMarkerModel) {
                return null;
            }

            if ($scope.currentSelectedMarkerModel.PhotoUrl) {
                return false;
            }
            return true;
        };
    });
