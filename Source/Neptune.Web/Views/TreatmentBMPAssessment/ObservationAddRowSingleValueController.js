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
angular.module("NeptuneApp").controller("ObservationAddRowSingleValueController", function ($scope, $timeout, angularModelAndViewData)
{
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.ObservationDetailTypeToAdd = $scope.AngularViewData.ObservationDetailTypeSimples[0];

    $scope.initializeWithEmptyRow = function()
    {
        if ($scope.AngularModel.TreatmentBMPObservationDetailSimples.length === 0)
        {
            $scope.addTreatmentBMPObservationDetail();
        }
    }

    $scope.addTreatmentBMPObservationDetail = function()
    {
        if ($scope.ObservationDetailTypeToAdd != null)
        {
            var newTreatmentBMPObservationDetailSimple = {
                TreatmentBMPObservationDetailTypeID: $scope.ObservationDetailTypeToAdd.TreatmentBMPObservationDetailTypeID,
                TreatmentBMPObservationValue: "",
                Notes: ""
            };
            $scope.AngularModel.TreatmentBMPObservationDetailSimples.push(newTreatmentBMPObservationDetailSimple);
        }
    };

    $scope.deleteTreatmentBMPObservationDetail = function(observationDetail)
    {            
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentBMPObservationDetailSimples, observationDetail);
    };

    $scope.observationDetailTypes = function()
    {
        return $scope.AngularViewData.ObservationDetailTypeSimples;
    };

    $scope.getTreatmentBMPObservationDetailTypeDisplayName = function(observationDetail)
    {
        var desiredDetailType = _.chain($scope.AngularViewData.ObservationDetailTypeSimples)
            .filter(function(observationDetailTypeSimples)
            {
                return observationDetail.TreatmentBMPObservationDetailTypeID ==
                    observationDetailTypeSimples.TreatmentBMPObservationDetailTypeID;
            })
            .head()
            .value();
        return desiredDetailType.TreatmentBMPObservationDetailTypeDisplayName;
    }

});
