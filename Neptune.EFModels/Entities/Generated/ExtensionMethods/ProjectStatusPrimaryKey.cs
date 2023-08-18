//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectStatus


namespace Neptune.EFModels.Entities
{
    public class ProjectStatusPrimaryKey : EntityPrimaryKey<ProjectStatus>
    {
        public ProjectStatusPrimaryKey() : base(){}
        public ProjectStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectStatusPrimaryKey(ProjectStatus projectStatus) : base(projectStatus){}

        public static implicit operator ProjectStatusPrimaryKey(int primaryKeyValue)
        {
            return new ProjectStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectStatusPrimaryKey(ProjectStatus projectStatus)
        {
            return new ProjectStatusPrimaryKey(projectStatus);
        }
    }
}