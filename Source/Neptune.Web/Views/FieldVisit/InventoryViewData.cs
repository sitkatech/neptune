using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class InventoryViewData : FieldVisitSectionViewData
    {
        public string AssessUrl { get; }
        public string LocationUrl { get; }

        public InventoryViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.Inventory)
        {
            LocationUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Location(fieldVisit));
            AssessUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Assessment(fieldVisit));
        }
    }
}