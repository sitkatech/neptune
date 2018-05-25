using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using System;
using System.Linq;
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
    }

    public partial class FieldVisitSectionAssessment
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Assessment(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return FieldVisitSectionImpl.GetAssessmentSubsections(fieldVisit);
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
            return new List<FieldVisitSubsectionData>();
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
            return FieldVisitSectionImpl.GetAssessmentSubsections(fieldVisit);
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

    public partial class FieldVisitSectionManageVisit
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.ManageVisit(fieldVisit));
        }

        public override IEnumerable<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return new List<FieldVisitSubsectionData>();
        }
    }

    public class FieldVisitSectionImpl
    {
        public static IEnumerable<FieldVisitSubsectionData> GetAssessmentSubsections(FieldVisit fieldVisit)
        {
            var treatmentBMP = fieldVisit.TreatmentBMP;
            foreach (var fart in treatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .SortByOrderThenName().Select(x => x.TreatmentBMPAssessmentObservationType))
            {
                yield return new FieldVisitSubsectionData()
                {
                    SubsectionName = fart.TreatmentBMPAssessmentObservationTypeName
                };
            }
        }
    }
}