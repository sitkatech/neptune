angular.module("NeptuneApp")
    .controller("TreatmentBMPMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.selectedTrashCaptureStatusIDs = _.map($scope.AngularViewData.TrashCaptureStatusTypes, function (m) {
            return m.TrashCaptureStatusTypeID.toString();
        });

        $scope.visibleBMPIDs = [];
        $scope.activeTreatmentBMP = {};

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);
        

        $scope.initializeTreatmentBMPClusteredLayer = function () {
            $scope.searchableLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.SearchableLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {
                        return _.includes($scope.selectedTrashCaptureStatusIDs,
                            feature.properties.TrashCaptureStatusTypeID.toString());
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
            if ($scope.markerClusterGroup) {
                $scope.neptuneMap.layerControl.removeLayer($scope.markerClusterGroup);
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
            $scope.searchableLayerGeoJson.addTo($scope.markerClusterGroup);
            $scope.markerClusterGroup.addTo($scope.neptuneMap.map);
            $scope.searchableLayerGeoJson.on('click',
                function (e) {
                    $scope.setActiveByID(e.layer.feature.properties.TreatmentBMPID);
                    $scope.$apply();
                });

            $scope.neptuneMap.layerControl.addOverlay($scope.markerClusterGroup, "Treatment BMPs");
        };

        $scope.initializeTreatmentBMPClusteredLayer();

        $scope.neptuneMap.map.on('zoomend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('animationend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('moveend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('viewreset', function () { $scope.$apply(); });
        $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color

        $scope.applyMap = function(marker, treatmentBMPID) {
            $scope.setSelectedMarker(marker);
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            $scope.activeTreatmentBMP = treatmentBMP;
            $scope.$apply();
        };
        
        $scope.$watch(function () {
            var foundIDs = [];
            var map = $scope.neptuneMap.map;
            map.eachLayer(function(layer) {
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
            $scope.visibleBMPIDs = foundIDs.filter(function(element, index, array) {
                return array.indexOf(element) === index;
            });
        });

        $scope.visibleBMPs = function() {
            var filteredBMPs = _.filter($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return $scope.visibleBMPIDs.includes(t.TreatmentBMPID);
                });
            var orderedBMPs = _.sortBy(filteredBMPs,
                function(t) {
                    return (t.DisplayName);
                });
            return orderedBMPs;
        };

        $scope.filterMapByTrashCaptureStatus = function () {
            $scope.initializeTreatmentBMPClusteredLayer();
        };

        $scope.setSelectedMarker = function(layer) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            $scope.lastSelected = L.geoJson(layer.toGeoJSON(),
                {
                    pointToLayer: function(feature, latlng) {
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
                    style: function(feature) {
                        return {
                            fillColor: "#FFFF00",
                            fill: true,
                            fillOpacity: 0.2,
                            color: "#FFFF00",
                            weight: 5,
                            stroke: true
                        };
                    }
                });

            $scope.lastSelected.addTo($scope.neptuneMap.map);
        };

        $scope.loadSummaryPanel = function(mapSummaryUrl) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty(mapSummaryUrl)) {
                jQuery.get(mapSummaryUrl)
                    .done(function(data) {
                        jQuery('#mapSummaryResults').empty();
                        jQuery('#mapSummaryResults').append(data);
                    });
            }
        };

        $scope.markerClicked = function(self, e) {
            $scope.setSelectedMarker(e.layer);
            $scope.loadSummaryPanel(e.layer.feature.properties.MapSummaryUrl);
        };

        // only used when selecting from the list 
        $scope.setActive = function(treatmentBMP) {
            var layer = _.find($scope.searchableLayerGeoJson._layers,
                function(layer) { return treatmentBMP.TreatmentBMPID === layer.feature.properties.TreatmentBMPID; });
            setActiveImpl(layer, treatmentBMP, false);
        };

        $scope.setActiveByID = function (treatmentBMPID) {
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            var layer = _.find($scope.searchableLayerGeoJson._layers,
                function (layer) { return treatmentBMPID === layer.feature.properties.TreatmentBMPID; });
            setActiveImpl(layer, treatmentBMP, true);
        };

        function setActiveImpl(layer, treatmentBMP, updateMap) {
            if (updateMap) {
                $scope.neptuneMap.map.panTo(layer.getLatLng());
            }

            // multi-way binding
            $scope.loadSummaryPanel(layer.feature.properties.MapSummaryUrl);
            $scope.setSelectedMarker(layer);
            $scope.activeTreatmentBMP = treatmentBMP;
        };

        $scope.isActive = function(treatmentBMP) {
            return $scope.activeTreatmentBMP &&
                $scope.activeTreatmentBMP.TreatmentBMPID === treatmentBMP.TreatmentBMPID;
        };

        $scope.visibleBMPCount = function() {
            return $scope.visibleBMPIDs.length;
        };
        
        $scope.zoomMapToCurrentLocation = function() {
            $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
        };
    });