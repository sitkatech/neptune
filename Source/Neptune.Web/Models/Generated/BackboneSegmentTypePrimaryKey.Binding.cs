//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.BackboneSegmentType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class BackboneSegmentTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<BackboneSegmentType>
    {
        public BackboneSegmentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public BackboneSegmentTypePrimaryKey(BackboneSegmentType backboneSegmentType) : base(backboneSegmentType){}

        public static implicit operator BackboneSegmentTypePrimaryKey(int primaryKeyValue)
        {
            return new BackboneSegmentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator BackboneSegmentTypePrimaryKey(BackboneSegmentType backboneSegmentType)
        {
            return new BackboneSegmentTypePrimaryKey(backboneSegmentType);
        }
    }
}