/*-----------------------------------------------------------------------
<copyright file="AssessmentViewData.cs" company="Tahoe Regional Planning Agency">
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

using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public abstract class AssessmentViewData : NeptuneViewData
    {
        public readonly Models.TreatmentBMPAssessment TreatmentBMPAssessment;
        public readonly Models.TreatmentBMP TreatmentBMP;
        public readonly string AssessmentInformationUrl;
        public readonly string ScoreUrl;
        public readonly string SectionName;
        public readonly bool AssessmentInformationComplete;

        protected AssessmentViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment, string sectionName) : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            TreatmentBMP = treatmentBMPAssessment.TreatmentBMP;
            TreatmentBMPAssessment = treatmentBMPAssessment;

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMPAssessment.PrimaryKey))
            {
                AssessmentInformationUrl = "#";
                AssessmentInformationComplete = false;
            }
            else
            {
                AssessmentInformationUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x => x.Edit(TreatmentBMPAssessment));
                AssessmentInformationComplete = true;
            }

            ScoreUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x => x.Score(TreatmentBMPAssessment));

            SectionName = sectionName;


            SubEntityName = TreatmentBMP.FormattedNameAndType;
            SubEntityUrl = TreatmentBMP.GetDetailUrl();
           
            PageTitle = "Assessment";
        }
    }
}
