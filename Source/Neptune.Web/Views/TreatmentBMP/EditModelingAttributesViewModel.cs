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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

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

        [FieldDefinitionDisplay(FieldDefinitionEnum.DesignResidenceTimeforPermanentPool)]
        public double? DesignResidenceTimeforPermanentPool { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DiversionRate)]
        public double? DiversionRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DrawdownTimeforWQDetentionVolume)]
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

        [FieldDefinitionDisplay(FieldDefinitionEnum.PermanentPoolorWetlandVolume)]
        public double? PermanentPoolorWetlandVolume { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.RoutingConfiguration)]
        public int? RoutingConfigurationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.StorageVolumeBelowLowestOutletElevation)]
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.SummerHarvestedWaterDemand)]
        public double? SummerHarvestedWaterDemand { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TimeofConcentration)]
        public int? TimeOfConcentrationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalDrawdownTime)]
        public double? TotalDrawdownTime { get; set; }

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

        [FieldDefinitionDisplay(FieldDefinitionEnum.MonthsofOperation)]
        public List<int> MonthsofOperation { get; set; }

        public int? TreatmentBMPModelingTypeID { get; set; }

        public EditModelingAttributesViewModel()
        {
        }

        public EditModelingAttributesViewModel(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute, List<int> treatmentBMPOperationMonths, int? treatmentBMPModelingTypeID)
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
                TotalDrawdownTime = treatmentBMPModelingAttribute.TotalDrawdownTime;
                TotalEffectiveBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveBMPVolume;
                TotalEffectiveDrywellBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume;
                TreatmentRate = treatmentBMPModelingAttribute.TreatmentRate;
                UnderlyingHydrologicSoilGroupID = treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroupID;
                UnderlyingInfiltrationRate = treatmentBMPModelingAttribute.UnderlyingInfiltrationRate;
                WaterQualityDetentionVolume = treatmentBMPModelingAttribute.WaterQualityDetentionVolume;
                WettedFootprint = treatmentBMPModelingAttribute.WettedFootprint;
                WinterHarvestedWaterDemand = treatmentBMPModelingAttribute.WinterHarvestedWaterDemand;
            }
            MonthsofOperation = treatmentBMPOperationMonths;
        }

        public void UpdateModel(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute,
            Person currentPerson,
            List<TreatmentBMPOperationMonth> treatmentBMPOperationMonths,
            ObservableCollection<TreatmentBMPOperationMonth> allTreatmentBMPOperationMonths)
        {
            //treatmentBMPModelingAttribute.UpstreamTreatmentBMPID = UpstreamTreatmentBMPID;
            treatmentBMPModelingAttribute.TotalEffectiveBMPVolume = TotalEffectiveBMPVolume;
            treatmentBMPModelingAttribute.AverageDivertedFlowrate = AverageDivertedFlowrate;
            treatmentBMPModelingAttribute.AverageTreatmentFlowrate = AverageTreatmentFlowrate;
            treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity = DesignDryWeatherTreatmentCapacity;
            treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity = DesignLowFlowDiversionCapacity;
            treatmentBMPModelingAttribute.DesignMediaFiltrationRate = DesignMediaFiltrationRate;
            treatmentBMPModelingAttribute.DesignResidenceTimeforPermanentPool = DesignResidenceTimeforPermanentPool;
            treatmentBMPModelingAttribute.DiversionRate = DiversionRate;
            treatmentBMPModelingAttribute.DrawdownTimeforWQDetentionVolume = DrawdownTimeforWQDetentionVolume;
            treatmentBMPModelingAttribute.EffectiveFootprint = EffectiveFootprint;
            treatmentBMPModelingAttribute.EffectiveRetentionDepth = EffectiveRetentionDepth;
            treatmentBMPModelingAttribute.InfiltrationDischargeRate = InfiltrationDischargeRate;
            treatmentBMPModelingAttribute.InfiltrationSurfaceArea = InfiltrationSurfaceArea;
            treatmentBMPModelingAttribute.MediaBedFootprint = MediaBedFootprint;
            treatmentBMPModelingAttribute.PermanentPoolorWetlandVolume = PermanentPoolorWetlandVolume;
            treatmentBMPModelingAttribute.RoutingConfigurationID = RoutingConfigurationID;
            treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation = StorageVolumeBelowLowestOutletElevation;
            treatmentBMPModelingAttribute.SummerHarvestedWaterDemand = SummerHarvestedWaterDemand;
            treatmentBMPModelingAttribute.TimeOfConcentrationID = TimeOfConcentrationID;
            treatmentBMPModelingAttribute.TotalDrawdownTime = TotalDrawdownTime;
            treatmentBMPModelingAttribute.TotalEffectiveBMPVolume = TotalEffectiveBMPVolume;
            treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume = TotalEffectiveDrywellBMPVolume;
            treatmentBMPModelingAttribute.TreatmentRate = TreatmentRate;
            treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroupID = UnderlyingHydrologicSoilGroupID;
            treatmentBMPModelingAttribute.UnderlyingInfiltrationRate = UnderlyingInfiltrationRate;
            treatmentBMPModelingAttribute.WaterQualityDetentionVolume = WaterQualityDetentionVolume;
            treatmentBMPModelingAttribute.WettedFootprint = WettedFootprint;
            treatmentBMPModelingAttribute.WinterHarvestedWaterDemand = WinterHarvestedWaterDemand;

            var postedMonthsOfOperation = new List<TreatmentBMPOperationMonth>();
            if (MonthsofOperation != null && MonthsofOperation.Any())
            {
                postedMonthsOfOperation = MonthsofOperation.Select(x => new TreatmentBMPOperationMonth(treatmentBMPModelingAttribute.TreatmentBMPID, x)).ToList();
            }

            treatmentBMPOperationMonths.Merge(postedMonthsOfOperation, allTreatmentBMPOperationMonths, (x, y) => x.OperationMonth == y.OperationMonth);
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
                    case TreatmentBMPModelingTypeEnum.Bioinfiltrationbioretentionwithraisedunderdrain:
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Storage Volume Below Lowest Outlet Elevation",
                            StorageVolumeBelowLowestOutletElevation);
                        ValidateFieldIsRequired(validationResults, "Media Bed Footprint", MediaBedFootprint);
                        ValidateFieldIsRequired(validationResults, "Design Media Filtration Rate",
                            DesignMediaFiltrationRate);
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionwithnoUnderdrain:
                    case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                    case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                    case TreatmentBMPModelingTypeEnum.PermeablePavement:
                    case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Infiltration Surface Area",
                            InfiltrationSurfaceArea);
                        ValidateFieldIsRequired(validationResults, "Underlying Infiltration Rate",
                            UnderlyingInfiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionwithUnderdrainandImperviousLiner:
                    case TreatmentBMPModelingTypeEnum.SandFilters:
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Media Bed Footprint", MediaBedFootprint);
                        ValidateFieldIsRequired(validationResults, "Design Media Filtration Rate",
                            DesignMediaFiltrationRate);
                        break;
                    case TreatmentBMPModelingTypeEnum.CisternsforHarvestandUse:
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Winter Harvested Water Demand",
                            WinterHarvestedWaterDemand);
                        ValidateFieldIsRequired(validationResults, "Summer Harvested Water Demand",
                            SummerHarvestedWaterDemand);
                        break;
                    case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                    case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
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
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
                        ValidateFieldIsRequired(validationResults, "Total Effective BMP Volume",
                            TotalEffectiveBMPVolume);
                        ValidateFieldIsRequired(validationResults, "Storage Volume Below Lowest Outlet Elevation",
                            StorageVolumeBelowLowestOutletElevation);
                        ValidateFieldIsRequired(validationResults, "Effective Footprint", EffectiveFootprint);
                        ValidateFieldIsRequired(validationResults, "Total Drawdown Time", TotalDrawdownTime);
                        break;
                    case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                        ValidateFieldIsRequired(validationResults, "Design Dry Weather Treatment Capacity",
                            DesignDryWeatherTreatmentCapacity);
                        ValidateFieldIsRequired(validationResults, "Average Treatment Flowrate",
                            AverageTreatmentFlowrate);
                        ValidateFieldIsRequired(validationResults, "Months of Operation", MonthsofOperation);
                        break;
                    case TreatmentBMPModelingTypeEnum.Drywell:
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
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
                        ValidateFieldIsRequired(validationResults, "Months of Operation", MonthsofOperation);
                        break;
                    case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                    case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                        ValidateFieldIsRequired(validationResults, "Routing Configuration", RoutingConfigurationID);
                        ValidateDiversionRate(validationResults);
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
