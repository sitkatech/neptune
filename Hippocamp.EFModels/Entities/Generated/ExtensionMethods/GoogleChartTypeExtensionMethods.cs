//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GoogleChartType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class GoogleChartTypeExtensionMethods
    {
        public static GoogleChartTypeDto AsDto(this GoogleChartType googleChartType)
        {
            var googleChartTypeDto = new GoogleChartTypeDto()
            {
                GoogleChartTypeID = googleChartType.GoogleChartTypeID,
                GoogleChartTypeName = googleChartType.GoogleChartTypeName,
                GoogleChartTypeDisplayName = googleChartType.GoogleChartTypeDisplayName,
                SeriesDataDisplayType = googleChartType.SeriesDataDisplayType
            };
            DoCustomMappings(googleChartType, googleChartTypeDto);
            return googleChartTypeDto;
        }

        static partial void DoCustomMappings(GoogleChartType googleChartType, GoogleChartTypeDto googleChartTypeDto);

        public static GoogleChartTypeSimpleDto AsSimpleDto(this GoogleChartType googleChartType)
        {
            var googleChartTypeSimpleDto = new GoogleChartTypeSimpleDto()
            {
                GoogleChartTypeID = googleChartType.GoogleChartTypeID,
                GoogleChartTypeName = googleChartType.GoogleChartTypeName,
                GoogleChartTypeDisplayName = googleChartType.GoogleChartTypeDisplayName,
                SeriesDataDisplayType = googleChartType.SeriesDataDisplayType
            };
            DoCustomSimpleDtoMappings(googleChartType, googleChartTypeSimpleDto);
            return googleChartTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(GoogleChartType googleChartType, GoogleChartTypeSimpleDto googleChartTypeSimpleDto);
    }
}