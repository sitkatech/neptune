angular.module("NeptuneApp").controller("EditObservationTypeController", function ($scope, $timeout, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;   

    var newDiscreteObservationTypeSchema = {
        MeasurementUnitLabel: null,
        MeasurementUnitTypeID: null,
        PropertiesToObserve: [],
        MinimumNumberOfObservations: null,
        MaximumNumberOfObservations: null,
        MinimumValueOfObservations: null,
        MaximumValueOfObservations: null,
        BenchmarkDescription: null,
        ThresholdDescription: null,
        AssessmentDescription: null
    };
    var newRateObservationTypeSchema = {
        DiscreteRateMeasurementUnitLabel: null,
        DiscreteRateMeasurementUnitTypeID: null,
        TimeMeasurementUnitLabel: null,
        TimeMeasurementUnitTypeID: null,
        ReadingMeasurementUnitLabel: null,
        ReadingMeasurementUnitTypeID: null,
        PropertiesToObserve: [],
        DiscreteRateMinimumNumberOfObservations: null,
        DiscreteRateMaximumNumberOfObservations: null,
        DiscreteRateMinimumValueOfObservations: null,
        DiscreteRateMaximumValueOfObservations: null,
        TimeReadingMinimumNumberOfObservations: null,
        TimeReadingMaximumNumberOfObservations: null,
        TimeReadingMinimumValueOfObservations: null,
        TimeReadingMaximumValueOfObservations: null,
        BenchmarkDescription: null,
        ThresholdDescription: null,
        AssessmentDescription: null
    };
    var newPassFailObservationTypeSchema = {
        PropertiesToObserve: [],
        AssessmentDescription: null
    };
    var newPercentageObservationTypeSchema = {
        MeasurementUnitLabel: null,
        PropertiesToObserve: [],
        BenchmarkDescription: null,
        ThresholdDescription: null,
        AssessmentDescription: null
    };
    

    $scope.getObservationCollectionMethod = function(idToFind) {
        return Sitka.Methods.findElementInJsonArray($scope.AngularViewData.ObservationTypeCollectionMethods,"ID",idToFind);
     }
    
    $scope.updateCollectionMethod = function () {
        var observationCollectionMethod = $scope.getObservationCollectionMethod($scope.ObservationTypeCollectionMethodID);
        $scope.ObservationTypeCollectionMethodSelected = observationCollectionMethod;


        if ($scope.selectedCollectionMethodIsDiscrete()) {
            $scope.ObservationTypeSchema = newDiscreteObservationTypeSchema;
        }
        else if ($scope.selectedCollectionMethodIsRate()) {
            $scope.ObservationTypeSchema = newRateObservationTypeSchema;
        }
        else if ($scope.selectedCollectionMethodIsPassFail()) {
            $scope.ObservationTypeSchema = newPassFailObservationTypeSchema;
        }
        else if ($scope.selectedCollectionMethodIsPercentage()) {
            $scope.ObservationTypeSchema = newPercentageObservationTypeSchema;
        } else {
            $scope.ObservationTypeSchema = {};
        }

        if ($scope.ObservationTypeSchema.PropertiesToObserve.length == 0) {
            $scope.addInput();
        }
        
    }

    $scope.selectedCollectionMethodHasBenchmarkAndThresholds = function() {
        return $scope.ObservationTypeCollectionMethodSelected != null &&
            $scope.ObservationTypeCollectionMethodSelected.HasBenchmarkAndThresholds;
    }

    $scope.selectedCollectionMethodIsDiscrete = function () {
        return $scope.ObservationTypeCollectionMethodSelected != null &&
            $scope.ObservationTypeCollectionMethodSelected.ID === $scope.AngularViewData.DiscreteObservationTypeCollectionMethodID;
    }

    $scope.selectedCollectionMethodIsRate = function () {
        return $scope.ObservationTypeCollectionMethodSelected != null &&
            $scope.ObservationTypeCollectionMethodSelected.ID === $scope.AngularViewData.RateObservationTypeCollectionMethodID;
    }

    $scope.selectedCollectionMethodIsPassFail = function () {
        return $scope.ObservationTypeCollectionMethodSelected != null &&
            $scope.ObservationTypeCollectionMethodSelected.ID === $scope.AngularViewData.PassFailObservationTypeCollectionMethodID;
    }

    $scope.selectedCollectionMethodIsPercentage = function () {
        return $scope.ObservationTypeCollectionMethodSelected != null &&
            $scope.ObservationTypeCollectionMethodSelected.ID === $scope.AngularViewData.PercentageObservationTypeCollectionMethodID;
    }

    $scope.filteredTargetTypes = function() {
        var compatibleTargetTypeIDs = _($scope.AngularViewData.ObservationTypeSpecificationSimples).filter(function (f) {            
            return f.ObservationTypeCollectionMethodID == $scope.ObservationTypeCollectionMethodSelected.ID;
        }).value().map(function(f) {
            return f.ObservationTargetTypeID;
            });

        return _($scope.AngularViewData.ObservationTargetTypes).filter(function (f) {
            return _.includes(compatibleTargetTypeIDs, f.ID);
        }).value();
    }

    $scope.filteredThresholdTypes = function () {
        var compatibleThresholdTypeIDs = _($scope.AngularViewData.ObservationTypeSpecificationSimples).filter(function (f) {
            return f.ObservationTypeCollectionMethodID == $scope.ObservationTypeCollectionMethodSelected.ID;
        }).value().map(function (f) {
            return f.ObservationThresholdTypeID;
        });

        return _($scope.AngularViewData.ObservationThresholdTypes).filter(function (f) {
            return _.includes(compatibleThresholdTypeIDs, f.ID);
        }).value();
    }


    $scope.addInput = function () {
        $scope.ObservationTypeSchema.PropertiesToObserve.push("");
    }

    $scope.removeInput = function (index) {
        $scope.ObservationTypeSchema.PropertiesToObserve.splice(index, 1);
    }

    $scope.submit = function () {
        $scope.AngularModel.ObservationTypeSchema = JSON.stringify($scope.ObservationTypeSchema);
    }

    $scope.ObservationTypeSchema = JSON.parse($scope.AngularModel.ObservationTypeSchema) == undefined ? {} : JSON.parse($scope.AngularModel.ObservationTypeSchema);
    $scope.ObservationTypeCollectionMethodSelected = $scope.AngularModel.ObservationTypeCollectionMethodID != null
        ? Sitka.Methods.findElementInJsonArray($scope.AngularViewData.ObservationTypeCollectionMethods,
            "ID",
            $scope.AngularModel.ObservationTypeCollectionMethodID)
        : null;

});
