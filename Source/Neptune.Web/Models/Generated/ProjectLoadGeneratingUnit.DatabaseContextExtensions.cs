//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLoadGeneratingUnit]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ProjectLoadGeneratingUnit GetProjectLoadGeneratingUnit(this IQueryable<ProjectLoadGeneratingUnit> projectLoadGeneratingUnits, int projectLoadGeneratingUnitID)
        {
            var projectLoadGeneratingUnit = projectLoadGeneratingUnits.SingleOrDefault(x => x.ProjectLoadGeneratingUnitID == projectLoadGeneratingUnitID);
            Check.RequireNotNullThrowNotFound(projectLoadGeneratingUnit, "ProjectLoadGeneratingUnit", projectLoadGeneratingUnitID);
            return projectLoadGeneratingUnit;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteProjectLoadGeneratingUnit(this IQueryable<ProjectLoadGeneratingUnit> projectLoadGeneratingUnits, List<int> projectLoadGeneratingUnitIDList)
        {
            if(projectLoadGeneratingUnitIDList.Any())
            {
                projectLoadGeneratingUnits.Where(x => projectLoadGeneratingUnitIDList.Contains(x.ProjectLoadGeneratingUnitID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteProjectLoadGeneratingUnit(this IQueryable<ProjectLoadGeneratingUnit> projectLoadGeneratingUnits, ICollection<ProjectLoadGeneratingUnit> projectLoadGeneratingUnitsToDelete)
        {
            if(projectLoadGeneratingUnitsToDelete.Any())
            {
                var projectLoadGeneratingUnitIDList = projectLoadGeneratingUnitsToDelete.Select(x => x.ProjectLoadGeneratingUnitID).ToList();
                projectLoadGeneratingUnits.Where(x => projectLoadGeneratingUnitIDList.Contains(x.ProjectLoadGeneratingUnitID)).Delete();
            }
        }

        public static void DeleteProjectLoadGeneratingUnit(this IQueryable<ProjectLoadGeneratingUnit> projectLoadGeneratingUnits, int projectLoadGeneratingUnitID)
        {
            DeleteProjectLoadGeneratingUnit(projectLoadGeneratingUnits, new List<int> { projectLoadGeneratingUnitID });
        }

        public static void DeleteProjectLoadGeneratingUnit(this IQueryable<ProjectLoadGeneratingUnit> projectLoadGeneratingUnits, ProjectLoadGeneratingUnit projectLoadGeneratingUnitToDelete)
        {
            DeleteProjectLoadGeneratingUnit(projectLoadGeneratingUnits, new List<ProjectLoadGeneratingUnit> { projectLoadGeneratingUnitToDelete });
        }
    }
}