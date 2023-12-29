//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingAttribute]
namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPModelingAttribute : IHavePrimaryKey
    {
        public int PrimaryKey => TreatmentBMPModelingAttributeID;
        public RoutingConfiguration RoutingConfiguration => RoutingConfigurationID.HasValue ? RoutingConfiguration.AllLookupDictionary[RoutingConfigurationID.Value] : null;
        public TimeOfConcentration TimeOfConcentration => TimeOfConcentrationID.HasValue ? TimeOfConcentration.AllLookupDictionary[TimeOfConcentrationID.Value] : null;
        public UnderlyingHydrologicSoilGroup UnderlyingHydrologicSoilGroup => UnderlyingHydrologicSoilGroupID.HasValue ? UnderlyingHydrologicSoilGroup.AllLookupDictionary[UnderlyingHydrologicSoilGroupID.Value] : null;
        public MonthsOfOperation MonthsOfOperation => MonthsOfOperationID.HasValue ? MonthsOfOperation.AllLookupDictionary[MonthsOfOperationID.Value] : null;
        public DryWeatherFlowOverride DryWeatherFlowOverride => DryWeatherFlowOverrideID.HasValue ? DryWeatherFlowOverride.AllLookupDictionary[DryWeatherFlowOverrideID.Value] : null;

        public static class FieldLengths
        {

        }
    }
}