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

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly Models.TreatmentBMP TreatmentBMP;
        public readonly MapInitJson MapInitJson;
        public readonly string AddBenchmarkAndThresholdUrl;
        public readonly string NewAssessmentUrl;
        public readonly bool HasSettableBenchmarkAndThresholdValues;
        public readonly bool CurrentPersonCanManage;

        public readonly bool CanEditBenchmarkAndThresholds;

        public readonly TreatmentBMPAssessmentGridSpec AssessmentGridSpec;
        public readonly string AssessmentGridName;
        public readonly string AssessmentGridDataUrl;

        public DetailViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, MapInitJson mapInitJson)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP, null)
        {
            TreatmentBMP = treatmentBMP;
            PageTitle = treatmentBMP.FormattedNameAndType;
            MapInitJson = mapInitJson;
            AddBenchmarkAndThresholdUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.Instructions(treatmentBMP.TreatmentBMPID));
            NewAssessmentUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.New(treatmentBMP.TreatmentBMPID));
            HasSettableBenchmarkAndThresholdValues = TreatmentBMP.HasSettableBenchmarkAndThresholdValues();
            CurrentPersonCanManage = CurrentPerson.CanManageStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdiction);

            CanEditBenchmarkAndThresholds = CurrentPersonCanManage && HasSettableBenchmarkAndThresholdValues;

            AssessmentGridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson);
            AssessmentGridName = "Assessment";
            AssessmentGridDataUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.AssessmentGridJsonData(treatmentBMP));
        }
    }
}
