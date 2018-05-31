/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency">
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

using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly Models.TreatmentBMPAssessment TreatmentBMPAssessment;
        public readonly bool CurrentPersonCanManage;
        public readonly bool CanEdit;
        public readonly ScoreDetailViewData ScoreDetailViewData;
        public readonly string EditBenchmarkAndThresholdUrl;

        
        public DetailViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            TreatmentBMPAssessment = treatmentBMPAssessment;
            CurrentPersonCanManage = CurrentPerson.CanManageStormwaterJurisdiction(treatmentBMPAssessment.TreatmentBMP.StormwaterJurisdiction);
            ScoreDetailViewData = new ScoreDetailViewData(treatmentBMPAssessment);
            EditBenchmarkAndThresholdUrl =
                SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(x =>
                    x.Instructions(treatmentBMPAssessment.TreatmentBMP));

            CanEdit = CurrentPersonCanManage && treatmentBMPAssessment.CanEdit(CurrentPerson);

            EntityName = "Treatment BMP Assessments";
            EntityUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPName;
            SubEntityUrl = treatmentBMPAssessment.TreatmentBMP.GetDetailUrl();
            PageTitle = treatmentBMPAssessment.GetAssessmentDate.ToStringDate();
        }
    }
}
