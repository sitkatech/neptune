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
            var dto = new WaterQualityManagementPlanPermitTermSimpleDto()
            {
                WaterQualityManagementPlanPermitTermID = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermID,
                WaterQualityManagementPlanPermitTermName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermName,
                WaterQualityManagementPlanPermitTermDisplayName = waterQualityManagementPlanPermitTerm.WaterQualityManagementPlanPermitTermDisplayName
            };
            return dto;
        }
    }
}