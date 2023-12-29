//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
namespace Neptune.EFModels.Entities
{
    public partial class QuickBMP : IHavePrimaryKey
    {
        public int PrimaryKey => QuickBMPID;
        public DryWeatherFlowOverride DryWeatherFlowOverride => DryWeatherFlowOverrideID.HasValue ? DryWeatherFlowOverride.AllLookupDictionary[DryWeatherFlowOverrideID.Value] : null;

        public static class FieldLengths
        {
            public const int QuickBMPName = 100;
            public const int QuickBMPNote = 200;
        }
    }
}