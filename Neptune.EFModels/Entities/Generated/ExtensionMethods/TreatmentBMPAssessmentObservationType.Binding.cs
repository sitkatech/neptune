//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]
namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPAssessmentObservationType : IHavePrimaryKey
    {
        public int PrimaryKey => TreatmentBMPAssessmentObservationTypeID;
        public ObservationTypeSpecification ObservationTypeSpecification => ObservationTypeSpecification.AllLookupDictionary[ObservationTypeSpecificationID];
    }
}