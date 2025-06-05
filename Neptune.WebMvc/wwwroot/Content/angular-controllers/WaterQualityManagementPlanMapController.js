angular.module("NeptuneApp")
    .controller("WaterQualityManagementPlanMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.selectedWaterQualityManagementPlanIDs = _.map($scope.AngularViewData.WaterQualityManagementPlans, function (m) {
            return m.WaterQualityManagementPlanID;
        });

        $scope.selectedJurisdictionIDs = _.map($scope.AngularViewData.Jurisdictions, function (m) {
            return m.StormwaterJurisdictionID;
        });

        $scope.visibleBMPIDs = [];
        $scope.activeTreatmentBMP = {};

        var selector = '#treatmentBMPFinder';
        var selectorButton = '#treatmentBMPFinderButton';
        var summaryUrl = $scope.AngularViewData.FindTreatmentBMPByNameUrl;

        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson);
        console.log("Hello")
        
        //$scope.typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton, summaryUrl) {
        //    $scope.typeaheadSelector = typeaheadSelector;
        //    var finder = jQuery(typeaheadSelector);
        //    finder.typeahead({
        //        highlight: true,
        //        minLength: 1
        //    },
        //        {
        //            source: new Bloodhound({
        //                datumTokenizer: Bloodhound.tokenizers.whitespace,
        //                queryTokenizer: Bloodhound.tokenizers.whitespace,
        //                remote: {
        //                    cache: false,
        //                    url: '/WaterQualityManagementPlan/FindByName#%QUERY',
        //                    wildcard: '%QUERY',
        //                    transport: function (opts, onSuccess, onError) {
        //                        var url = opts.url.split("#")[0];
        //                        var query = opts.url.split("#")[1];
        //                        $.ajax({
        //                            url: url,
        //                            data: {
        //                                SearchTerm: query,
        //                                WaterQualityManagementPlanIDs: $scope.selectedWaterQualityManagementPlanIDs,
        //                                StormwaterJurisdictionIDs: $scope.selectedJurisdictionIDs
        //                            },
        //                            type: "POST",
        //                            success: onSuccess,
        //                            error: onError
        //                        });
        //                    }
        //                }
        //            }),
        //            display: 'Text',
        //            limit: Number.MAX_VALUE
        //        });

        //    finder.bind('typeahead:select',
        //        function (ev, suggestion) {
        //            var summaryDataJson = JSON.parse(suggestion.Value);
        //            $scope.loadSummaryPanel(summaryDataJson.MapSummaryUrl);
        //            $scope.neptuneMap.map.setView(new L.LatLng(summaryDataJson.Latitude, summaryDataJson.Longitude), 18);
        //            $scope.neptuneMap.map.invalidateSize();
        //            setTimeout(function () {
        //                    $scope.applyMap(L.GeoJSON.geometryToLayer(summaryDataJson.GeometryJson), summaryDataJson.EntityID);
        //                },
        //                500);
        //        });

        //    jQuery(typeaheadSelectorButton).click(function () { selectFirstSuggestionFunction(finder); });

        //    finder.keypress(function (e) {
        //        if (e.which == 13) {
        //            e.preventDefault();
        //            selectFirstSuggestionFunction(this);
        //        }
        //    });
        //};

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
                    },
                    onEachFeature: function (feature, layer) {
                        layer.on("mouseover",
                            function () {
                                layer.setStyle({ fillOpacity: .6 }); // this is what looks the best.
                            });
                        layer.on("mouseout", (e) => {
                            layer.setStyle({ fillOpacity: 0 });
                        });
                        layer.on("click", (e) => {
                            console.log("Clicked!");
                        });
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
                                layer.setStyle({ fillOpacity: .6 }); // this is what looks the best.
                            });
                        layer.on("mouseout", (e) => {
                            layer.setStyle({ fillOpacity: 0 });
                        });
                        layer.on("click", (e) => {
                            console.log("Clicked!");
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
            $scope.wqmps.on('click',
                function (e) {
                    console.log("Clicked!")
                    $scope.setActiveByID(e.layer.feature.properties.WaterQualityManagementPlanID);
                    $scope.$apply();
                });
            var legendSpan = "<span><img src='/Content/img/legendImages/wqmp.png' height='20px' /> WQMPs</span>";
            $scope.neptuneMap.layerControl.addOverlay($scope.wqmps, legendSpan);
        }

        $scope.initalizeWQMPLayer();

        //$scope.refreshSelectedJurisdictionsLayer();

        //$scope.neptuneMap.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
            //"<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>", true);

        $scope.neptuneMap.map.on('zoomend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('animationend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('moveend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('viewreset', function () { $scope.$apply(); });
        $scope.lastSelected = null; //cache for the last clicked item so we can reset it's color

        

        //$scope.typeaheadSearch(selector, selectorButton, summaryUrl);

        $scope.applyMap = function (marker, waterQualityManagementPlanID) {
            console.log("apply map")
            $scope.setSelectedMarker(marker);
            var waterQualityManagementPlan = _.find($scope.AngularViewData.WaterQualityManagementPlan,
                function(t) {
                    return t.WaterQualityManagementPlanID == waterQualityManagementPlanID;
                });
            $scope.activeWaterQualityManagementPlan = waterQualityManagementPlan;
            $scope.$apply();
        };
        $scope.filterMapByBmpType = function () {
            console.log("Filter map by bmp type");
        };

        $scope.filterMapByJurisdiction = function () {
            //$scope.refreshSelectedJurisdictionsLayer();
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
            console.log(mapSummaryUrl)
            if (!Sitka.Methods.isUndefinedNullOrEmpty(mapSummaryUrl)) {
                jQuery.get(mapSummaryUrl)
                    .done(function(data) {
                        jQuery('#mapSummaryResults').empty();
                        jQuery('#mapSummaryResults').append(data);
                    });
            }
        };

        $scope.markerClicked = function (self, e) {
            console.log("marker clickeed")
            $scope.setSelectedMarker(e.layer);
            //$scope.loadSummaryPanel(e.layer.feature.properties.MapSummaryUrl);
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
            return $scope.activeTreatmentBMP &&
                $scope.activeWaterQualityManagementPlan.WaterQualityManagementPlanID === waterQualityManagementPlan.WaterQualityManagementPlanID;
        };

        $scope.visibleBMPCount = function() {
            return $scope.visibleBMPIDs.length;
        };
        
        $scope.zoomMapToCurrentLocation = function() {
            $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
        };
    });