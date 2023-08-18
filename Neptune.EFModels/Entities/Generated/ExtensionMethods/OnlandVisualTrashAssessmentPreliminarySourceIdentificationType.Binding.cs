//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]
namespace Neptune.EFModels.Entities
{
    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationType : IHavePrimaryKey
    {
        public int PrimaryKey => OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID;
        public PreliminarySourceIdentificationType PreliminarySourceIdentificationType => PreliminarySourceIdentificationType.AllLookupDictionary[PreliminarySourceIdentificationTypeID];
    }
}