//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPOperationMonth
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPOperationMonthPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPOperationMonth>
    {
        public TreatmentBMPOperationMonthPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPOperationMonthPrimaryKey(TreatmentBMPOperationMonth treatmentBMPOperationMonth) : base(treatmentBMPOperationMonth){}

        public static implicit operator TreatmentBMPOperationMonthPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPOperationMonthPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPOperationMonthPrimaryKey(TreatmentBMPOperationMonth treatmentBMPOperationMonth)
        {
            return new TreatmentBMPOperationMonthPrimaryKey(treatmentBMPOperationMonth);
        }
    }
}