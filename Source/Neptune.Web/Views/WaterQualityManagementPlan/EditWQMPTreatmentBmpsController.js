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






        $scope.addQuickBMPRow = function (quickBMP) {
            var newQuickBMP = $scope.createNewQuickBMPRow(quickBMP, 5);
            quickBMP.push(newQuickBMP);
        };

        $scope.createNewQuickBMPRow = function (quickBMP, quickBMPID) {
            //var fundingSource = $scope.getFundingSource(quickBMPID);
            var newQuickBMP = {
                QuickBMPID: quickBMPID,
                DisplayName : "",
                QuickBMPTypeName: null,
                QuickBMPNote : ""
            };
            return newQuickBMP;
        };
    });
