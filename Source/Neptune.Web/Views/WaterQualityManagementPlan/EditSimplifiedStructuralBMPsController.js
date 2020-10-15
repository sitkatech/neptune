angular.module("NeptuneApp")
    .controller("EditSimplifiedStructuralBMPsController", function ($scope, angularModelAndViewData) {
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

        $scope.createNewQuickBMPRow = function () {
            var newQuickBMP = {
                QuickBMPID: null,
                DisplayName : "",
                QuickBMPTypeName: null,
                QuickBMPNote: "",
                QuickTreatmentBMPTypeID: 0,
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

        $scope.calculateRemainingPercent = function () {
            var sum = _.reduce($scope.AngularModel.QuickBmpSimples,
                function (sum, n) {
                    var toAdd = n.PercentOfSiteTreated == null ? 0 : n.PercentOfSiteTreated;
                    return sum + toAdd;
                },
                0);
            return Math.round((100 - sum) * 100) / 100;
        };
    });
