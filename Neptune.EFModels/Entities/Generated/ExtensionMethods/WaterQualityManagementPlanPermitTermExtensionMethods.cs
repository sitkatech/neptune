//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPermitTerm]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanPermitTermExtensionMethods
    {

        public static WaterQualityManagementPlanPermitTermSimpleDto AsSimpleDto(this WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm)
        {
            var waterQualityManagementPlanPermitTermSimpleDto = new WaterQualityManagementPlanPermitTermSimpleDto()
            {
                WaterQualityManagementPlanPermitTermID = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermID,
                WaterQualityManagementPlanPermitTermName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermName,
                WaterQualityManagementPlanPermitTermDisplayName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermDisplayName
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanPermitTerm, waterQualityManagementPlanPermitTermSimpleDto);
            return waterQualityManagementPlanPermitTermSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm, WaterQualityManagementPlanPermitTermSimpleDto waterQualityManagementPlanPermitTermSimpleDto);
    }
}