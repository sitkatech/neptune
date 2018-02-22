//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAttributeTypePurpose
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeTypePurposePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAttributeTypePurpose>
    {
        public TreatmentBMPAttributeTypePurposePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAttributeTypePurposePrimaryKey(TreatmentBMPAttributeTypePurpose treatmentBMPAttributeTypePurpose) : base(treatmentBMPAttributeTypePurpose){}

        public static implicit operator TreatmentBMPAttributeTypePurposePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAttributeTypePurposePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAttributeTypePurposePrimaryKey(TreatmentBMPAttributeTypePurpose treatmentBMPAttributeTypePurpose)
        {
            return new TreatmentBMPAttributeTypePurposePrimaryKey(treatmentBMPAttributeTypePurpose);
        }
    }
}