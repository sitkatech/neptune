//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TimeOfConcentration]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TimeOfConcentrationExtensionMethods
    {
        public static TimeOfConcentrationSimpleDto AsSimpleDto(this TimeOfConcentration timeOfConcentration)
        {
            var dto = new TimeOfConcentrationSimpleDto()
            {
                TimeOfConcentrationID = timeOfConcentration.TimeOfConcentrationID,
                TimeOfConcentrationName = timeOfConcentration.TimeOfConcentrationName,
                TimeOfConcentrationDisplayName = timeOfConcentration.TimeOfConcentrationDisplayName
            };
            return dto;
        }
    }
}