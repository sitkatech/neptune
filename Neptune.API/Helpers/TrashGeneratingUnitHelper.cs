using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.API.Helpers;


public static class TrashGeneratingUnitHelper
{
    public const decimal FullTrashCaptureLoading = 2.5m;

    public static double TargetLoadReduction(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
    {
        var landUseBlocks = dbContext.LandUseBlocks.AsNoTracking().Where(x => x.PermitTypeID == (int)PermitTypeEnum.PhaseIMS4 &&
            x.StormwaterJurisdictionID == stormwaterJurisdictionID && x.PriorityLandUseTypeID != (int)PriorityLandUseTypeEnum.ALU);

        return landUseBlocks.Any()
            ? landUseBlocks.Sum(x =>
                x.LandUseBlockGeometry.Area * (double)(x.TrashGenerationRate - FullTrashCaptureLoading) *
                Constants.SquareMetersToAcres)
            : 0;
    }


    public static double EquivalentAreaAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return trashGeneratingUnits.Where(x =>
            x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID ==
            (int)OnlandVisualTrashAssessmentScoreEnum.A &&
            !x.IsFullTrashCapture() &&
            x.IsPLU()
        ).GetArea();
    }

    public static double FullTrashCaptureAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return trashGeneratingUnits.Where(x =>
            x.IsFullTrashCapture() &&
            x.IsPLU()
        ).GetArea();
    }

    public static double GetArea(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return Math.Round(trashGeneratingUnits
            .Select(x => x.TrashGeneratingUnitGeometry.Area * Constants.SquareMetersToAcres).Sum(), 0); // will never be null
    }

    public static bool IsFullTrashCapture(this TrashGeneratingUnit trashGeneratingUnit)
    {
        return (trashGeneratingUnit.Delineation?.TreatmentBMP.TrashCaptureStatusTypeID ==
            (int)TrashCaptureStatusTypeEnum.Full ||
            trashGeneratingUnit.WaterQualityManagementPlan?.TrashCaptureStatusTypeID ==
            (int)TrashCaptureStatusTypeEnum.Full);
    }
    
    public static bool IsPLU(this TrashGeneratingUnit trashGeneratingUnit)
    {
        // This is how to check "PLU == true"
        return trashGeneratingUnit.LandUseBlock?.PriorityLandUseTypeID != (int)PriorityLandUseTypeEnum.ALU;
    }

    // OVTA-based calculations

    public static double AlternateOVTAScoreDAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.D);
    }

    public static double AlternateOVTAScoreBAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.B);
    }

    public static double PriorityOVTAScoreDAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.D);
    }

    public static double PriorityOVTAScoreBAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.B);
    }

    public static double AlternateOVTAScoreCAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.C);
    }

    public static double AlternateOVTAScoreAAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetAlternativeOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.A);
    }

    public static double PriorityOVTAScoreCAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.C);
    }

    public static double PriorityOVTAScoreAAcreage(this List<TrashGeneratingUnit> trashGeneratingUnits)
    {
        return GetPriorityOVTAScoreAcreageImpl(trashGeneratingUnits, OnlandVisualTrashAssessmentScore.A);
    }

    private static double GetAlternativeOVTAScoreAcreageImpl(List<TrashGeneratingUnit> trashGeneratingUnits,
        OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
    {
        return trashGeneratingUnits.Where(x =>
            x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID ==
            onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreID &&
            x.IsPLU()).GetArea();
    }

    private static double GetPriorityOVTAScoreAcreageImpl(List<TrashGeneratingUnit> trashGeneratingUnits,
        OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
    {
        return trashGeneratingUnits.Where(x =>
            x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentBaselineScoreID ==
            onlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentScoreID &&
            x.IsPLU()).GetArea();
    }
}