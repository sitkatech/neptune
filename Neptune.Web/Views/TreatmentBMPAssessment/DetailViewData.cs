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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMPAssessment TreatmentBMPAssessment { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CanEdit { get; }
        public ScoreDetailViewData ScoreDetailViewData { get; }
        public string EditBenchmarkAndThresholdUrl { get; }
        public ImageCarouselViewData ImageCarouselViewData { get; }
        public string EditUrl { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMPAssessment = treatmentBMPAssessment;
            CurrentPersonCanManage = CurrentPerson.IsAssignedToStormwaterJurisdiction(treatmentBMPAssessment.TreatmentBMP.StormwaterJurisdictionID);
            ScoreDetailViewData = new ScoreDetailViewData(treatmentBMPAssessment);
            EditBenchmarkAndThresholdUrl =
                SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(linkGenerator, x =>
                    x.Instructions(treatmentBMPAssessment.TreatmentBMP));

            CanEdit = CurrentPersonCanManage && treatmentBMPAssessment.CanEdit(CurrentPerson) && !treatmentBMPAssessment.IsAssessmentComplete;

            EntityName = "Treatment BMP Assessments";
            EntityUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMPAssessment.TreatmentBMP));
            PageTitle = treatmentBMPAssessment.GetAssessmentDate().ToStringDate();
            EditUrl = (treatmentBMPAssessment.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.Initial) ?
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Assessment(treatmentBMPAssessment.FieldVisitID)) :
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.PostMaintenanceAssessment(treatmentBMPAssessment.FieldVisitID));

            var carouselImages = TreatmentBMPAssessment.TreatmentBMPAssessmentPhotos;
            ImageCarouselViewData = new ImageCarouselViewData(carouselImages, 400, linkGenerator);
        }
    }
}
