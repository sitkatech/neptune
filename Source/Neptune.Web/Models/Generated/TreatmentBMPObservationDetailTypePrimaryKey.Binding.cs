//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPObservationDetailType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationDetailTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPObservationDetailType>
    {
        public TreatmentBMPObservationDetailTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPObservationDetailTypePrimaryKey(TreatmentBMPObservationDetailType treatmentBMPObservationDetailType) : base(treatmentBMPObservationDetailType){}

        public static implicit operator TreatmentBMPObservationDetailTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPObservationDetailTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPObservationDetailTypePrimaryKey(TreatmentBMPObservationDetailType treatmentBMPObservationDetailType)
        {
            return new TreatmentBMPObservationDetailTypePrimaryKey(treatmentBMPObservationDetailType);
        }
    }
}