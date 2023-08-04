//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MonthsOfOperation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MonthsOfOperationExtensionMethods
    {
        public static MonthsOfOperationDto AsDto(this MonthsOfOperation monthsOfOperation)
        {
            var monthsOfOperationDto = new MonthsOfOperationDto()
            {
                MonthsOfOperationID = monthsOfOperation.MonthsOfOperationID,
                MonthsOfOperationName = monthsOfOperation.MonthsOfOperationName,
                MonthsOfOperationDisplayName = monthsOfOperation.MonthsOfOperationDisplayName,
                MonthsOfOperationNereidAlias = monthsOfOperation.MonthsOfOperationNereidAlias
            };
            DoCustomMappings(monthsOfOperation, monthsOfOperationDto);
            return monthsOfOperationDto;
        }

        static partial void DoCustomMappings(MonthsOfOperation monthsOfOperation, MonthsOfOperationDto monthsOfOperationDto);

        public static MonthsOfOperationSimpleDto AsSimpleDto(this MonthsOfOperation monthsOfOperation)
        {
            var monthsOfOperationSimpleDto = new MonthsOfOperationSimpleDto()
            {
                MonthsOfOperationID = monthsOfOperation.MonthsOfOperationID,
                MonthsOfOperationName = monthsOfOperation.MonthsOfOperationName,
                MonthsOfOperationDisplayName = monthsOfOperation.MonthsOfOperationDisplayName,
                MonthsOfOperationNereidAlias = monthsOfOperation.MonthsOfOperationNereidAlias
            };
            DoCustomSimpleDtoMappings(monthsOfOperation, monthsOfOperationSimpleDto);
            return monthsOfOperationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(MonthsOfOperation monthsOfOperation, MonthsOfOperationSimpleDto monthsOfOperationSimpleDto);
    }
}