//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DryWeatherFlowOverride]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DryWeatherFlowOverrideExtensionMethods
    {
        public static DryWeatherFlowOverrideSimpleDto AsSimpleDto(this DryWeatherFlowOverride dryWeatherFlowOverride)
        {
            var dto = new DryWeatherFlowOverrideSimpleDto()
            {
                DryWeatherFlowOverrideID = dryWeatherFlowOverride.DryWeatherFlowOverrideID,
                DryWeatherFlowOverrideName = dryWeatherFlowOverride.DryWeatherFlowOverrideName,
                DryWeatherFlowOverrideDisplayName = dryWeatherFlowOverride.DryWeatherFlowOverrideDisplayName
            };
            return dto;
        }
    }
}