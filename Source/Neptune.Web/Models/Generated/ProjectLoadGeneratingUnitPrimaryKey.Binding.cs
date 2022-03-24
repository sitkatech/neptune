//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ProjectLoadGeneratingUnit
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ProjectLoadGeneratingUnitPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ProjectLoadGeneratingUnit>
    {
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