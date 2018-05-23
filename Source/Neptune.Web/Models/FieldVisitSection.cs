using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public abstract partial class FieldVisitSection
    {
        public abstract string GetSectionUrl(FieldVisit fieldVisit);
    }

    public partial class FieldVisitSectionInventory : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Inventory(fieldVisit));
        }
    }

    public partial class FieldVisitSectionAssessment : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Assessment(fieldVisit));
        }
    }

    public partial class FieldVisitSectionMaintain : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Maintain(fieldVisit));
        }
    }

    public partial class FieldVisitSectionPostMaintenanceAssessment : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.PostMaintenanceAssessment(fieldVisit));
        }
    }

    public partial class FieldVisitSectionWrapUpVisit : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.WrapUpVisit(fieldVisit));
        }
    }

    public partial class FieldVisitSectionManageVisit : FieldVisitSection
    {
        public override string GetSectionUrl(FieldVisit fieldVisit)
        {
            return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.ManageVisit(fieldVisit));
        }
    }
}