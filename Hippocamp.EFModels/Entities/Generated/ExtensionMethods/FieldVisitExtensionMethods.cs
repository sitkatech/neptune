//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class FieldVisitExtensionMethods
    {
        public static FieldVisitDto AsDto(this FieldVisit fieldVisit)
        {
            var fieldVisitDto = new FieldVisitDto()
            {
                FieldVisitID = fieldVisit.FieldVisitID,
                TreatmentBMP = fieldVisit.TreatmentBMP.AsDto(),
                FieldVisitStatus = fieldVisit.FieldVisitStatus.AsDto(),
                PerformedByPerson = fieldVisit.PerformedByPerson.AsDto(),
                VisitDate = fieldVisit.VisitDate,
                InventoryUpdated = fieldVisit.InventoryUpdated,
                FieldVisitType = fieldVisit.FieldVisitType.AsDto(),
                IsFieldVisitVerified = fieldVisit.IsFieldVisitVerified
            };
            DoCustomMappings(fieldVisit, fieldVisitDto);
            return fieldVisitDto;
        }

        static partial void DoCustomMappings(FieldVisit fieldVisit, FieldVisitDto fieldVisitDto);

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