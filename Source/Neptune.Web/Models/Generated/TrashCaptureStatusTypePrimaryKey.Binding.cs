//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TrashCaptureStatusType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TrashCaptureStatusTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TrashCaptureStatusType>
    {
        public TrashCaptureStatusTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TrashCaptureStatusTypePrimaryKey(TrashCaptureStatusType trashCaptureStatusType) : base(trashCaptureStatusType){}

        public static implicit operator TrashCaptureStatusTypePrimaryKey(int primaryKeyValue)
        {
            return new TrashCaptureStatusTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TrashCaptureStatusTypePrimaryKey(TrashCaptureStatusType trashCaptureStatusType)
        {
            return new TrashCaptureStatusTypePrimaryKey(trashCaptureStatusType);
        }
    }
}