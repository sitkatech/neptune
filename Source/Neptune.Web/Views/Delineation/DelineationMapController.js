angular.module("NeptuneApp")
    .controller("DelineationMapController", function($scope) {
        console.log("Delineation Map Controller is GO!");

        $scope.delineationMapState = {
            isInDelineationMode: false,
            isInEditLocationMode: false,
            selectedTreatmentBMPFeature: null
        };

        angular.element(document).ready(function () {
            var delineationMap = new NeptuneMaps.DelineationMap(mapInitJson, "Terrain", geoserverUrl, config);
            window.delineationMap = delineationMap;
            $scope.delineationMap = delineationMap;

            delineationMap.buildSelectedAssetControl();
            delineationMap.hookupSelectTreatmentBMPOnClick($scope);
            delineationMap.hookupDeselectOnClick($scope);
            delineationMap.hookupSelectTreatmentBMPByDelineation($scope);
        });

        // UI element handlers
        $scope.beginDelineation = function ($event) {
            $scope.delineationMapState.isInDelineationMode = true;
            $scope.delineationMap.addBeginDelineationControl();
        };

        $scope.beginEditTreatmentBMPLocation = function ($event) {
            $scope.delineationMapState.isInEditLocationMode = true;
            $scope.delineationMap.selectedAssetControl.launchEditLocationMode();
            $scope.delineationMap.launchEditLocationMode();
            //    e.stopPropagation();
        };

        // UI element data-getters
        $scope.treatmentBMPName = function() {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.Name;
            }
        };

        $scope.treatmentBMPType = function() {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.TreatmentBMPType;
            }
        };

        $scope.treatmentBMPID = function() {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.TreatmentBMPID;
            }


        };
    });