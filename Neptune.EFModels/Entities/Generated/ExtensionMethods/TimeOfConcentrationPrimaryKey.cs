//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TimeOfConcentration


namespace Neptune.EFModels.Entities
{
    public class TimeOfConcentrationPrimaryKey : EntityPrimaryKey<TimeOfConcentration>
    {
        public TimeOfConcentrationPrimaryKey() : base(){}
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