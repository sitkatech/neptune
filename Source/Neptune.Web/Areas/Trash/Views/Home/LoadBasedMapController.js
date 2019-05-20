angular.module("NeptuneApp")
    .controller("LoadBasedMapController", function ($scope, angularModelAndViewData, trashMapService) {

        var loadResultsControl = L.control.loadBasedResultsControl({
            position: 'topleft',
            loadCalculationsUrlTemplate: angularModelAndViewData.AngularViewData.LoadBasedResultsUrlTemplate,
            showDropdown: angularModelAndViewData.AngularViewData.ShowDropdown
        });

        NeptuneMaps.initTrashMapController($scope,
            angularModelAndViewData,
            trashMapService,
            angularModelAndViewData.AngularViewData.LoadBasedMapInitJson,
            loadResultsControl,
            {
                showTrashGeneratingUnits: true,
                disallowedTrashCaptureStatusTypeIDs: [3, 4],
                tabSelector: "#loadResultsTab",
                resultsSelector: "#loadResults"
            });


        $scope.applyJurisdictionMask();
    });
