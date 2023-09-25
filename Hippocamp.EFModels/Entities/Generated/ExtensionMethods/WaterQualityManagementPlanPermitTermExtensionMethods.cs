//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPermitTerm]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanPermitTermExtensionMethods
    {
        public static WaterQualityManagementPlanPermitTermDto AsDto(this WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm)
        {
            var waterQualityManagementPlanPermitTermDto = new WaterQualityManagementPlanPermitTermDto()
            {
                WaterQualityManagementPlanPermitTermID = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermID,
                WaterQualityManagementPlanPermitTermName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermName,
                WaterQualityManagementPlanPermitTermDisplayName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermDisplayName,
                SortOrder = waterQualityManagementPlanPermitTerm.SortOrder
            };
            DoCustomMappings(waterQualityManagementPlanPermitTerm, waterQualityManagementPlanPermitTermDto);
            return waterQualityManagementPlanPermitTermDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm, WaterQualityManagementPlanPermitTermDto waterQualityManagementPlanPermitTermDto);

        public static WaterQualityManagementPlanPermitTermSimpleDto AsSimpleDto(this WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm)
        {
            var waterQualityManagementPlanPermitTermSimpleDto = new WaterQualityManagementPlanPermitTermSimpleDto()
            {
                WaterQualityManagementPlanPermitTermID = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermID,
                WaterQualityManagementPlanPermitTermName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermName,
                WaterQualityManagementPlanPermitTermDisplayName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermDisplayName,
                SortOrder = waterQualityManagementPlanPermitTerm.SortOrder
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanPermitTerm, waterQualityManagementPlanPermitTermSimpleDto);
            return waterQualityManagementPlanPermitTermSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm, WaterQualityManagementPlanPermitTermSimpleDto waterQualityManagementPlanPermitTermSimpleDto);
    }
}