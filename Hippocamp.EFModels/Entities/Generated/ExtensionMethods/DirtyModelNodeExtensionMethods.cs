//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DirtyModelNode]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class DirtyModelNodeExtensionMethods
    {
        public static DirtyModelNodeDto AsDto(this DirtyModelNode dirtyModelNode)
        {
            var dirtyModelNodeDto = new DirtyModelNodeDto()
            {
                DirtyModelNodeID = dirtyModelNode.DirtyModelNodeID,
                TreatmentBMPID = dirtyModelNode.TreatmentBMPID,
                WaterQualityManagementPlanID = dirtyModelNode.WaterQualityManagementPlanID,
                RegionalSubbasinID = dirtyModelNode.RegionalSubbasinID,
                DelineationID = dirtyModelNode.DelineationID,
                CreateDate = dirtyModelNode.CreateDate
            };
            DoCustomMappings(dirtyModelNode, dirtyModelNodeDto);
            return dirtyModelNodeDto;
        }

        static partial void DoCustomMappings(DirtyModelNode dirtyModelNode, DirtyModelNodeDto dirtyModelNodeDto);

        public static DirtyModelNodeSimpleDto AsSimpleDto(this DirtyModelNode dirtyModelNode)
        {
            var dirtyModelNodeSimpleDto = new DirtyModelNodeSimpleDto()
            {
                DirtyModelNodeID = dirtyModelNode.DirtyModelNodeID,
                TreatmentBMPID = dirtyModelNode.TreatmentBMPID,
                WaterQualityManagementPlanID = dirtyModelNode.WaterQualityManagementPlanID,
                RegionalSubbasinID = dirtyModelNode.RegionalSubbasinID,
                DelineationID = dirtyModelNode.DelineationID,
                CreateDate = dirtyModelNode.CreateDate
            };
            DoCustomSimpleDtoMappings(dirtyModelNode, dirtyModelNodeSimpleDto);
            return dirtyModelNodeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(DirtyModelNode dirtyModelNode, DirtyModelNodeSimpleDto dirtyModelNodeSimpleDto);
    }
}