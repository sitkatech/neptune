//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]
namespace Neptune.EFModels.Entities
{
    public partial class Project : IHavePrimaryKey
    {
        public int PrimaryKey => ProjectID;
        public ProjectStatus ProjectStatus => ProjectStatus.AllLookupDictionary[ProjectStatusID];

        public static class FieldLengths
        {
            public const int ProjectName = 200;
            public const int ProjectDescription = 500;
            public const int AdditionalContactInformation = 500;
            public const int OCTAWatersheds = 500;
        }
    }
}