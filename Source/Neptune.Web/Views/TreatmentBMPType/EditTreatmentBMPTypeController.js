angular.module("NeptuneApp").controller("EditTreatmentBMPTypeController", function ($scope, angularModelAndViewData) {
    $scope.resetObservationTypeToAdd = function () { $scope.ObservationTypeToAdd = null; };
    $scope.resetCustomAttributeTypeToAdd = function () { $scope.CustomAttributeTypeToAdd = null; };

    $scope.getAllUsedObservationTypeIDs = function () { return _.map($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, function (p) { return p.TreatmentBMPAssessmentObservationTypeID; }); };

    $scope.filteredObservationTypes = function () {
        var usedObservationTypeIDs = $scope.getAllUsedObservationTypeIDs();
        return _($scope.AngularViewData.TreatmentBMPAssessmentObservationTypes).filter(function (f) { return !_.includes(usedObservationTypeIDs, f.TreatmentBMPAssessmentObservationTypeID); })
            .sortBy(function (fs) {
                return [fs.TreatmentBMPAssessmentObservationTypeName.toLowerCase()];
            }).value();
    };

    $scope.getObservationTypeName = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.TreatmentBMPAssessmentObservationTypeName;
    };

    $scope.getObservationCollectionMethodTypeName = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.CollectionMethodDisplayName;
    };

    $scope.getObservationTypeBenchmarkUnit = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.TreatmentBMPAssessmentObservationTypeID);
        return observationTypeToFind.BenchmarkUnitLegendDisplayName;
    };

    $scope.getObservationTypeThresholdUnit = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.TreatmentBMPAssessmentObservationTypeID);
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

    $scope.checkOverrideIfFailing = function (treatmentBMPTypeObservationTypeSimple) {
        treatmentBMPTypeObservationTypeSimple.AssessmentScoreWeight = null;
        getWeightTotal();
    }

    $scope.getWeightTotal = function () {
        return _.reduce($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, function(m, x) {
             return m + x.AssessmentScoreWeight;
        }, 0);
    };

    $scope.findTreatmentBMPTypeObservationTypeSimpleRow = function (observationTypeID) { return _.find($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, function (pfse) { return pfse.TreatmentBMPAssessmentObservationTypeID == observationTypeID; }); }

    $scope.addObservationTypeRow = function () {
        if ($scope.ObservationTypeToAdd == null) {
            return;
        }
        var newTreatmentBMPTypeObservationTypeSimple = $scope.createNewObservationTypeRow($scope.ObservationTypeToAdd.TreatmentBMPAssessmentObservationTypeID);       

        $scope.AngularModel.TreatmentBMPTypeObservationTypeSimples.push(newTreatmentBMPTypeObservationTypeSimple);
        $scope.resetObservationTypeToAdd();
    };

    $scope.createNewObservationTypeRow = function (observationTypeID) {
        var newTreatmentBMPTypeObservationTypeSimple = {
            TreatmentBMPAssessmentObservationTypeID: observationTypeID,
            AssessmentScoreWeight: null,
            DefaultThresholdValue: null,
            DefaultBenchmarkValue: null,
            OverrideAssessmentScoreWeight: null
        };
        return newTreatmentBMPTypeObservationTypeSimple;
    };

    $scope.deleteObservationTypeRow = function (rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, rowToDelete); };

    $scope.getAllUsedCustomAttributeTypeIDs = function () { return _.map($scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples, function (p) { return p.CustomAttributeTypeID; }); };

    $scope.filteredCustomAttributeTypes = function () {
        var usedCustomAttributeTypeIDs = $scope.getAllUsedCustomAttributeTypeIDs();
        return _($scope.AngularViewData.CustomAttributeTypes).filter(function (f) { return !_.includes(usedCustomAttributeTypeIDs, f.CustomAttributeTypeID); })
            .sortBy(function (fs) {
                return [fs.CustomAttributeTypeName.toLowerCase()];
            }).value();
    };

    $scope.getCustomAttributeTypeName = function (treatmentBMPTypeAttributeTypeSimple) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeTypeSimple.CustomAttributeTypeID);
        return customAttributeTypeToFind.CustomAttributeTypeName;
    };

    $scope.getCustomAttributeTypePurpose = function (treatmentBMPTypeAttributeTypeSimple) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeTypeSimple.CustomAttributeTypeID);
        return customAttributeTypeToFind.Purpose;
    };

    $scope.getCustomAttributeTypeDataTypeName = function (treatmentBMPTypeAttributeTypeSimple) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeTypeSimple.CustomAttributeTypeID);
        return customAttributeTypeToFind.DataTypeDisplayName;
    };

    $scope.getCustomAttributeTypeMeasurementUnitName = function (treatmentBMPTypeAttributeTypeSimple) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeTypeSimple.CustomAttributeTypeID);
        return customAttributeTypeToFind.MeasurementUnitDisplayName;
    };

    $scope.getCustomAttributeTypeIsRequired = function (treatmentBMPTypeAttributeTypeSimple) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeTypeSimple.CustomAttributeTypeID);
        return customAttributeTypeToFind.IsRequired ? "Yes" : "No";
    };

    $scope.getCustomAttributeTypeDescription = function (treatmentBMPTypeAttributeTypeSimple) {
        var customAttributeTypeToFind = $scope.getCustomAttributeType(treatmentBMPTypeAttributeTypeSimple.CustomAttributeTypeID);
        return customAttributeTypeToFind.Description;
    };

    $scope.getCustomAttributeType = function (customAttributeTypeID) {
        return _.find($scope.AngularViewData.CustomAttributeTypes, function (f) { return customAttributeTypeID == f.CustomAttributeTypeID; });
    };

    $scope.addCustomAttributeTypeRow = function () {
        if ($scope.CustomAttributeTypeToAdd == null) {
            return;
        }
        var newTreatmentBMPTypeAttributeTypeSimple = $scope.createNewCustomAttributeTypeRow($scope.CustomAttributeTypeToAdd.CustomAttributeTypeID);
        
        $scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples.push(newTreatmentBMPTypeAttributeTypeSimple);
        $scope.resetCustomAttributeTypeToAdd();
    };

    $scope.createNewCustomAttributeTypeRow = function (treatmentBmpAttributeTypeID) {
        var newTreatmentBMPTypeAttributeTypeSimple = {
            CustomAttributeTypeID: treatmentBmpAttributeTypeID
        };
        return newTreatmentBMPTypeAttributeTypeSimple;
    };

    $scope.deleteCustomAttributeTypeRow = function (rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples, rowToDelete); };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    if ($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples == null) {
        $scope.AngularModel.TreatmentBMPTypeObservationTypeSimples = [];
    }
    if ($scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples == null) {
        $scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples = [];
    }
    $scope.resetObservationTypeToAdd();
});
