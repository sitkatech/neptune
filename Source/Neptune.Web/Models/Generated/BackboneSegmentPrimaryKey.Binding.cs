//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.BackboneSegment
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class BackboneSegmentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<BackboneSegment>
    {
        public BackboneSegmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public BackboneSegmentPrimaryKey(BackboneSegment backboneSegment) : base(backboneSegment){}

        public static implicit operator BackboneSegmentPrimaryKey(int primaryKeyValue)
        {
            return new BackboneSegmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator BackboneSegmentPrimaryKey(BackboneSegment backboneSegment)
        {
            return new BackboneSegmentPrimaryKey(backboneSegment);
        }
    }
}