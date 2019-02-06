angular.module("NeptuneApp")
    .controller("AddOrRemoveParcelsController", function ($scope, angularModelAndViewData) {
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);

        $scope.observationsLayerGeoJson = L.geoJson(
            $scope.AngularViewData.MapInitJson.ObservationsLayerGeoJson.GeoJsonFeatureCollection,
            {
                pointToLayer: function(feature, latlng) {
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
                }
            });
        $scope.observationsLayerGeoJson.addTo($scope.neptuneMap.map);
        $scope.areaLayerGeoJson = L.geoJson(
            $scope.AngularViewData.MapInitJson.AssessmentAreaLayerGeoJson.GeoJsonFeatureCollection,
            {
                style: function(feature) {
                    return {
                        color: "#b2b2b2",
                        weight: .5,
                        fillColor: NeptuneMaps.Constants.defaultPolyFill,
                        fillOpacity: .5
                    };
                }
            });
        $scope.areaLayerGeoJson.addTo($scope.neptuneMap.map);
    });
