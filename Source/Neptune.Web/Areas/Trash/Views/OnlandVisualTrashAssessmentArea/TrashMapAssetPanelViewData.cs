using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class TrashMapAssetPanelViewData
    {
        public Models.OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; }
        public bool UserHasViewDetailsPermission { get; }

        public TrashMapAssetPanelViewData(Person currentPerson, Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentArea;
            UserHasViewDetailsPermission =
                currentPerson.CanEditStormwaterJurisdiction(onlandVisualTrashAssessmentArea.StormwaterJurisdiction);
        }
    }
}