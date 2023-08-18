//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]
namespace Neptune.EFModels.Entities
{
    public partial class LandUseBlock : IHavePrimaryKey
    {
        public int PrimaryKey => LandUseBlockID;
        public PriorityLandUseType PriorityLandUseType => PriorityLandUseTypeID.HasValue ? PriorityLandUseType.AllLookupDictionary[PriorityLandUseTypeID.Value] : null;
        public PermitType PermitType => PermitType.AllLookupDictionary[PermitTypeID];
    }
}