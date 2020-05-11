/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency">
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

using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditModelingAttributesViewModel : FormViewModel, IValidatableObject
    {
        [FieldDefinitionDisplay(FieldDefinitionEnum.AverageDivertedFlowrate)]
        public double? AverageDivertedFlowrate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AverageTreatmentFlowrate)]
        public double? AverageTreatmentFlowrate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DesignDryWeatherTreatmentCapacity)]
        public double? DesignDryWeatherTreatmentCapacity { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DesignLowFlowDiversionCapacity)]
        public double? DesignLowFlowDiversionCapacity { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DesignMediaFiltrationRate)]
        public double? DesignMediaFiltrationRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DesignResidenceTimeForPermanentPool)]
        public double? DesignResidenceTimeforPermanentPool { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DiversionRate)]
        public double? DiversionRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DrawdownTimeForWQDetentionVolume)]
        public double? DrawdownTimeforWQDetentionVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.EffectiveFootprint)]
        public double? EffectiveFootprint { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.EffectiveRetentionDepth)]
        public double? EffectiveRetentionDepth { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InfiltrationDischargeRate)]
        public double? InfiltrationDischargeRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InfiltrationSurfaceArea)]
        public double? InfiltrationSurfaceArea { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.MediaBedFootprint)]
        public double? MediaBedFootprint { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PermanentPoolOrWetlandVolume)]
        public double? PermanentPoolorWetlandVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.RoutingConfiguration)]
        public int? RoutingConfigurationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.StorageVolumeBelowLowestOutletElevation)]
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.SummerHarvestedWaterDemand)]
        public double? SummerHarvestedWaterDemand { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TimeOfConcentration)]
        public int? TimeOfConcentrationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DrawdownTimeForDetentionVolume)]
        public double? DrawdownTimeForDetentionVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalEffectiveBMPVolume)]
        public double? TotalEffectiveBMPVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalEffectiveDrywellBMPVolume)]
        public double? TotalEffectiveDrywellBMPVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentRate)]
        public double? TreatmentRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.UnderlyingHydrologicSoilGroupHSG)]
        public int? UnderlyingHydrologicSoilGroupID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.UnderlyingInfiltrationRate)]
        public double? UnderlyingInfiltrationRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.WaterQualityDetentionVolume)]
        public double? WaterQualityDetentionVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.WettedFootprint)]
        public double? WettedFootprint { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.WinterHarvestedWaterDemand)]
        public double? WinterHarvestedWaterDemand { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.UpstreamBMP)]
        public int? UpstreamTreatmentBMPID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.MonthsOfOperation)]
        public int? OperationMonthID { get; set; }

        public int? TreatmentBMPModelingTypeID { get; set; }

        public EditModelingAttributesViewModel()
        {
        }

        public EditModelingAttributesViewModel(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute, int? treatmentBMPModelingTypeID)
        {
            TreatmentBMPModelingTypeID = treatmentBMPModelingTypeID;
            if (treatmentBMPModelingAttribute != null)
            {
                //UpstreamTreatmentBMPID = treatmentBMPModelingAttribute.UpstreamTreatmentBMPID;
                AverageDivertedFlowrate = treatmentBMPModelingAttribute.AverageDivertedFlowrate;
                AverageTreatmentFlowrate = treatmentBMPModelingAttribute.AverageTreatmentFlowrate;
                DesignDryWeatherTreatmentCapacity = treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity;
                DesignLowFlowDiversionCapacity = treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity;
                DesignMediaFiltrationRate = treatmentBMPModelingAttribute.DesignMediaFiltrationRate;
                DesignResidenceTimeforPermanentPool = treatmentBMPModelingAttribute.DesignResidenceTimeforPermanentPool;
                DiversionRate = treatmentBMPModelingAttribute.DiversionRate;
                DrawdownTimeforWQDetentionVolume = treatmentBMPModelingAttribute.DrawdownTimeforWQDetentionVolume;
                EffectiveFootprint = treatmentBMPModelingAttribute.EffectiveFootprint;
                EffectiveRetentionDepth = treatmentBMPModelingAttribute.EffectiveRetentionDepth;
                InfiltrationDischargeRate = treatmentBMPModelingAttribute.InfiltrationDischargeRate;
                InfiltrationSurfaceArea = treatmentBMPModelingAttribute.InfiltrationSurfaceArea;
                MediaBedFootprint = treatmentBMPModelingAttribute.MediaBedFootprint;
                PermanentPoolorWetlandVolume = treatmentBMPModelingAttribute.PermanentPoolorWetlandVolume;
                RoutingConfigurationID = treatmentBMPModelingAttribute.RoutingConfigurationID;
                StorageVolumeBelowLowestOutletElevation = treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation;
                SummerHarvestedWaterDemand = treatmentBMPModelingAttribute.SummerHarvestedWaterDemand;
                TimeOfConcentrationID = treatmentBMPModelingAttribute.TimeOfConcentrationID;
                DrawdownTimeForDetentionVolume = treatmentBMPModelingAttribute.DrawdownTimeForDetentionVolume;
                TotalEffectiveBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveBMPVolume;
                TotalEffectiveDrywellBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume;
                TreatmentRate = treatmentBMPModelingAttribute.TreatmentRate;
                UnderlyingHydrologicSoilGroupID = treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroupID;
                UnderlyingInfiltrationRate = treatmentBMPModelingAttribute.UnderlyingInfiltrationRate;
                WaterQualityDetentionVolume = treatmentBMPModelingAttribute.WaterQualityDetentionVolume;
                WettedFootprint = treatmentBMPModelingAttribute.WettedFootprint;
                WinterHarvestedWaterDemand = treatmentBMPModelingAttribute.WinterHarvestedWaterDemand;
                OperationMonthID = treatmentBMPModelingAttribute.OperationMonthID;
            }
        }

        public void UpdateModel(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute,
            Person currentPerson)
        {
            //treatmentBMPModelingAttribute.UpstreamTreatmentBMPID = UpstreamTreatmentBMPID;
            treatmentBMPModelingAttribute.TotalEffectiveBMPVolume = TotalEffectiveBMPVolume;
            treatmentBMPModelingAttribute.AverageDivertedFlowrate = AverageDivertedFlowrate;
            treatmentBMPModelingAttribute.AverageTreatmentFlowrate = AverageTreatmentFlowrate;
            treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity = DesignDryWeatherTreatmentCapacity;
            treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity = DesignLowFlowDiversionCapacity;
            treatmentBMPModelingAttribute.DesignMediaFiltrationRate = DesignMediaFiltrationRate;
            treatmentBMPModelingAttribute.DesignResidenceTimeforPermanentPool = DesignResidenceTimeforPermanentPool;
            treatmentBMPModelingAttribute.DiversionRate = null;
            treatmentBMPModelingAttribute.DrawdownTimeforWQDetentionVolume = DrawdownTimeforWQDetentionVolume;
            treatmentBMPModelingAttribute.EffectiveFootprint = EffectiveFootprint;
            treatmentBMPModelingAttribute.EffectiveRetentionDepth = EffectiveRetentionDepth;
            treatmentBMPModelingAttribute.InfiltrationDischargeRate = InfiltrationDischargeRate;
            treatmentBMPModelingAttribute.InfiltrationSurfaceArea = InfiltrationSurfaceArea;
            treatmentBMPModelingAttribute.MediaBedFootprint = MediaBedFootprint;
            treatmentBMPModelingAttribute.PermanentPoolorWetlandVolume = PermanentPoolorWetlandVolume;
            treatmentBMPModelingAttribute.RoutingConfigurationID = (int) RoutingConfigurationEnum.Online;
            treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation = StorageVolumeBelowLowestOutletElevation;
            treatmentBMPModelingAttribute.SummerHarvestedWaterDemand = SummerHarvestedWaterDemand;
            //Because some TreatmentBMPTypes see this, but others don't, check for null and then default to 5 minutes
            treatmentBMPModelingAttribute.TimeOfConcentrationID = TimeOfConcentrationID ?? (int) TimeOfConcentrationEnum.FiveMinutes;
            treatmentBMPModelingAttribute.DrawdownTimeForDetentionVolume = DrawdownTimeForDetentionVolume;
            treatmentBMPModelingAttribute.TotalEffectiveBMPVolume = TotalEffectiveBMPVolume;
            treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume = TotalEffectiveDrywellBMPVolume;
            treatmentBMPModelingAttribute.TreatmentRate = TreatmentRate;
            treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroupID = UnderlyingHydrologicSoilGroupID;
            treatmentBMPModelingAttribute.UnderlyingInfiltrationRate = UnderlyingInfiltrationRate;
            treatmentBMPModelingAttribute.WaterQualityDetentionVolume = WaterQualityDetentionVolume;
            treatmentBMPModelingAttribute.WettedFootprint = WettedFootprint;
            treatmentBMPModelingAttribute.WinterHarvestedWaterDemand = WinterHarvestedWaterDemand;
            treatmentBMPModelingAttribute.OperationMonthID = OperationMonthID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (TreatmentBMPModelingTypeID.HasValue)
            {
                var treatmentBMPModelingTypeEnum = TreatmentBMPModelingType
                    .AllLookupDictionary[TreatmentBMPModelingTypeID.Value].ToEnum;
                switch (treatmentBMPModelingTypeEnum)
                {
                    case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Storage Volume Below Lowest Outlet Elevation",
                            StorageVolumeBelowLowestOutletElevation);
                        ValidateFieldIsRequired(validationResults, "Media Bed Footprint", MediaBedFootprint);
                        ValidateFieldIsRequired(validationResults, "Design Media Filtration Rate",
                            DesignMediaFiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                    case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                    case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                    case TreatmentBMPModelingTypeEnum.PermeablePavement:
                    case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Infiltration Surface Area",
                            InfiltrationSurfaceArea);
                        ValidateFieldIsRequired(validationResults, "Underlying Infiltration Rate",
                            UnderlyingInfiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                    case TreatmentBMPModelingTypeEnum.SandFilters:
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Media Bed Footprint", MediaBedFootprint);
                        ValidateFieldIsRequired(validationResults, "Design Media Filtration Rate",
                            DesignMediaFiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Winter Harvested Water Demand",
                            WinterHarvestedWaterDemand);
                        ValidateFieldIsRequired(validationResults, "Summer Harvested Water Demand",
                            SummerHarvestedWaterDemand);
                        break;
                    case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                    case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                        ValidateFieldIsRequired(validationResults, "Permanent Pool or Wetland Volume",
                            PermanentPoolorWetlandVolume);
                        ValidateFieldIsRequired(validationResults, "Water Quality Detention Volume",
                            WaterQualityDetentionVolume);
                        ValidateFieldIsRequired(validationResults, "Drawdown Time for Water Quality Detention Volume",
                            DrawdownTimeforWQDetentionVolume);
                        ValidateFieldIsRequired(validationResults, "Winter Harvested Water Demand",
                            WinterHarvestedWaterDemand);
                        ValidateFieldIsRequired(validationResults, "Summer Harvested Water Demand",
                            SummerHarvestedWaterDemand);
                        break;
                    case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Storage Volume Below Lowest Outlet Elevation",
                            StorageVolumeBelowLowestOutletElevation);
                        ValidateFieldIsRequired(validationResults, "Effective Footprint", EffectiveFootprint);
                        ValidateFieldIsRequired(validationResults, "Drawdown Time For Water Quality Detention Volume", DrawdownTimeforWQDetentionVolume);
                        break;
                    case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                        if (!DesignDryWeatherTreatmentCapacity.HasValue && !AverageTreatmentFlowrate.HasValue)
                        {
                            validationResults.Add(new ValidationResult("At least one of either Design Dry Weather Treatment Capacity or Average Treatment Flowrate is required"));
                        }
                        break;
                    case TreatmentBMPModelingTypeEnum.Drywell:
                        ValidateFieldIsRequired(validationResults, "Total Effective Drywell BMP Volume",
                            TotalEffectiveDrywellBMPVolume);
                        ValidateFieldIsRequired(validationResults, "InfiltrationDischargeRate",
                            InfiltrationDischargeRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                    case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                    case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                        ValidateFieldIsRequired(validationResults, "Treatment Rate", TreatmentRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                        if (!DesignLowFlowDiversionCapacity.HasValue && !AverageDivertedFlowrate.HasValue)
                        {
                            validationResults.Add(new ValidationResult("At least one of either Design Low Flow Diversion Capacity or Average Diverted Flowrate is required"));
                        }
                        break;
                    case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                    case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                        ValidateFieldIsRequired(validationResults, "Treatment Rate", TreatmentRate);
                        ValidateFieldIsRequired(validationResults, "Wetted Footprint", WettedFootprint);
                        ValidateFieldIsRequired(validationResults, "Effective Retention Depth",
                            EffectiveRetentionDepth);
                        break;
                }
            }

            return validationResults;
        }

        private void ValidateDiversionRate(List<ValidationResult> validationResults)
        {
            if (RoutingConfigurationID == (int) RoutingConfigurationEnum.Offline)
            {
                if (DiversionRate == null)
                {
                    validationResults.Add(
                        new ValidationResult("Diversion Rate is required when Routing Configuration is 'Offline'"));
                }
            }
        }

        private static void ValidateFieldIsRequired(List<ValidationResult> validationResults, string fieldName, object valueToCheck)
        {
            if (valueToCheck == null)
            {
                validationResults.Add(new ValidationResult($"{fieldName} is required"));
            }
        }
    }
}
