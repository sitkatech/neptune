//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessmentPhoto
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentPhotoPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAssessmentPhoto>
    {
        public TreatmentBMPAssessmentPhotoPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentPhotoPrimaryKey(TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto) : base(treatmentBMPAssessmentPhoto){}

        public static implicit operator TreatmentBMPAssessmentPhotoPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentPhotoPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentPhotoPrimaryKey(TreatmentBMPAssessmentPhoto treatmentBMPAssessmentPhoto)
        {
            return new TreatmentBMPAssessmentPhotoPrimaryKey(treatmentBMPAssessmentPhoto);
        }
    }
}