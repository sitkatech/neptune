//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
namespace Neptune.EFModels.Entities
{
    public partial class QuickBMP
    {
        public DryWeatherFlowOverride DryWeatherFlowOverride => DryWeatherFlowOverrideID.HasValue ? DryWeatherFlowOverride.AllLookupDictionary[DryWeatherFlowOverrideID.Value] : null;
    }
}