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
                disallowedTrashCaptureStatusTypeIDs: [3, 4],
                tabSelector: "#areaResultsTab",
                resultsSelector: "#areaResults"
            });

        // these trashMapService calls just need to happen on whichever map is active when the page is loaded, which is this one.
        trashMapService.saveZoom($scope.neptuneMap.map.getZoom());
        trashMapService.saveBounds($scope.neptuneMap.map.getBounds());
        trashMapService.saveCenter($scope.neptuneMap.map.getCenter());
        trashMapService.saveStormwaterJurisdictionID(resultsControl.getSelectedJurisdictionID());

        function onMapClick(event) {
            var layerName = "OCStormwater:TrashGeneratingUnits";
            var mapServiceUrl = $scope.AngularViewData.MapServiceUrl;
            if (!layerName || !mapServiceUrl) {
                return;
            }

            debugger;

            var latlng = event.latLng;
            var latLngWrapped = latlng.wrap();
            var parameters = L.Util.extend($scope.neptuneMap.createWfsParamsWithLayerName(layerName),
                {
                    typeName: layerName,
                    cql_filter: "intersects(ParcelGeometry, POINT(" + latLngWrapped.lat + " " + latLngWrapped.lng + "))"
                });
            jQuery.ajax({
                url: mapServiceUrl + L.Util.getParamString(parameters),
                dataTpe: "json",
                jsonCallback: "getJson"
            },
                function (response) {
                    if (response.features.length == 0) {
                        return;
                    }
                    var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));
                    $scope.toggle(mergedProperties.JurisdictionID,
                        function () {
                            $scope.$apply();
                        });
                },
                function () {
                    console.error("There was an error selecting the " +
                        $scope.AngularViewData.JurisdictionID +
                        "from list");
                });
        }

    });
