using System.Linq;
using System.Web;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class DetailViewData : TrashModuleViewData
    {
        public Models.OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; }
        public HtmlString LastAssessmentDateHtmlString { get; }
        public HtmlString ScoreHtmlString { get; }
        public string NewUrl { get; }
        public OVTAIndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public DetailViewData(Person currentPerson, Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea) : base(currentPerson)
        {
            EntityName = "Trash Module";
            SubEntityName = "OVTA Areas";
            PageTitle = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName;
            OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentArea;
            var completedAssessments = OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentStatus == OnlandVisualTrashAssessmentStatus.Complete).ToList();

            LastAssessmentDateHtmlString = new HtmlString( completedAssessments.Max(x => x.CompletedDate)?.ToShortDateString() ?? "<p class='systemText'>No completed assessments</p>");
            ScoreHtmlString = new HtmlString(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScore != null
                ? OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
            NewUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x => x.NewAssessment(onlandVisualTrashAssessmentArea));


            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new OVTAIndexGridSpec(currentPerson, showDelete, showEdit)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true
            };
            GridName = "onlandVisualTrashAssessmentsGrid";
            GridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(j => j.OVTAGridJsonDataForAreaDetails(onlandVisualTrashAssessmentArea));
        }

        
    }
}