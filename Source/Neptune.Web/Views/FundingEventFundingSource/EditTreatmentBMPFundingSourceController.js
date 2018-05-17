/*-----------------------------------------------------------------------
<copyright file="FundingEventFundingSourceController.js" company="Tahoe Regional Planning Agency">
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
angular.module("NeptuneApp").controller("EditFundingEventFundingSourceController", function ($scope, angularModelAndViewData)
{
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

    $scope.resetFundingSourceIDToAdd = function () { $scope.FundingSourceIDToAdd = ($scope.FromFundingSource) ? $scope.getFundingSource(angularModelAndViewData.AngularViewData.FundingSourceID).FundingSourceID : null; };

    $scope.resetTreatmentBMPIDToAdd = function () { $scope.TreatmentBMPIDToAdd = ($scope.FromTreatmentBMP) ? $scope.getTreatmentBMP(angularModelAndViewData.AngularViewData.TreatmentBMPID).TreatmentBMPID : null; };

    $scope.getAllUsedFundingSourceIds = function () {
        return _.map($scope.AngularModel.FundingEventFundingSources, function (p) { return p.FundingSourceID; });
    };

    $scope.filteredFundingSources = function () {
        var usedFundingSourceIDs = $scope.getAllUsedFundingSourceIds();
        return _($scope.AngularViewData.AllFundingSources).filter(function (f) {
            return f.IsActive && !_.contains(usedFundingSourceIDs, f.FundingSourceID);
        }).sortBy(function (fs) {
            return [fs.FundingSourceName.toLowerCase()];
        }).value();
    };

    $scope.getAllUsedTreatmentBMPIDs = function () {
        return _.map($scope.AngularModel.FundingEventFundingSources, function (p) { return p.TreatmentBMPID; });
    };

    $scope.filteredTreatmentBMPs = function () {
        var usedTreatmentBMPIDs = $scope.getAllUsedTreatmentBMPIDs();
        return _($scope.AngularViewData.AllTreatmentBMPs).filter(function (f) { return !_.includes(usedTreatmentBMPIDs, f.TreatmentBMPID); })
            .sortBy(["DisplayName"]).value();
    };

    $scope.getTreatmentBMPName = function (fundingEventFundingSource)
    {
        var treatmentBMPToFind = $scope.getTreatmentBMP(fundingEventFundingSource.TreatmentBMPID);
        return treatmentBMPToFind.DisplayName;
    };

    $scope.getTreatmentBMP = function (treatmentBMPID) {
        return _.find($scope.AngularViewData.AllTreatmentBMPs, function (f) { return treatmentBMPID == f.TreatmentBMPID; });
    };

    $scope.getFundingSourceName = function (fundingEventFundingSource) {
        var fundingSourceToFind = $scope.getFundingSource(fundingEventFundingSource.FundingSourceID);
        return fundingSourceToFind.DisplayName;
    };

    $scope.getFundingSource = function (fundingSourceID) {
        return _.find($scope.AngularViewData.AllFundingSources, function (f) { return fundingSourceID == f.FundingSourceID; });
    };

    $scope.getTotal = function ()
    {
        return _.reduce($scope.AngularModel.FundingEventFundingSources, function (m, x) { return m + x.Amount; }, 0);
    };
    
    $scope.findFundingEventFundingSourceRow = function(treatmentBMPID, fundingSourceID) { return _.find($scope.AngularModel.FundingEventFundingSources, function(pfse) { return pfse.TreatmentBMPID == treatmentBMPID && pfse.FundingSourceID == fundingSourceID; }); }

    $scope.addRow = function()
    {
        if (($scope.FundingSourceIDToAdd == null) || ($scope.TreatmentBMPIDToAdd == null)) {
            return;
        }
        var newFundingEventFundingSource = $scope.createNewRow($scope.TreatmentBMPIDToAdd, $scope.FundingSourceIDToAdd);
        $scope.AngularModel.FundingEventFundingSources.push(newFundingEventFundingSource);
        $scope.resetFundingSourceIDToAdd();
        $scope.resetTreatmentBMPIDToAdd();
    };

    $scope.createNewRow = function (treatmentBMPID, fundingSourceID)
    {
        var treatmentBMP = $scope.getTreatmentBMP(treatmentBMPID);
        var fundingSource = $scope.getFundingSource(fundingSourceID);
        var newFundingEventFundingSource = {
            TreatmentBMPID: treatmentBMP.TreatmentBMPID,
            FundingSourceID: fundingSource.FundingSourceID,
            Amount: null
    };
        return newFundingEventFundingSource;
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.FundingEventFundingSources, rowToDelete);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.FromFundingSource = angularModelAndViewData.AngularViewData.FromFundingSource;
    $scope.FromTreatmentBMP = !$scope.FromFundingSource;
    $scope.resetFundingSourceIDToAdd();
    $scope.resetTreatmentBMPIDToAdd();
});

