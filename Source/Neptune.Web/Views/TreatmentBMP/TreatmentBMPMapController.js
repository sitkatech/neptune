﻿angular.module("NeptuneApp")
    .controller("TreatmentBMPMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;
        $scope.neptuneMap = new NeptuneMaps.StormwaterSearch($scope.AngularViewData.MapInitJson);

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

        $scope.neptuneMap.searchableLayerGeoJson.on('click', function (e) {
            $scope.setActiveByID(e.layer.feature.properties.TreatmentBMPID);
            $scope.$apply();
        });

        $scope.visibleBMPCount = function() {
            return $scope.visibleBMPIDs.length;
        };
        
        $scope.zoomMapToCurrentLocation = function() {
            $scope.neptuneMap.map.locate({ setView: true, maxZoom: 15 });
        };
    });