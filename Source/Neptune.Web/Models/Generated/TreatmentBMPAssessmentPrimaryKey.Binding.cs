//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPAssessment
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPAssessment>
    {
        public TreatmentBMPAssessmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPAssessmentPrimaryKey(TreatmentBMPAssessment treatmentBMPAssessment) : base(treatmentBMPAssessment){}

        public static implicit operator TreatmentBMPAssessmentPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPAssessmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPAssessmentPrimaryKey(TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return new TreatmentBMPAssessmentPrimaryKey(treatmentBMPAssessment);
        }
    }
}