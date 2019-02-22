using System;
using System.Linq;
using System.Web;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class OVTASection
    {
        public abstract string GetSectionUrl(OnlandVisualTrashAssessment ovta);

        public abstract OVTASection GetNextSection(OnlandVisualTrashAssessment ovta);

        public string GetNextSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return GetNextSection(ovta).GetSectionUrl(ovta);
        }

        public abstract bool IsSectionComplete(OnlandVisualTrashAssessment ovta);

        public abstract bool IsSectionEnabled(OnlandVisualTrashAssessment ovta);

        public abstract bool IsSectionRelevant(OnlandVisualTrashAssessment ovta);

        public abstract string GetSectionDisabledMessage();

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

        public override OVTASection GetNextSection(OnlandVisualTrashAssessment ovta)
        {
            return InitiateOVTA;
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            throw new InvalidOperationException("Instructions does not have a completness status; cannot check completeness");
        }

        public override bool IsSectionEnabled(OnlandVisualTrashAssessment ovta)
        {
            return true;
        }

        public override bool IsSectionRelevant(OnlandVisualTrashAssessment ovta)
        {
            return true;
        }

        public override string GetSectionDisabledMessage()
        {
            throw new InvalidOperationException("Instructions should never be disabled.");
        }
    }

    public partial class OVTASectionInitiateOVTA
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.InitiateOVTA(ovta));
        }

        public override OVTASection GetNextSection(OnlandVisualTrashAssessment ovta)
        {
            return RecordObservations;
        }
        
        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            var viewModel = new InitiateOVTAViewModel(ovta);
            return !viewModel.GetValidationResults().Any();
        }

        public override bool IsSectionEnabled(OnlandVisualTrashAssessment ovta)
        {
            return ovta != null;
        }

        public override bool IsSectionRelevant(OnlandVisualTrashAssessment ovta)
        {
            return true;
        }

        public override string GetSectionDisabledMessage()
        {
            throw new InvalidOperationException("InitiateOVTA should never be disabled.");
        }
    }

    public partial class OVTASectionRecordObservations
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.RecordObservations(ovta));
        }

        public override OVTASection GetNextSection(OnlandVisualTrashAssessment ovta)
        {
            return ovta.AssessingNewArea.GetValueOrDefault() ?
                (OVTASection)AddOrRemoveParcels
                : FinalizeOVTA;
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            return ovta?.OnlandVisualTrashAssessmentObservations?.Any() ?? false;
        }

        public override bool IsSectionEnabled(OnlandVisualTrashAssessment ovta)
        {
            return InitiateOVTA.IsSectionComplete(ovta);
        }

        public override bool IsSectionRelevant(OnlandVisualTrashAssessment ovta)
        {
            return true;
        }

        public override string GetSectionDisabledMessage()
        {
            return "You must complete the Initiate OVTA section before you can record observations.";
        }
    }

    public partial class OVTASectionAddOrRemoveParcels
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.AddOrRemoveParcels(ovta));
        }

        public override OVTASection GetNextSection(OnlandVisualTrashAssessment ovta)
        {
            return RefineAssessmentArea;
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            return ovta.DraftGeometry != null || (ovta.AssessingNewArea.HasValue && !ovta.AssessingNewArea.Value);
        }

        public override bool IsSectionEnabled(OnlandVisualTrashAssessment ovta)
        {
            return RecordObservations.IsSectionComplete(ovta);
        }

        public override bool IsSectionRelevant(OnlandVisualTrashAssessment ovta)
        {
            return ovta?.AssessingNewArea.GetValueOrDefault() ?? false;
        }

        public override string GetSectionDisabledMessage()
        {
            return "You must complete the Record Observations section before you can add or remove parcels.";
        }
    }

    public partial class OVTASectionRefineAssessmentArea
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.RefineAssessmentArea(ovta));
        }

        public override OVTASection GetNextSection(OnlandVisualTrashAssessment ovta)
        {
            return FinalizeOVTA;
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            throw new InvalidOperationException(
                "RefineAssessmentArea does not have a completness status; cannot check completeness");
        }

        public override bool IsSectionEnabled(OnlandVisualTrashAssessment ovta)
        {
            return RecordObservations.IsSectionComplete(ovta) && AddOrRemoveParcels.IsSectionComplete(ovta);
        }

        public override bool IsSectionRelevant(OnlandVisualTrashAssessment ovta)
        {
            return ovta?.AssessingNewArea.GetValueOrDefault() ?? false;
        }

        public override string GetSectionDisabledMessage()
        {
            return "You must complete the Record Observations section and the Add or Remove Parcels section before you can refine the assessment area.";
        }
    }

    public partial class OVTASectionFinalizeOVTA
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.FinalizeOVTA(ovta));
        }

        public override OVTASection GetNextSection(OnlandVisualTrashAssessment ovta)
        {
            throw new InvalidOperationException("Finalize OVTA is the final step; cannot get next section");
        }

        public override bool IsSectionComplete(OnlandVisualTrashAssessment ovta)
        {
            throw new InvalidOperationException("Finalize does not have a completness status; cannot check completeness");
        }

        public override bool IsSectionEnabled(OnlandVisualTrashAssessment ovta)
        {
            return RecordObservations.IsSectionComplete(ovta) && AddOrRemoveParcels.IsSectionComplete(ovta);
        }

        public override bool IsSectionRelevant(OnlandVisualTrashAssessment ovta)
        {
            return true;
        }

        public override string GetSectionDisabledMessage()
        {
            return "You must complete the Record Observations section and the Add or Remove Parcels section before you can finalize the OVTA";
        }
    }
}
