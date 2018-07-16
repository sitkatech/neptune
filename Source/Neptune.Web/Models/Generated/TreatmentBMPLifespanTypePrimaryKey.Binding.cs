//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPLifespanType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPLifespanTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPLifespanType>
    {
        public TreatmentBMPLifespanTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPLifespanTypePrimaryKey(TreatmentBMPLifespanType treatmentBMPLifespanType) : base(treatmentBMPLifespanType){}

        public static implicit operator TreatmentBMPLifespanTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPLifespanTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPLifespanTypePrimaryKey(TreatmentBMPLifespanType treatmentBMPLifespanType)
        {
            return new TreatmentBMPLifespanTypePrimaryKey(treatmentBMPLifespanType);
        }
    }
}