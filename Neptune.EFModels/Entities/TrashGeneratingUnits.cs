using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class TrashGeneratingUnits
{
     public static List<TrashGeneratingUnitGridDto> List(NeptuneDbContext dbContext, PersonDto currentPerson)
    {
        var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, currentPerson.PersonID);
        var trashGeneratingUnitLoadStatistics = dbContext.vTrashGeneratingUnitLoadStatistics
            .Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID))
            .OrderByDescending(x => x.LastUpdateDate)
            .Select(x => new TrashGeneratingUnitGridDto()
            {
                TrashGeneratingUnitID = x.TrashGeneratingUnitID,
                TrashCaptureEffectivenessBMP = x.TrashCaptureEffectivenessBMP,
                TrashCaptureStatusBMP = x.TrashCaptureStatusBMP,
                TreatmentBMPName = x.TreatmentBMPName,
                TreatmentBMPID = x.TreatmentBMPID,
                StormwaterJurisdictionID = x.StormwaterJurisdictionID,
                StormwaterJurisdictionName = x.OrganizationName,
                BaselineLoadingRate = x.BaselineLoadingRate,
                ProgressLoadingRate = x.ProgressLoadingRate,
                LandUseType = x.LandUseType,
                CurrentLoadingRate = x.CurrentLoadingRate,
                PriorityLandUseTypeDisplayName = x.PriorityLandUseTypeDisplayName,
                OnlandVisualTrashAssessmentAreaID = x.OnlandVisualTrashAssessmentAreaID,
                OnlandVisualTrashAssessmentAreaName = x.OnlandVisualTrashAssessmentAreaName,
                OnlandVisualTrashAssessmentAreaBaselineScore = x.OnlandVisualTrashAssessmentAreaBaselineScore,
                WaterQualityManagementPlanID = x.WaterQualityManagementPlanID,
                WaterQualityManagementPlanName = x.WaterQualityManagementPlanName,
                TrashCaptureStatusWQMP = x.TrashCaptureStatusWQMP,
                TrashCaptureEffectivenessWQMP = x.TrashCaptureEffectivenessWQMP,
                LastUpdateDate = x.LastUpdateDate,
                MedianHouseholdIncomeResidential = x.MedianHouseholdIncomeResidential,
                MedianHouseholdIncomeRetail = x.MedianHouseholdIncomeRetail,
                PermitClass = x.PermitClass,
                LandUseForTGR = x.LandUseForTGR,
                TrashGenerationRate = x.TrashGenerationRate,
                Area = x.Area * Constants.SquareMetersToAcres,
                LoadingRateDelta = x.LoadingRateDelta,
        }).ToList();
        return trashGeneratingUnitLoadStatistics;
    }
}