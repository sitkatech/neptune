//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockStaging]
namespace Neptune.EFModels.Entities
{
    public partial class LandUseBlockStaging : IHavePrimaryKey
    {
        public int PrimaryKey => LandUseBlockStagingID;


        public static class FieldLengths
        {
            public const int PriorityLandUseType = 255;
            public const int LandUseDescription = 500;
            public const int LandUseForTGR = 80;
            public const int PermitType = 255;
        }
    }
}