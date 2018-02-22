angular.module("NeptuneApp").controller("EditTreatmentBMPTypeController", function ($scope, angularModelAndViewData) {
    $scope.resetObservationTypeToAdd = function () { $scope.ObservationTypeToAdd = null; };
    $scope.resetTreatmentBMPAttributeTypeToAdd = function () { $scope.TreatmentBMPAttributeTypeToAdd = null; };

    $scope.getAllUsedObservationTypeIDs = function () { return _.map($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, function (p) { return p.ObservationTypeID; }); };

    $scope.filteredObservationTypes = function () {
        var usedObservationTypeIDs = $scope.getAllUsedObservationTypeIDs();
        return _($scope.AngularViewData.ObservationTypes).filter(function (f) { return !_.includes(usedObservationTypeIDs, f.ObservationTypeID); })
            .sortBy(function (fs) {
                return [fs.ObservationTypeName.toLowerCase()];
            }).value();
    };

    $scope.getObservationTypeName = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.ObservationTypeID);
        return observationTypeToFind.ObservationTypeName;
    };

    $scope.getObservationCollectionMethodTypeName = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.ObservationTypeID);
        return observationTypeToFind.CollectionMethodDisplayName;
    };

    $scope.getObservationTypeBenchmarkUnit = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.ObservationTypeID);
        return observationTypeToFind.BenchmarkUnitLegendDisplayName;
    };

    $scope.getObservationTypeThresholdUnit = function (treatmentBMPTypeObservationTypeSimple) {
        var observationTypeToFind = $scope.getObservationType(treatmentBMPTypeObservationTypeSimple.ObservationTypeID);
        return observationTypeToFind.ThresholdUnitLegendDisplayName;
    };

    $scope.getObservationType = function(observationTypeID) {
         return _.find($scope.AngularViewData.ObservationTypes, function (f) { return observationTypeID == f.ObservationTypeID; });
    };

    $scope.observationTypeHasBenchmarkAndThresholds = function(observationTypeID) {
        return $scope.getObservationType(observationTypeID).HasBenchmarkAndThresholds;
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

    $scope.findTreatmentBMPTypeObservationTypeSimpleRow = function (observationTypeID) { return _.find($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, function (pfse) { return pfse.ObservationTypeID == observationTypeID; }); }

    $scope.addObservationTypeRow = function () {
        if ($scope.ObservationTypeToAdd == null) {
            return;
        }
        var newTreatmentBMPTypeObservationTypeSimple = $scope.createNewObservationTypeRow($scope.ObservationTypeToAdd.ObservationTypeID);       

        $scope.AngularModel.TreatmentBMPTypeObservationTypeSimples.push(newTreatmentBMPTypeObservationTypeSimple);
        $scope.resetObservationTypeToAdd();
    };

    $scope.createNewObservationTypeRow = function (observationTypeID) {
        var newTreatmentBMPTypeObservationTypeSimple = {
            ObservationTypeID: observationTypeID,
            AssessmentScoreWeight: null,
            DefaultThresholdValue: null,
            DefaultBenchmarkValue: null,
            OverrideAssessmentScoreWeight: null
        };
        return newTreatmentBMPTypeObservationTypeSimple;
    };

    $scope.deleteObservationTypeRow = function (rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, rowToDelete); };

    $scope.getAllUsedTreatmentBMPAttributeTypeIDs = function () { return _.map($scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples, function (p) { return p.TreatmentBMPAttributeTypeID; }); };

    $scope.filteredTreatmentBMPAttributeTypes = function () {
        var usedTreatmentBMPAttributeTypeIDs = $scope.getAllUsedTreatmentBMPAttributeTypeIDs();
        return _($scope.AngularViewData.TreatmentBMPAttributeTypes).filter(function (f) { return !_.includes(usedTreatmentBMPAttributeTypeIDs, f.TreatmentBMPAttributeTypeID); })
            .sortBy(function (fs) {
                return [fs.TreatmentBMPAttributeTypeName.toLowerCase()];
            }).value();
    };

    $scope.getTreatmentBMPAttributeTypeName = function (treatmentBMPTypeAttributeTypeSimple) {
        var treatmentBMPAttributeTypeToFind = $scope.getTreatmentBMPAttributeType(treatmentBMPTypeAttributeTypeSimple.TreatmentBMPAttributeTypeID);
        return treatmentBMPAttributeTypeToFind.TreatmentBMPAttributeTypeName;
    };

    $scope.getTreatmentBMPAttributeTypeDataTypeName = function (treatmentBMPTypeAttributeTypeSimple) {
        var treatmentBMPAttributeTypeToFind = $scope.getTreatmentBMPAttributeType(treatmentBMPTypeAttributeTypeSimple.TreatmentBMPAttributeTypeID);
        return treatmentBMPAttributeTypeToFind.DataTypeDisplayName;
    };

    $scope.getTreatmentBMPAttributeTypeMeasurementUnitName = function (treatmentBMPTypeAttributeTypeSimple) {
        var treatmentBMPAttributeTypeToFind = $scope.getTreatmentBMPAttributeType(treatmentBMPTypeAttributeTypeSimple.TreatmentBMPAttributeTypeID);
        return treatmentBMPAttributeTypeToFind.MeasurementUnitDisplayName;
    };

    $scope.getTreatmentBMPAttributeTypeIsRequired = function (treatmentBMPTypeAttributeTypeSimple) {
        var treatmentBMPAttributeTypeToFind = $scope.getTreatmentBMPAttributeType(treatmentBMPTypeAttributeTypeSimple.TreatmentBMPAttributeTypeID);
        return treatmentBMPAttributeTypeToFind.IsRequired ? "Yes" : "No";
    };

    $scope.getTreatmentBMPAttributeTypeDescription = function (treatmentBMPTypeAttributeTypeSimple) {
        var treatmentBMPAttributeTypeToFind = $scope.getTreatmentBMPAttributeType(treatmentBMPTypeAttributeTypeSimple.TreatmentBMPAttributeTypeID);
        return treatmentBMPAttributeTypeToFind.Description;
    };

    $scope.getTreatmentBMPAttributeType = function (treatmentBMPAttributeTypeID) {
        return _.find($scope.AngularViewData.TreatmentBMPAttributeTypes, function (f) { return treatmentBMPAttributeTypeID == f.TreatmentBMPAttributeTypeID; });
    };

    $scope.addTreatmentBMPAttributeTypeRow = function () {
        if ($scope.TreatmentBMPAttributeTypeToAdd == null) {
            return;
        }
        var newTreatmentBMPTypeAttributeTypeSimple = $scope.createNewTreatmentBMPAttributeTypeRow($scope.TreatmentBMPAttributeTypeToAdd.TreatmentBMPAttributeTypeID);

        $scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples.push(newTreatmentBMPTypeAttributeTypeSimple);
        $scope.resetTreatmentBMPAttributeTypeToAdd();
    };

    $scope.createNewTreatmentBMPAttributeTypeRow = function (treatmentBmpAttributeTypeID) {
        var newTreatmentBMPTypeAttributeTypeSimple = {
            TreatmentBMPAttributeTypeID: treatmentBmpAttributeTypeID
        };
        return newTreatmentBMPTypeAttributeTypeSimple;
    };

    $scope.deleteTreatmentBMPAttributeTypeRow = function (rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPTypeAttributeTypeSimples, rowToDelete); };

    $scope.isObservationTypeInUse = function (treatmentBMPTypeObservationTypeSimple) {
        return _.includes($scope.AngularViewData.ObservationTypeIDsWithData, treatmentBMPTypeObservationTypeSimple.ObservationTypeID);
    };

    $scope.isTreatmentBMPAttributeTypeInUse = function (treatmentBMPTypeAttributeTypeSimple) {
        return _.includes($scope.AngularViewData.TreatmentBMPAttributeTypeIDsWithData, treatmentBMPTypeAttributeTypeSimple.TreatmentBMPAttributeTypeID);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    if ($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples == null) {
        $scope.AngularModel.TreatmentBMPTypeObservationTypeSimples = [];
    }
    $scope.resetObservationTypeToAdd();
});
