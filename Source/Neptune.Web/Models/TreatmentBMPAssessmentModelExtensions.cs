/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPAssessmentModelExtensions.cs" company="Tahoe Regional Planning Agency">
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

using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class TreatmentBMPAssessmentModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            if (treatmentBMPAssessment == null) { return ""; }
            return DetailUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return EditUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }

        public static readonly UrlTemplate<int> EditScoreUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Score(UrlTemplate.Parameter1Int)));
        public static string GetEditScoreUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return EditScoreUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return DeleteUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }

        public static HtmlString GetDateAsDetailUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return treatmentBMPAssessment == null ? new HtmlString(string.Empty) : UrlTemplate.MakeHrefString(DetailUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID), treatmentBMPAssessment.AssessmentDate.ToShortDateString());
        }


    }
}
