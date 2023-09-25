//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TimeOfConcentration]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TimeOfConcentrationExtensionMethods
    {
        public static TimeOfConcentrationDto AsDto(this TimeOfConcentration timeOfConcentration)
        {
            var timeOfConcentrationDto = new TimeOfConcentrationDto()
            {
                TimeOfConcentrationID = timeOfConcentration.TimeOfConcentrationID,
                TimeOfConcentrationName = timeOfConcentration.TimeOfConcentrationName,
                TimeOfConcentrationDisplayName = timeOfConcentration.TimeOfConcentrationDisplayName
            };
            DoCustomMappings(timeOfConcentration, timeOfConcentrationDto);
            return timeOfConcentrationDto;
        }

        static partial void DoCustomMappings(TimeOfConcentration timeOfConcentration, TimeOfConcentrationDto timeOfConcentrationDto);

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