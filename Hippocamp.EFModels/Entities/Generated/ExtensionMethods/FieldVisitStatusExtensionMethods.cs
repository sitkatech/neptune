//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitStatus]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class FieldVisitStatusExtensionMethods
    {
        public static FieldVisitStatusDto AsDto(this FieldVisitStatus fieldVisitStatus)
        {
            var fieldVisitStatusDto = new FieldVisitStatusDto()
            {
                FieldVisitStatusID = fieldVisitStatus.FieldVisitStatusID,
                FieldVisitStatusName = fieldVisitStatus.FieldVisitStatusName,
                FieldVisitStatusDisplayName = fieldVisitStatus.FieldVisitStatusDisplayName
            };
            DoCustomMappings(fieldVisitStatus, fieldVisitStatusDto);
            return fieldVisitStatusDto;
        }

        static partial void DoCustomMappings(FieldVisitStatus fieldVisitStatus, FieldVisitStatusDto fieldVisitStatusDto);

        public static FieldVisitStatusSimpleDto AsSimpleDto(this FieldVisitStatus fieldVisitStatus)
        {
            var fieldVisitStatusSimpleDto = new FieldVisitStatusSimpleDto()
            {
                FieldVisitStatusID = fieldVisitStatus.FieldVisitStatusID,
                FieldVisitStatusName = fieldVisitStatus.FieldVisitStatusName,
                FieldVisitStatusDisplayName = fieldVisitStatus.FieldVisitStatusDisplayName
            };
            DoCustomSimpleDtoMappings(fieldVisitStatus, fieldVisitStatusSimpleDto);
            return fieldVisitStatusSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(FieldVisitStatus fieldVisitStatus, FieldVisitStatusSimpleDto fieldVisitStatusSimpleDto);
    }
}