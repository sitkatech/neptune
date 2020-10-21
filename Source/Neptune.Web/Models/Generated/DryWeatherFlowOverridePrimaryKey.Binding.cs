//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DryWeatherFlowOverride
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DryWeatherFlowOverridePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<DryWeatherFlowOverride>
    {
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