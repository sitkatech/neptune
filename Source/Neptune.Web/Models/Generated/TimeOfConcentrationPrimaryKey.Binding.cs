//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TimeOfConcentration
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TimeOfConcentrationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TimeOfConcentration>
    {
        public TimeOfConcentrationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TimeOfConcentrationPrimaryKey(TimeOfConcentration timeOfConcentration) : base(timeOfConcentration){}

        public static implicit operator TimeOfConcentrationPrimaryKey(int primaryKeyValue)
        {
            return new TimeOfConcentrationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TimeOfConcentrationPrimaryKey(TimeOfConcentration timeOfConcentration)
        {
            return new TimeOfConcentrationPrimaryKey(timeOfConcentration);
        }
    }
}