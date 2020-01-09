angular.module("NeptuneApp")
    .controller("EditWqmpTreatmentBmpsController", function ($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.AngularViewData = angularModelAndViewData.AngularViewData;

        $scope.removeTreatmentBmp = function(treatmentBmpID) {
            _.remove($scope.AngularModel.TreatmentBmpIDs, function(x) {
                return x === treatmentBmpID;
            });
        };
        $scope.getTreatmentBmpDisplayName = function (treatmentBmpID) {
            var treatmentBmp = _.find($scope.AngularViewData.TreatmentBmps, function(x) {
                return x.TreatmentBMPID === treatmentBmpID;
            });
            return treatmentBmp ? treatmentBmp.DisplayName : "Not found D:";
        };
        $scope.addTreatmentBmp = function() {
            if (!_.includes($scope.AngularModel.TreatmentBmpIDs, $scope.treatmentBmp.TreatmentBMPID)) {
                $scope.AngularModel.TreatmentBmpIDs.push($scope.treatmentBmp.TreatmentBMPID);
            }
        };
        $scope.unselectedTreatmentBmps = function() {
            return _.filter($scope.AngularViewData.TreatmentBmps, function(x) {
                return !_.includes($scope.AngularModel.TreatmentBmpIDs, x.TreatmentBMPID);
            });
        };

        $scope.ifAnyTreatmentBMPSimple = function(treatmentBmpIDs) {
            if (treatmentBmpIDs && treatmentBmpIDs.length > 0) {
                return true;
            }
            return false;
        }

        $scope.addQuickBMPRow = function (quickBMP) {
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

        $scope.orderSourceControlBMPsByAttributeCategory = _.sortBy(_.groupBy($scope.AngularModel.SourceControlBMPSimples, 'SourceControlBMPAttributeCategoryName'), [function (o) { return o.SourceControlBMPAttributeID; }]);

    });
