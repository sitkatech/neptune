//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ModelBasinStaging


namespace Neptune.EFModels.Entities
{
    public class ModelBasinStagingPrimaryKey : EntityPrimaryKey<ModelBasinStaging>
    {
        public ModelBasinStagingPrimaryKey() : base(){}
        public ModelBasinStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ModelBasinStagingPrimaryKey(ModelBasinStaging modelBasinStaging) : base(modelBasinStaging){}

        public static implicit operator ModelBasinStagingPrimaryKey(int primaryKeyValue)
        {
            return new ModelBasinStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ModelBasinStagingPrimaryKey(ModelBasinStaging modelBasinStaging)
        {
            return new ModelBasinStagingPrimaryKey(modelBasinStaging);
        }
    }
}