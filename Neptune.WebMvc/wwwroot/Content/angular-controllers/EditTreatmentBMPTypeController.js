angular.module("NeptuneApp").controller("EditTreatmentBMPTypeController", function ($scope, angularModelAndViewData) {
    $scope.resetObservationTypeToAdd = function () { $scope.ObservationTypeToAdd = null; };
    $scope.resetCustomAttributeTypeToAdd = function () { $scope.CustomAttributeTypeToAdd = null; };

    $scope.getAllUsedObservationTypeIDs = function () { return _.map($scope.AngularModel.TreatmentBMPTypeObservationTypes, function (p) { return p.TreatmentBMPAssessmentObservationTypeID; }); };

    $scope.filteredObservationTypes = function () {
        var usedObservationTypeIDs = $scope.getAllUsedObservationTypeIDs();
        return _($scope.AngularViewData.TreatmentBMPAssessmentObservationTypes).filter(function (f) { return !_.includes(usedObservationTypeIDs, f.TreatmentBMPAssessmentObservationTypeID); })
            .sortBy(function (fs) {
                return [fs.TreatmentBMPAssessmentObservationTypeName.toLowerCase()];
            }).value();
    };

    $scope.getObservationTypeName = function (treatmentBMPTypeObservationType) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationType.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.TreatmentBMPAssessmentObservationTypeName;
    };

    $scope.getObservationTypeSortOrder = function (treatmentBMPTypeObservationType) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationType.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.TreatmentBMPAssessmentObservationTypeSortOrder;
    };

    $scope.getObservationCollectionMethodTypeName = function (treatmentBMPTypeObservationType) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationType.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.CollectionMethodDisplayName;
    };

    $scope.getObservationTypeBenchmarkUnit = function (treatmentBMPTypeObservationType) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationType.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.BenchmarkUnitLegendDisplayName;
    };

    $scope.getObservationTypeThresholdUnit = function (treatmentBMPTypeObservationType) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationType.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.ThresholdUnitLegendDisplayName;
    };

    $scope.getObservationType = function(observationTypeID) {
         return _.find($scope.AngularViewData.TreatmentBMPAssessmentObservationTypes, function (f) { return observationTypeID == f.TreatmentBMPAssessmentObservationTypeID; });
    };

    $scope.observationTypeHasBenchmarkAndThresholds = function(observationTypeID) {
        return $scope.getObservationType(observationTypeID).HasBenchmarkAndThresholds;
    }

    $scope.observationTypeTargetIsSweetSpot = function (observationTypeID) {
        return $scope.getObservationType(observationTypeID).TargetIsSweetSpot;
    }

    $scope.checkOverrideIfFailing = function (treatmentBMPTypeObservationType) {
        treatmentBMPTypeObservationType.AssessmentScoreWeight = null;
        getWeightTotal();
    }

    $scope.getWeightTotal = function () {
        return _.reduce($scope.AngularModel.TreatmentBMPTypeObservationTypes, function(m, x) {
             return m + x.AssessmentScoreWeight;
        }, 0);
    };

    $scope.findTreatmentBMPTypeObservationTypeRow = function (observationTypeID) { return _.find($scope.AngularModel.TreatmentBMPTypeObservationTypes, function (pfse) { return pfse.TreatmentBMPAssessmentObservationTypeID == observationTypeID; }); }

    $scope.addObservationTypeRow = function () {
        if ($scope.ObservationTypeToAdd == null) {
            return;
        }
        var newTreatmentBMPTypeObservationType = $scope.createNewObservationTypeRow($scope.ObservationTypeToAdd.TreatmentBMPAssessmentObservationTypeID);       

        $scope.AngularModel.TreatmentBMPTypeObservationTypes.push(newTreatmentBMPTypeObservationType);
        $scope.resetObservationTypeToAdd();
    };

    $scope.createNewObservationTypeRow = function (observationTypeID) {
        var newTreatmentBMPTypeObservationType = {
            TreatmentBMPAssessmentObservationTypeID: observationTypeID,
            AssessmentScoreWeight: null,
            DefaultThresholdValue: null,
            DefaultBenchmarkValue: null,
            OverrideAssessmentScoreWeight: null
        };
        return newTreatmentBMPTypeObservationType;
    };

    $scope.deleteObservationTypeRow = function (rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPTypeObservationTypes, rowToDelete); };

    $scope.getAllUsedCustomAttributeTypeIDs = function () { return _.map($scope.AngularModel.TreatmentBMPTypeAttributeTypes, function (p) { return p.CustomAttributeTypeID; }); };

    $scope.filteredCustomAttributeTypes = function () {
        var usedCustomAttributeTypeIDs = $scope.getAllUsedCustomAttributeTypeIDs();
        return _($scope.AngularViewData.CustomAttributeTypes).filter(function (f) { return !_.includes(usedCustomAttributeTypeIDs, f.CustomAttributeTypeID); })
            .sortBy(function (fs) {
                return [fs.CustomAttributeTypeName.toLowerCase()];
            }).value();
    };

    $scope.getCustomAttributeTypeName = function (treatmentBMPTypeAttributeType) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeType.CustomAttributeTypeID);
        return customAttributeTypeToFind.CustomAttributeTypeName;
    };

    $scope.getCustomAttributeTypeSortOrder = function (treatmentBMPTypeAttributeType) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeType.CustomAttributeTypeID);
        return customAttributeTypeToFind.CustomAttributeTypeSortOrder;
    };

    $scope.getCustomAttributeTypePurpose = function (treatmentBMPTypeAttributeType) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeType.CustomAttributeTypeID);
        return customAttributeTypeToFind.Purpose;
    };

    $scope.getCustomAttributeTypeDataTypeName = function (treatmentBMPTypeAttributeType) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeType.CustomAttributeTypeID);
        return customAttributeTypeToFind.DataTypeDisplayName;
    };

    $scope.getCustomAttributeTypeMeasurementUnitName = function (treatmentBMPTypeAttributeType) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeType.CustomAttributeTypeID);
        return customAttributeTypeToFind.MeasurementUnitDisplayName;
    };

    $scope.getCustomAttributeTypeIsRequired = function (treatmentBMPTypeAttributeType) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeType.CustomAttributeTypeID);
        return customAttributeTypeToFind.IsRequired ? "Yes" : "No";
    };

    $scope.getCustomAttributeTypeDescription = function (treatmentBMPTypeAttributeType) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeType.CustomAttributeTypeID);
        return customAttributeTypeToFind.CustomAttributeTypeDescription;
    };

    $scope.getCustomAttributeType = function (customAttributeTypeID) {
        return _.find($scope.AngularViewData.CustomAttributeTypes, function (f) { return customAttributeTypeID == f.CustomAttributeTypeID; });
    };

    $scope.addCustomAttributeTypeRow = function () {
        if ($scope.CustomAttributeTypeToAdd == null) {
            return;
        }
        var newTreatmentBMPTypeAttributeType = $scope.createNewCustomAttributeTypeRow($scope.CustomAttributeTypeToAdd.CustomAttributeTypeID);
        
        $scope.AngularModel.TreatmentBMPTypeAttributeTypes.push(newTreatmentBMPTypeAttributeType);
        $scope.resetCustomAttributeTypeToAdd();
    };

    $scope.createNewCustomAttributeTypeRow = function (treatmentBmpAttributeTypeID) {
        var newTreatmentBMPTypeAttributeType = {
            CustomAttributeTypeID: treatmentBmpAttributeTypeID
        };
        return newTreatmentBMPTypeAttributeType;
    };

    $scope.deleteCustomAttributeTypeRow = function (rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPTypeAttributeTypes, rowToDelete); };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    if ($scope.AngularModel.TreatmentBMPTypeObservationTypes == null) {
        $scope.AngularModel.TreatmentBMPTypeObservationTypes = [];
    }
    if ($scope.AngularModel.TreatmentBMPTypeAttributeTypes == null) {
        $scope.AngularModel.TreatmentBMPTypeAttributeTypes = [];
    }
    $scope.resetObservationTypeToAdd();
});
