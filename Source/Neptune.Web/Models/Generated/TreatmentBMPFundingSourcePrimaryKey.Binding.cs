//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPFundingSource
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPFundingSourcePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPFundingSource>
    {
        public TreatmentBMPFundingSourcePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPFundingSourcePrimaryKey(TreatmentBMPFundingSource treatmentBMPFundingSource) : base(treatmentBMPFundingSource){}

        public static implicit operator TreatmentBMPFundingSourcePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPFundingSourcePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPFundingSourcePrimaryKey(TreatmentBMPFundingSource treatmentBMPFundingSource)
        {
            return new TreatmentBMPFundingSourcePrimaryKey(treatmentBMPFundingSource);
        }
    }
}