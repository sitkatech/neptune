using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vHRUCharacteristics
{
    private static IQueryable<vHRUCharacteristic> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.vHRUCharacteristics.AsNoTracking();
    }
    public static List<vHRUCharacteristic> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).ToList();
    }

    public static List<vHRUCharacteristic> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext,
        int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(dbContext, waterQualityManagementPlanID);
        if (waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID == (int) WaterQualityManagementPlanModelingApproachEnum.Simplified)
        {
            return GetImpl(dbContext).Where(x =>
                x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
        }

        var treatmentBMPIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
        return GetImpl(dbContext).Where(x =>
            x.TreatmentBMPID.HasValue &&
            treatmentBMPIDs.Contains(x.TreatmentBMPID.Value)).ToList();
    }

    public static List<vHRUCharacteristic> ListByRegionalSubbasinID(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        return GetImpl(dbContext)
            .Where(x => x.RegionalSubbasinID == regionalSubbasinID).ToList();
    }

    public static List<vHRUCharacteristic> ListByLoadGeneratingUnitID(NeptuneDbContext dbContext,
        int loadGeneratingUnitID)
    {
        return GetImpl(dbContext).Where(x => x.LoadGeneratingUnitID == loadGeneratingUnitID).ToList();
    }

    public static List<vHRUCharacteristic> ListByTreatmentBMP(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP, Delineation? delineation)
    {
        if (delineation == null)
        {
            return new List<vHRUCharacteristic>();
        }

        if (delineation.DelineationType == DelineationType.Centralized && treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID.HasValue)
        {
            if (!treatmentBMP.RegionalSubbasinID.HasValue)
            {
                return new List<vHRUCharacteristic>(Enumerable.Empty<vHRUCharacteristic>());
            }
            var catchmentRegionalSubbasins = vRegionalSubbasinUpstreams.ListUpstreamRegionalBasinIDs(dbContext, treatmentBMP.RegionalSubbasinID.Value);

            return GetImpl(dbContext).Where(x =>
                x.RegionalSubbasinID != null &&
                catchmentRegionalSubbasins.Contains(x.RegionalSubbasinID.Value)).ToList();
        }

        return GetImpl(dbContext)
            .Where(x => x.TreatmentBMPID != null && x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToList();
    }

    //This should be done in the database
    //Waiting for a PR that has building Upstream RSBs as a sproc, and once that gets in we should use that, but I don't want to duplicate work
    public static Dictionary<int, double> ListAcreageSumByTreatmentBMPDictionary(NeptuneDbContext dbContext)
    {
        //Get all the HRU Characteristics the exist for individual BMPs (ie. non-centralized delineations)
        var acreageSumByTreatmentBMPDictionary = GetImpl(dbContext).Where(x => x.TreatmentBMPID != null).ToList().GroupBy(x => (int)x.TreatmentBMPID).ToDictionary(x => x.Key, x => x.Sum(y => y.Area));
        
        //For our modeled treatment BMPs that have centralized delineations
        var acreageSumByIndividualRegionalSubbasinDictionary = GetImpl(dbContext).Where(x => x.RegionalSubbasinID != null).ToList().GroupBy(x => (int)x.RegionalSubbasinID).ToDictionary(x => x.Key, x => x.Sum(y => y.Area));
        var acreageSumByRSBUpstreamDictionary = vRegionalSubbasinUpstreams.List(dbContext).GroupBy(x => (int)x.PrimaryKey).ToDictionary(x => x.Key, y => y.Sum(z => acreageSumByIndividualRegionalSubbasinDictionary.TryGetValue(z.RegionalSubbasinID.Value, out var value) ? value : 0));

        var remainingModeledTreatmentBMPs = TreatmentBMPs.ListModelingTreatmentBMPsWithCentralizedDelineations(dbContext)
            .Where(x => !acreageSumByTreatmentBMPDictionary.ContainsKey(x.TreatmentBMPID)).ToList();

        foreach (var remainingModeledTreatmentBMP in remainingModeledTreatmentBMPs)
        {
            
            acreageSumByTreatmentBMPDictionary.Add(remainingModeledTreatmentBMP.TreatmentBMPID, acreageSumByRSBUpstreamDictionary[remainingModeledTreatmentBMP.RegionalSubbasinID.Value]);
        }

        return acreageSumByTreatmentBMPDictionary;
    }
}