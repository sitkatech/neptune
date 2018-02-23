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
            SingleValueObservations: []
        }
        for (var i = 0; i < $scope.AngularViewData.PropertiesToObserve.length; i++) {
            newObservationData.SingleValueObservations.push({
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
            if ($scope.ObservationData.SingleValueObservations == null) {
                $scope.initializeWithEmptyRows();
            }
        }
    };

    $scope.calculateRemainingPercent = function () {
        var sum = _.reduce($scope.ObservationData.SingleValueObservations, function (sum, n) {
            var toAdd = n.ObservationValue == null ? 0 : n.ObservationValue;
            return sum + toAdd;
        }, 0);
        return Math.round((100 - sum) * 100) / 100;
    }

    $scope.percentIsNegative = function () { return $scope.calculateRemainingPercent() < 0 ? true : false; }

    $scope.submit = function () {
        $scope.AngularModel.ObservationData = JSON.stringify($scope.ObservationData);
    };
});
