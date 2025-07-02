angular.module("NeptuneApp")
    .controller("WaterQualityManagementPlanMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.selectedJurisdictionIDs = _.map($scope.AngularViewData.Jurisdictions, function (m) {
            return m.StormwaterJurisdictionID;
        });

        $scope.activeWQMP = {};

        var selector = '#treatmentBMPFinder';
        var selectorButton = '#treatmentBMPFinderButton';
        var summaryUrl = $scope.AngularViewData.FindWQMPByNameUrl;

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);


        $scope.typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton, summaryUrl) {
            $scope.typeaheadSelector = typeaheadSelector;
            var finder = jQuery(typeaheadSelector);
            finder.typeahead({
                highlight: true,
                minLength: 1
            },
                {
                    source: new Bloodhound({
                        datumTokenizer: Bloodhound.tokenizers.whitespace,
                        queryTokenizer: Bloodhound.tokenizers.whitespace,
                        remote: {
                            cache: false,
                            url: '/WaterQualityManagementPlan/FindByName#%QUERY',
                            wildcard: '%QUERY',
                            transport: function (opts, onSuccess, onError) {
                                var url = opts.url.split("#")[0];
                                var query = opts.url.split("#")[1];
                                $.ajax({
                                    url: url,
                                    data: {
                                        SearchTerm: query,
                                        StormwaterJurisdictionIDs: $scope.selectedJurisdictionIDs
                                    },
                                    type: "POST",
                                    success: onSuccess,
                                    error: onError
                                });
                            }
                        }
                    }),
                    display: 'Text',
                    limit: Number.MAX_VALUE
                });

            finder.bind('typeahead:select',
                function (ev, suggestion) {
                    var summaryDataJson = JSON.parse(suggestion.Value);
                    $scope.loadSummaryPanel(summaryDataJson.MapSummaryUrl);
                    $scope.neptuneMap.map.setView(new L.LatLng(summaryDataJson.Latitude, summaryDataJson.Longitude), 18);
                    $scope.neptuneMap.map.invalidateSize();
                    setTimeout(function () {
                            $scope.applyMap(L.GeoJSON.geometryToLayer(summaryDataJson.GeometryJson), summaryDataJson.EntityID);
                        },
                        500);
                });

            jQuery(typeaheadSelectorButton).click(function () { selectFirstSuggestionFunction(finder); });

            finder.keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    selectFirstSuggestionFunction(this);
                }
            });
        };

        $scope.refreshSelectedJurisdictionsLayer = function () {
            if ($scope.selectedJurisdictionsLayerGeoJson) {
                $scope.neptuneMap.layerControl.removeLayer($scope.selectedJurisdictionsLayerGeoJson);
                $scope.neptuneMap.map.removeLayer($scope.selectedJurisdictionsLayerGeoJson);
            }

            $scope.selectedJurisdictionsLayerGeoJson = L.geoJson(
                $scope.AngularViewData.MapInitJson.JurisdictionLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {
                        return _.includes($scope.selectedJurisdictionIDs,
                                feature.properties.StormwaterJurisdictionID.toString());
                    },
                    style: function (feature) {
                        return {
                            color: $scope.AngularViewData.MapInitJson.JurisdictionLayerGeoJson.LayerColor,
                            weight: 2,
                            fillOpacity: 0 
                        };
                    }
                });
            
            $scope.selectedJurisdictionsLayerGeoJson.addTo($scope.neptuneMap.map);
            var legendSpan = "<span><img src='/Content/img/legendImages/jurisdiction.png' height='20px' /> Jurisdictions</span>";
            $scope.neptuneMap.layerControl.addOverlay($scope.selectedJurisdictionsLayerGeoJson, legendSpan);
        };

        $scope.initalizeWQMPLayer = function () {
            if ($scope.wqmps) {
                $scope.neptuneMap.layerControl.removeLayer($scope.wqmps);
                $scope.neptuneMap.map.removeLayer($scope.wqmps);
            }

            $scope.wqmps = L.geoJson(
                $scope.AngularViewData.MapInitJson.SearchableLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {
                        return _.includes($scope.selectedJurisdictionIDs,
                            feature.properties.StormwaterJurisdictionID.toString());
                    },
                    onEachFeature: function (feature, layer) {
                        layer.on("mouseover",
                            function () {
                                layer.setStyle({ fillOpacity: .6 });
                            });
                        layer.on("mouseout", (e) => {
                            layer.setStyle({ fillOpacity: 0 });
                        });
                    },
                    style: function (feature) {
                        return {
                            color: $scope.AngularViewData.MapInitJson.SearchableLayerGeoJson.LayerColor,
                            weight: 2,
                            fillOpacity: 0
                        };
                    }
                });

            $scope.wqmps.addTo($scope.neptuneMap.map);
            $scope.wqmps.on("click", function (e) {
                $scope.setActiveByID(e.layer.feature.properties.WaterQualityManagementPlanID);
                $scope.$apply();
            })
            var legendSpan = "<span><img src='/Content/img/legendImages/wqmp.png' height='20px' /> WQMPs</span>";
            $scope.neptuneMap.layerControl.addOverlay($scope.wqmps, legendSpan);
        }

        $scope.initalizeParcelLayer = function () {
            var parcelsLegendUrl = "/Content/img/legendImages/parcel.png";
            var parcelsLabel = "<span><img src='" + parcelsLegendUrl + "' height='14px'/> Parcels</span>";
            $scope.wmsOptions = {
                service: "WMS",
                version: "1.1.1",
                info_format: "application/json",
                layers: "OCStormwater:Parcels",
                transparent: true,
                format: "image/png",
                tiled: true,
                styles: "parcel"
            };

            $scope.parcels = L.tileLayer.wms($scope.AngularViewData.GeoServerUrl + "/wms?", $scope.wmsOptions);
            $scope.neptuneMap.layerControl.addOverlay($scope.parcels, parcelsLabel);
        }

        $scope.initalizeDelineationlLayers = function () {

            var verifiedLegendUrl = '/Content/img/legendImages/delineationVerified.png';
            var verifiedLabel = "<span>Delineations (Verified) </br><img src='" + verifiedLegendUrl + "'/></span>";
            $scope.wmsOptions = {
                service: "WMS",
                version: "1.1.1",
                info_format: "application/json",
                layers: "OCStormwater:Delineations",
                transparent: true,
                format: "image/png",
                tiled: true,
                styles: "delineation",
                maxZoom: 22,
                cql_filter: "DelineationStatus = 'Verified'"
            };

            $scope.verifiedDelineations = L.tileLayer.wms($scope.AngularViewData.GeoServerUrl + "/wms?", $scope.wmsOptions);
            $scope.neptuneMap.layerControl.addOverlay($scope.verifiedDelineations, verifiedLabel);

            var provisionalLegendUrl = '/Content/img/legendImages/delineationProvisional.png';
            var provisionalLabel = "<span>Delineations (Provisional) </br><img src='" + provisionalLegendUrl + "'/></span>";
            $scope.wmsOptions = {
                service: "WMS",
                version: "1.1.1",
                info_format: "application/json",
                layers: "OCStormwater:Delineations",
                transparent: true,
                format: "image/png",
                tiled: true,
                styles: "delineation",
                maxZoom: 22,
                cql_filter: "DelineationStatus = 'Provisional'"
            };

            $scope.provisionalDelineations = L.tileLayer.wms($scope.AngularViewData.GeoServerUrl + "/wms?", $scope.wmsOptions);
            $scope.neptuneMap.layerControl.addOverlay($scope.provisionalDelineations, provisionalLabel);
        }

        $scope.initalizeWQMPLayer();

        $scope.refreshSelectedJurisdictionsLayer();

        $scope.initalizeParcelLayer();

        $scope.initalizeDelineationlLayers();

        $scope.neptuneMap.map.on('zoomend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('animationend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('moveend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('viewreset', function () { $scope.$apply(); });
        $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color

        

        $scope.typeaheadSearch(selector, selectorButton, summaryUrl);

        $scope.applyMap = function (marker, waterQualityManagementPlanID) {
            $scope.setSelectedMarker(marker);
            var waterQualityManagementPlan = _.find($scope.AngularViewData.WaterQualityManagementPlan,
                function(t) {
                    return t.WaterQualityManagementPlanID == waterQualityManagementPlanID;
                });
            $scope.activeWaterQualityManagementPlan = waterQualityManagementPlan;
            $scope.$apply();
        };

        $scope.filterMapByJurisdiction = function () {
            $scope.refreshSelectedJurisdictionsLayer();
            $scope.initalizeWQMPLayer();
        };

        $scope.setSelectedMarker = function(layer) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty($scope.lastSelected)) {
                $scope.neptuneMap.map.removeLayer($scope.lastSelected);
            }

            $scope.lastSelected = L.geoJson(layer.toGeoJSON(),
                {
                    style: function(feature) {
                        return {
                            fillColor: "#FFFF00",
                            fill: true,
                            fillOpacity: 0.2,
                            color: "#FFFF00",
                            weight: 5,
                            stroke: true
                        };
                    }
                });

            $scope.lastSelected.addTo($scope.neptuneMap.map);
        };

        $scope.loadSummaryPanel = function (mapSummaryUrl) {
            if (!Sitka.Methods.isUndefinedNullOrEmpty(mapSummaryUrl)) {
                jQuery.get(mapSummaryUrl)
                    .done(function(data) {
                        jQuery('#mapSummaryResults').empty();
                        jQuery('#mapSummaryResults').append(data);
                    });
            }
        };

        // only used when selecting from the list 
        $scope.setActive = function (waterQualityManagementPlan) {
            var layer = _.find($scope.searchableLayerGeoJson._layers,
                function (layer) { return waterQualityManagementPlan.WaterQualityManagementPlanID === layer.feature.properties.WaterQualityManagementPlanID; });
            setActiveImpl(layer, waterQualityManagementPlan, false);
        };

        $scope.setActiveByID = function (waterQualityManagementPlanID) {
            var waterQualityManagementPlan = _.find($scope.AngularViewData.WaterQualityManagementPlans,
                function(t) {
                    return t.WaterQualityManagementPlanID == waterQualityManagementPlanID;
                });
            var layer = _.find($scope.wqmps._layers,
                function (layer) { return waterQualityManagementPlanID === layer.feature.properties.WaterQualityManagementPlanID; });
            setActiveImpl(layer, waterQualityManagementPlan, true);
        };

        function setActiveImpl(layer, waterQualityManagementPlan, updateMap) {
            if (updateMap) {
                var latlngs = layer._originalLatLngs[0][0][0] == undefined ? layer._originalLatLngs[0][0] : layer._originalLatLngs[0][0][0];
                $scope.neptuneMap.map.panTo(latlngs);
            }

            // multi-way binding
            jQuery($scope.typeaheadSelector).typeahead('val', '');
            $scope.loadSummaryPanel(layer.feature.properties.MapSummaryUrl);
            $scope.setSelectedMarker(layer);
            $scope.activeWaterQualityManagementPlan = waterQualityManagementPlan;
        };

        $scope.isActive = function (waterQualityManagementPlan) {
            return $scope.activeWQMP &&
                $scope.activeWaterQualityManagementPlan.WaterQualityManagementPlanID === waterQualityManagementPlan.WaterQualityManagementPlanID;
        };
    });