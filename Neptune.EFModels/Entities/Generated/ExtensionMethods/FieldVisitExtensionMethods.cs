//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FieldVisitExtensionMethods
    {

        public static FieldVisitSimpleDto AsSimpleDto(this FieldVisit fieldVisit)
        {
            var fieldVisitSimpleDto = new FieldVisitSimpleDto()
            {
                FieldVisitID = fieldVisit.FieldVisitID,
                TreatmentBMPID = fieldVisit.TreatmentBMPID,
                FieldVisitStatusID = fieldVisit.FieldVisitStatusID,
                PerformedByPersonID = fieldVisit.PerformedByPersonID,
                VisitDate = fieldVisit.VisitDate,
                InventoryUpdated = fieldVisit.InventoryUpdated,
                FieldVisitTypeID = fieldVisit.FieldVisitTypeID,
                IsFieldVisitVerified = fieldVisit.IsFieldVisitVerified
            };
            DoCustomSimpleDtoMappings(fieldVisit, fieldVisitSimpleDto);
            return fieldVisitSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FieldVisit fieldVisit, FieldVisitSimpleDto fieldVisitSimpleDto);
    }
}