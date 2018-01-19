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
angular.module("NeptuneApp").controller("PassFailCollectionMethodController", function ($scope, $timeout, angularModelAndViewData)
{
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.initializeWithEmptyRows = function () {
        if (JSON.parse($scope.AngularModel.ObservationData) == null)
        {
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
        }
        else
        {
            $scope.ObservationData = JSON.parse($scope.AngularModel.ObservationData);
        }
    }

    $scope.submit = function () {
        $scope.AngularModel.ObservationData = JSON.stringify($scope.ObservationData);
    }

    
});
