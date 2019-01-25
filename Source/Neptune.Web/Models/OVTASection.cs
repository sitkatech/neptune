using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class OVTASection
    {
        public bool ExpandMenu(OnlandVisualTrashAssessment ovta)
        {
            return false;
        }

        public IEnumerable<OVTASubsectionData> GetSubsections(OnlandVisualTrashAssessment ovta)
        {
            throw new NotImplementedException();
        }

        public abstract string GetSectionUrl(OnlandVisualTrashAssessment ovta);

        public abstract OVTASection GetNextSection();

        public abstract bool IsSectionComplete(OnlandVisualTrashAssessment ovta);

        public HtmlString SectionCompletionStatusIndicator(OnlandVisualTrashAssessment ovta)
        {
            if (!HasCompletionStatus || ovta == null) // all menu items are disabled if ovta hasn't been created yet, so don't display an indicator
            {
                return new HtmlString(string.Empty);
            }

            return IsSectionComplete(ovta)
                ? new HtmlString(
                    "<span class='glyphicon glyphicon-ok field-validation-success text-left'></span>")
                : new HtmlString(
                    "<span class='glyphicon glyphicon-exclamation-sign field-validation-warning text-left'></span>");
        }
    }

    public partial class OVTASectionInstructions
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Instructions(ovta.OnlandVisualTrashAssessmentID));
        }

        public override OVTASection GetNextSection()
        {
            return InitiateOVTA;
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            throw new InvalidOperationException("Instructions does not have a completness status; cannot check completeness");
        }
    }

    public partial class OVTASectionInitiateOVTA
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.InitiateOVTA(ovta));
        }

        public override OVTASection GetNextSection()
        {
            return RecordObservations;
        }
        
        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            var viewModel = new InitiateOVTAViewModel(ovta);
            return !viewModel.GetValidationResults().Any();
        }

    }

    public partial class OVTASectionRecordObservations
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.RecordObservations(ovta));
        }

        public override OVTASection GetNextSection()
        {
            return FinalizeOVTA;
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            return ovta?.OnlandVisualTrashAssessmentObservations?.Any() ?? false;
        }
    }

    public partial class OVTASectionFinalizeOVTA
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.FinalizeOVTA(ovta));
        }

        public override OVTASection GetNextSection()
        {
            throw new InvalidOperationException("Finalize OVTA is the final step; cannot get next section");
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            throw new InvalidOperationException("Finalize does not have a completness status; cannot check completeness");
        }
    }

    public class OVTASubsectionData
    {
        public string SubsectionName { get; set; }
        public string SubsectionUrl { get; set; }
        public HtmlString SectionCompletionStatusIndicator { get; set; }
    }
}