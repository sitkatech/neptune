using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class NewViewData : NeptuneViewData
    {
        public NewViewData(Person currentPerson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
        }
    }
}