//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAttributeDataType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeDataTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAttributeDataType>
    {
        public TreatmentBMPAttributeDataTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAttributeDataTypePrimaryKey(TreatmentBMPAttributeDataType treatmentBMPAttributeDataType) : base(treatmentBMPAttributeDataType){}

        public static implicit operator TreatmentBMPAttributeDataTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAttributeDataTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAttributeDataTypePrimaryKey(TreatmentBMPAttributeDataType treatmentBMPAttributeDataType)
        {
            return new TreatmentBMPAttributeDataTypePrimaryKey(treatmentBMPAttributeDataType);
        }
    }
}