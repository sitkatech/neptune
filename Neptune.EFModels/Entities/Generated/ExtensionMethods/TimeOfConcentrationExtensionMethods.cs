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
            var timeOfConcentrationSimpleDto = new TimeOfConcentrationSimpleDto()
            {
                TimeOfConcentrationID = timeOfConcentration.TimeOfConcentrationID,
                TimeOfConcentrationName = timeOfConcentration.TimeOfConcentrationName,
                TimeOfConcentrationDisplayName = timeOfConcentration.TimeOfConcentrationDisplayName
            };
            DoCustomSimpleDtoMappings(timeOfConcentration, timeOfConcentrationSimpleDto);
            return timeOfConcentrationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TimeOfConcentration timeOfConcentration, TimeOfConcentrationSimpleDto timeOfConcentrationSimpleDto);
    }
}