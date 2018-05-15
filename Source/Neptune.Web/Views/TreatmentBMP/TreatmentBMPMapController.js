angular.module("NeptuneApp")
    .controller("TreatmentBMPMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.model = {};
    

        $scope.neptuneMap = new NeptuneMaps.StormwaterSearch($scope.AngularViewData.MapInitJson);

        var selector = '#treatmentBMPFinder';
        var selectorButton = '#treatmentBMPFinderButton';
        var summaryUrl = $scope.AngularViewData.FindTreatmentBMPByNameUrl;

        $scope.neptuneMap.typeaheadSearch(selector, selectorButton, summaryUrl);

        var url = "https://www.ocgis.com/arcpub/rest/services/Map_Layers/Outfall_Inspections/FeatureServer/0";
        var outfallsPopup = function (layer) {
            return L.Util.template('<p>Facility ID: {FACILITYID}<br>Facility Type: {FACTYPE}',
                layer.feature.properties);
        };
        var layerName = "Outfalls";
        $scope.neptuneMap.addEsriReferenceLayer(url, layerName, outfallsPopup);

        $scope.visibleBMPIDs = [];

        // todo: for some reason this bad boy is multicounting whenever we do a zoom. why does it multicount when we do a zoom?
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
            $scope.visibleBMPIDs = foundIDs.filter(function(element, index, array) {
                return array.indexOf(element) === index;
            })
        });

        $scope.whatDoICallThis = function() {
            var filteredBMPs = _.filter($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return $scope.visibleBMPIDs.includes(t.TreatmentBMPID);
                });
            var orderedBMPs = _.sortBy(filteredBMPs,
                function(t) {
                    return !($scope.isActive(t));
                });
            return orderedBMPs;
        }

        $scope.neptuneMap.map.on('zoomend', function() { $scope.$apply(); });
        $scope.neptuneMap.map.on('animationend', function() { $scope.$apply(); });
        $scope.neptuneMap.map.on('moveend', function () { $scope.$apply(); });


        $scope.activeTreatmentBMP = {};
        $scope.setActive = function(treatmentBMP) {
            var layer = _.find($scope.neptuneMap.searchableLayerGeoJson._layers,
                function(layer) { return treatmentBMP.TreatmentBMPID === layer.feature.properties.TreatmentBMPID; });
            setActiveImpl(layer, treatmentBMP);
        };

        $scope.setActiveByID = function (treatmentBMPID) {
            var treatmentBMP = _.find($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return t.TreatmentBMPID == treatmentBMPID;
                });
            $scope.activeTreatmentBMP = treatmentBMP;
        };

        function setActiveImpl(layer, treatmentBMP) {
            // zoom to marker
            var latLngs = [layer.getLatLng()];
            var markerBounds = L.latLngBounds(latLngs);
            $scope.neptuneMap.map.fitBounds(markerBounds);

            // multi-way binding
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
    });