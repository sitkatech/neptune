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
                showTrashGeneratingUnits: false,
                showTrashGeneratingUnitLoads: true,
                disallowedTrashCaptureStatusTypeIDs: [3, 4],
                tabSelector: "#loadResultsTab",
                resultsSelector: "#loadResults"
            });

        var loadCurrentOrNetChangeControl = L.control.loadBasedCurrentOrNetChangeControl({
            position: 'topright',
            loadBasedCurrentOrNetChangeGeoserverUrl: angularModelAndViewData.AngularViewData.GeoServerUrl
        });

        loadCurrentOrNetChangeControl.addTo($scope.neptuneMap.map);
        loadCurrentOrNetChangeControl.toggleLoad($scope.neptuneMap);
        loadCurrentOrNetChangeControl.setShownLayerLegendImg($scope.neptuneMap.currentLoadLegendUrl);
        
        jQuery("#loadResults .leaflet-top.leaflet-right")
            .append(jQuery("#loadResults .leaflet-control-layers"));

        $scope.applyJurisdictionMask();

        $scope.neptuneMap.map.on("click",
            function (event) {
                if (!window.freeze) {
                    getTGUPopup(event);
                }
            });

        function getTGUPopup(event) {
            var layerName = "OCStormwater:TrashGeneratingUnitLoads";

            var latlng = event.latlng;
            $scope.neptuneMap.getFeatureInfo(layerName, [latlng.lng, latlng.lat]).then(function (response) {
                if (response.features.length == 0) {
                    return;
                }

                var trashGeneratingUnit = response.features[0];

                var popup = L.popup({ minWidth: 200 })
                    .setLatLng(latlng)
                    .setContent(createPopupContent(trashGeneratingUnit.properties))
                    .openOn($scope.neptuneMap.map).bindPopup();

            });
        }

        function createPopupContent(properties) {

            var organizationDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.OrganizationUrlTemplate).ParameterReplace(properties.OrganizationID);
            var BMPDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.BMPUrlTemplate).ParameterReplace(properties.TreatmentBMPID);
            var WQMPDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.WQMPUrlTemplate).ParameterReplace(properties.WaterQualityManagementPlanID);

            var landUseType = "<strong>Land Use Type:   </strong>" + properties.LandUseType + "<br>";
            var BMPName = "<strong>BMP Name:   </strong>";
            if (properties.TreatmentBMPID) {
                BMPName += "<a href='" + BMPDetailUrl + " 'target='_blank'>" + properties.TreatmentBMPName + "</a><br>";
            } else {
                BMPName += "--<br>";
            }
            var WQMPName = "<strong>WQMP Name:   </strong>";
            if (properties.WaterQualityManagementPlanID) {
                WQMPName += "<a href='" + WQMPDetailUrl + " 'target='_blank'>" + properties.WaterQualityManagementPlanName + "</a><br>";
            } else {
                WQMPName += "--<br>";
            }
            var stormwaterJurisdictionName = "<strong>Stormwater Jurisdiction:   </strong><a href='" + organizationDetailUrl + "' target='_blank'>" + properties.OrganizationName + "</a><br>";

            var baselineScore = "<strong>Baseline Loading Rate:    </strong>" + properties.BaselineLoadingRate + "</br>";
            var currentScore = "<strong>Loading Rate via Trash Capture:    </strong>" + properties.CurrentLoadingRate + "</br>";
            var progressScore = "<strong>Loading Rate via OVTA:    </strong>" + properties.ProgressLoadingRate + "</br>";
            
            var lastCalculatedDate = "<strong>Last Calculated Date:   </strong>";
            if (properties.LastCalculatedDate != null) {
                var date = new Date(properties.LastCalculatedDate);
                lastCalculatedDate += date.toLocaleDateString() + "<br>";
            } else {
                lastCalculatedDate += "--";
            }

            return landUseType +
                //ovtaScore +
                BMPName +
                WQMPName + 
                stormwaterJurisdictionName +
                lastCalculatedDate +
                baselineScore +
                currentScore +
                progressScore;
        }
    });
