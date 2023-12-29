//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.LandUseBlock


namespace Neptune.EFModels.Entities
{
    public class LandUseBlockPrimaryKey : EntityPrimaryKey<LandUseBlock>
    {
        public LandUseBlockPrimaryKey() : base(){}
        public LandUseBlockPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public LandUseBlockPrimaryKey(LandUseBlock landUseBlock) : base(landUseBlock){}

        public static implicit operator LandUseBlockPrimaryKey(int primaryKeyValue)
        {
            return new LandUseBlockPrimaryKey(primaryKeyValue);
        }

        public static implicit operator LandUseBlockPrimaryKey(LandUseBlock landUseBlock)
        {
            return new LandUseBlockPrimaryKey(landUseBlock);
        }
    }
}