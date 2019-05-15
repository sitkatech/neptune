﻿angular.module("NeptuneApp")
    .controller("OVTABasedMapController", function ($scope, angularModelAndViewData, trashMapService) {
        var resultsControl = L.control.ovtaBasedResultsControl({
            position: 'topleft',
            OVTABasedResultsUrlTemplate: angularModelAndViewData.AngularViewData.OVTABasedResultsUrlTemplate,
            showDropdown: angularModelAndViewData.AngularViewData.ShowDropdown
        });

        NeptuneMaps.initTrashMapController($scope,
            angularModelAndViewData,
            trashMapService,
            angularModelAndViewData.AngularViewData.OVTABasedMapInitJson,
            resultsControl,
            {
                showLandUseBlocks: true,
                disallowedTrashCaptureStatusTypeIDs: [],
                tabSelector: "#ovtaResultsTab",
                resultsSelector: "#ovtaResults"
            });

        if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.AngularViewData.StormwaterJurisdictionCqlFilter)) {
            $scope.AngularViewData.StormwaterJurisdictionCqlFilter =
                $scope.AngularViewData.StormwaterJurisdictionCqlFilter + " AND ";
        }

        var ovtaLayerLegendUrl = $scope.AngularViewData.GeoServerUrl +
            "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3AOnlandVisualTrashAssessmentAreas&style=ovta_score&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
        var ovtaLayerLabel = "<span>Assessment Areas </br><img src='" + ovtaLayerLegendUrl + "'/></span>";
        $scope.ovtaLayer =
            $scope.neptuneMap.addWmsLayer("OCStormwater:OnlandVisualTrashAssessmentAreas", ovtaLayerLabel);

        $scope.neptuneMap.map.on("click", function (event) {
            var cqlFilter = "contains(OnlandVisualTrashAssessmentAreaGeometry, POINT(" + event.latlng.lat + " " + event.latlng.lng + "))";

            var customParams = {
                "cql_filter": cqlFilter,
                "typeName": "OnlandVisualTrashAssessmentAreas"
            };

            $scope.selectOVTAArea(customParams);
        });

        $scope.selectOVTAArea = function (customParams) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            var parameters = L.Util.extend($scope.neptuneMap.wfsParams, customParams);

            jQuery.ajax({
                url: $scope.neptuneMap.geoserverUrlOWS + L.Util.getParamString(parameters),
                type: "GET"
            }).then(function (response) {
                if (!response.features || !response.features[0]) {
                    return;
                }
                $scope.lastSelected = L.geoJson(response,
                    {
                        style: function (feature) {
                            return {
                                stroke: true,
                                strokeColor: "#ffff00",
                                fillColor: '#ffff00',
                                fillOpacity: 0
                            };
                        },
                    }).addTo($scope.neptuneMap.map);
            });
        };

        $scope.initializeParcelLayer = function () {
            if ($scope.parcelLayerGeoJson) {
                $scope.neptuneMap.map.removeLayer($scope.parcelLayerGeoJson);
            }
            $scope.parcelLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.ParcelLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {
                        return _.includes($scope.selectedTrashCaptureStatusIDsForParcelLayer,
                            feature.properties.TrashCaptureStatusTypeID);
                    },
                    style: function (feature) {
                        return {
                            color: feature.properties.FeatureColor,
                            weight: .5,
                            fill: true,
                            fillOpacity: .35
                        };
                    }
                });

            $scope.parcelLayerGeoJson.addTo($scope.neptuneMap.map);
            $scope.parcelLayerGeoJson.on('click',
                function (e) {
                    $scope.setActiveParcelByID(e.layer.feature.properties.ParcelID);
                    $scope.$apply();
                });
        };
        $scope.initializeParcelLayer();

        $scope.setActiveParcelByID = function (parcelID) {
            var parcel = _.find($scope.AngularViewData.Parcels,
                function (t) {
                    return t.ParcelID == parcelID;
                });
            var layer = _.find($scope.parcelLayerGeoJson._layers,
                function (layer) { return parcelID === layer.feature.properties.ParcelID; });
            setActiveImpl(layer, true);
        };

        $scope.filterParcelsByTrashCaptureStatusType = function (trashCaptureStatusTypeID, isOn) {

            // if the trash capture status is selected, be sure to display on the map. else, be sure it's not displayed
            if (isOn) {
                if (!_.includes($scope.selectedTrashCaptureStatusIDsForParcelLayer, trashCaptureStatusTypeID)) {
                    $scope.selectedTrashCaptureStatusIDsForParcelLayer.push(trashCaptureStatusTypeID);
                }
            } else {

                if (_.includes($scope.selectedTrashCaptureStatusIDsForParcelLayer, trashCaptureStatusTypeID)) {
                    Sitka.Methods.removeFromJsonArray($scope.selectedTrashCaptureStatusIDsForParcelLayer,
                        trashCaptureStatusTypeID);
                }
            }
            $scope.initializeParcelLayer();
        };

        $scope.neptuneMap.map.on("click",
            function (event) {
                if (!window.freeze) {
                    makeOVTAPopup(event);
                }
            });

        function makeOVTAPopup(event) {
            var layerName = "OCStormwater:OnlandVisualTrashAssessmentAreas";
            var mapServiceUrl = $scope.neptuneMap.geoserverUrlOWS;

            var latlng = event.latlng;
            var latLngWrapped = latlng.wrap();
            var parameters = L.Util.extend($scope.neptuneMap.createWfsParamsWithLayerName(layerName),
                {
                    typeName: layerName,
                    cql_filter: "intersects(OnlandVisualTrashAssessmentAreaGeometry, POINT(" + latLngWrapped.lat + " " + latLngWrapped.lng + "))"
                });
            jQuery.ajax({
                url: mapServiceUrl + L.Util.getParamString(parameters),
                type: "GET"
            }).then(function (response) {
                if (response.features.length == 0) {
                    return;
                }

                var content = "";
                var ovtas = _.sortBy(response.features, [function (o) {return o.properties.CompletedDate}]).reverse();

                for (var i = 0; i < ovtas.length; i++) {
                    content += createPopupContent(ovtas[i].properties);
                }         

                var popup = L.popup({ minWidth: 200, maxWidth: 500})
                    .setLatLng(latlng)
                    .setContent(content)
                    .openOn($scope.neptuneMap.map).bindPopup();

            }).fail(function () {
                console.error("There was an error selecting the " +
                    $scope.AngularViewData.JurisdictionID +
                    "from list");
            });
        }

        function createPopupContent(properties) {

            var OVTAADetailUrl = new Sitka.UrlTemplate($scope.AngularViewData.OVTAAUrlTemplate).ParameterReplace(properties.OnlandVisualTrashAssessmentAreaID);

            var ovtaName = "<strong>Assessment Area:   </strong><a href='" + OVTAADetailUrl + "' target='_blank'>" + properties.OnlandVisualTrashAssessmentAreaName + "</a> ";
            var lastCalculatedDateAndScore = "<strong>Last Assessment: </strong>";
            if (properties.OnlandVisualTrashAssessmentScoreDisplayName != null && properties.CompletedDate != null) {
                var date = new Date(properties.CompletedDate);
                var lastCalculatedDate = "";
                if (properties.CompletedDate != null) {
                    lastCalculatedDate = date.toLocaleDateString() + ", ";
                } else {
                    lastCalculatedDate = "Not Assessed";
                }

                var ovtaScore = "<strong>Score: </strong>";
                if (properties.Score != "NotProvided") {
                    ovtaScore += properties.OnlandVisualTrashAssessmentScoreDisplayName;
                } else {
                    ovtaScore += "Not Assessed";
                }

                lastCalculatedDateAndScore = lastCalculatedDate + ovtaScore;
            } else {
                lastCalculatedDateAndScore = "Not Assessed";
            }
            

            return ovtaName + "(" + lastCalculatedDateAndScore + ")<br>";
        }
    });
