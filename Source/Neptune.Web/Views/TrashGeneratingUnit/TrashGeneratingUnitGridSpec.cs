﻿using System;
using System.Globalization;
using System.Web;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TrashGeneratingUnit
{
    public class TrashGeneratingUnitGridSpec : GridSpec<Models.TrashGeneratingUnit>
    {
        public TrashGeneratingUnitGridSpec()
        {

            Add("Trash Generating Unit ID", x => x.TrashGeneratingUnitID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x =>
            {
                if (x.LandUseBlock == null)
                {
                    return "No data provided";
                }

                return x.LandUseBlock?.PriorityLandUseType?.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use";
            }, 140, DhtmlxGridColumnFilterType.Text);
            Add("Governing OVTA Area", x => x.OnlandVisualTrashAssessmentArea?.GetDisplayNameAsDetailUrlNoPermissionCheck() ?? new HtmlString(""), 255, DhtmlxGridColumnFilterType.Html);
            Add("Governing Treatment BMP", x => x.TreatmentBMP?.GetDisplayNameAsUrl() ?? new HtmlString(""), 190, DhtmlxGridColumnFilterType.Html);
            Add("Stormwater Jurisdiction", x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString(""), 170, DhtmlxGridColumnFilterType.Html);
            Add("Area", x => ((x.TrashGeneratingUnitGeometry.Area ?? 0) * DbSpatialHelper.SqlGeometryAreaToAcres).ToString("F2", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
            Add("Loading Rate", x =>
            {
                if (x.LandUseBlock == null || x.LandUseBlock.PriorityLandUseTypeID == PriorityLandUseType.ALU.PriorityLandUseTypeID)
                {
                    return "N/A";
                }

                if (x.TreatmentBMP?.TrashCaptureStatusTypeID == TrashCaptureStatusType.Full.TrashCaptureStatusTypeID)
                {
                    return "2.5";
                }

                var landUseBlockTrashGenerationRate = x.LandUseBlock.TrashGenerationRate;
                var assessmentScoreTrashGenerationRate =
                    x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScore?.TrashGenerationRate;

                var preCaptureLoadingRate = assessmentScoreTrashGenerationRate != null
                    ? Math.Min(assessmentScoreTrashGenerationRate.Value, landUseBlockTrashGenerationRate)
                    : landUseBlockTrashGenerationRate;

                var treatmentBMPTrashCaptureEffectiveness = x.TreatmentBMP?.TrashCaptureEffectiveness;
                if (x.TreatmentBMP?.TrashCaptureStatusTypeID !=
                    TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID &&
                    treatmentBMPTrashCaptureEffectiveness != null)
                {
                    var postCaptureLoadingRate = (1.0 - (treatmentBMPTrashCaptureEffectiveness.Value / 100.0));
                    return Math.Max(2.5, postCaptureLoadingRate).ToString(CultureInfo.InvariantCulture);
                }

                return preCaptureLoadingRate.ToString(CultureInfo.InvariantCulture);
            }, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Last Updated", x => x.LastUpdateDate, 120,DhtmlxGridColumnFormatType.DateTime);
        }

    }
}
