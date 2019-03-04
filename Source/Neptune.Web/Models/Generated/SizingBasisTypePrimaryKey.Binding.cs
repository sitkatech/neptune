//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SizingBasisType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class SizingBasisTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<SizingBasisType>
    {
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