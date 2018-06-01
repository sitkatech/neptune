/*-----------------------------------------------------------------------
<copyright file="FieldVisitSection.cs" company="Tahoe Regional Planning Agency">
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
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using System;
using System.Linq;
using System.Web;
using Neptune.Web.Views.Shared.SortOrder;

namespace Neptune.Web.Models
{
    public abstract partial class FieldVisitSection
    {
        public abstract string GetSectionUrl(FieldVisit fieldVisit);
        public abstract IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit);
    }

    public partial class FieldVisitSectionInventory : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Inventory(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            yield return new FieldVisitSubsectionData
            {
                SubsectionName = "Location",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Location(fieldVisit))
            };
            yield return new FieldVisitSubsectionData
            {
                SubsectionName = "Photos",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Photos(fieldVisit))
            };
            yield return new FieldVisitSubsectionData
            {
                SubsectionName = "Attributes",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Attributes(fieldVisit))
            };
        }
    }

    public class FieldVisitSubsectionData
    {
        public string SubsectionName { get; set; }
        public string SubsectionUrl { get; set; }
        public HtmlString AssessmentCompletionStatusIndicator { get; set; }
    }

    public partial class FieldVisitSectionAssessment
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Assessment(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return FieldVisitSectionImpl.GetAssessmentSubsections(fieldVisit, FieldVisitAssessmentType.Initial);
        }
    }

    public partial class FieldVisitSectionMaintenance
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Maintain(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            yield return new FieldVisitSubsectionData
            {
                SubsectionName = "Edit Maintenance Record",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x=>x.EditMaintenanceRecord(fieldVisit))
            };
        }
    }

    public partial class FieldVisitSectionPostMaintenanceAssessment
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.PostMaintenanceAssessment(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return FieldVisitSectionImpl.GetAssessmentSubsections(fieldVisit, FieldVisitAssessmentType.PostMaintenance);
        }
    }

    public partial class FieldVisitSectionWrapUpVisit
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.WrapUpVisit(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return new List<FieldVisitSubsectionData>();
        }
    }

    public class FieldVisitSectionImpl
    {
        public static IEnumerable<FieldVisitSubsectionData> GetAssessmentSubsections(FieldVisit fieldVisit, FieldVisitAssessmentType fieldVisitAssessmentType)
        {
            var treatmentBMP = fieldVisit.TreatmentBMP;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);

            foreach (var observationType in treatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .SortByOrderThenName().Select(x => x.TreatmentBMPAssessmentObservationType))
            {
                yield return new FieldVisitSubsectionData
                {
                    SubsectionName = observationType.TreatmentBMPAssessmentObservationTypeName,
                    SubsectionUrl = observationType.AssessmentUrl(fieldVisit, fieldVisitAssessmentType),
                    AssessmentCompletionStatusIndicator = treatmentBMPAssessment.IsObservationComplete(observationType)
                    ? new HtmlString("<span class='glyphicon glyphicon-ok field-validation-success text-left' style='color: #5cb85c; margin-right: 4px'></span>")
                        : new HtmlString("<span class='glyphicon glyphicon-exclamation-sign field-validation-warning text-left' style='margin-right: 4px'></span>")
                };
            }
        }
    }
}
