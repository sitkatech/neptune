//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnit]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class LoadGeneratingUnitExtensionMethods
    {
        public static LoadGeneratingUnitDto AsDto(this LoadGeneratingUnit loadGeneratingUnit)
        {
            var loadGeneratingUnitDto = new LoadGeneratingUnitDto()
            {
                LoadGeneratingUnitID = loadGeneratingUnit.LoadGeneratingUnitID,
                ModelBasin = loadGeneratingUnit.ModelBasin?.AsDto(),
                RegionalSubbasin = loadGeneratingUnit.RegionalSubbasin?.AsDto(),
                Delineation = loadGeneratingUnit.Delineation?.AsDto(),
                WaterQualityManagementPlan = loadGeneratingUnit.WaterQualityManagementPlan?.AsDto(),
                IsEmptyResponseFromHRUService = loadGeneratingUnit.IsEmptyResponseFromHRUService
            };
            DoCustomMappings(loadGeneratingUnit, loadGeneratingUnitDto);
            return loadGeneratingUnitDto;
        }

        static partial void DoCustomMappings(LoadGeneratingUnit loadGeneratingUnit, LoadGeneratingUnitDto loadGeneratingUnitDto);

        public static LoadGeneratingUnitSimpleDto AsSimpleDto(this LoadGeneratingUnit loadGeneratingUnit)
        {
            var loadGeneratingUnitSimpleDto = new LoadGeneratingUnitSimpleDto()
            {
                LoadGeneratingUnitID = loadGeneratingUnit.LoadGeneratingUnitID,
                ModelBasinID = loadGeneratingUnit.ModelBasinID,
                RegionalSubbasinID = loadGeneratingUnit.RegionalSubbasinID,
                DelineationID = loadGeneratingUnit.DelineationID,
                WaterQualityManagementPlanID = loadGeneratingUnit.WaterQualityManagementPlanID,
                IsEmptyResponseFromHRUService = loadGeneratingUnit.IsEmptyResponseFromHRUService
            };
            DoCustomSimpleDtoMappings(loadGeneratingUnit, loadGeneratingUnitSimpleDto);
            return loadGeneratingUnitSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(LoadGeneratingUnit loadGeneratingUnit, LoadGeneratingUnitSimpleDto loadGeneratingUnitSimpleDto);
    }
}