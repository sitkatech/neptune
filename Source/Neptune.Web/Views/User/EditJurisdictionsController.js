/*-----------------------------------------------------------------------
<copyright file="EditStormwaterUserController.js" company="Tahoe Regional Planning Agency">
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
angular.module("NeptuneApp").controller("EditJurisdictionsController", function ($scope, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.RoleID = $scope.AngularModel.RoleID;

    $scope.filteredStormwaterJurisdictions = function ()
    {
        var usedStormwaterJurisdictionIDs = ($scope.AngularModel.StormwaterJurisdictionPersonSimples).map(function (s) { return s.StormwaterJurisdictionID });
        var filter = _($scope.AngularViewData.StormwaterJurisdictionsCurrentPersonCanManage).filter(function (f) { return !_.includes(usedStormwaterJurisdictionIDs, f.StormwaterJurisdictionID); });
        var orgsFilteredAndSorted = filter.sortBy(["StormwaterJurisdictionDisplayName"]).value();
        return orgsFilteredAndSorted;
    };

    $scope.addRow = function() {
        if ($scope.StormwaterJurisdictionToAdd == null) {
            return;
        }
        var newStormwaterJurisdictionPersonSimple = {
            StormwaterJurisdictionID: $scope.StormwaterJurisdictionToAdd.StormwaterJurisdictionID,
            PersonID: $scope.AngularModel.PersonID,
            CurrentPersonCanRemove: true
        }
        $scope.AngularModel.StormwaterJurisdictionPersonSimples.push(newStormwaterJurisdictionPersonSimple);
        $scope.StormwaterJurisdictionToAdd = null;
    };

    $scope.getStormwaterJurisdictionDisplayName = function (stromwaterJurisdictionOnViewModel)
    {
        var stormwaterJurisdictionID = stromwaterJurisdictionOnViewModel.StormwaterJurisdictionID;
        var stormwaterJurisdiction = _.find($scope.AngularViewData.AllStormwaterJurisdictions, function (x) { return x.StormwaterJurisdictionID == stormwaterJurisdictionID; });
        return stormwaterJurisdiction.StormwaterJurisdictionDisplayName;
    };

    $scope.deleteRow = function (stormwaterJurisdictionSimple) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.StormwaterJurisdictionPersonSimples, stormwaterJurisdictionSimple);
    };

    $scope.canRemoveRow = function (stormwaterJurisdictionPersonSimple) {
        return stormwaterJurisdictionPersonSimple.CurrentPersonCanRemove;
    };

});

