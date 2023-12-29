//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]
namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPAssessment : IHavePrimaryKey
    {
        public int PrimaryKey => TreatmentBMPAssessmentID;
        public TreatmentBMPAssessmentType TreatmentBMPAssessmentType => TreatmentBMPAssessmentType.AllLookupDictionary[TreatmentBMPAssessmentTypeID];

        public static class FieldLengths
        {
            public const int Notes = 1000;
        }
    }
}