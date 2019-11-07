using Neptune.Web.Areas.Modeling.Controllers;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.TrashGeneratingUnit;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Modeling.Views.HRUCharacteristic
{
    public class IndexViewData : ModelingModuleViewData
    {
        public HRUCharacteristicGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public IndexViewData(Person currentPerson) : base(currentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.HRUCharacteristics))

        {
            EntityName = "HRU Characteristics";
            PageTitle = "Index";
            GridSpec = new HRUCharacteristicGridSpec() { ObjectNameSingular = "HRU Characteristic", ObjectNamePlural = "HRU Characteristics", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<HRUCharacteristicController>.BuildUrlFromExpression(j => j.HRUCharacteristicGridJsonData());
        }
    }
}