//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPType>
    {
        public TreatmentBMPTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPTypePrimaryKey(TreatmentBMPType treatmentBMPType) : base(treatmentBMPType){}

        public static implicit operator TreatmentBMPTypePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPTypePrimaryKey(TreatmentBMPType treatmentBMPType)
        {
            return new TreatmentBMPTypePrimaryKey(treatmentBMPType);
        }
    }
}