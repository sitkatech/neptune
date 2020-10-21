angular.module("NeptuneApp")
    .controller("EditSimplifiedStructuralBMPsController", function ($scope, $timeout, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.addQuickBMPRow = function () {
            var newQuickBMP = $scope.createNewQuickBMPRow();
            if ($scope.AngularModel.QuickBmpSimples) {
                $scope.AngularModel.QuickBmpSimples.push(newQuickBMP);
            } else {
                $scope.AngularModel.QuickBmpSimples = [newQuickBMP];
            }
        };

        $scope.getQuickBMPSimples = function () {
            console.log($scope.AngularModel.QuickBmpSimples);
            return $scope.AngularModel.QuickBmpSimples;
        }

        $scope.setAllDryWeatherOverridesToYes = function () {
            $scope.AngularModel.QuickBmpSimples.forEach(x => x.DryWeatherFlowOverrideID = $scope.AngularViewData.DryWeatherFlowOverrideYesID);
        }

        $scope.updateEntireSiteEliminatesDryWeatherFlow = function() {
            $scope.entireSiteEliminatesDryWeatherFlow =
                $scope.AngularModel.QuickBmpSimples.every(x => x.DryWeatherFlowOverrideID ===
                    $scope.dryWeatherFlowOverrideTrue);
        }

        $scope.createNewQuickBMPRow = function () {
            var newQuickBMP = {
                QuickBMPID: null,
                DisplayName : "",
                QuickBMPTypeName: null,
                QuickBMPNote: "",
                QuickTreatmentBMPTypeID: 0,
                DryWeatherFlowOverrideID: $scope.AngularViewData.DryWeatherFlowOverrideDefaultID,
                PercentOfSiteTreated: null,
                PercentCaptured: null,
                PercentRetained: null
            };
            return newQuickBMP;
        };

        $scope.deleteQuickBMPRow = function (quickBmps, rowToDelete) {
            Sitka.Methods.removeFromJsonArray(quickBmps, rowToDelete);
        };

        $scope.ifAnyQuickBMPSimple = function(quickBmpSimples) {
            if (quickBmpSimples && quickBmpSimples.length > 0) {
                return true;
            }
            return false;
        };

        $scope.isTreatmentBMPTypeSelected = function (treatmentBmpType, quickBmp) {
            return treatmentBmpType.TreatmentBMPTypeID === quickBmp.QuickTreatmentBMPTypeID;
        };

        $scope.isDryWeatherFlowOverrideSelected = function (dryWeatherFlowOverride, quickBmp) {
            return dryWeatherFlowOverride.DryWeatherFlowOverrideID === quickBmp.DryWeatherFlowOverrideID;
        };

        $scope.updateDryWeatherFlowOverrideIDForQuickBmp = function(index, quickBmp) {
            console.log($scope.AngularModel.QuickBmpSimples[index]);
            console.log(quickBmp);
            $scope.AngularModel.QuickBmpSimples[index].DryWeatherFlowOverrideID = quickBmp.DryWeatherFlowOverrideID;
        }

        $scope.calculateRemainingPercent = function () {
            var sum = _.reduce($scope.AngularModel.QuickBmpSimples,
                function (sum, n) {
                    var toAdd = n.PercentOfSiteTreated == null ? 0 : n.PercentOfSiteTreated;
                    return sum + toAdd;
                },
                0);
            return Math.round((100 - sum) * 100) / 100;
        };

        //In order for the 'Select All' function for DryWeatherFlowOverrideID to work,
        //the select list populates with all  the values as 'number:{ID}' which obviously
        //won't parse well to an integer in the model. So, for now we can just replace 
        //that part of the string with nothing and the number will parse correctly.
        $scope.prepareOptionsForParsing = function () {
            $("option[value*='number:']").each(function(i, obj) {
                $(obj).attr("value", obj.value.replace("number:", ""));
            });
        }
    });
