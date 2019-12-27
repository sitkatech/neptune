//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPModelingAttribute
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPModelingAttributePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPModelingAttribute>
    {
        public TreatmentBMPModelingAttributePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPModelingAttributePrimaryKey(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute) : base(treatmentBMPModelingAttribute){}

        public static implicit operator TreatmentBMPModelingAttributePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPModelingAttributePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPModelingAttributePrimaryKey(TreatmentBMPModelingAttribute treatmentBMPModelingAttribute)
        {
            return new TreatmentBMPModelingAttributePrimaryKey(treatmentBMPModelingAttribute);
        }
    }
}