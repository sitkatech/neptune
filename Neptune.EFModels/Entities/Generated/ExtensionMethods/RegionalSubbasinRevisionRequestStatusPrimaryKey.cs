//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RegionalSubbasinRevisionRequestStatus


namespace Neptune.EFModels.Entities
{
    public class RegionalSubbasinRevisionRequestStatusPrimaryKey : EntityPrimaryKey<RegionalSubbasinRevisionRequestStatus>
    {
        public RegionalSubbasinRevisionRequestStatusPrimaryKey() : base(){}
        public RegionalSubbasinRevisionRequestStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RegionalSubbasinRevisionRequestStatusPrimaryKey(RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus) : base(regionalSubbasinRevisionRequestStatus){}

        public static implicit operator RegionalSubbasinRevisionRequestStatusPrimaryKey(int primaryKeyValue)
        {
            return new RegionalSubbasinRevisionRequestStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator RegionalSubbasinRevisionRequestStatusPrimaryKey(RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus)
        {
            return new RegionalSubbasinRevisionRequestStatusPrimaryKey(regionalSubbasinRevisionRequestStatus);
        }
    }
}