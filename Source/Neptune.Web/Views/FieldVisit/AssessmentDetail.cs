/*-----------------------------------------------------------------------
<copyright file="AssessmentDetail.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Views.FieldVisit
{
    public abstract class AssessmentDetail : TypedWebPartialViewPage<AssessmentDetailViewData>
    {
        public static void RenderPartialView(HtmlHelper html, AssessmentDetailViewData viewData)
        {
            html.RenderRazorSitkaPartial<AssessmentDetail, AssessmentDetailViewData>(viewData);
        }
    }

    public class AssessmentDetailViewData
    {
        public Models.TreatmentBMPAssessment TreatmentBMPAssessment { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CanEdit { get; }
        public ScoreDetailViewData ScoreDetailViewData { get; }
        public string EditBenchmarkAndThresholdUrl { get; }
        public ImageCarouselViewData ImageCarouselViewData { get; }

        public AssessmentDetailViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            TreatmentBMPAssessment = treatmentBMPAssessment;
            CurrentPersonCanManage = currentPerson.IsAssignedToStormwaterJurisdiction(treatmentBMPAssessment.TreatmentBMP.StormwaterJurisdiction);
            ScoreDetailViewData = new ScoreDetailViewData(treatmentBMPAssessment);
            EditBenchmarkAndThresholdUrl =
                SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(x =>
                    x.Instructions(treatmentBMPAssessment.TreatmentBMP));

            CanEdit = CurrentPersonCanManage && treatmentBMPAssessment.CanEdit(currentPerson) && !treatmentBMPAssessment.IsAssessmentComplete();

            var carouselImages = TreatmentBMPAssessment.TreatmentBMPAssessmentPhotos;
            ImageCarouselViewData = new ImageCarouselViewData(carouselImages, 400);
        }
    }
}