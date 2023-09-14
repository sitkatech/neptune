﻿angular.module("NeptuneApp").controller("EditCustomAttributeTypeController", function ($scope, $timeout, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.getCustomAttributeDataType = function (idToFind) {
        return Sitka.Methods.findElementInJsonArray($scope.AngularViewData.CustomAttributeDataTypes, "CustomAttributeDataTypeID", idToFind);
    }

    $scope.updateCustomAttributeDataType = function () {
        if ($scope.OptionsSchema.length == 0) {
            $scope.addInput();
        }
    }

    $scope.selectedCustomAttributeDataTypeHasMeasurementUnit = function () {
        return $scope.CustomAttributeDataTypeSelected != null &&
            $scope.CustomAttributeDataTypeSelected.HasMeasurementUnit;
    }

    $scope.selectedCustomAttributeDataTypeHasOptions = function () {
        return $scope.CustomAttributeDataTypeSelected != null &&
            $scope.CustomAttributeDataTypeSelected.HasOptions;
    }

    $scope.addInput = function () {
        $scope.OptionsSchema.push("");
    }

    $scope.removeInput = function (index) {
        $scope.OptionsSchema.splice(index, 1);
    }

    $scope.submit = function () {
        $scope.AngularModel.CustomAttributeTypeOptionsSchema = JSON.stringify($scope.OptionsSchema);
    }

    $scope.OptionsSchema = JSON.parse($scope.AngularModel.CustomAttributeTypeOptionsSchema) == undefined ? [] : JSON.parse($scope.AngularModel.CustomAttributeTypeOptionsSchema);   
    $scope.CustomAttributeDataTypeSelected = $scope.AngularModel.CustomAttributeDataTypeID != null
        ? Sitka.Methods.findElementInJsonArray($scope.AngularViewData.CustomAttributeDataTypes, "CustomAttributeDataTypeID", $scope.AngularModel.CustomAttributeDataTypeID)
        : null;
});
