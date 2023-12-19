using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Controllers;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class DetailViewData : TrashModuleViewData
    {
        public EFModels.Entities.OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; }
        public TrashAssessmentSummaryMapViewData TrashAssessmentSummaryMapViewData { get; }
        public string ReturnToEditUrl { get; }
        public bool UserHasReturnToEditPermission { get; }
        public bool HasViewAssessmentAreaPermission { get; set; }
        public string IsProgressAssessment { get; }
        public string? OnlandVisualTrashAssessmentAreaDetailUrl { get; }
        public string StormwaterJurisdictionDetailUrl { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.OnlandVisualTrashAssessment onlandVisualTrashAssessment, TrashAssessmentSummaryMapViewData trashAssessmentSummaryMapViewData, string returnToEditUrl, bool userHasReturnToEditPermission, bool hasViewAssessmentAreaPermission) : base(httpContext, linkGenerator, currentPerson, webConfiguration)
        {
            OnlandVisualTrashAssessment = onlandVisualTrashAssessment;
            IsProgressAssessment = onlandVisualTrashAssessment.ToBaselineProgress();
            TrashAssessmentSummaryMapViewData = trashAssessmentSummaryMapViewData;
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea
                .OnlandVisualTrashAssessmentAreaName;
            SubEntityUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x =>
                x.Detail(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea));
            // ReSharper disable once PossibleInvalidOperationException
            PageTitle = OnlandVisualTrashAssessment.CompletedDate.Value.ToShortDateString();
            ReturnToEditUrl = returnToEditUrl;
            UserHasReturnToEditPermission = userHasReturnToEditPermission;
            HasViewAssessmentAreaPermission = hasViewAssessmentAreaPermission;
            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null)
            {
                OnlandVisualTrashAssessmentAreaDetailUrl =
                    SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator,
                        x => x.Detail(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea));
            }
            StormwaterJurisdictionDetailUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(onlandVisualTrashAssessment.StormwaterJurisdictionID));
        }
    }
}
