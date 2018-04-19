angular.module("NeptuneApp").controller("EditTreatmentBMPAttributeTypeController", function ($scope, $timeout, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.getTreatmentBMPAttributeDataType = function (idToFind) {
        return Sitka.Methods.findElementInJsonArray($scope.AngularViewData.TreatmentBMPAttributeDataTypes, "ID", idToFind);
    }

    $scope.updateTreatmentBMPAttributeDataType = function () {
        var treatmentBMPAttributeDataType = $scope.getTreatmentBMPAttributeDataType($scope.TreatmentBMPAttributeDataTypeID);
        $scope.TreatmentBMPAttributeDataTypeSelected = treatmentBMPAttributeDataType;

        if ($scope.OptionsSchema.length == 0) {
            $scope.addInput();
        }
    }

    $scope.selectedTreatmentBMPAttributeDataTypeHasMeasurementUnit = function () {
        return $scope.TreatmentBMPAttributeDataTypeSelected != null &&
            $scope.TreatmentBMPAttributeDataTypeSelected.HasMeasurementUnit;
    }

    $scope.selectedTreatmentBMPAttributeDataTypeHasOptions = function () {
        return $scope.TreatmentBMPAttributeDataTypeSelected != null &&
            $scope.TreatmentBMPAttributeDataTypeSelected.HasOptions;
    }

    $scope.addInput = function () {
        $scope.OptionsSchema.push("");
    }

    $scope.removeInput = function (index) {
        $scope.OptionsSchema.splice(index, 1);
    }

    $scope.submit = function () {
        $scope.AngularModel.TreatmentBMPAttributeTypeOptionsSchema = JSON.stringify($scope.OptionsSchema);
    }

    $scope.OptionsSchema = JSON.parse($scope.AngularModel.TreatmentBMPAttributeTypeOptionsSchema) == undefined ? [] : JSON.parse($scope.AngularModel.TreatmentBMPAttributeTypeOptionsSchema);   
    $scope.TreatmentBMPAttributeDataTypeSelected = $scope.AngularModel.TreatmentBMPAttributeDataTypeID != null
        ? Sitka.Methods.findElementInJsonArray($scope.AngularViewData.TreatmentBMPAttributeDataTypes,
            "ID",
            $scope.AngularModel.TreatmentBMPAttributeDataTypeID)
        : null;
});
