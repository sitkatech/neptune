using Neptune.Web.Models;

namespace Neptune.Web.Areas.DroolTool.Views.Home
{
    public class AboutViewData : DroolToolModuleViewData
    {
        public AboutViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage, false)
        {
            EntityName = "Urban Drool Tool";
            PageTitle = "About";
        }
    }
}