angular.module("NeptuneApp")
    .controller("AssessmentAreaMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.neptuneMap = new NeptuneMaps.TrashAssessmentMap($scope.AngularViewData.MapInitJson, "Terrain", $scope.AngularViewData.GeoServerUrl);

        $scope.lastSelectedLayer = null;
        $scope.lastSelectedID = null;
        $scope.lastSelectedName = null;

        var selectAssessmentArea = function(event) {
            $scope.setSelectedFeature(event.layer.feature);
            $scope.$apply();
        };
        
        $scope.setSelectedFeatureByID = function (areaID) {
            if (!areaID) {
                return;
            }

            var layer = _.find($scope.assessmentAreaLayerGeoJson._layers,
                // here we WANT to coerce before comparing; don't use triple-equals
                function (layer) { return areaID == layer.feature.properties["OnlandVisualTrashAssessmentAreaID"]; });
            $scope.setSelectedFeature(layer.feature);
            $scope.lastSelectedID = areaID;
        };

        $scope.setSelectedFeature = function (featureLayer) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelectedLayer)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelectedLayer);
            }
            $scope.lastSelectedID = null;

            $scope.lastSelectedLayer = L.geoJson(featureLayer,
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
                            fillOpacity: 0.75,
                            color: "#FFFF00",
                            weight: 5,
                            stroke: true
                        };
                    }
                });

            $scope.lastSelectedLayer.bindTooltip(featureLayer.properties["OnlandVisualTrashAssessmentAreaName"]);
            $scope.lastSelectedLayer.on('mouseover',
                function (e) {
                    e.target.openPopup();
                });
            $scope.lastSelectedLayer.on('mouseout',
                function (e) {
                    e.target.closePopup();
                });

            $scope.lastSelectedLayer.addTo($scope.neptuneMap.map);
            $scope.lastSelectedID = featureLayer.properties["OnlandVisualTrashAssessmentAreaID"];
            $scope.lastSelectedName = featureLayer.properties["OnlandVisualTrashAssessmentAreaName"];
            jQuery("#assessmentAreaFinder").val($scope.lastSelectedName);

            $scope.neptuneMap.zoomAndPanToLayer($scope.lastSelectedLayer);
        };

        $scope.initializeMap = function (overrideJurisdictionFilterForSelectedArea) {
            if ($scope.assessmentAreaLayerGeoJson) {
                $scope.neptuneMap.layerControl.removeLayer($scope.assessmentAreaLayerGeoJson);
                $scope.neptuneMap.map.removeLayer($scope.assessmentAreaLayerGeoJson);
                $scope.assessmentAreaLayerGeoJson = null;
            }

            $scope.assessmentAreaLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.AssessmentAreaLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function(feature, layer) {
                        return feature.properties["StormwaterJurisdictionID"] == $scope.AngularModel.StormwaterJurisdiction.StormwaterJurisdictionID ||
                            (overrideJurisdictionFilterForSelectedArea && feature.properties["OnlandVisualTrashAssessmentAreaID"] ==
                            $scope.AngularModel.OnlandVisualTrashAssessmentID);
                    },

                    onEachFeature: function (feature, layer) {
                        layer.bindTooltip(feature.properties["OnlandVisualTrashAssessmentAreaName"]);
                        layer.on('mouseover',
                            function (e) {
                                e.target.openPopup();
                            });
                        layer.on('mouseout',
                            function (e) {
                                e.target.closePopup();
                            });
                    },

                    style: function (feature) {
                        return {
                            color: NeptuneMaps.Constants.defaultPolyColor,
                            weight: .5,
                            fillColor: NeptuneMaps.Constants.defaultPolyColor,
                            fillOpacity: .5
                        };
                    }
                });

            $scope.assessmentAreaLayerGeoJson.addTo($scope.neptuneMap.map);
            
            var legendSpan = "<span><img src='/Content/img/legendImages/workflowAssessmentArea.png' height='12px' style='margin-bottom:3px;'/> Assessment Areas</span>";
            $scope.neptuneMap.layerControl.addOverlay($scope.assessmentAreaLayerGeoJson, legendSpan);

            $scope.assessmentAreaLayerGeoJson.on('click', selectAssessmentArea);
        };

        $scope.zoomToLocation = function () {
            $scope.neptuneMap.map.locate({ setView: true });
        };

        // typeahead stuff

        $scope.typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton) {
            $scope.typeaheadSelector = typeaheadSelector;
            var finder = jQuery(typeaheadSelector);
            finder.typeahead({
                highlight: true,
                minLength: 1
            },
                {
                    source: new Bloodhound({
                        datumTokenizer: Bloodhound.tokenizers.whitespace,
                        queryTokenizer: Bloodhound.tokenizers.whitespace,
                        remote: {
                            cache: false,
                            url: '/OnlandVisualTrashAssessmentArea/FindByName#%QUERY',
                            wildcard: '%QUERY',
                            transport: function (opts, onSuccess, onError) {
                                var url = opts.url.split("#")[0];
                                var query = opts.url.split("#")[1];
                                $.ajax({
                                    url: url,
                                    data: {
                                        SearchTerm: query,
                                        JurisdictionID: $scope.AngularModel.StormwaterJurisdiction.StormwaterJurisdictionID
                                    },
                                    type: "POST",
                                    success: onSuccess,
                                    error: onError
                                });
                            }
                        }
                    }),
                    display: 'Text',
                    limit: Number.MAX_VALUE
                });

            finder.bind('typeahead:select',
                function (ev, suggestion) {
                    $scope.setSelectedFeatureByID(suggestion.Value);
                    $scope.$apply();
                });

            jQuery(typeaheadSelectorButton).click(function () { selectFirstSuggestionFunction(finder); });

            finder.keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    selectFirstSuggestionFunction(this);
                }
            });
        };

        $scope.isMapEnabled = function () {
            return $scope.AngularModel.StormwaterJurisdiction && $scope.AngularModel.StormwaterJurisdiction.StormwaterJurisdictionID && !$scope.AngularModel.AssessingNewArea;
        };

        $scope.deselectAll = function() {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelectedLayer)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelectedLayer);
            }
            $scope.lastSelectedID = null;
            $scope.lastSelectedName = null;
        };


        $scope.$watch('AngularModel.AssessingNewArea', function (assessingNewArea) {
            if (assessingNewArea) {
                $scope.AngularModel.OnlandVisualTrashAssessmentAreaID = null;
                $scope.lastSelectedID = null;
                $scope.lastSelectedName = null;
                jQuery("#assessmentAreaFinder").val("");
                if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelectedLayer)) {
                    $scope.neptuneMap.map.removeLayer($scope.lastSelectedLayer);
                }
            }
        });

        // init

        if (!$scope.AngularModel.StormwaterJurisdiction) {
            $scope.AngularModel.StormwaterJurisdiction = {};
        }
        $scope.typeaheadSearch('#assessmentAreaFinder', '#assessmentAreaFinderButton');
        $scope.initializeMap($scope.AngularModel.OnlandVisualTrashAssessmentID !== null);

        $scope.setSelectedFeatureByID($scope.AngularModel.OnlandVisualTrashAssessmentAreaID);

        $scope.setZoomToAbsolutelyCorrectZoom = function() {
            if ($scope.lastSelectedLayer) {
                $scope.neptuneMap.zoomAndPanToLayer($scope.lastSelectedLayer);
                return;
            }

            if ($scope.assessmentAreaLayerGeoJson.getLayers().length) {
                $scope.neptuneMap.zoomAndPanToLayer($scope.assessmentAreaLayerGeoJson);
                return;
            }

            var geoJson = L
                .geoJson(_.find($scope.AngularViewData.MapInitJson.Layers[0].GeoJsonFeatureCollection.features,
                    function (f) {
                        return f.properties.StormwaterJurisdictionID ==
                            $scope.AngularModel.StormwaterJurisdiction.StormwaterJurisdictionID;
                    }));
            var bounds = geoJson.getBounds();
            if (geoJson.getLayers().length) {
                $scope.neptuneMap.map.fitBounds(bounds);
            }
        };

        $scope.setZoomToAbsolutelyCorrectZoom();

        $scope.jurisdictionChanged = function () {
            if ($scope.lastSelectedLayer) {
                $scope.lastSelectedName = null;
                jQuery("#assessmentAreaFinder").val("");
                $scope.neptuneMap.map.removeLayer($scope.lastSelectedLayer);
                $scope.lastSelectedLayer = null;
            }
            $scope.initializeMap();
            $scope.setZoomToAbsolutelyCorrectZoom();
        }
    });
