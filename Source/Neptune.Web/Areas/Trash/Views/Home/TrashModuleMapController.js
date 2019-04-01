angular.module("NeptuneApp")
    .controller("TreatmentBMPMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.selectedTrashCaptureStatusIDs = _.map($scope.AngularViewData.TrashCaptureStatusTypes,
            function(m) {
                return m.TrashCaptureStatusTypeID.toString();
            });

        $scope.neptuneMap = new NeptuneMaps.GeoServerMap($scope.AngularViewData.MapInitJson,
            "Terrain",
            $scope.AngularViewData.GeoServerUrl);

        $scope.neptuneMap.addWmsLayer("OCStormwater:LandUseBlocks", "Land Use Blocks");


        $scope.ovtaLayers = {
            4: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "OVTA Areas - Score A",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 4
                }),
            3: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "OVTA Areas - Score B",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 3
                }),
            2: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "OVTA Areas - Score C",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 2
                }),
            1: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "OVTA Areas - Score D",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=" + 1
                }),
            0: $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas",
                "OVTA Areas - Not Yet Assessed",
                {
                    cql_filter: $scope.AngularViewData.StormwaterJurisdictionCqlFilter + "Score=0"
                })
    };

        $scope.selectedOVTAScores = function() {
            var scores = [];
            for (var i = 0; i <= 5; i++) {
                if ($scope.neptuneMap.map.hasLayer($scope.ovtaLayers[i])) {
                    scores.push(i);
                }
            }
            return scores;
        };

        $scope.buildSelectOVTAAreaParameters = function(latlng) {
            var scores = $scope.selectedOVTAScores();
            var scoresClause = "Score in (" + scores.join(',') + ")";

            var intersectionClause = "contains(OnlandVisualTrashAssessmentAreaGeometry, POINT(" + latlng.lat + " " + latlng.lng + "))";

            return scoresClause + " and " + intersectionClause;
        };

        $scope.neptuneMap.map.on("click", function(event) {
            var customParams = {
                "cql_filter": $scope.buildSelectOVTAAreaParameters(event.latlng),
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
            $scope.treatmentBMPLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.TreatmentBMPLayerGeoJson.GeoJsonFeatureCollection,
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
            $scope.treatmentBMPLayerGeoJson.addTo($scope.markerClusterGroup);
            $scope.markerClusterGroup.addTo($scope.neptuneMap.map);
            $scope.treatmentBMPLayerGeoJson.on('click',
                function (e) {
                    $scope.setActiveBMPByID(e.layer.feature.properties.TreatmentBMPID);
                    $scope.$apply();
                });

            $scope.neptuneMap.layerControl.addOverlay($scope.markerClusterGroup, "Treatment BMPs");
        };

        $scope.initializeParcelLayer = function () {
            if ($scope.parcelLayerGeoJson) {
                $scope.neptuneMap.layerControl.removeLayer($scope.parcelLayerGeoJson);
                $scope.neptuneMap.map.removeLayer($scope.parcelLayerGeoJson);
            } 
            $scope.parcelLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.ParcelLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {
                        return _.includes($scope.selectedTrashCaptureStatusIDs,
                            feature.properties.TrashCaptureStatusTypeID.toString());
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

            $scope.neptuneMap.layerControl.addOverlay($scope.parcelLayerGeoJson, "Parcels");
        };

        $scope.initializeTreatmentBMPClusteredLayer();
        $scope.initializeParcelLayer();

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

        $scope.filterMapByTrashCaptureStatus = function () {
            $scope.initializeTreatmentBMPClusteredLayer();
            $scope.initializeParcelLayer();
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
                            fillOpacity: 0.5,
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
        
        $scope.setActiveBMPByID = function (treatmentBMPID) {
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            var layer = _.find($scope.treatmentBMPLayerGeoJson._layers,
                function (layer) { return treatmentBMPID === layer.feature.properties.TreatmentBMPID; });
            setActiveImpl(layer, true);
        };

        $scope.setActiveParcelByID = function(parcelID) {
            var parcel = _.find($scope.AngularViewData.Parcels,
                function(t) {
                    return t.ParcelID == parcelID;
                });
            var layer = _.find($scope.parcelLayerGeoJson._layers,
                function(layer) { return parcelID === layer.feature.properties.ParcelID; });
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
            $scope.loadSummaryPanel(layer.feature.properties.MapSummaryUrl);
            $scope.setSelectedMarker(layer);
        }


        $scope.visibleBMPCount = function() {
            return $scope.visibleBMPIDs.length;
        };
        
        $scope.zoomMapToCurrentLocation = function() {
            $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
        };
    });
