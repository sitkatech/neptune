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
using System.Linq;
using System.Web;

namespace Neptune.Web.Models
{
    public abstract partial class FieldVisitSection
    {
        public abstract string GetSectionUrl(FieldVisit fieldVisit);
        public abstract IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit);

        public abstract bool ExpandMenu(FieldVisit fieldVisit);
    }

    public partial class FieldVisitSectionInventory
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Inventory(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return new[]
            {
                new FieldVisitSubsectionData
                {
                    SubsectionName = "Location",
                    SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Location(fieldVisit))
                },
                new FieldVisitSubsectionData
                {
                    SubsectionName = "Photos",
                    SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Photos(fieldVisit))
                },
                new FieldVisitSubsectionData
                {
                    SubsectionName = "Attributes",
                    SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Attributes(fieldVisit))
                }
            };
        }

        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.InventoryUpdated;
        }
    }

    public class FieldVisitSubsectionData
    {
        public string SubsectionName { get; set; }
        public string SubsectionUrl { get; set; }
        public HtmlString SectionCompletionStatusIndicator { get; set; }
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

        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.InitialAssessmentID != null;
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

        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.MaintenanceRecordID != null;
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

        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.PostMaintenanceAssessmentID != null;
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

        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return false;
        }
    }

    public class FieldVisitSectionImpl
    {
        public static IEnumerable<FieldVisitSubsectionData> GetAssessmentSubsections(FieldVisit fieldVisit, FieldVisitAssessmentType fieldVisitAssessmentType)
        {
            var treatmentBMP = fieldVisit.TreatmentBMP;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);

            var assessmentSubsections = new List<FieldVisitSubsectionData>
            {
                new FieldVisitSubsectionData
                {
                    SubsectionName = "Observations",
                    SubsectionUrl =
                        SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c =>
                            c.Observations(fieldVisit, (int) fieldVisitAssessmentType)),
                    SectionCompletionStatusIndicator =
                        treatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.All(x =>
                            treatmentBMPAssessment.IsObservationComplete(x.TreatmentBMPAssessmentObservationType))
                            ? new HtmlString("<span class='glyphicon glyphicon-ok field-validation-success text-left' style='color: #5cb85c; margin-right: 4px'></span>")
                            : new HtmlString("<span class='glyphicon glyphicon-exclamation-sign field-validation-warning text-left' style='margin-right: 4px'></span>")
                },
                new FieldVisitSubsectionData
                {
                    SubsectionName = "Photos",
                    SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c =>
                        c.AssessmentPhotos(fieldVisit, (int) fieldVisitAssessmentType)),
                    SectionCompletionStatusIndicator = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Any()
                        ? new HtmlString(
                            "<span class='glyphicon glyphicon-ok field-validation-success text-left' style='color: #5cb85c; margin-right: 4px'></span>")
                        : new HtmlString("<span style=\"width: 19px; display: inline-block;\"></span>")
                }
            };

            return assessmentSubsections;
        }
    }
}
