//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPTypeAttributeType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeAttributeTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPTypeAttributeType>
    {
        public TreatmentBMPTypeAttributeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypeAttributeTypePrimaryKey(TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeType) : base(treatmentBMPTypeAttributeType){}

        public static implicit operator TreatmentBMPTypeAttributeTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypeAttributeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypeAttributeTypePrimaryKey(TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeType)
        {
            return new TreatmentBMPTypeAttributeTypePrimaryKey(treatmentBMPTypeAttributeType);
        }
    }
}