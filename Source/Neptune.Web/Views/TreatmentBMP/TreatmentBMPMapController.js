angular.module("NeptuneApp")
    .controller("TreatmentBMPMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.neptuneMap = new NeptuneMaps.StormwaterSearch($scope.AngularViewData.MapInitJson);

        $scope.selectedTreatmentBMPTypeIDs = [];
        $scope.visibleBMPIDs = [];
        $scope.activeTreatmentBMP = {};

        var selector = '#treatmentBMPFinder';
        var selectorButton = '#treatmentBMPFinderButton';
        var summaryUrl = $scope.AngularViewData.FindTreatmentBMPByNameUrl;

        $scope.neptuneMap.typeaheadSearch(selector, selectorButton, summaryUrl);
        $scope.neptuneMap.apply = function (marker, treatmentBMPID) {
            $scope.neptuneMap.setSelectedMarker(marker);
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function (t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            $scope.activeTreatmentBMP = treatmentBMP;

            $scope.$apply();
        }

        //OCs layer we were referencing here died. Removing for now.

        //var url = "https://www.ocgis.com/arcpub/rest/services/Map_Layers/Outfall_Inspections/FeatureServer/0";
        //var outfallsPopup = function (layer) {
        //    return L.Util.template('<p>Facility ID: {FACILITYID}<br>Facility Type: {FACTYPE}',
        //        layer.feature.properties);
        //};
        //var layerName = "Outfalls";
        //$scope.neptuneMap.addEsriReferenceLayer(url, layerName, outfallsPopup);
        
        $scope.$watch(function () {
            var foundIDs = [];
            var map = $scope.neptuneMap.map;
            map.eachLayer(function(layer) {
                if (layer instanceof L.Marker && !(layer instanceof L.MarkerCluster)) {
                    if (map.getBounds().contains(layer.getLatLng())) {
                        foundIDs.push(layer.feature.properties.TreatmentBMPID);
                    }
                }
                if (layer instanceof L.MarkerCluster) {
                    if (map.getBounds().contains(layer.getLatLng())) {
                        var markers = layer.getAllChildMarkers();
                        for (var i = 0; i < markers.length; i++) {
                            foundIDs.push(markers[i].feature.properties.TreatmentBMPID);
                        }
                    }
                }
            });
            // clusters get multicounted, so we need to use this function to pick out the unique IDs only
            $scope.visibleBMPIDs = foundIDs.filter(function(element, index, array) {
                return array.indexOf(element) === index;
            });
        });
        $scope.neptuneMap.map.on('zoomend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('animationend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('moveend', function () { $scope.$apply(); });
        $scope.neptuneMap.map.on('viewreset', function () { $scope.$apply(); });

        $scope.visibleBMPs = function() {
            var filteredBMPs = _.filter($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return $scope.visibleBMPIDs.includes(t.TreatmentBMPID);
                });
            var orderedBMPs = _.sortBy(filteredBMPs,
                function(t) {
                    return (t.DisplayName);
                });
            return orderedBMPs;
        };

        $scope.filterMapByBmpType = function () {
            $scope.neptuneMap.searchableLayerGeoJson = L.geoJson($scope.AngularViewData.MapInitJson.SearchableLayerGeoJson.GeoJsonFeatureCollection,
                {
                    filter: function (feature, layer) {
                        return _.includes($scope.selectedTreatmentBMPTypeIDs, feature.properties.TreatmentBMPTypeID.toString());
                    },
                    pointToLayer: function (feature, latlng) {
                        var icon = L.MakiMarkers.icon({
                            icon: feature.properties.FeatureGlyph,
                            color: feature.properties.FeatureColor,
                            size: "m"
                        });

                        return L.marker(latlng,
                            {
                                icon: icon,
                                title: feature.properties.Name,
                                alt: feature.properties.Name
                            });
                    },
                    style: function (feature) {
                        return {
                            color: feature.properties.FeatureColor = feature.properties.FeatureColor,
                            weight: feature.properties.FeatureWeight = feature.properties.FeatureWeight,
                            fill: feature.properties.FillPolygon = feature.properties.FillPolygon,
                            fillOpacity: feature.properties.FillOpacity = feature.properties.FillOpacity
                        };
                    }
                });
            $scope.neptuneMap.map.removeLayer($scope.neptuneMap.markerClusterGroup);
            $scope.neptuneMap.markerClusterGroup = L.markerClusterGroup({
                maxClusterRadius: 40,
                showCoverageOnHover: false,
                iconCreateFunction: function (cluster) {
                    return new L.DivIcon({
                        html: '<div><span>' + cluster.getChildCount() + '</span></div>',
                        className: 'treatmentBMPCluster',
                        iconSize: new L.Point(40, 40),
                    });
                }
            });
            $scope.neptuneMap.searchableLayerGeoJson.addTo($scope.neptuneMap.markerClusterGroup);
            $scope.neptuneMap.markerClusterGroup.addTo($scope.neptuneMap.map);
            $scope.reinitializeMap();
        }

        // only used when selecting from the list 
        $scope.setActive = function(treatmentBMP) {
            var layer = _.find($scope.neptuneMap.searchableLayerGeoJson._layers,
                function(layer) { return treatmentBMP.TreatmentBMPID === layer.feature.properties.TreatmentBMPID; });
            setActiveImpl(layer, treatmentBMP, false);
        };

        $scope.setActiveByID = function (treatmentBMPID) {
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            var layer = _.find($scope.neptuneMap.searchableLayerGeoJson._layers,
                function (layer) { return treatmentBMPID === layer.feature.properties.TreatmentBMPID; });
            setActiveImpl(layer, treatmentBMP, true);
        };

        function setActiveImpl(layer, treatmentBMP, updateMap) {
            if (updateMap) {
                $scope.neptuneMap.map.panTo(layer.getLatLng());
            }

            // multi-way binding
            jQuery($scope.neptuneMap.typeaheadSelector).typeahead('val', '');
            $scope.neptuneMap.loadSummaryPanel(layer.feature.properties.MapSummaryUrl);
            $scope.neptuneMap.setSelectedMarker(layer);
            $scope.activeTreatmentBMP = treatmentBMP;
        };

        $scope.isActive = function(treatmentBMP) {
            return $scope.activeTreatmentBMP &&
                $scope.activeTreatmentBMP.TreatmentBMPID === treatmentBMP.TreatmentBMPID;
        };

        $scope.reinitializeMap = function() {
            $scope.neptuneMap.searchableLayerGeoJson.on('click',
                function(e) {
                    $scope.setActiveByID(e.layer.feature.properties.TreatmentBMPID);
                    $scope.$apply();
                });
        };

        $scope.visibleBMPCount = function() {
            return $scope.visibleBMPIDs.length;
        };
        
        $scope.zoomMapToCurrentLocation = function() {
            $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
        };

        $scope.reinitializeMap();
    });