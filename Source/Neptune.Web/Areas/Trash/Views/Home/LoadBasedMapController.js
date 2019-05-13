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
    });
