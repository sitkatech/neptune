using System.Collections.Generic;
using LtInfo.Common;
using LtInfo.Common.Models;
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
        public string SectionHeader { get; set; }
        public List<string> ValidationWarnings { get; set; }

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

            SectionHeader = fieldVisitSection.SectionHeader;
            ValidationWarnings = new List<string>();
        }

        protected FieldVisitSectionViewData(Person currentPerson) : base(currentPerson)
        {
            ValidationWarnings = new List<string>();
        }
    }
}
