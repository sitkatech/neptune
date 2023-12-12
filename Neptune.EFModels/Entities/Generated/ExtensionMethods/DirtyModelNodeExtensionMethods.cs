//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DirtyModelNode]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DirtyModelNodeExtensionMethods
    {
        public static DirtyModelNodeSimpleDto AsSimpleDto(this DirtyModelNode dirtyModelNode)
        {
            var dto = new DirtyModelNodeSimpleDto()
            {
                DirtyModelNodeID = dirtyModelNode.DirtyModelNodeID,
                TreatmentBMPID = dirtyModelNode.TreatmentBMPID,
                WaterQualityManagementPlanID = dirtyModelNode.WaterQualityManagementPlanID,
                RegionalSubbasinID = dirtyModelNode.RegionalSubbasinID,
                DelineationID = dirtyModelNode.DelineationID,
                CreateDate = dirtyModelNode.CreateDate
            };
            return dto;
        }
    }
}