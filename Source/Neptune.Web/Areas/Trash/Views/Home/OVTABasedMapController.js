angular.module("NeptuneApp")
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
            $scope.selectOVTAArea(event.latlng);
        });

        $scope.selectOVTAArea = function (latlng) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }
            $scope.neptuneMap.getFeatureInfo("OCStormwater:OnlandVisualTrashAssessmentAreas",
                [latlng.lng, latlng.lat]).then(function (response) {
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

        function setActiveImpl(layer, updateMap) {
            if (updateMap) {
                if (layer.getLatLng) {
                    $scope.neptuneMap.map.panTo(layer.getLatLng());
                } else {
                    $scope.neptuneMap.map.panTo(layer.getCenter());
                }
            }

            // multi-way binding
            $scope.setSelectedMarker(layer);
        }

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

            var latlng = event.latlng;

            $scope.neptuneMap.getFeatureInfo("OCStormwater:OnlandVisualTrashAssessmentAreas",
                [latlng.lng, latlng.lat]).then(function(response) {
                if (response.features.length == 0) {
                    return;
                }

                var content = "";
                var ovtas = _.sortBy(response.features, function(o) { return o.properties.CompletedDate }).reverse();
                var idsToAvoid = [];
                
                for (var i = 0; i < ovtas.length; i++) {
                    var properties = ovtas[i].properties;
                    if (idsToAvoid.indexOf(properties.OnlandVisualTrashAssessmentAreaID) >= 0) {
                        continue;
                    } else {
                        idsToAvoid.push(properties.OnlandVisualTrashAssessmentAreaID);
                        var filteredOVTAs = _.filter(ovtas, function(o) {
                            return o.properties.OnlandVisualTrashAssessmentAreaID ==
                                properties.OnlandVisualTrashAssessmentAreaID;
                        });
                        content += "<strong>Assessment Area:   </strong>"
                        if (!$scope.AngularViewData.CurrentUserIsAnonymousOrUnassigned) {
                            var OVTAADetailUrl =
                                new Sitka.UrlTemplate($scope.AngularViewData.OVTAAUrlTemplate).ParameterReplace(
                                    properties.OnlandVisualTrashAssessmentAreaID);
                            content += "<a href='" +
                                OVTAADetailUrl +
                                "' target='_blank'>" +
                                properties.OnlandVisualTrashAssessmentAreaName +
                                "</a><br/>";
                        } else {
                            content += properties.OnlandVisualTrashAssessmentAreaName + "<br/>";
                        }
                        for (var j = 0; j < filteredOVTAs.length; j++) {
                            content += createPopupContent(filteredOVTAs[j].properties);
                        }
                    }
                }

                var popup = L.popup({ minWidth: 200, maxWidth: 500 })
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
            var OVTADetailUrl =
                new Sitka.UrlTemplate($scope.AngularViewData.OVTAUrlTemplate).ParameterReplace(properties
                    .OnlandVisualTrashAssessmentID);

            var lastCalculatedDateTypeAndScore = "";
            if (properties.Score != null && properties.CompletedDate != null) {
                var date = new Date(properties.CompletedDate);
                var lastCalculatedDate = "&nbsp;&nbsp;";
                if (!$scope.AngularViewData.CurrentUserIsAnonymousOrUnassigned) {
                    if (properties.CompletedDate != null) {
                        lastCalculatedDate = "<a href='" + OVTADetailUrl + "' target='_blank'>" +
                            date.toLocaleDateString() + "</a>, ";
                    } else {
                        lastCalculatedDate = "Not Assessed";
                    }
                }

                var type = "<strong>Type: </strong>";
                    type += properties.IsProgressAssessment ? "Progress, " : "Baseline, ";


                var ovtaScore = "<strong>Score: </strong>";
                if (properties.Score != "NotProvided") {
                    ovtaScore += properties.Score;
                } else {
                    ovtaScore += "Not Assessed";
                }

                lastCalculatedDateTypeAndScore = lastCalculatedDate + type + ovtaScore;
            } else {
                lastCalculatedDateTypeAndScore = "Not Assessed";
            }

            return lastCalculatedDateTypeAndScore + "<br>";
        }
    });
