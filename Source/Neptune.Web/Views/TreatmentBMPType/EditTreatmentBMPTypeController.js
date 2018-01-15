angular.module("NeptuneApp").controller("EditTreatmentBMPTypeController", function ($scope, angularModelAndViewData) {
    $scope.resetObservationTypeToAdd = function () { $scope.ObservationTypeToAdd = null; };

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

    $scope.addRow = function () {
        if ($scope.ObservationTypeToAdd == null) {
            return;
        }
        var newTreatmentBMPTypeObservationTypeSimple = $scope.createNewRow($scope.ObservationTypeToAdd.ObservationTypeID);       

        $scope.AngularModel.TreatmentBMPTypeObservationTypeSimples.push(newTreatmentBMPTypeObservationTypeSimple);
        $scope.resetObservationTypeToAdd();
    };

    $scope.createNewRow = function (observationTypeID) {
        var newTreatmentBMPTypeObservationTypeSimple = {
            ObservationTypeID: observationTypeID,
            AssessmentScoreWeight: null,
            DefaultThresholdValue: null,
            DefaultBenchmarkValue: null,
            OverrideAssessmentScoreWeight: null
        };
        return newTreatmentBMPTypeObservationTypeSimple;
    };

    $scope.deleteRow = function (rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples, rowToDelete); };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    if ($scope.AngularModel.TreatmentBMPTypeObservationTypeSimples == null) {
        $scope.AngularModel.TreatmentBMPTypeObservationTypeSimples = [];
    }
    $scope.resetObservationTypeToAdd();
});
