//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class FieldVisitStatusExtensionMethods
    {
        public static FieldVisitStatusSimpleDto AsSimpleDto(this FieldVisitStatus fieldVisitStatus)
        {
            var dto = new FieldVisitStatusSimpleDto()
            {
                FieldVisitStatusID = fieldVisitStatus.FieldVisitStatusID,
                FieldVisitStatusName = fieldVisitStatus.FieldVisitStatusName,
                FieldVisitStatusDisplayName = fieldVisitStatus.FieldVisitStatusDisplayName
            };
            return dto;
        }
    }
}