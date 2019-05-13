using Neptune.Web.Models;

namespace Neptune.Web.Areas.DroolTool.Views.Home
{
    public class IndexViewData : DroolToolModuleViewData
    {
        public IndexViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage)
        {
            EntityName = "Urban Drool Tool";
            PageTitle = "Welcome";
        }
    }
}