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

        $scope.visibleBMPs = [];

        // todo: for some reason this bad boy is multicounting whenever we do a zoom. why does it multicount when we do a zoom?
        $scope.$watch(function () {
            $scope.visibleBMPs = [];
            var map = $scope.neptuneMap.map;
            map.eachLayer(function(layer) {
                if (layer instanceof L.Marker && !(layer instanceof L.MarkerCluster)) {
                    if (map.getBounds().contains(layer.getLatLng())) {
                        $scope.visibleBMPs.push(layer.feature.properties.TreatmentBMPID);
                    }
                }
                if (layer instanceof L.MarkerCluster) {
                    if (map.getBounds().contains(layer.getLatLng())) {
                        var markers = layer.getAllChildMarkers();
                        for (var i = 0; i < markers.length; i++) {
                            $scope.visibleBMPs.push(markers[i].feature.properties.TreatmentBMPID);
                        }
                    }
                }
            });
        });

        $scope.whatDoICallThis = function() {
            return _.filter($scope.AngularViewData.TreatmentBMPs,
                function(t) {
                    return $scope.visibleBMPs.includes(t.TreatmentBMPID);
                });
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

        function setActiveImpl(layer, treatmentBMP) {
            $scope.neptuneMap.setSelectedMarker(layer);
            $scope.activeTreatmentBMP = treatmentBMP;
        }

        $scope.isActive = function(treatmentBMP) {
            return $scope.activeTreatmentBMP &&
                $scope.activeTreatmentBMP.TreatmentBMPID === treatmentBMP.TreatmentBMPID;
        };
    });