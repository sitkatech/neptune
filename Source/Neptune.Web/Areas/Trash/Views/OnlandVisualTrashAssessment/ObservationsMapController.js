angular.module("NeptuneApp")
    .controller("ObservationsMapController", function ($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);
        $scope.currentSelectedMarkerModel = null;
        $scope.currentFakeID = -1;
        $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color

        $scope.initializeMap = function() {
            $scope.observationsLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.ObservationsLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {

                        return true;
                    },
                    onEachFeature: function(feature, layer) {
                        var modelID = feature.properties.ObservationID;
                        var observationModel = _($scope.AngularModel.Observations).find(function(f) {
                            return f.OnlandVisualTrashAssessmentObservationID == modelID;
                        });
                        observationModel.MapMarker = layer;
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
                    style: function (feature) {
                        return {
                            color: feature.properties.FeatureColor = feature.properties.FeatureColor,
                            weight: feature.properties.FeatureWeight = feature.properties.FeatureWeight,
                            fill: feature.properties.FillPolygon = feature.properties.FillPolygon,
                            fillOpacity: feature.properties.FillOpacity = feature.properties.FillOpacity
                        };
                    }
                });
            $scope.observationsLayerGeoJson.addTo($scope.neptuneMap.map);
            $scope.observationsLayerGeoJson.on('click',
                function (e) {
                    $scope.setSelectedMarker(e.layer.feature);
                    $scope.$apply();
                });


            $scope.neptuneMap.map.on("click", onMapClick);
        };

        $scope.initializeMap();

        function onMapClick(event) {
            var latlng = event.latlng;
            setPointOnMap(latlng);
            $scope.$apply();
        }

        function setPointOnMap(latlng) {
            var feature = {
                "type": "Feature",
                "properties": {
                    "ObservationID" : $scope.currentFakeID
                },
                "geometry": {
                    "type": "Point",
                    "coordinates": [latlng.lng, latlng.lat]
                }
            };

            var featurePlaceholder;

            var newMarkerLayer = L.geoJson(feature, {
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

            var observation = {
                ObservationDateTime: (new Date()).toISOString(),
                LocationX: L.Util.formatNum(latlng.lng),
                LocationY: L.Util.formatNum(latlng.lat),
                MapMarker: newMarkerLayer,
                OnlandVisualTrashAssessmentID: $scope.AngularModel.OVTAID,
                OnlandVisualTrashAssessmentObservationID: $scope.currentFakeID
            };

            $scope.AngularModel.Observations.push(observation);

            $scope.currentSelectedMarkerModel = observation;

            newMarkerLayer.addTo($scope.observationsLayerGeoJson);
            $scope.setSelectedMarker(featurePlaceholder);

            $scope.neptuneMap.map.panTo(latlng);
            $scope.currentFakeID--;
        }

        $scope.setSelectedMarker = function (layer) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            if (layer.properties["ObservationID"]) {
                $scope.currentSelectedMarkerModel = _($scope.AngularModel.Observations).find(function(f) {
                    return f.OnlandVisualTrashAssessmentObservationID == layer.properties["ObservationID"];
                });
            }

            $scope.lastSelected = L.geoJson(layer,
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

        $scope.deleteObservation = function() {
            $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            $scope.lastSelected = null;
            $scope.neptuneMap.map.removeLayer($scope.currentSelectedMarkerModel.MapMarker);
            $scope.currentSelectedMarkerModel.MapMarker = null;
            Sitka.Methods.removeFromJsonArray($scope.AngularModel.Observations, $scope.currentSelectedMarkerModel);
            $scope.currentSelectedMarkerModel = null;
        };

        $scope.addObservationAtCurrentLocation = function () {
            $scope.neptuneMap.map.locate({setView:true});
        };

        $scope.neptuneMap.map.on("locationfound", onMapClick);
    });

