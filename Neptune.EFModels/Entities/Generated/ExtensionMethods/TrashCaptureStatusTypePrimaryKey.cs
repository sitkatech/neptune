//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TrashCaptureStatusType


namespace Neptune.EFModels.Entities
{
    public class TrashCaptureStatusTypePrimaryKey : EntityPrimaryKey<TrashCaptureStatusType>
    {
        public TrashCaptureStatusTypePrimaryKey() : base(){}
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