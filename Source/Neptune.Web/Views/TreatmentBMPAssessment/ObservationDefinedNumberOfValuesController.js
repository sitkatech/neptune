/*-----------------------------------------------------------------------
<copyright file="ObservationDefinedNumberOfValuesController.js" company="Tahoe Regional Planning Agency">
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
angular.module("NeptuneApp").controller("ObservationDefinedNumberOfValuesController", function($scope, $timeout, angularModelAndViewData)
{
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.ObservationDetailTypeToAdd = null;
    $scope.RemainingPercent = 100;

    $scope.observationDetailTypes = function()
    {
        return $scope.AngularViewData.ObservationDetailTypeSimples;
    };

    $scope.getTreatmentBMPObservationDetailTypeDisplayName = function(observationDetail)
    {
        var desiredDetailType = _.chain($scope.AngularViewData.ObservationDetailTypeSimples)
            .filter(function(ObservationDetailTypeSimples)
            {
                return observationDetail.TreatmentBMPObservationDetailTypeID ==
                    ObservationDetailTypeSimples.TreatmentBMPObservationDetailTypeID;
            })
            .head()
            .value();
        return desiredDetailType.TreatmentBMPObservationDetailTypeDisplayName;
    }

    $scope.calculateRemainingPercent = function()
    {
        var sum = _.reduce($scope.AngularModel.TreatmentBMPObservationDetailSimples, function (sum, n)
        {
            var toAdd = n.TreatmentBMPObservationValue == null ? 0 : n.TreatmentBMPObservationValue;
            return sum + toAdd;
        }, 0);
        return Math.round((100 - sum) * 100) / 100;
    }

    $scope.percentIsNegative = function() { return $scope.calculateRemainingPercent() < 0 ? true : false; }
});
