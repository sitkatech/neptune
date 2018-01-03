angular.module("NeptuneApp").controller("EditObservationTypeController", function ($scope, $timeout, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.initiateWithAtLeastOneInput = function () {
        if ($scope.AngularModel.PropertiesToObserve == undefined) {
            $scope.AngularModel.PropertiesToObserve = [""];
        }
    }

    $scope.addInput = function () {
        console.log($scope.AngularModel.PropertiesToObserve);
        $scope.AngularModel.PropertiesToObserve.push("");
    }

    $scope.removeInput = function (index) {
        console.log(index);
        $scope.AngularModel.PropertiesToObserve.splice(index, 1);
    }
});
