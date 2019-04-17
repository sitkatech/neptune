using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class DetailViewData : TrashModuleViewData
    {
        public Models.OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; }
        public TrashAssessmentSummaryMapViewData TrashAssessmentSummaryMapViewData { get; }
        public string ReturnToEditUrl { get; }
        public bool UserHasReturnToEditPermission { get; }

        public DetailViewData(Person currentPerson, Models.OnlandVisualTrashAssessment onlandVisualTrashAssessment, TrashAssessmentSummaryMapViewData trashAssessmentSummaryMapViewData, string returnToEditUrl, bool userHasReturnToEditPermission) : base(currentPerson)
        {
            OnlandVisualTrashAssessment = onlandVisualTrashAssessment;
            TrashAssessmentSummaryMapViewData = trashAssessmentSummaryMapViewData;
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea
                .OnlandVisualTrashAssessmentAreaName;
            SubEntityUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x =>
                x.Detail(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea));
            // ReSharper disable once PossibleInvalidOperationException
            PageTitle = OnlandVisualTrashAssessment.CompletedDate.Value.ToShortDateString();
            ReturnToEditUrl = returnToEditUrl;
            UserHasReturnToEditPermission = userHasReturnToEditPermission;
        }
    }
}
