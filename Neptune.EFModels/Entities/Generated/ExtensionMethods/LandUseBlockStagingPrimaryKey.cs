//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LandUseBlockStaging


namespace Neptune.EFModels.Entities
{
    public class LandUseBlockStagingPrimaryKey : EntityPrimaryKey<LandUseBlockStaging>
    {
        public LandUseBlockStagingPrimaryKey() : base(){}
        public LandUseBlockStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LandUseBlockStagingPrimaryKey(LandUseBlockStaging landUseBlockStaging) : base(landUseBlockStaging){}

        public static implicit operator LandUseBlockStagingPrimaryKey(int primaryKeyValue)
        {
            return new LandUseBlockStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LandUseBlockStagingPrimaryKey(LandUseBlockStaging landUseBlockStaging)
        {
            return new LandUseBlockStagingPrimaryKey(landUseBlockStaging);
        }
    }
}