//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DryWeatherFlowOverride]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DryWeatherFlowOverrideExtensionMethods
    {
        public static DryWeatherFlowOverrideDto AsDto(this DryWeatherFlowOverride dryWeatherFlowOverride)
        {
            var dryWeatherFlowOverrideDto = new DryWeatherFlowOverrideDto()
            {
                DryWeatherFlowOverrideID = dryWeatherFlowOverride.DryWeatherFlowOverrideID,
                DryWeatherFlowOverrideName = dryWeatherFlowOverride.DryWeatherFlowOverrideName,
                DryWeatherFlowOverrideDisplayName = dryWeatherFlowOverride.DryWeatherFlowOverrideDisplayName
            };
            DoCustomMappings(dryWeatherFlowOverride, dryWeatherFlowOverrideDto);
            return dryWeatherFlowOverrideDto;
        }

        static partial void DoCustomMappings(DryWeatherFlowOverride dryWeatherFlowOverride, DryWeatherFlowOverrideDto dryWeatherFlowOverrideDto);

        public static DryWeatherFlowOverrideSimpleDto AsSimpleDto(this DryWeatherFlowOverride dryWeatherFlowOverride)
        {
            var dryWeatherFlowOverrideSimpleDto = new DryWeatherFlowOverrideSimpleDto()
            {
                DryWeatherFlowOverrideID = dryWeatherFlowOverride.DryWeatherFlowOverrideID,
                DryWeatherFlowOverrideName = dryWeatherFlowOverride.DryWeatherFlowOverrideName,
                DryWeatherFlowOverrideDisplayName = dryWeatherFlowOverride.DryWeatherFlowOverrideDisplayName
            };
            DoCustomSimpleDtoMappings(dryWeatherFlowOverride, dryWeatherFlowOverrideSimpleDto);
            return dryWeatherFlowOverrideSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(DryWeatherFlowOverride dryWeatherFlowOverride, DryWeatherFlowOverrideSimpleDto dryWeatherFlowOverrideSimpleDto);
    }
}