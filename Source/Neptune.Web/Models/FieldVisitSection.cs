using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using System;

namespace Neptune.Web.Models
{
    public abstract partial class FieldVisitSection
    {
        public abstract string GetSectionUrl(FieldVisit fieldVisit);
        public abstract List<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit);
    }

    public partial class FieldVisitSectionInventory : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Inventory(fieldVisit));
        }

        public override List<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            FieldVisitSubsectionData LocationSubsection = new FieldVisitSubsectionData
            {
                SubsectionName = "Location",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Location(fieldVisit))
            };
            FieldVisitSubsectionData PhotosSubsection = new FieldVisitSubsectionData
            {
                SubsectionName = "Photos",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Photos(fieldVisit))
            };
            FieldVisitSubsectionData AttributesSubsection = new FieldVisitSubsectionData
            {
                SubsectionName = "Attributes",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Attributes(fieldVisit))
            };

            return new List<FieldVisitSubsectionData> {LocationSubsection, PhotosSubsection, AttributesSubsection};
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

        public override List<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return new List<FieldVisitSubsectionData>();
        }
    }

    public partial class FieldVisitSectionMaintenance
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Maintain(fieldVisit));
        }

        public override List<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
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

        public override List<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return new List<FieldVisitSubsectionData>();
        }
    }

    public partial class FieldVisitSectionWrapUpVisit
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.WrapUpVisit(fieldVisit));
        }

        public override List<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
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

        public override List<FieldVisitSubsectionData> GetSubsections(FieldVisit fieldVisit)
        {
            return new List<FieldVisitSubsectionData>();
        }
    }
}