angular.module("NeptuneApp")
    .controller("AssessmentAreaMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);

        $scope.initializeMap = function() {
            $scope.assessmentAreaLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.AssessmentAreaLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function(feature, layer) {

                        return true;
                    },
                    //onEachFeature: function(feature, layer) {
                    //    var modelID = feature.properties.ObservationID;
                    //    var observationModel = _($scope.AngularModel.Observations).find(function(f) {
                    //        return f.OnlandVisualTrashAssessmentObservationID == modelID;
                    //    });
                    //    observationModel.MapMarker = layer;
                    ////},
                    //pointToLayer: function(feature, latlng) {
                    //    var icon = L.MakiMarkers.icon({
                    //        icon: feature.properties.FeatureGlyph,
                    //        color: feature.properties.FeatureColor,
                    //        size: "m"
                    //    });

                    //    return L.marker(latlng,
                    //        {
                    //            icon: icon,
                    //            title: feature.properties.Name,
                    //            alt: feature.properties.Name
                    //        });
                    //},
                    style: function (feature) {
                        return {
                            color: "#b2b2b2",
                            weight: .5,
                            fillColor: "#ffff00",
                            fillOpacity: .5
                        };
                    }
                });
            $scope.assessmentAreaLayerGeoJson.addTo($scope.neptuneMap.map);
            $scope.assessmentAreaLayerGeoJson.on('click',
                function() {
                });
        };

        $scope.initializeMap();

        $scope.zoomToLocation = function () {
            $scope.neptuneMap.map.locate({ setView: true });
        };
    });

