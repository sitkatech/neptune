using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment;
using OVTASection = Neptune.EFModels.Entities.OVTASection;

namespace Neptune.WebMvc.Models
{
    public static class OVTASectionModelExtensions
    {
        public static string? GetSectionUrl(this OVTASection ovtaSection, OnlandVisualTrashAssessment? ovta, LinkGenerator linkGenerator)
        {
            switch (ovtaSection.ToEnum)
            {
                case OVTASectionEnum.Instructions:
                    return ovta == null ? SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Instructions(ModelObjectHelpers.NotYetAssignedID)) : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Instructions(ovta));
                case OVTASectionEnum.InitiateOVTA:
                    return ovta == null ? SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.InitiateOVTA(ModelObjectHelpers.NotYetAssignedID)) : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.InitiateOVTA(ovta));
                case OVTASectionEnum.RecordObservations:
                    return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.RecordObservations(ovta));
                case OVTASectionEnum.AddOrRemoveParcels:
                    return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.AddOrRemoveParcels(ovta));
                case OVTASectionEnum.RefineAssessmentArea:
                    return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.RefineAssessmentArea(ovta));
                case OVTASectionEnum.FinalizeOVTA:
                    return ovta == null ? null : SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.FinalizeOVTA(ovta));
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown OVTASection {ovtaSection.OVTASectionName}");
            }
        }

        public static OVTASection GetNextSection(this OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            switch (ovtaSection.ToEnum)
            {
                case OVTASectionEnum.Instructions:
                    return OVTASection.InitiateOVTA;
                case OVTASectionEnum.InitiateOVTA:
                    return OVTASection.RecordObservations;
                case OVTASectionEnum.RecordObservations:
                    return ovta.AssessingNewArea.GetValueOrDefault() ?
                        OVTASection.AddOrRemoveParcels
                        : OVTASection.FinalizeOVTA;
                case OVTASectionEnum.AddOrRemoveParcels:
                    return OVTASection.RefineAssessmentArea;
                case OVTASectionEnum.RefineAssessmentArea:
                    return OVTASection.FinalizeOVTA;
                case OVTASectionEnum.FinalizeOVTA:
                    throw new InvalidOperationException("Finalize OVTA is the final step; cannot get next section");
                default:
                    throw new ArgumentOutOfRangeException($"Unknown OVTASection {ovtaSection.OVTASectionName}");
            }
        }

        public static string GetNextSectionUrl(this OVTASection ovtaSection, OnlandVisualTrashAssessment ovta,
            LinkGenerator linkGenerator)
        {
            return ovtaSection.GetNextSection(ovta).GetSectionUrl(ovta, linkGenerator);
        }

        public static bool IsSectionComplete(this OVTASection ovtaSection, OnlandVisualTrashAssessment? ovta)
        {
            switch (ovtaSection.ToEnum)
            {
                case OVTASectionEnum.Instructions:
                    throw new InvalidOperationException("Instructions does not have a completeness status; cannot check completeness");
                case OVTASectionEnum.InitiateOVTA:
                    var viewModel = new InitiateOVTAViewModel(ovta);
                    return !viewModel.GetValidationResults().Any();
                case OVTASectionEnum.RecordObservations:
                    return ovta?.OnlandVisualTrashAssessmentObservations.Any() ?? false;
                case OVTASectionEnum.AddOrRemoveParcels:
                    return ovta?.DraftGeometry != null || (ovta != null && ovta.AssessingNewArea.HasValue && !ovta.AssessingNewArea.Value);
                case OVTASectionEnum.RefineAssessmentArea:
                    throw new InvalidOperationException("RefineAssessmentArea does not have a completeness status; cannot check completeness");
                case OVTASectionEnum.FinalizeOVTA:
                    throw new InvalidOperationException("Finalize does not have a completeness status; cannot check completeness");
                default:
                    throw new ArgumentOutOfRangeException($"Unknown OVTASection {ovtaSection.OVTASectionName}");
            }
        }

        public static bool IsSectionEnabled(this OVTASection ovtaSection, OnlandVisualTrashAssessment? ovta)
        {
            switch (ovtaSection.ToEnum)
            {
                case OVTASectionEnum.Instructions:
                    return true;
                case OVTASectionEnum.InitiateOVTA:
                    return true;
                case OVTASectionEnum.RecordObservations:
                    return OVTASection.InitiateOVTA.IsSectionComplete(ovta);
                case OVTASectionEnum.AddOrRemoveParcels:
                    return OVTASection.RecordObservations.IsSectionComplete(ovta);
                case OVTASectionEnum.RefineAssessmentArea:
                    return OVTASection.RecordObservations.IsSectionComplete(ovta) && OVTASection.AddOrRemoveParcels.IsSectionComplete(ovta);
                case OVTASectionEnum.FinalizeOVTA:
                    return OVTASection.InitiateOVTA.IsSectionComplete(ovta) && OVTASection.RecordObservations.IsSectionComplete(ovta) && OVTASection.AddOrRemoveParcels.IsSectionComplete(ovta);
                default:
                    throw new ArgumentOutOfRangeException($"Unknown OVTASection {ovtaSection.OVTASectionName}");
            }
        }

        public static bool IsSectionRelevant(this OVTASection ovtaSection, OnlandVisualTrashAssessment? ovta)
        {
            switch (ovtaSection.ToEnum)
            {
                case OVTASectionEnum.Instructions:
                    return true;
                case OVTASectionEnum.InitiateOVTA:
                    return true;
                case OVTASectionEnum.RecordObservations:
                    return true;
                case OVTASectionEnum.AddOrRemoveParcels:
                    return ovta?.AssessingNewArea.GetValueOrDefault() ?? false;
                case OVTASectionEnum.RefineAssessmentArea:
                    return ovta?.AssessingNewArea.GetValueOrDefault() ?? false;
                case OVTASectionEnum.FinalizeOVTA:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown OVTASection {ovtaSection.OVTASectionName}");
            }
        }

        public static string GetSectionDisabledMessage(this OVTASection ovtaSection)
        {
            switch (ovtaSection.ToEnum)
            {
                case OVTASectionEnum.Instructions:
                    throw new InvalidOperationException("Instructions should never be disabled.");
                case OVTASectionEnum.InitiateOVTA:
                    throw new InvalidOperationException("InitiateOVTA should never be disabled.");
                case OVTASectionEnum.RecordObservations:
                    return "You must complete the Initiate OVTA section before you can record observations.";
                case OVTASectionEnum.AddOrRemoveParcels:
                    return "You must complete the Record Observations section before you can add or remove parcels.";
                case OVTASectionEnum.RefineAssessmentArea:
                    return "You must complete the Record Observations section and the Add or Remove Parcels section before you can refine the assessment area.";
                case OVTASectionEnum.FinalizeOVTA:
                    return "You must complete the Record Observations section and the Add or Remove Parcels section before you can finalize the OVTA";
                default:
                    throw new ArgumentOutOfRangeException($"Unknown OVTASection {ovtaSection.OVTASectionName}");
            }
        }

        public static HtmlString SectionCompletionStatusIndicator(this OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            if (!ovtaSection.HasCompletionStatus || ovta == null) // all menu items are disabled if ovta hasn't been created yet, so don't display an indicator
            {
                return new HtmlString(string.Empty);
            }

            return ovtaSection.IsSectionComplete(ovta)
                ? new HtmlString(
                    "<span class='glyphicon glyphicon-ok field-validation-success text-left'></span>")
                : new HtmlString(
                    "<span class='glyphicon glyphicon-exclamation-sign field-validation-warning text-left'></span>");
        }
    }
}
