//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LoadGeneratingUnit4326


namespace Neptune.EFModels.Entities
{
    public class LoadGeneratingUnit4326PrimaryKey : EntityPrimaryKey<LoadGeneratingUnit4326>
    {
        public LoadGeneratingUnit4326PrimaryKey() : base(){}
        public LoadGeneratingUnit4326PrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LoadGeneratingUnit4326PrimaryKey(LoadGeneratingUnit4326 loadGeneratingUnit4326) : base(loadGeneratingUnit4326){}

        public static implicit operator LoadGeneratingUnit4326PrimaryKey(int primaryKeyValue)
        {
            return new LoadGeneratingUnit4326PrimaryKey(primaryKeyValue);
        }

        public static implicit operator LoadGeneratingUnit4326PrimaryKey(LoadGeneratingUnit4326 loadGeneratingUnit4326)
        {
            return new LoadGeneratingUnit4326PrimaryKey(loadGeneratingUnit4326);
        }
    }
}