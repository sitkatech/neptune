angular.module("NeptuneApp")
    .controller("AssessmentAreaMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);
        $scope.lastSelectedLayer = null;
        $scope.lastSelectedID = null;
        $scope.lastSelectedName = null;

        var selectAssessmentArea = function(event) {
            $scope.setSelectedFeature(event.layer.feature);
            $scope.$apply();
        };
        
        $scope.setSelectedFeatureByID = function (areaID) {
            var layer = _.find($scope.assessmentAreaLayerGeoJson._layers,
                // we WANT to coerce before comparing, don't use triple-equals here
                function (layer) { return areaID == layer.feature.properties["OnlandVisualTrashAssessmentAreaID"]; });
            $scope.setSelectedFeature(layer.feature);
            $scope.$apply();
        };

        $scope.setSelectedFeature = function (featureLayer) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelectedLayer)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelectedLayer);
            }
            $scope.lastSelectedID = null;

            $scope.lastSelectedLayer = L.geoJson(featureLayer,
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

            $scope.lastSelectedLayer.addTo($scope.neptuneMap.map);
            $scope.lastSelectedID = featureLayer.properties["OnlandVisualTrashAssessmentAreaID"];
            $scope.lastSelectedName = featureLayer.properties["OnlandVisualTrashAssessmentAreaName"];
        };

        $scope.initializeMap = function() {
            $scope.assessmentAreaLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.AssessmentAreaLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function(feature, layer) {
                        return true;
                    },

                    style: function (feature) {
                        return {
                            color: "#b2b2b2",
                            weight: .5,
                            fillColor: "#ff00ff",
                            fillOpacity: .5
                        };
                    }
                });

            $scope.assessmentAreaLayerGeoJson.addTo($scope.neptuneMap.map);
            $scope.assessmentAreaLayerGeoJson.on('click', selectAssessmentArea);

            if ($scope.AngularModel.OnlandVisualTrashAssessmentAreaID) {
                $scope.setSelectedFeatureByID($scope.AngularModel.OnlandVisualTrashAssessmentAreaID);
            }
        };

        $scope.initializeMap();

        $scope.zoomToLocation = function () {
            $scope.neptuneMap.map.locate({ setView: true });
        };

        // typeahead stuff

        $scope.typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton) {
            $scope.typeaheadSelector = typeaheadSelector;
            var finder = jQuery(typeaheadSelector);
            finder.typeahead({
                highlight: true,
                minLength: 3
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
                });

            jQuery(typeaheadSelectorButton).click(function () { selectFirstSuggestionFunction(finder); });

            finder.keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    selectFirstSuggestionFunction(this);
                }
            });
        };
        $scope.typeaheadSearch('#assessmentAreaFinder', '#assessmentAreaFinderButton');        
    });
