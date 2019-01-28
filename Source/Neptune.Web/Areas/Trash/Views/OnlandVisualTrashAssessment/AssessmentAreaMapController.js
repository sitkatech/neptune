angular.module("NeptuneApp")
    .controller("AssessmentAreaMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);

        $scope.initializeMap = function() {
            //$scope.observationsLayerGeoJson = L.geoJson(
            //    $scope.AngularViewData.MapInitJson.ObservationsLayerGeoJson.GeoJsonFeatureCollection,
            //    {
            //        filter: function(feature, layer) {

            //            return true;
            //        },
            //        onEachFeature: function(feature, layer) {
            //            var modelID = feature.properties.ObservationID;
            //            var observationModel = _($scope.AngularModel.Observations).find(function(f) {
            //                return f.OnlandVisualTrashAssessmentObservationID == modelID;
            //            });
            //            observationModel.MapMarker = layer;
            //        },
            //        pointToLayer: function(feature, latlng) {
            //            var icon = L.MakiMarkers.icon({
            //                icon: feature.properties.FeatureGlyph,
            //                color: feature.properties.FeatureColor,
            //                size: "m"
            //            });

            //            return L.marker(latlng,
            //                {
            //                    icon: icon,
            //                    title: feature.properties.Name,
            //                    alt: feature.properties.Name
            //                });
            //        },
            //        style: function(feature) {
            //            return {
            //                color: feature.properties.FeatureColor = feature.properties.FeatureColor,
            //                weight: feature.properties.FeatureWeight = feature.properties.FeatureWeight,
            //                fill: feature.properties.FillPolygon = feature.properties.FillPolygon,
            //                fillOpacity: feature.properties.FillOpacity = feature.properties.FillOpacity
            //            };
            //        }
            //    });
            //$scope.observationsLayerGeoJson.addTo($scope.neptuneMap.map);
            //$scope.observationsLayerGeoJson.on('click',
            //    function() {
            //    });
        };

        $scope.initializeMap();

        $scope.zoomToLocation = function () {
            $scope.neptuneMap.map.locate({ setView: true });
        };
    });

