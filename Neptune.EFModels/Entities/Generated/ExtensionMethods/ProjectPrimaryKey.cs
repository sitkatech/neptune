//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Project


namespace Neptune.EFModels.Entities
{
    public class ProjectPrimaryKey : EntityPrimaryKey<Project>
    {
        public ProjectPrimaryKey() : base(){}
        public ProjectPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectPrimaryKey(Project project) : base(project){}

        public static implicit operator ProjectPrimaryKey(int primaryKeyValue)
        {
            return new ProjectPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectPrimaryKey(Project project)
        {
            return new ProjectPrimaryKey(project);
        }
    }
}