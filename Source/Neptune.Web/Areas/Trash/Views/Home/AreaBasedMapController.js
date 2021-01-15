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

        $scope.neptuneMap.map.on("click",
            function (event) {
                if (!window.freeze) {
                    getTGUPopup(event);
                }
            });

        function getTGUPopup(event) {
            var layerName = "OCStormwater:TrashGeneratingUnits";

            var latlng = event.latlng;
            $scope.neptuneMap.getFeatureInfo(layerName, [latlng.lng, latlng.lat]).then(function (response) {
                if (response.features == null || response.features == undefined || response.features.length == 0) {
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
            console.log(properties);

            var organizationDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.OrganizationUrlTemplate).ParameterReplace(properties.OrganizationID);
            var BMPDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.BMPUrlTemplate).ParameterReplace(properties.TreatmentBMPID);
            var OVTAADetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.OVTAAUrlTemplate).ParameterReplace(properties.OnlandVisualTrashAssessmentAreaID);
            var WQMPDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.WQMPUrlTemplate).ParameterReplace(properties.WaterQualityManagementPlanID);

            var landUseType = "<strong>Land Use Type:   </strong>" + properties.LandUseType + "<br>";
            var ovtaScore = "";
            if (!$scope.AngularViewData.CurrentUserIsAnonymousOrUnassigned) {
                ovtaScore = "<strong>Governing OVTA Score:   </strong>";
                if (properties.AssessmentScore != "NotProvided") {
                    ovtaScore += "<a href='" + OVTAADetailUrl + "' target='_blank'>" + properties.AssessmentScore + "</a><br>";
                } else {
                    ovtaScore += "--<br>";
                }
            }
            var BMPName = "<strong>Governing BMP:   </strong>";
            if (properties.TreatmentBMPID) {
                BMPName += "<a href='" + BMPDetailUrl + " 'target='_blank'>" + properties.TreatmentBMPName + "</a><br>";
            } else {
                BMPName += "--<br>";
            }
            var WQMPName = "<strong>Governing WQMP:   </strong>";
            if (properties.WaterQualityManagementPlanID) {
                WQMPName += "<a href='" + WQMPDetailUrl + " 'target='_blank'>" + properties.WaterQualityManagementPlanName + "</a><br>";
            } else {
                WQMPName += "--<br>";
            }
            var stormwaterJurisdictionName = "<strong>Stormwater Jurisdiction:   </strong><a href='" + organizationDetailUrl + "' target='_blank'>" + properties.OrganizationName + "</a><br>";

            var lastCalculatedDate = "<strong>Last Calculated Date:   </strong>";
            if (properties.LastCalculatedDate != null) {
                var date = new Date(properties.LastCalculatedDate);
                lastCalculatedDate += date.toLocaleDateString() + "<br>";
            } else {
                lastCalculatedDate += "--";
            }
            

            return landUseType + ovtaScore + BMPName + WQMPName + stormwaterJurisdictionName + lastCalculatedDate;
        }

    });

