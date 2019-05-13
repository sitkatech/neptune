angular.module("NeptuneApp").factory("trashMapService", ['$window', function (win) {
    var trashMapServiceInstance = {};

    trashMapServiceInstance.saveBounds = function (bounds) {
        this.bounds = bounds;
    };

    trashMapServiceInstance.saveCenter = function(center) {
        this.center = center;
    };

    trashMapServiceInstance.saveZoom = function(zoom) {
        this.zoom = zoom;
    };

    trashMapServiceInstance.saveStormwaterJurisdictionID = function(stormwaterJurisdictionID) {
        this.stormwaterJurisdictionID = stormwaterJurisdictionID;
    };

    trashMapServiceInstance.getMapState = function() {
        return {
            center: this.center,
            zoom: this.zoom,
            stormwaterJurisdictionID: this.stormwaterJurisdictionID
        };
    };

    window.test = function() {
        console.log(trashMapServiceInstance);
    };

    return trashMapServiceInstance;
}]);

NeptuneMaps.initTrashMapController = function ($scope, angularModelAndViewData, trashMapService, mapInitJson, resultsControl) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.selectedTrashCaptureStatusIDsForParcelLayer = [1, 2];
    $scope.treatmentBMPLayerLookup = new Map();

    $scope.neptuneMap = new NeptuneMaps.GeoServerMap(mapInitJson,
        "Terrain",
        $scope.AngularViewData.GeoServerUrl);

    // initialize reference layers
    $scope.neptuneMap.vectorLayerGroups[0].addTo($scope.neptuneMap.map);

    var trashGeneratingUnitsLegendUrl = $scope.AngularViewData.GeoServerUrl +
        "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ATrashGeneratingUnits&style=tgu_style&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
    var trashGeneratingUnitsLabel = "<span>Trash Capture Status </br><img src='" + trashGeneratingUnitsLegendUrl + "'/></span>";
    $scope.neptuneMap.addWmsLayer("OCStormwater:TrashGeneratingUnits", trashGeneratingUnitsLabel);

    var wmsParamsForBackgroundLayer = $scope.neptuneMap.createWmsParamsWithLayerName("OCStormwater:MaskLayers");
    var backgroundLayer = L.tileLayer.wms($scope.neptuneMap.geoserverUrlOWS, wmsParamsForBackgroundLayer);
    backgroundLayer.addTo($scope.neptuneMap.map);
    backgroundLayer.bringToFront();


    // initialize results control
    if (resultsControl) {
        resultsControl.addTo($scope.neptuneMap.map);

        $scope.applyJurisdictionMask = function(stormwaterJurisdictionID) {
            if ($scope.maskLayer) {
                $scope.neptuneMap.map.removeLayer($scope.maskLayer);
                $scope.maskLayer = null;
            }

            var wmsParams = $scope.neptuneMap.createWmsParamsWithLayerName("OCStormwater:Jurisdictions");
            wmsParams.cql_filter = "StormwaterJurisdictionID <> " + stormwaterJurisdictionID;
            $scope.maskLayer = L.tileLayer.wms($scope.neptuneMap.geoserverUrlOWS, wmsParams);
            $scope.maskLayer.addTo($scope.neptuneMap.map);
            $scope.maskLayer.bringToFront();
        };

        // this is brittle--it expects the 0th layer in the Layers object to be the Stormwater Jurisdictions
        resultsControl.zoomToJurisdictionOnLoad($scope.AngularViewData.JurisdictionsGeoJson.features,
            $scope.applyJurisdictionMask);
        resultsControl.loadAreaBasedCalculationOnLoad();
        resultsControl.registerZoomToJurisdictionHandler($scope.AngularViewData.JurisdictionsGeoJson.features);

        resultsControl.registerAdditionalHandler($scope.applyJurisdictionMask);

        resultsControl.registerAdditionalHandler(function(stormwaterJurisdictionID) {
            trashMapService.saveStormwaterJurisdictionID(stormwaterJurisdictionID);
        });
    }
};