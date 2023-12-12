//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MonthsOfOperation]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class MonthsOfOperationExtensionMethods
    {
        public static MonthsOfOperationSimpleDto AsSimpleDto(this MonthsOfOperation monthsOfOperation)
        {
            var dto = new MonthsOfOperationSimpleDto()
            {
                MonthsOfOperationID = monthsOfOperation.MonthsOfOperationID,
                MonthsOfOperationName = monthsOfOperation.MonthsOfOperationName,
                MonthsOfOperationDisplayName = monthsOfOperation.MonthsOfOperationDisplayName,
                MonthsOfOperationNereidAlias = monthsOfOperation.MonthsOfOperationNereidAlias
            };
            return dto;
        }
    }
}