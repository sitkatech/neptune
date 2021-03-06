﻿angular.module("NeptuneApp").factory("DelineationMapService",
    function ($rootScope) {
        var delineationMapServiceInstance = {};

        delineationMapServiceInstance.setDelineation = function (delineationFeature) {
            this.delineationFeature = delineationFeature;
        };

        delineationMapServiceInstance.getDelineation = function () {
            return this.delineationFeature;
        };

        delineationMapServiceInstance.broadcastDelineationMapState = function (delineationMapState, noApply) {
            if (noApply) {
                delineationMapState.noApply = noApply;
            }
            $rootScope.$broadcast("neptune:delineationMapStateChange", delineationMapState);
        };

        delineationMapServiceInstance.resetDelineationMapEditingState = function (noApply) {
            var stateData = {
                isInDelineationMode: false,
                isEditedDelineationPresent: false,

                isInEditLocationMode: false,

                isEditingCentralizedDelineation: false,

                isInThinningMode: false
            };
            if (noApply) {
                stateData.noApply = noApply;
            }

            $rootScope.$broadcast("neptune:delineationMapStateChange", stateData);

        };

        delineationMapServiceInstance.adjustZoom = function(zoomData) {
            $rootScope.$broadcast("neptune:pleaseAdjustZoom", zoomData);
        };
        return delineationMapServiceInstance;
    });

angular.module("NeptuneApp")
    .controller("DelineationMapController", function ($scope, DelineationMapService) {
        console.log("Delineation Map Controller is GO!");

        $scope.delineationMapState = {
            isInDelineationMode: false,
            isEditedDelineationPresent: false,

            isInEditLocationMode: false,

            isEditingCentralizedDelineation: false,

            isInThinningMode: false,

            selectedTreatmentBMPFeature: null
        };

        angular.element(document).ready(function () {
            var delineationMap = new NeptuneMaps.DelineationMap(window.mapInitJson, "Terrain", window.geoserverUrl, window.dmConfig, DelineationMapService);
            window.delineationMap = delineationMap;
            $scope.delineationMap = delineationMap;



            delineationMap.buildSelectedAssetControl();
            if (window.isInitialiTreatmentBMPProvided) {
                delineationMap.preselectTreatmentBMP(window.initialTreatmentBMPID);
            }
        });

        // UI element handlers
        $scope.beginDelineation = function () {
            //Make sure we're cleaned up from any other editing requests
            DelineationMapService.resetDelineationMapEditingState(true);
            $scope.delineationMapState.isInDelineationMode = true;
            $scope.delineationMap.addBeginDelineationControl();
        };

        $scope.deleteDelineation = function () {
            $scope.delineationMap.deleteDelineation(function () { $scope.$apply() });
        };

        $scope.beginEditTreatmentBMPLocation = function () {
            $scope.delineationMapState.isInEditLocationMode = true;
            $scope.delineationMap.launchEditLocationMode();
        };

        $scope.saveLocationEdit = function () {
            $scope.delineationMapState.isInEditLocationMode = false;
            $scope.delineationMap.exitEditLocationMode(true);
        };

        $scope.cancelLocationEdit = function () {
            $scope.delineationMapState.isInEditLocationMode = false;
            $scope.delineationMap.exitEditLocationMode(false);
        };

        $scope.saveDelineation = function () {
            this.wrapUpDelineationImpl(true);
        };

        $scope.cancelDelineation = function () {
            this.wrapUpDelineationImpl(false);
        };

        $scope.wrapUpDelineationImpl = function (doSave) {
            this.delineationMapState.isInDelineationMode = false;
            this.isEditedDelineationPresent = false;
            // handle the centralized case separately since it doesn't involve drawing anymore
            if (this.delineationMapState.isEditingCentralizedDelineation) {
                $scope.delineationMap.exitTraceMode(doSave);
                this.delineationMapState.isEditingCentralizedDelineation = false;
            } else {
                $scope.delineationMap.exitDrawCatchmentMode(doSave);
            }
        };

        $scope.thinDelineation = function () {
            this.delineationMapState.isInThinningMode = true;
            $scope.delineationMap.selectedAssetControl.thinButtonHandler();
        };

        // UI element data-getters
        $scope.treatmentBMPName = function () {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.Name;
            }
        };

        $scope.treatmentBMPType = function () {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.TreatmentBMPType;
            }
        };

        $scope.treatmentBMPID = function () {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.TreatmentBMPID;
            }
        };

        $scope.upstreamBMPID = function () {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.UpstreamBMPID
                    ? this.delineationMapState.selectedTreatmentBMPFeature.properties.UpstreamBMPID
                    : "";
            }
        }

        $scope.upstreamBMPName = function() {
            if (!this.delineationMapState.selectedTreatmentBMPFeature) {
                return "";
            } else {
                return this.delineationMapState.selectedTreatmentBMPFeature.properties.UpstreamBMPName
                    ? this.delineationMapState.selectedTreatmentBMPFeature.properties.UpstreamBMPName
                    : "";
            }
        }

        $scope.delineationType = function () {
            var delineationFeature = DelineationMapService.getDelineation();
            if (delineationFeature) {
                return delineationFeature.properties.DelineationType;
            } else {
                return "No delineation provided";
            }
        };

        $scope.delineationArea = function () {
            var delineationFeature = DelineationMapService.getDelineation();
            if (delineationFeature) {
                return delineationFeature.properties.Area + " ac";
            } else {
                return "-";
            }
        };

        $scope.delineationStatus = function () {
            var delineationFeature = DelineationMapService.getDelineation();
            if (delineationFeature) {
                return delineationFeature.properties.DelineationStatus;
            } else {
                return "";
            }
        };

        $scope.hasDelineation = function () {
            return DelineationMapService.getDelineation();
        };

        // UI element show/hide logic

        $scope.showRequestRevisionButton = function () {
            return DelineationMapService.getDelineation() &&
                this.delineationMapState.selectedTreatmentBMPFeature &&
                this.delineationMapState.selectedTreatmentBMPFeature.properties.DelineationType ===
                DELINEATION_CENTRALIZED;
        };

        $scope.showDeleteButton = function() {
            return this.hasDelineation() &&
                !(this.delineationMapState.isInDelineationMode || this.delineationMapState.isInEditLocationMode);
        };

        $scope.showMoreActions = function() {
            return this.showDeleteButton() ||
                (delineationMapState.isInDelineationMode || delineationMapState.isInEditLocationMode);
        }

        // listeners
        $scope.$on("neptune:delineationMapStateChange",
            function (event, data) {
                
                Object.assign($scope.delineationMapState, data);
                if (!data.noApply) {
                    $scope.$apply();
                }
            });

        $scope.$on("neptune:pleaseAdjustZoom",
            function(event, data) {
                $scope.delineationMap.adjustZoom(data);
            });
    });
