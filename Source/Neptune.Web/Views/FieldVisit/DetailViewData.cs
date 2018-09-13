using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Views.FieldVisit
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.FieldVisit FieldVisit { get; }

        public DetailViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity,
            Models.FieldVisit fieldVisit) : base(currentPerson, stormwaterBreadCrumbEntity)
        {
            FieldVisit  = fieldVisit;
            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = FieldVisit.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = FieldVisit.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = FieldVisit.VisitDate.ToStringDate();
        }
    }
}
