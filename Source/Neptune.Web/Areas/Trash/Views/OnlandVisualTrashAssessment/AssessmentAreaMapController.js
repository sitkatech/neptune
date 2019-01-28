angular.module("NeptuneApp")
    .controller("AssessmentAreaMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);
        $scope.lastSelectedLayer = null;
        $scope.AngularModel.lastSelectedID = null;

        var selectAssessmentArea = function(event) {
            $scope.setSelectedMarker(event.layer.feature);
            $scope.$apply();
        };

        $scope.setSelectedMarker = function (featureLayer) {
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
            $scope.AngularModel.lastSelectedID = featureLayer.properties["OnlandVisualTrashAssessmentAreaID"];
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
        };

        $scope.initializeMap();

        $scope.zoomToLocation = function () {
            $scope.neptuneMap.map.locate({ setView: true });
        };
    });

