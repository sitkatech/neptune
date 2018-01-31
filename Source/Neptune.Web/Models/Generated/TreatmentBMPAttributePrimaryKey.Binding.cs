//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAttribute
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAttribute>
    {
        public TreatmentBMPAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAttributePrimaryKey(TreatmentBMPAttribute treatmentBMPAttribute) : base(treatmentBMPAttribute){}

        public static implicit operator TreatmentBMPAttributePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAttributePrimaryKey(TreatmentBMPAttribute treatmentBMPAttribute)
        {
            return new TreatmentBMPAttributePrimaryKey(treatmentBMPAttribute);
        }
    }
}