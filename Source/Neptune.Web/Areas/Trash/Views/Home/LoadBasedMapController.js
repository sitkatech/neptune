angular.module("NeptuneApp")
    .controller("LoadBasedMapController", function ($scope, angularModelAndViewData, trashMapService) {
        NeptuneMaps.initTrashMapController($scope,
            angularModelAndViewData,
            trashMapService,
            angularModelAndViewData.AngularViewData.LoadBasedMapInitJson,
            null,
            {
                showTrashGeneratingUnits: true,
                disallowedTrashCaptureStatusTypeIDs: [3, 4],
                tabSelector: "#loadResultsTab"
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

        console.log("Load based results loaded successfully");
    });
