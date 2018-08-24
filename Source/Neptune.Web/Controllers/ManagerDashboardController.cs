/*-----------------------------------------------------------------------
<copyright file="FieldVisitController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Microsoft.Ajax.Utilities;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.ManagerDashboard;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.EditAttributes;
using Neptune.Web.Views.Shared.Location;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;
using FieldVisitSection = Neptune.Web.Models.FieldVisitSection;


namespace Neptune.Web.Controllers
{
    public class ManagerDashboardController : NeptuneBaseController
    {
        [HttpGet]
        [FieldVisitViewFeature]
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
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, null, false);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldVisit>(fieldVisits, gridSpec);

            return gridJsonNetJObjectResult;
        }

        /// <summary>
        /// Gets the Field Visits for a given Treatment BMP and out-returns the appropriate grid spec.
        /// If treatmentBMP is null, returns all Field Visits 
        /// </summary>
        /// <param name="gridSpec"></param>
        /// <param name="currentPerson"></param>
        /// <param name="treatmentBMP"></param>
        /// <param name="detailPage"></param>
        /// <returns></returns>
        private static List<FieldVisit> GetFieldVisitsAndGridSpec(out ProvisionalFieldVisitGridSpec gridSpec, Person currentPerson,
            TreatmentBMP treatmentBMP, bool detailPage)
        {
            gridSpec = new ProvisionalFieldVisitGridSpec(currentPerson, detailPage);
            var fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits.ToList().Where(x => x.TreatmentBMP.CanView(currentPerson));
            var fieldVisitsAndGridSpec = (treatmentBMP != null
                ? fieldVisits.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                : fieldVisits).ToList().Where(x => x.IsFieldVisitVerified == false).ToList();
            return fieldVisitsAndGridSpec;
        }


        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> ProvisionalTreatmentBMPGridJsonData()
        {
            var gridSpec = new ProvisionalTreatmentBMPGridSpec(CurrentPerson, HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes);
            var bmpAssessments = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments.ToList().Where(x => x.TreatmentBMP.CanView(CurrentPerson) && x.TreatmentBMP.InventoryIsVerified == false)
                .OrderByDescending(x => x.GetAssessmentDate()).ToList();
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<TreatmentBMPAssessment>(bmpAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }
    }
}