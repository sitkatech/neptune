//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectLoadGeneratingUnit


namespace Neptune.EFModels.Entities
{
    public class ProjectLoadGeneratingUnitPrimaryKey : EntityPrimaryKey<ProjectLoadGeneratingUnit>
    {
        public ProjectLoadGeneratingUnitPrimaryKey() : base(){}
        public ProjectLoadGeneratingUnitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ProjectLoadGeneratingUnitPrimaryKey(ProjectLoadGeneratingUnit projectLoadGeneratingUnit) : base(projectLoadGeneratingUnit){}

        public static implicit operator ProjectLoadGeneratingUnitPrimaryKey(int primaryKeyValue)
        {
            return new ProjectLoadGeneratingUnitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ProjectLoadGeneratingUnitPrimaryKey(ProjectLoadGeneratingUnit projectLoadGeneratingUnit)
        {
            return new ProjectLoadGeneratingUnitPrimaryKey(projectLoadGeneratingUnit);
        }
    }
}