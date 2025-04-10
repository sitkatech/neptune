/*-----------------------------------------------------------------------
<copyright file="WQMPAnnualReportController.js" company="Tahoe Regional Planning Agency">
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
angular.module("NeptuneApp").controller("WQMPAnnualReportController", function ($scope, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;


    $scope.reloadGridsForSelections = function ()
    {
        var jurisdictionID = $scope.SelectedStormwaterJurisdiction == undefined ? -1 : $scope.SelectedStormwaterJurisdiction.StormwaterJurisdictionID

        var approvalSummaryGridUrl = new Sitka.UrlTemplate($scope.AngularViewData.ApprovalSummaryGridUrlTemplateString).ParameterReplace(
            $scope.SelectedReportingYear.ReportingYear, jurisdictionID);

        var approvalSummaryGrid = Sitka.wqmpApprovalSummaryGrid;

        jQuery.ajax(approvalSummaryGridUrl).done(function (jsonData) {
            approvalSummaryGrid.reloadDataFromJson(jsonData);
        }).fail(function (error) {
            console.log(error);
        });

        var postConstructionInspectionAndVerificationGridUrl = new Sitka.UrlTemplate($scope.AngularViewData.PostConstructionInspectionAndVerificationGridUrlTemplateString).ParameterReplace(
            $scope.SelectedReportingYear.ReportingYear, jurisdictionID);

        var postConstructionInspectionAndVerificationGrid = Sitka.wqmpPostConstructionInspectionAndVerificationGrid;

        jQuery.ajax(postConstructionInspectionAndVerificationGridUrl).done(function (jsonData) {
            postConstructionInspectionAndVerificationGrid.reloadDataFromJson(jsonData);
        }).fail(function (error) {
            console.log(error);
        });

    };

    jQuery(function () {
        var approvalSummaryGridUrl= new Sitka.UrlTemplate($scope.AngularViewData.ApprovalSummaryGridUrlTemplateString).ParameterReplace(
            $scope.SelectedReportingYear.ReportingYear, $scope.SelectedStormwaterJurisdiction.StormwaterJurisdictionID);
        var approvalSummaryGrid = Sitka.wqmpApprovalSummaryGrid;
        approvalSummaryGrid.load(approvalSummaryGridUrl);

        var postConstructionInspectionAndVerificationGridUrl = new Sitka.UrlTemplate($scope.AngularViewData.PostConstructionInspectionAndVerificationGridUrlTemplateString).ParameterReplace(
            $scope.SelectedReportingYear.ReportingYear, $scope.SelectedStormwaterJurisdiction.StormwaterJurisdictionID);
        var postConstructionInspectionAndVerificationGrid = Sitka.wqmpPostConstructionInspectionAndVerificationGrid;
        postConstructionInspectionAndVerificationGrid.load(postConstructionInspectionAndVerificationGridUrl);
    });

});

