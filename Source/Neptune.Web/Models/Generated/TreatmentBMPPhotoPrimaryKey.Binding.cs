//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPPhoto
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPPhotoPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPPhoto>
    {
        public TreatmentBMPPhotoPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPPhotoPrimaryKey(TreatmentBMPPhoto treatmentBMPPhoto) : base(treatmentBMPPhoto){}

        public static implicit operator TreatmentBMPPhotoPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPPhotoPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPPhotoPrimaryKey(TreatmentBMPPhoto treatmentBMPPhoto)
        {
            return new TreatmentBMPPhotoPrimaryKey(treatmentBMPPhoto);
        }
    }
}