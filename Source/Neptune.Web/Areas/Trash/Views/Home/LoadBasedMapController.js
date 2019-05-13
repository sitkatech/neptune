angular.module("NeptuneApp")
    .controller("LoadBasedMapController", function ($scope, angularModelAndViewData, trashMapService) {
        NeptuneMaps.initTrashMapController($scope,
            angularModelAndViewData,
            trashMapService,
            angularModelAndViewData.AngularViewData.LoadBasedMapInitJson,
            null,
            {
                showTrashGeneratingUnits: true,
                disallowedTrashCaptureStatusTypeIDs: [3, 4]
            });
        $scope.applyJurisdictionMask();

        $scope.fullBmpOn = false;
        $scope.partialBmpOn = false;
        $scope.fullParcelOn = false;
        $scope.partialParcelOn = false;
        _.forEach($scope.AngularViewData.TrashCaptureStatusTypes,
            function (tcs) {
                $scope.filterBMPsByTrashCaptureStatusType(tcs.TrashCaptureStatusTypeID, false, true);
            });
        $scope.rebuildMarkerClusterGroup();

        jQuery("#loadResultsTab").on("shown.bs.tab", function () {
            var mapState = trashMapService.getMapState();
            $scope.neptuneMap.map.invalidateSize(false);

            $scope.applyJurisdictionMask(mapState.stormwaterJurisdictionID);
            $scope.neptuneMap.map.setView(mapState.center, mapState.zoom, { animate: false });
        });

        console.log("Load based results loaded successfully");
    });
