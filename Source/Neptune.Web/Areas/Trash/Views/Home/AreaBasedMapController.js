﻿angular.module("NeptuneApp")
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
                    onMapClick(event);
                }
            });

        function onMapClick(event) {
            var layerName = "OCStormwater:TrashGeneratingUnits";
            var mapServiceUrl = $scope.neptuneMap.geoserverUrlOWS;

            var latlng = event.latlng;
            var latLngWrapped = latlng.wrap();
            var parameters = L.Util.extend($scope.neptuneMap.createWfsParamsWithLayerName(layerName),
                {
                    typeName: layerName,
                    cql_filter: "intersects(TrashGeneratingUnitGeometry, POINT(" + latLngWrapped.lat + " " + latLngWrapped.lng + "))"
                });
            jQuery.ajax({
                url: mapServiceUrl + L.Util.getParamString(parameters),
                type: "GET"
            }).then(function (response) {
                if (response.features.length == 0) {
                    return;
                }

                var trashGeneratingUnit = response.features[0];

                var popup = L.popup({ minWidth: 200 })
                    .setLatLng(latlng)
                    .setContent(createPopupContent(trashGeneratingUnit.properties))
                    .openOn($scope.neptuneMap.map).bindPopup();

            }).fail(function () {
                console.error("There was an error selecting the " +
                    $scope.AngularViewData.JurisdictionID +
                    "from list");
            });
        }

        function createPopupContent(properties) {
            console.log(properties);

            var organizationDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.OrganizationUrlTemplate).ParameterReplace(properties.OrganizationID);
            var BMPDetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.BMPUrlTemplate).ParameterReplace(properties.TreatmentBMPID);
            var OVTAADetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.OVTAAUrlTemplate).ParameterReplace(properties.OnlandVisualTrashAssessmentAreaID);

            var landUseType = "<strong>Land Use Type:   </strong>" + properties.LandUseType + "<br>";
            var ovtaScore = "<strong>Governing OVTA Score:   </strong>";
            if (properties.AssessmentScore != "NotProvided") {
                ovtaScore += "<a href='" + OVTAADetailUrl + "' target='_blank'>" + properties.AssessmentScore + "</a><br>";
            } else {
                ovtaScore += "Not Assessed<br>";
            }
            var BMPName = "<strong>BMP Name:   </strong>";
            if (properties.TreatmentBMPID) {
                BMPName += "<a href='" + BMPDetailUrl + " 'target='_blank'>" + properties.TreatmentBMPName + "</a><br>";
            } else {
                BMPName += "Delineation Not Provided<br>";
            }
            var stormwaterJurisdictionName = "<strong>Stormwater Jurisdiction:   </strong><a href='" + organizationDetailUrl + "' target='_blank'>" + properties.OrganizationName + "</a><br>";

            var lastCalculatedDate = "<strong>Last Calculated Date:   </strong>";
            if (properties.LastCalculatedDate != null) {
                var date = new Date(properties.LastCalculatedDate);
                lastCalculatedDate += date.toLocaleDateString() + "<br>";
            } else {
                lastCalculatedDate += "Not Assessed";
            }
            

            return landUseType + ovtaScore + BMPName + stormwaterJurisdictionName + lastCalculatedDate;
        }

    });

