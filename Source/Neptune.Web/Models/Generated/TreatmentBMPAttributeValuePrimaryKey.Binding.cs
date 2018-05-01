//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAttributeValue
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeValuePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAttributeValue>
    {
        public TreatmentBMPAttributeValuePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAttributeValuePrimaryKey(TreatmentBMPAttributeValue treatmentBMPAttributeValue) : base(treatmentBMPAttributeValue){}

        public static implicit operator TreatmentBMPAttributeValuePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAttributeValuePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAttributeValuePrimaryKey(TreatmentBMPAttributeValue treatmentBMPAttributeValue)
        {
            return new TreatmentBMPAttributeValuePrimaryKey(treatmentBMPAttributeValue);
        }
    }
}