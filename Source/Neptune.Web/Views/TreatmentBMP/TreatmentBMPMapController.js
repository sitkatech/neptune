angular.module("NeptuneApp")
    .controller("TreatmentBMPMapController", function($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;


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
        neptuneMap.addEsriReferenceLayer(url, layerName, outfallsPopup);
    });