angular.module("NeptuneApp")
    .controller("AddOrRemoveParcelsController", function ($scope, angularModelAndViewData) {
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.AngularModel = angularModelAndViewData.AngularModel;

        function onMapClick(event) {
            var parcelMapSericeLayerName = $scope.AngularViewData.ParcelMapServiceLayerName,
                mapServiceUrl = $scope.AngularViewData.GeoServerUrl;

            if (!parcelMapSericeLayerName || !mapServiceUrl)
                return;

            var latlng = event.latlng;

            $scope.neptuneMap.getFeatureInfo("OCStormwater:Parcels", [latlng.lng, latlng.lat]).then(function(response) {
                if (response.features.length === 0)
                    return;

                var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));

                $scope.toggleParcel(mergedProperties.ParcelID,
                    function() {
                        $scope.$apply();
                    });
            }).fail(function() {
                console.error("There was an error selecting the " +
                    $scope.AngularViewData.ParcelFieldDefinitionLabel +
                    " from list");
            });
        }

        $scope.toggleParcel = function(parcelId, callback) {
            if (_.includes($scope.AngularModel.ParcelIDs, parcelId)) {
                _.pull($scope.AngularModel.ParcelIDs, parcelId);
            } else {
                $scope.AngularModel.ParcelIDs.push(parcelId);
            }

            updateSelectedParcelLayer();

            if (typeof callback === "function") {
                callback.call();
            }
        };

        function updateSelectedParcelLayer() {

            if ($scope.AngularModel.ParcelIDs == null) {
                $scope.AngularModel.ParcelIDs = [];
            }

            if ($scope.neptuneMap.selectedParcelLayer) {
                $scope.neptuneMap.layerControl.removeLayer($scope.neptuneMap.selectedParcelLayer);
                $scope.neptuneMap.map.removeLayer($scope.neptuneMap.selectedParcelLayer);
            }

            // FIXME: Using L.Util.extend here will overwrite the neptuneMap.wmsParams object with the result of the extend, which may not be ideal
            var wmsParameters = L.Util.extend($scope.neptuneMap.wmsParams,{
                    layers: $scope.AngularViewData.ParcelMapServiceLayerName,
                    cql_filter: "ParcelID in (" + $scope.AngularModel.ParcelIDs.join(",") + ")",
                    styles: "parcel_yellow"
                });

            $scope.neptuneMap.selectedParcelLayer = L.tileLayer.wms($scope.AngularViewData.GeoServerUrl, wmsParameters);
            var labelText = "Selected " + $scope.AngularViewData.ParcelFieldDefinitionLabel;
            var label = "<span><img src='/Content/img/legendImages/selectedGeometry.png' height='12px' style='margin-bottom:3px;'/> " + labelText + "</span>";
            $scope.neptuneMap.layerControl.addOverlay($scope.neptuneMap.selectedParcelLayer, label);
            $scope.neptuneMap.map.addLayer($scope.neptuneMap.selectedParcelLayer);

            // Update map extent to selected parcels
            if (_.any($scope.AngularModel.ParcelIDs)) {
                var wfsParameters = L.Util.extend($scope.neptuneMap.wfsParams,
                    {
                        typeName: $scope.AngularViewData.ParcelMapServiceLayerName,
                        cql_filter: "ParcelID in (" + $scope.AngularModel.ParcelIDs.join(",") + ")"
                    });
                SitkaAjax.ajax({
                    url: $scope.AngularViewData.GeoServerUrl + L.Util.getParamString(wfsParameters),
                    dataType: "json",
                    jsonpCallback: "getJson"
                },
                    function (response) {
                        if (response.features.length === 0)
                            return;
                        
                        $scope.$apply();
                    },
                    function () {
                        console.error("There was an error setting map extent to the selected " + $scope.AngularViewData.ParcelFieldDefinitionLabel + "s");
                    });
            }
        }

        $scope.initializeMap = function () {
            $scope.neptuneMap = new NeptuneMaps.TrashAssessmentMap($scope.AngularViewData.MapInitJson,
                "Terrain",
                $scope.AngularViewData.GeoServerUrl);
            $scope.neptuneMap.map.on("click", onMapClick);
            $scope.observationsLayerGeoJson = $scope.neptuneMap.CreateObservationsLayer($scope.AngularViewData
                .MapInitJson.ObservationsLayerGeoJson.GeoJsonFeatureCollection);

            updateSelectedParcelLayer();
            var zoom = Math.min($scope.neptuneMap.map.getZoom(), 18);
            $scope.neptuneMap.map.setZoom(zoom);
        };

        $scope.initializeMap();
    });
