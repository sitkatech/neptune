using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.FieldVisit
{
    public class InventoryViewData : FieldVisitSectionViewData
    {
        public string AssessUrl { get; }
        public string LocationUrl { get; }

        public InventoryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Inventory)
        {
            LocationUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Location(fieldVisit));
            AssessUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Assessment(fieldVisit));
        }
    }
}