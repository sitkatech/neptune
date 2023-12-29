//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SizingBasisType


namespace Neptune.EFModels.Entities
{
    public class SizingBasisTypePrimaryKey : EntityPrimaryKey<SizingBasisType>
    {
        public SizingBasisTypePrimaryKey() : base(){}
        public SizingBasisTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SizingBasisTypePrimaryKey(SizingBasisType sizingBasisType) : base(sizingBasisType){}

        public static implicit operator SizingBasisTypePrimaryKey(int primaryKeyValue)
        {
            return new SizingBasisTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator SizingBasisTypePrimaryKey(SizingBasisType sizingBasisType)
        {
            return new SizingBasisTypePrimaryKey(sizingBasisType);
        }
    }
}