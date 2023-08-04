//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]
namespace Neptune.EFModels.Entities
{
    public partial class Project
    {
        public ProjectStatus ProjectStatus => ProjectStatus.AllLookupDictionary[ProjectStatusID];
    }
}