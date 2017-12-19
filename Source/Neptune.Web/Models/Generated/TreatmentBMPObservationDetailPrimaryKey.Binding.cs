//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPObservationDetail
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationDetailPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPObservationDetail>
    {
        public TreatmentBMPObservationDetailPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPObservationDetailPrimaryKey(TreatmentBMPObservationDetail treatmentBMPObservationDetail) : base(treatmentBMPObservationDetail){}

        public static implicit operator TreatmentBMPObservationDetailPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPObservationDetailPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPObservationDetailPrimaryKey(TreatmentBMPObservationDetail treatmentBMPObservationDetail)
        {
            return new TreatmentBMPObservationDetailPrimaryKey(treatmentBMPObservationDetail);
        }
    }
}