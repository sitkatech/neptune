//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPTypeCustomAttributeType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeCustomAttributeTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPTypeCustomAttributeType>
    {
        public TreatmentBMPTypeCustomAttributeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypeCustomAttributeTypePrimaryKey(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType) : base(treatmentBMPTypeCustomAttributeType){}

        public static implicit operator TreatmentBMPTypeCustomAttributeTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypeCustomAttributeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypeCustomAttributeTypePrimaryKey(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            return new TreatmentBMPTypeCustomAttributeTypePrimaryKey(treatmentBMPTypeCustomAttributeType);
        }
    }
}