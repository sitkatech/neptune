using Neptune.Web.Models;
using Neptune.Web.Views;
using Neptune.Web.Views.Map;
using Neptune.Web.Views.Shared.JurisdictionControls;

namespace Neptune.Web.Areas.Trash.Views.Home
{
    public class IndexViewData : NeptuneViewData
    {
        public JurisdictionsMapViewData JurisdictionsMapViewData { get; }
        public JurisdictionsMapInitJson JurisdictionsMapInitJson { get; }

        public IndexViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage)
        {
            EntityName = "Trash Module";
            PageTitle = "Welcome";
        }

        public IndexViewData(Person currentPerson, NeptunePage neptunePage, JurisdictionsMapViewData jurisdictionsMapViewData, JurisdictionsMapInitJson jurisdictionsMapInitJson) : this(currentPerson, neptunePage)
        {
            JurisdictionsMapViewData = jurisdictionsMapViewData;
            JurisdictionsMapInitJson = jurisdictionsMapInitJson;
        }
    }
}