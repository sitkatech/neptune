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
        public string FieldVisitInformationUrl { get; }
        public string ScoreUrl { get; }
        public string SectionName { get; }
        public bool FieldVisitInformationComplete { get; }
        public string SubsectionName { get; set; }
        public string SectionHeader { get; set; }

        public FieldVisitSectionViewData(Person currentPerson, Models.FieldVisit fieldVisit, Models.FieldVisitSection fieldVisitSection)
            : base(currentPerson, StormwaterBreadCrumbEntity.FieldVisits)
        {
            FieldVisit = fieldVisit;
            SectionName = fieldVisitSection.FieldVisitSectionName;

            EntityName = "Treatment BMP Field Visits";
            // todo: are we goign to have a Field Visit Index Page?
            //EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = fieldVisit.TreatmentBMP?.FormattedNameAndType ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = fieldVisit.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = fieldVisit.VisitDate.ToStringDate();

            SectionHeader = fieldVisitSection.SectionHeader;
        }
    }
}
