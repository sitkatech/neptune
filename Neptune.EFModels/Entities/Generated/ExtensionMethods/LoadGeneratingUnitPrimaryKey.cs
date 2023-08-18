//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LoadGeneratingUnit


namespace Neptune.EFModels.Entities
{
    public class LoadGeneratingUnitPrimaryKey : EntityPrimaryKey<LoadGeneratingUnit>
    {
        public LoadGeneratingUnitPrimaryKey() : base(){}
        public LoadGeneratingUnitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LoadGeneratingUnitPrimaryKey(LoadGeneratingUnit loadGeneratingUnit) : base(loadGeneratingUnit){}

        public static implicit operator LoadGeneratingUnitPrimaryKey(int primaryKeyValue)
        {
            return new LoadGeneratingUnitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LoadGeneratingUnitPrimaryKey(LoadGeneratingUnit loadGeneratingUnit)
        {
            return new LoadGeneratingUnitPrimaryKey(loadGeneratingUnit);
        }
    }
}