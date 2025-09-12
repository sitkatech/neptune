using Neptune.EFModels.Nereid;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPTypeExtensionMethods
{
    public static TreatmentBMPTypeSimpleDto AsSimpleDto(this TreatmentBMPType treatmentBMPType)
    {
        var dto = new TreatmentBMPTypeSimpleDto()
        {
            TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName,
            TreatmentBMPTypeDescription = treatmentBMPType.TreatmentBMPTypeDescription,
            IsAnalyzedInModelingModule = treatmentBMPType.IsAnalyzedInModelingModule,
            TreatmentBMPModelingTypeID = treatmentBMPType.TreatmentBMPModelingTypeID
        };
        return dto;
    }

    public static TreatmentBMPTypeDisplayDto AsDisplayDto(this TreatmentBMPType treatmentBMPType)
    {
        var treatmentBMPTypeDisplayDto = new TreatmentBMPTypeDisplayDto()
        {
            TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName,
        };
        return treatmentBMPTypeDisplayDto;
    }

    public static List<string> MissingModelingAttributes(this TreatmentBMPType treatmentBMPType, vTreatmentBMPModelingAttribute? treatmentBMPModelingAttribute)
    {
        var missingRequiredFields = new List<string>();

        if (!treatmentBMPType.IsAnalyzedInModelingModule)
        {
            return missingRequiredFields;
        }

        var treatmentBMPModelingTypeEnum = treatmentBMPType.TreatmentBMPModelingType.ToEnum;
        if (treatmentBMPModelingAttribute != null)
        {
            switch (treatmentBMPModelingTypeEnum)
            {
                case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                    CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                        treatmentBMPModelingAttribute.TotalEffectiveBMPVolume);
                    CheckFieldIsRequired(missingRequiredFields, "Storage Volume Below Lowest Outlet Elevation",
                        treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation);
                    CheckFieldIsRequired(missingRequiredFields, "Media Bed Footprint", treatmentBMPModelingAttribute.MediaBedFootprint);
                    CheckFieldIsRequired(missingRequiredFields, "Design Media Filtration Rate",
                        treatmentBMPModelingAttribute.DesignMediaFiltrationRate);
                    break;
                case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                case TreatmentBMPModelingTypeEnum.PermeablePavement:
                case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                    CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                        treatmentBMPModelingAttribute.TotalEffectiveBMPVolume);
                    CheckFieldIsRequired(missingRequiredFields, "Infiltration Surface Area",
                        treatmentBMPModelingAttribute.InfiltrationSurfaceArea);
                    CheckFieldIsRequired(missingRequiredFields, "Underlying Infiltration Rate",
                        treatmentBMPModelingAttribute.UnderlyingInfiltrationRate);
                    break;
                case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                case TreatmentBMPModelingTypeEnum.SandFilters:
                    CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                        treatmentBMPModelingAttribute.TotalEffectiveBMPVolume);
                    CheckFieldIsRequired(missingRequiredFields, "Media Bed Footprint", treatmentBMPModelingAttribute.MediaBedFootprint);
                    CheckFieldIsRequired(missingRequiredFields, "Design Media Filtration Rate",
                        treatmentBMPModelingAttribute.DesignMediaFiltrationRate);
                    break;
                case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                    CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                        treatmentBMPModelingAttribute.TotalEffectiveBMPVolume);
                    CheckFieldIsRequired(missingRequiredFields, "Winter Harvested Water Demand",
                        treatmentBMPModelingAttribute.WinterHarvestedWaterDemand);
                    CheckFieldIsRequired(missingRequiredFields, "Summer Harvested Water Demand",
                        treatmentBMPModelingAttribute.SummerHarvestedWaterDemand);
                    break;
                case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                    CheckFieldIsRequired(missingRequiredFields, "Permanent Pool or Wetland Volume",
                        treatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume);
                    CheckFieldIsRequired(missingRequiredFields, "Extended Detention Surcharge Volume",
                        treatmentBMPModelingAttribute.WaterQualityDetentionVolume);
                    break;
                case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                    CheckFieldIsRequired(missingRequiredFields, "Total Effective BMP Volume",
                        treatmentBMPModelingAttribute.TotalEffectiveBMPVolume);
                    CheckFieldIsRequired(missingRequiredFields, "Storage Volume Below Lowest Outlet Elevation",
                        treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation);
                    CheckFieldIsRequired(missingRequiredFields, "Effective Footprint", treatmentBMPModelingAttribute.EffectiveFootprint);
                    CheckFieldIsRequired(missingRequiredFields, "Extended Detention Surcharge Volume", treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume);
                    break;
                case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                    if (!treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity.HasValue && !treatmentBMPModelingAttribute.AverageTreatmentFlowrate.HasValue)
                    {
                        missingRequiredFields.Add("At least one of either Design Dry Weather Treatment Capacity or Average Treatment Flowrate is required");
                    }
                    break;
                case TreatmentBMPModelingTypeEnum.Drywell:
                    CheckFieldIsRequired(missingRequiredFields, "Total Effective Drywell BMP Volume",
                        treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume);
                    CheckFieldIsRequired(missingRequiredFields, "InfiltrationDischargeRate",
                        treatmentBMPModelingAttribute.InfiltrationDischargeRate);
                    break;
                case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                    CheckFieldIsRequired(missingRequiredFields, "Treatment Rate", treatmentBMPModelingAttribute.TreatmentRate);
                    break;
                case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                    if (!treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity.HasValue && !treatmentBMPModelingAttribute.AverageDivertedFlowrate.HasValue)
                    {
                        missingRequiredFields.Add("At least one of either Design Low Flow Diversion Capacity or Average Diverted Flowrate is required");
                    }
                    break;
                case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                    CheckFieldIsRequired(missingRequiredFields, "Treatment Rate", treatmentBMPModelingAttribute.TreatmentRate);
                    CheckFieldIsRequired(missingRequiredFields, "Wetted Footprint", treatmentBMPModelingAttribute.WettedFootprint);
                    CheckFieldIsRequired(missingRequiredFields, "Effective Retention Depth",
                        treatmentBMPModelingAttribute.EffectiveRetentionDepth);
                    break;
            }
        }

        return missingRequiredFields;
    }

    private static void CheckFieldIsRequired(List<string> validationResults, string fieldName, object valueToCheck)
    {
        if (valueToCheck == null)
        {
            validationResults.Add(fieldName);
        }
    }

    public static List<CustomAttributeTypeDto> GetModelingAttributes(this TreatmentBMPType treatmentBMPType)
    {
        if (!treatmentBMPType.IsAnalyzedInModelingModule)
        {
            return new List<CustomAttributeTypeDto>();
        }

        return treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes
            .Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID == (int)CustomAttributeTypePurposeEnum.Modeling)
            .Select(x => new CustomAttributeTypeDto
            {
                CustomAttributeTypeID = x.CustomAttributeTypeID,
                CustomAttributeTypeName = x.CustomAttributeType.CustomAttributeTypeName,
                CustomAttributeDataTypeID = x.CustomAttributeType.CustomAttributeDataTypeID,
                MeasurementUnitTypeID = x.CustomAttributeType.MeasurementUnitTypeID,
                IsRequired = x.CustomAttributeType.IsRequired,
                CustomAttributeTypeDescription = x.CustomAttributeType.CustomAttributeTypeDescription,
                CustomAttributeTypePurposeID = x.CustomAttributeType.CustomAttributeTypePurposeID,
                CustomAttributeTypeOptionsSchema = x.CustomAttributeType.CustomAttributeTypeOptionsSchema,
                DataTypeDisplayName = x.CustomAttributeType.CustomAttributeDataType.CustomAttributeDataTypeDisplayName,
                MeasurementUnitDisplayName = x.CustomAttributeType.MeasurementUnitType?.MeasurementUnitTypeDisplayName,
                Purpose = x.CustomAttributeType.CustomAttributeTypePurpose.CustomAttributeTypePurposeDisplayName,
                CustomAttributeTypeSortOrder = x.SortOrder
            }).ToList();
    }

    public static bool HasMissingRequiredCustomAttributes(this TreatmentBMPType treatmentBMPType, CustomAttributeTypePurposeEnum customAttributeTypePurposeEnum, ICollection<CustomAttribute> customAttributes)
    {
        return treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes
                .Any(x =>
                    x.CustomAttributeType.CustomAttributeTypePurposeID ==
                    (int)customAttributeTypePurposeEnum &&
                    x.CustomAttributeType.IsRequired &&
                    !customAttributes
                        .Select(y => y.CustomAttributeTypeID)
                        .Contains(x.CustomAttributeTypeID)) ||
            customAttributes.Any(x =>
                x.CustomAttributeType.CustomAttributeTypePurposeID ==
                (int)customAttributeTypePurposeEnum &&
                x.CustomAttributeType.IsRequired &&
                (x.CustomAttributeValues == null ||
                 x.CustomAttributeValues.All(y => string.IsNullOrEmpty(y.AttributeValue)))
            );
    }
}