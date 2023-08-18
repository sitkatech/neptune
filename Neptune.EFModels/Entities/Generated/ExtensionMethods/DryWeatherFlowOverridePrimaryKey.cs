//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DryWeatherFlowOverride


namespace Neptune.EFModels.Entities
{
    public class DryWeatherFlowOverridePrimaryKey : EntityPrimaryKey<DryWeatherFlowOverride>
    {
        public DryWeatherFlowOverridePrimaryKey() : base(){}
        public DryWeatherFlowOverridePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DryWeatherFlowOverridePrimaryKey(DryWeatherFlowOverride dryWeatherFlowOverride) : base(dryWeatherFlowOverride){}

        public static implicit operator DryWeatherFlowOverridePrimaryKey(int primaryKeyValue)
        {
            return new DryWeatherFlowOverridePrimaryKey(primaryKeyValue);
        }

        public static implicit operator DryWeatherFlowOverridePrimaryKey(DryWeatherFlowOverride dryWeatherFlowOverride)
        {
            return new DryWeatherFlowOverridePrimaryKey(dryWeatherFlowOverride);
        }
    }
}