/*-----------------------------------------------------------------------
<copyright file="InfiltrationRateController.js" company="Tahoe Regional Planning Agency">
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
angular.module("NeptuneApp").controller("InfiltrationRateController", function ($scope, $timeout, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.ObservationDetailTypeToAdd = $scope.AngularViewData.ObservationDetailTypeSimples[0];
    $scope.HasReadings= true;

    $scope.addTreatmentBMPObservationDetail = function () {
        if ($scope.ObservationDetailTypeToAdd == null || $scope.HasReadings==null) {
            return;
        }

        var newReadings = null;
        if ($scope.HasReadings)
        {
            newReadings = [
                { ReadingValue: null, ReadingTime: null }, { ReadingValue: null, ReadingTime: null },
                { ReadingValue: null, ReadingTime: null }
            ];
        }
        
        var newTreatmentBMPInfiltrationObservationDetailSimple = {
            TreatmentBMPObservationDetailTypeID: $scope.ObservationDetailTypeToAdd.TreatmentBMPObservationDetailTypeID,
            TreatmentBMPObservationValue: "",
            Notes: "",
            HasReadings: $scope.HasReadings,
            TreatmentBMPInfiltrationReadingSimples: newReadings
        };
        $scope.AngularModel.TreatmentBMPInfiltrationObservationDetailSimples.push(newTreatmentBMPInfiltrationObservationDetailSimple);
    };

    $scope.deleteTreatmentBMPObservationDetail = function (observationDetail)
    {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPInfiltrationObservationDetailSimples, observationDetail);
    };

    $scope.getTreatmentBMPObservationDetailTypeDisplayName = function (observationDetail) {
        var desiredDetailType = _.chain($scope.AngularViewData.ObservationDetailTypeSimples)
            .filter(function (observationDetailTypeSimples) {
                return observationDetail.TreatmentBMPObservationDetailTypeID ==
                    observationDetailTypeSimples.TreatmentBMPObservationDetailTypeID;
            })
            .head()
            .value();
        return desiredDetailType.TreatmentBMPObservationDetailTypeDisplayName;
    }

});
