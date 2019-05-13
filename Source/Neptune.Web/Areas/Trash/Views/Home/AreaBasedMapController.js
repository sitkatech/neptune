angular.module("NeptuneApp")
    .controller("AreaBasedMapController", function ($scope, angularModelAndViewData, trashMapService) {

        var resultsControl = L.control.areaBasedCalculationControl({
            position: 'topleft',
            areaCalculationsUrlTemplate: angularModelAndViewData.AngularViewData.AreaBasedCalculationsUrlTemplate,
            showDropdown: angularModelAndViewData.AngularViewData.ShowDropdown
        });

        NeptuneMaps.initTrashMapController($scope,
            angularModelAndViewData,
            trashMapService,
            angularModelAndViewData.AngularViewData.AreaBasedMapInitJson,
            resultsControl,
            {
                showTrashGeneratingUnits: true,
                disallowedTrashCaptureStatusTypeIDs : [3, 4]
            });

        $scope.fullBmpOn = false;
        $scope.partialBmpOn = false;
        $scope.fullParcelOn = false;
        $scope.partialParcelOn = false;
        _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
            function (tcs) {
                $scope.filterBMPsByTrashCaptureStatusType(tcs.TrashCaptureStatusTypeID, false, true);
            });
        $scope.rebuildMarkerClusterGroup();

        jQuery("#areaResultsTab").on("shown.bs.tab", function () {
            var mapState = trashMapService.getMapState();
            $scope.neptuneMap.map.invalidateSize(false);

            $scope.applyJurisdictionMask(mapState.stormwaterJurisdictionID);
            resultsControl.selectJurisdiction(mapState.stormwaterJurisdictionID);
            $scope.neptuneMap.map.setView(mapState.center, mapState.zoom, { animate: false });
        });

        jQuery("#areaResults .leaflet-top.leaflet-left").append(jQuery("#areaResults .leaflet-control-zoom"));
        jQuery("#areaResults .leaflet-top.leaflet-left").append(jQuery("#areaResults .leaflet-control-fullscreen"));

        trashMapService.saveZoom($scope.neptuneMap.map.getZoom());
        trashMapService.saveBounds($scope.neptuneMap.map.getBounds());
        trashMapService.saveCenter($scope.neptuneMap.map.getCenter());
        trashMapService.saveStormwaterJurisdictionID(resultsControl.getSelectedJurisdictionID());

        console.log("Area Based Map loaded successfully");
    });
