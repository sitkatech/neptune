using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.TrashGeneratingUnit
{
    public class IndexViewData : TrashModuleViewData
    {
        public TrashGeneratingUnitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public IndexViewData(Person currentPerson) : base (currentPerson)
        {
            EntityName = "Trash Generating Unit";
            PageTitle = "Index";
            GridSpec = new TrashGeneratingUnitGridSpec { ObjectNameSingular = "Trash Generating Unit", ObjectNamePlural = "Trash Generating Units", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(j => j.TrashGeneratingUnitGridJsonData());
        }
    }
}