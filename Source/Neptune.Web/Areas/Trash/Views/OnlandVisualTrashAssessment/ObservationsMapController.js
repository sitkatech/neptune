﻿angular.module("NeptuneApp")
    .controller("ObservationsMapController", function ($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.selectedTrashCaptureStatusIDs = _.map($scope.AngularViewData.TrashCaptureStatusTypes, function (m) {
            return m.TrashCaptureStatusTypeID.toString();
        });

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);
        $scope.currentSelectedPoint = null;

        $scope.initializeMap = function() {
            // todo: drop the markers for existing observations
            // perhaps these are initialized as layers on the MapInitJson
            // as suggested by the name "MapInitJson"
            // but need to be able to interact with the model, too


            $scope.neptuneMap.map.on("click", onMapClick);
        };

        $scope.initializeMap();

        function onMapClick(event) {
            var latlng = event.latlng;
            setPointOnMap(latlng);
            $scope.$apply();
        }

        function setPointOnMap(latlng) {
            var newMarker = L.marker(latlng,
                {
                    icon: L.MakiMarkers.icon({
                        icon: "marker",
                        color: "#FF00FF",
                        size: "m"
                    })
                });

            var observation = {
                ObservationDateTime: new Date(Date.now()),
                LocationX: L.Util.formatNum(latlng.lng),
                LocationY: L.Util.formatNum(latlng.lat),
                MapMarker: newMarker
            };
            $scope.AngularModel.Observations.push(observation);

            $scope.currentSelectedPoint = newMarker;
            $scope.neptuneMap.map.addLayer(newMarker);

            $scope.neptuneMap.map.panTo(latlng);
        }

        // todo: may not need these 
        $scope.neptuneMap.map.on('zoomend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('animationend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('moveend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('viewreset', function () { $scope.$apply(); });
        $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color
        
        // todo: will be used to interact with previously-placed markers
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

        // todo: will be used to interact with previously-placed markers
        function setActiveImpl(layer, updateMap) {
            if (updateMap) {
                if (layer.getLatLng) {
                    $scope.neptuneMap.map.panTo(layer.getLatLng());
                } else {
                    $scope.neptuneMap.map.panTo(layer.getCenter());
                }
            }

            // multi-way binding
            $scope.loadSummaryPanel(layer.feature.properties.MapSummaryUrl);
            $scope.setSelectedMarker(layer);
        }

        // todo: might be good to remember about map.locate for the "set marker at my current location" buttom
        $scope.zoomMapToCurrentLocation = function () {
            $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
        };
    });

