//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DelineationStaging


namespace Neptune.EFModels.Entities
{
    public class DelineationStagingPrimaryKey : EntityPrimaryKey<DelineationStaging>
    {
        public DelineationStagingPrimaryKey() : base(){}
        public DelineationStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationStagingPrimaryKey(DelineationStaging delineationStaging) : base(delineationStaging){}

        public static implicit operator DelineationStagingPrimaryKey(int primaryKeyValue)
        {
            return new DelineationStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationStagingPrimaryKey(DelineationStaging delineationStaging)
        {
            return new DelineationStagingPrimaryKey(delineationStaging);
        }
    }
}