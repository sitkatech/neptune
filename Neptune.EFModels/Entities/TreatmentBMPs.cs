/*-----------------------------------------------------------------------
<copyright file="TreatmentBMP.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public static class TreatmentBMPs
    {
        public static List<TreatmentBMP> GetProvisionalTreatmentBMPs(NeptuneDbContext dbContext, Person currentPerson)
        {
            return GetNonPlanningModuleBMPs(dbContext).Where(x => x.InventoryIsVerified == false).ToList().Where(x => x.CanView(currentPerson)).OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static IQueryable<TreatmentBMP> GetNonPlanningModuleBMPs(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs.AsNoTracking()
                .Include(x => x.TreatmentBMPBenchmarkAndThresholdTreatmentBMPs)
                .Include(x => x.StormwaterJurisdiction)
                .Include(x => x.TreatmentBMPType)
                .ThenInclude(x => x.TreatmentBMPTypeAssessmentObservationTypes)
                .Include(x => x.TreatmentBMPImages)
                .Where(x => x.ProjectID == null);
        }

        public static bool HasVerifiedDelineationForModelingPurposes(this TreatmentBMP treatmentBMP, List<int> treatmentBmpiDsTraversed)
        {
            if (treatmentBMP.UpstreamBMP != null)
            {
                if (treatmentBmpiDsTraversed.Contains(treatmentBMP.TreatmentBMPID))
                {
                    throw new OverflowException($"Infinite loop detected!  TreatmentBMPID {treatmentBMP.TreatmentBMPID} already in list of traversed TreatmentBMPIDs ({string.Join(", ", treatmentBmpiDsTraversed)})");
                }
                treatmentBmpiDsTraversed.Add(treatmentBMP.TreatmentBMPID);
                return treatmentBMP.UpstreamBMP.HasVerifiedDelineationForModelingPurposes(treatmentBmpiDsTraversed);
            }

            //Project BMPs don't need verified delineations
            if (treatmentBMP.ProjectID != null)
            {
                return true;
            }

            return treatmentBMP.Delineation?.IsVerified ?? false;
        }

        public static bool IsFullyParameterized(this TreatmentBMP treatmentBMP)
        {
            if (!treatmentBMP.HasVerifiedDelineationForModelingPurposes(new List<int>()))
            {
                return false;
            }

            if (treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType == null)
            {
                return false;
            }

            var bmpModelingType = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.ToEnum;
            var bmpModelingAttributes = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP;

            if (bmpModelingAttributes == null)
            {
                return false;
            }

            switch (bmpModelingType)
            {
                case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                    !bmpModelingAttributes.StorageVolumeBelowLowestOutletElevation.HasValue ||
                    !bmpModelingAttributes.MediaBedFootprint.HasValue ||
                    !bmpModelingAttributes.DesignMediaFiltrationRate.HasValue):

                case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain or TreatmentBMPModelingTypeEnum.InfiltrationBasin or TreatmentBMPModelingTypeEnum.InfiltrationTrench or 
                    TreatmentBMPModelingTypeEnum.PermeablePavement or TreatmentBMPModelingTypeEnum.UndergroundInfiltration when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                    !bmpModelingAttributes.InfiltrationSurfaceArea.HasValue ||
                    !bmpModelingAttributes.UnderlyingInfiltrationRate.HasValue):

                case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner or TreatmentBMPModelingTypeEnum.SandFilters when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                    !bmpModelingAttributes.MediaBedFootprint.HasValue ||
                    !bmpModelingAttributes.DesignMediaFiltrationRate.HasValue):

                case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.TotalEffectiveBMPVolume.HasValue ||
                    !bmpModelingAttributes.WinterHarvestedWaterDemand.HasValue ||
                    !bmpModelingAttributes.SummerHarvestedWaterDemand.HasValue):

                case TreatmentBMPModelingTypeEnum.ConstructedWetland or TreatmentBMPModelingTypeEnum.WetDetentionBasin when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.PermanentPoolorWetlandVolume.HasValue ||
                    !bmpModelingAttributes.WaterQualityDetentionVolume.HasValue):

                case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin or TreatmentBMPModelingTypeEnum.FlowDurationControlBasin or TreatmentBMPModelingTypeEnum.FlowDurationControlTank when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.DrawdownTimeforWQDetentionVolume.HasValue ||
                    !bmpModelingAttributes.StorageVolumeBelowLowestOutletElevation.HasValue ||
                    !bmpModelingAttributes.EffectiveFootprint.HasValue):

                case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems when (
                    !bmpModelingAttributes.DesignDryWeatherTreatmentCapacity.HasValue &&
                    !bmpModelingAttributes.AverageTreatmentFlowrate.HasValue):

                case TreatmentBMPModelingTypeEnum.Drywell when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.TotalEffectiveDrywellBMPVolume.HasValue ||
                    !bmpModelingAttributes.InfiltrationDischargeRate.HasValue):

                case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator or TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment or TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl when 
                    !bmpModelingAttributes.TreatmentRate.HasValue:

                case TreatmentBMPModelingTypeEnum.LowFlowDiversions when (
                    !bmpModelingAttributes.DesignLowFlowDiversionCapacity.HasValue &&
                    !bmpModelingAttributes.AverageDivertedFlowrate.HasValue):

                case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip or TreatmentBMPModelingTypeEnum.VegetatedSwale when (
                    !bmpModelingAttributes.RoutingConfigurationID.HasValue ||
                    (bmpModelingAttributes.RoutingConfigurationID == (int)RoutingConfigurationEnum.Offline &&
                     !bmpModelingAttributes.DiversionRate.HasValue) ||
                    !bmpModelingAttributes.TreatmentRate.HasValue ||
                    !bmpModelingAttributes.WettedFootprint.HasValue ||
                    !bmpModelingAttributes.EffectiveRetentionDepth.HasValue):

                    return false;

                default: 
                    return true;
            }
        }
    }

}
