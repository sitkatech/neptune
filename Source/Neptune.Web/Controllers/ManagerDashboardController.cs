/*-----------------------------------------------------------------------
<copyright file="ManagerDashboardController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.ManagerDashboard;


namespace Neptune.Web.Controllers
{
    public class ManagerDashboardController : NeptuneBaseController
    {
        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManagerDashboard);
            var maintenanceAttributeTypes =
                HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.Where(x =>
                    x.CustomAttributeTypePurposeID ==
                    CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID);
            var viewData = new IndexViewData(CurrentPerson, neptunePage,
                HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitViewFeature]
        public GridJsonNetJObjectResult<FieldVisit> AllFieldVisitsGridJsonData()
        {
            var gridSpec = new ProvisionalFieldVisitGridSpec(CurrentPerson);
            var fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits.GetProvisionalFieldVisits(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldVisit>(fieldVisits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> ProvisionalTreatmentBMPGridJsonData()
        {
            var gridSpec = new ProvisionalTreatmentBMPGridSpec(CurrentPerson);
            var treatmentBMPAssessments = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments.GetProvisionalTreatmentBMPAssessments(CurrentPerson);
            return new GridJsonNetJObjectResult<TreatmentBMPAssessment>(treatmentBMPAssessments, gridSpec);
        }
    }
}