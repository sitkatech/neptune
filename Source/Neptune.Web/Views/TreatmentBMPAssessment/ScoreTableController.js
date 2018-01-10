/*-----------------------------------------------------------------------
<copyright file="ScoreTableController.js" company="Tahoe Regional Planning Agency">
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
angular.module("NeptuneApp").controller("ScoreTableController", function ($scope, $timeout, angularModelAndViewData)
{
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.overrideScore = function()
    {
        var overridingObservationTypes = $scope.AngularViewData.ObservationTypeSimples.filter(function(observationTypeSimple) {
            console.log(observationTypeSimple);
            return observationTypeSimple.TreatmentBMPObservationSimple == null ? null : observationTypeSimple.TreatmentBMPObservationSimple.OverrideScore;
        });
        return overridingObservationTypes.length !== 0;
    }

    $scope.getHue = function (value) {
        if (value <= 2)
        {
            return "hsl(0,100%,59%";
        }
        var hue = (((value - 2) / 3) * 120);
        return "hsl(" + hue + ",100%,59%)}";
    }

}).filter('setDecimal', function ($filter) {
    return function (input, places) {
        if (isNaN(input)) return input;
        // If we want 1 decimal place, we want to mult/div by 10
        // If we want 2 decimal places, we want to mult/div by 100, etc
        // So use the following to create that factor
        var factor = "1" + Array(+(places > 0 && places + 1)).join("0");
        return Math.round(input * factor) / factor;
    };
});;
