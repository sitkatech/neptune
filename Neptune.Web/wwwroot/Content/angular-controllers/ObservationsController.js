/*-----------------------------------------------------------------------
<copyright file="ObservationAddRowSingleValueController.js" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
angular.module("NeptuneApp").controller("ObservationsController", function ($scope, $timeout, angularModelAndViewData)
{
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.initializeObservationTypeWithEmptyRows = function(currentObservationTypeSchema) {
        var newObservationData = {
            TreatmentBMPAssessmentObservationTypeID: currentObservationTypeSchema
                .TreatmentBMPAssessmentObservationTypeID,
            ObservationData: {
                SingleValueObservations: []
            }
        };

        // depends on the type of observation
        if (currentObservationTypeSchema.ObservationTypeCollectionMethod === 'DiscreteValue') {
            var defaultPropertyObserved = currentObservationTypeSchema.PropertiesToObserve.length === 1
                ? currentObservationTypeSchema.PropertiesToObserve[0].DisplayName
                : null;
            while (newObservationData.ObservationData.SingleValueObservations.length <
                currentObservationTypeSchema.MinimumNumberOfObservations) {
                newObservationData.ObservationData.SingleValueObservations.push({
                    PropertyObserved: defaultPropertyObserved,
                    ObservationValue: null,
                    Notes: null
                });
            }
        } else {
            for (var i = 0; i < currentObservationTypeSchema.PropertiesToObserve.length; i++) {
                newObservationData.ObservationData.SingleValueObservations.push({
                    PropertyObserved: currentObservationTypeSchema.PropertiesToObserve[i].DisplayName,
                    ObservationValue: null,
                    Notes: null
                });
            }
        }
        return newObservationData;
    };

    $scope.initializeData = function (existingObservations) {
        var existingObservationData = [];
        if (existingObservations.length > 0) {
            existingObservationData = existingObservations;
        }
        var observationData = [];
        for (var j = 0; j < $scope.AngularViewData.ObservationTypeSchemas.length; j++) {
            var currentObservationTypeSchema = $scope.AngularViewData.ObservationTypeSchemas[j];
            var currentObservation = _.find(existingObservationData, function (f) {
                return f.TreatmentBMPAssessmentObservationTypeID === currentObservationTypeSchema.TreatmentBMPAssessmentObservationTypeID;
            });
            if (currentObservation != null) {
                currentObservation.ObservationData = JSON.parse(currentObservation.ObservationData);
            }
            else
            {
                currentObservation = $scope.initializeObservationTypeWithEmptyRows(currentObservationTypeSchema);
            }
            observationData.push(currentObservation);
        }
        $scope.ObservationData = observationData;
    };

    $scope.getObservationData = function (observationTypeSchema) {
        var find = _.find($scope.ObservationData,
            function(f) {
                return f.TreatmentBMPAssessmentObservationTypeID == observationTypeSchema.TreatmentBMPAssessmentObservationTypeID;
            });
        return find.ObservationData;
    };

    $scope.jsonify = function(observation) {
        return angular.toJson(observation.ObservationData);
    };


    $scope.preloadWithInitialAssessmentData = function () {
        $scope.initializeData(_.cloneDeep($scope.InitialAssessmentObservations));
        $scope.showModal = {
            "display": "none"
        };
        $scope.showModalBackdrop = {
            "display": "none"
        };
    }




    $scope.openCopyDataFromInitialAssessmentModal = function (initialAssessmentObservations) {
        $scope.InitialAssessmentObservations = initialAssessmentObservations;
        $scope.modalContent = "This will overwrite any existing data on the Post-Maintenance Assessment for " + initialAssessmentObservations.length + " observations.";
        $scope.showModal = {
            "display": "block"
        };
        $scope.showModalBackdrop = {
            "display": "block"
        };
    }
    

    $scope.closeCopyDataFromInitialAssessmentModal = function () {
        $scope.showModal = {
            "display": "none"
        };
        $scope.showModalBackdrop = {
            "display": "none"
        };
    }

});


