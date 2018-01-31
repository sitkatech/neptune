//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAttributeType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAttributeType>
    {
        public TreatmentBMPAttributeTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAttributeTypePrimaryKey(TreatmentBMPAttributeType treatmentBMPAttributeType) : base(treatmentBMPAttributeType){}

        public static implicit operator TreatmentBMPAttributeTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAttributeTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAttributeTypePrimaryKey(TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            return new TreatmentBMPAttributeTypePrimaryKey(treatmentBMPAttributeType);
        }
    }
}