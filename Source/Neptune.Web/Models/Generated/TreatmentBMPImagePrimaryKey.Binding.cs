//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPImage
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPImagePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPImage>
    {
        public TreatmentBMPImagePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPImagePrimaryKey(TreatmentBMPImage treatmentBMPImage) : base(treatmentBMPImage){}

        public static implicit operator TreatmentBMPImagePrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPImagePrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPImagePrimaryKey(TreatmentBMPImage treatmentBMPImage)
        {
            return new TreatmentBMPImagePrimaryKey(treatmentBMPImage);
        }
    }
}