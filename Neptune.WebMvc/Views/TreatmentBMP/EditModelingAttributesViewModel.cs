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

using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class EditModelingAttributesViewModel : FormViewModel//, IValidatableObject
    {
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.AverageDivertedFlowrate)]
        public double? AverageDivertedFlowrate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.AverageTreatmentFlowrate)]
        public double? AverageTreatmentFlowrate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.DesignDryWeatherTreatmentCapacity)]
        public double? DesignDryWeatherTreatmentCapacity { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.DesignLowFlowDiversionCapacity)]
        public double? DesignLowFlowDiversionCapacity { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.DesignMediaFiltrationRate)]
        public double? DesignMediaFiltrationRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.DiversionRate)]
        public double? DiversionRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.DrawdownTimeForWQDetentionVolume)]
        public double? DrawdownTimeforWQDetentionVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.EffectiveFootprint)]
        public double? EffectiveFootprint { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.EffectiveRetentionDepth)]
        public double? EffectiveRetentionDepth { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.InfiltrationDischargeRate)]
        public double? InfiltrationDischargeRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.InfiltrationSurfaceArea)]
        public double? InfiltrationSurfaceArea { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.MediaBedFootprint)]
        public double? MediaBedFootprint { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.PermanentPoolOrWetlandVolume)]
        public double? PermanentPoolorWetlandVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.RoutingConfiguration)]
        public int? RoutingConfigurationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.StorageVolumeBelowLowestOutletElevation)]
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.SummerHarvestedWaterDemand)]
        public double? SummerHarvestedWaterDemand { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TimeOfConcentrationID)]
        public int? TimeOfConcentrationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.DrawdownTimeForDetentionVolume)]
        public double? DrawdownTimeForDetentionVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TotalEffectiveBMPVolume)]
        public double? TotalEffectiveBMPVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TotalEffectiveDrywellBMPVolume)]
        public double? TotalEffectiveDrywellBMPVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TreatmentRate)]
        public double? TreatmentRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.UnderlyingHydrologicSoilGroupID)]
        public int? UnderlyingHydrologicSoilGroupID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.UnderlyingInfiltrationRate)]
        public double? UnderlyingInfiltrationRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.WaterQualityDetentionVolume)]
        public double? WaterQualityDetentionVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.WettedFootprint)]
        public double? WettedFootprint { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.WinterHarvestedWaterDemand)]
        public double? WinterHarvestedWaterDemand { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.MonthsOfOperationID)]
        public int? MonthsOfOperationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.DryWeatherFlowOverrideID)]
        public int? DryWeatherFlowOverrideID { get; set; }

        public int? TreatmentBMPModelingTypeID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
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
                DiversionRate = treatmentBMPModelingAttribute.DiversionRate;
                DrawdownTimeforWQDetentionVolume = treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume;
                EffectiveFootprint = treatmentBMPModelingAttribute.EffectiveFootprint;
                EffectiveRetentionDepth = treatmentBMPModelingAttribute.EffectiveRetentionDepth;
                InfiltrationDischargeRate = treatmentBMPModelingAttribute.InfiltrationDischargeRate;
                InfiltrationSurfaceArea = treatmentBMPModelingAttribute.InfiltrationSurfaceArea;
                MediaBedFootprint = treatmentBMPModelingAttribute.MediaBedFootprint;
                PermanentPoolorWetlandVolume = treatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume;
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
                MonthsOfOperationID = treatmentBMPModelingAttribute.MonthsOfOperationID;
                DryWeatherFlowOverrideID = treatmentBMPModelingAttribute.DryWeatherFlowOverrideID;
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
            treatmentBMPModelingAttribute.DiversionRate = null;
            treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume = DrawdownTimeforWQDetentionVolume;
            treatmentBMPModelingAttribute.EffectiveFootprint = EffectiveFootprint;
            treatmentBMPModelingAttribute.EffectiveRetentionDepth = EffectiveRetentionDepth;
            treatmentBMPModelingAttribute.InfiltrationDischargeRate = InfiltrationDischargeRate;
            treatmentBMPModelingAttribute.InfiltrationSurfaceArea = InfiltrationSurfaceArea;
            treatmentBMPModelingAttribute.MediaBedFootprint = MediaBedFootprint;
            treatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume = PermanentPoolorWetlandVolume;
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
            treatmentBMPModelingAttribute.MonthsOfOperationID = MonthsOfOperationID;
            treatmentBMPModelingAttribute.DryWeatherFlowOverrideID = DryWeatherFlowOverrideID;
        }

        public List<string> CheckForRequiredFields()
        {
            var missingRequiredFields = new List<string>();
            if (TreatmentBMPModelingTypeID.HasValue)
            {
                var treatmentBMPModelingTypeEnum = TreatmentBMPModelingType
                    .AllLookupDictionary[TreatmentBMPModelingTypeID.Value].ToEnum;
                switch (treatmentBMPModelingTypeEnum)
                {
                    case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                        CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        CheckFieldIsRequired(missingRequiredFields, "Storage Volume Below Lowest Outlet Elevation",
                            StorageVolumeBelowLowestOutletElevation);
                        CheckFieldIsRequired(missingRequiredFields, "Media Bed Footprint", MediaBedFootprint);
                        CheckFieldIsRequired(missingRequiredFields, "Design Media Filtration Rate",
                            DesignMediaFiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                    case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                    case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                    case TreatmentBMPModelingTypeEnum.PermeablePavement:
                    case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                        CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        CheckFieldIsRequired(missingRequiredFields, "Infiltration Surface Area",
                            InfiltrationSurfaceArea);
                        CheckFieldIsRequired(missingRequiredFields, "Underlying Infiltration Rate",
                            UnderlyingInfiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                    case TreatmentBMPModelingTypeEnum.SandFilters:
                        CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        CheckFieldIsRequired(missingRequiredFields, "Media Bed Footprint", MediaBedFootprint);
                        CheckFieldIsRequired(missingRequiredFields, "Design Media Filtration Rate",
                            DesignMediaFiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                        CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        CheckFieldIsRequired(missingRequiredFields, "Winter Harvested Water Demand",
                            WinterHarvestedWaterDemand);
                        CheckFieldIsRequired(missingRequiredFields, "Summer Harvested Water Demand",
                            SummerHarvestedWaterDemand);
                        break;
                    case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                    case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                        CheckFieldIsRequired(missingRequiredFields, "Permanent Pool or Wetland Volume",
                            PermanentPoolorWetlandVolume);
                        CheckFieldIsRequired(missingRequiredFields, "Extended Detention Surcharge Volume",
                            WaterQualityDetentionVolume);
                        break;
                    case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                        CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        CheckFieldIsRequired(missingRequiredFields, "Storage Volume Below Lowest Outlet Elevation",
                            StorageVolumeBelowLowestOutletElevation);
                        CheckFieldIsRequired(missingRequiredFields, "Effective Footprint", EffectiveFootprint);
                        CheckFieldIsRequired(missingRequiredFields, "Extended Detention Surcharge Volume", DrawdownTimeforWQDetentionVolume);
                        break;
                    case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                        if (!DesignDryWeatherTreatmentCapacity.HasValue && !AverageTreatmentFlowrate.HasValue)
                        {
                            missingRequiredFields.Add("At least one of either Design Dry Weather Treatment Capacity or Average Treatment Flowrate is required");
                        }
                        break;
                    case TreatmentBMPModelingTypeEnum.Drywell:
                        CheckFieldIsRequired(missingRequiredFields, "Total Effective Drywell BMP Volume",
                            TotalEffectiveDrywellBMPVolume);
                        CheckFieldIsRequired(missingRequiredFields, "InfiltrationDischargeRate",
                            InfiltrationDischargeRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                    case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                    case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                        CheckFieldIsRequired(missingRequiredFields, "Treatment Rate", TreatmentRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                        if (!DesignLowFlowDiversionCapacity.HasValue && !AverageDivertedFlowrate.HasValue)
                        {
                            missingRequiredFields.Add("At least one of either Design Low Flow Diversion Capacity or Average Diverted Flowrate is required");
                        }
                        break;
                    case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                    case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                        CheckFieldIsRequired(missingRequiredFields, "Treatment Rate", TreatmentRate);
                        CheckFieldIsRequired(missingRequiredFields, "Wetted Footprint", WettedFootprint);
                        CheckFieldIsRequired(missingRequiredFields, "Effective Retention Depth",
                            EffectiveRetentionDepth);
                        break;
                }
            }

            return missingRequiredFields;
        }

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
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
                        ValidateFieldIsRequired(validationResults, "Extended Detention Surcharge Volume",
                            WaterQualityDetentionVolume);
                        break;
                    case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Storage Volume Below Lowest Outlet Elevation",
                            StorageVolumeBelowLowestOutletElevation);
                        ValidateFieldIsRequired(validationResults, "Effective Footprint", EffectiveFootprint);
                        ValidateFieldIsRequired(validationResults, "Extended Detention Surcharge Volume", DrawdownTimeforWQDetentionVolume);
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
        }*/

        private static void ValidateFieldIsRequired(List<ValidationResult> validationResults, string fieldName, object valueToCheck)
        {
            if (valueToCheck == null)
            {
                validationResults.Add(new ValidationResult($"{fieldName} is required"));
            }
        }

        private static void CheckFieldIsRequired(List<string> validationResults, string fieldName, object valueToCheck)
        {
            if (valueToCheck == null)
            {
                validationResults.Add(fieldName);
            }
        }
    }
}
