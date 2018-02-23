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
angular.module("NeptuneApp").controller("PercentageCollectionMethodController", function ($scope, $timeout, angularModelAndViewData)
{
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.initializeWithEmptyRows = function () {
        var newObservationData = {
            PercentageObservations: []
        }
        for (var i = 0; i < $scope.AngularViewData.PropertiesToObserve.length; i++) {
            newObservationData.PercentageObservations.push({
                PropertyObserved: $scope.AngularViewData.PropertiesToObserve[i].DisplayName,
                ObservationValue: null,
                Notes: null
            });
        }
        $scope.ObservationData = newObservationData;
    };

    $scope.initializeData = function () {
        if (JSON.parse($scope.AngularModel.ObservationData) == null) {
            $scope.initializeWithEmptyRows();
        } else {
            $scope.ObservationData = JSON.parse($scope.AngularModel.ObservationData);
            console.log($scope.ObservationData);
            if ($scope.ObservationData.PercentageObservations == null) {
                $scope.initializeWithEmptyRows();
            }
        }
    };

    $scope.calculateTotalPercent = function () {
        var sum = _.reduce($scope.ObservationData.PercentageObservations, function (sum, n) {
            var toAdd = n.ObservationValue == null ? 0 : n.ObservationValue;
            return sum + toAdd;
        }, 0);
        return Math.round(sum * 100) / 100;
    }

    $scope.percentExceeds100 = function () { return $scope.calculateTotalPercent() > 100 ? true : false; }

    $scope.percentDoesNotEqual100 = function () { return $scope.calculateTotalPercent() !== 100 ? true : false; }

    $scope.submit = function () {
        $scope.AngularModel.ObservationData = JSON.stringify($scope.ObservationData);
    };
});
