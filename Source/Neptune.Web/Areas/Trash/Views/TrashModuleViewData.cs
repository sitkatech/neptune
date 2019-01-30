using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Areas.Trash.Views
{
    public class TrashModuleViewData : NeptuneViewData
    {
        public TrashModuleViewData(Person currentPerson) : base(currentPerson)
        {
        }

        public TrashModuleViewData(Person currentPerson, NeptunePage neptunePage, bool isHomePage) : base(currentPerson, neptunePage, isHomePage)
        {
        }

        public TrashModuleViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage)
        {
        }

        public TrashModuleViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, NeptunePage neptunePage) : base(currentPerson, stormwaterBreadCrumbEntity, neptunePage)
        {
        }

        public TrashModuleViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity) : base(currentPerson, stormwaterBreadCrumbEntity)
        {
        }
    }
}