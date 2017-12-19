//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMP
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMP>
    {
        public TreatmentBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPPrimaryKey(TreatmentBMP treatmentBMP) : base(treatmentBMP){}

        public static implicit operator TreatmentBMPPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPPrimaryKey(TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPPrimaryKey(treatmentBMP);
        }
    }
}