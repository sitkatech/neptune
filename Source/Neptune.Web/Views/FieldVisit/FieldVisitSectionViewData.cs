using System.Collections.Generic;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class FieldVisitSectionViewData : NeptuneViewData
    {
        public Models.FieldVisit FieldVisit { get; }
        public string SectionName { get; }
        public string SubsectionName { get; set; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string SectionHeader { get; set; }
        public List<string> ValidationWarnings { get; set; }

        public string WrapupUrl { get; }


        public FieldVisitSectionViewData(Person currentPerson, Models.FieldVisit fieldVisit, Models.FieldVisitSection fieldVisitSection)
            : base(currentPerson, StormwaterBreadCrumbEntity.FieldVisits)
        {
            FieldVisit = fieldVisit;
            SectionName = fieldVisitSection.FieldVisitSectionName;

            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = fieldVisit.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = fieldVisit.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = fieldVisit.VisitDate.ToStringDate();

            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdiction);
            VerifiedUnverifiedFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.VerifyFieldVisit(FieldVisit.PrimaryKey));

            SectionHeader = fieldVisitSection.SectionHeader;
            ValidationWarnings = new List<string>();

            WrapupUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.WrapUpVisit(fieldVisit));
        }
    }
}
