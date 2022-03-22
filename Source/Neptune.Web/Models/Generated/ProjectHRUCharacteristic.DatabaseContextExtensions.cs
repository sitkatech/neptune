//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectHRUCharacteristic]
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
        public static ProjectHRUCharacteristic GetProjectHRUCharacteristic(this IQueryable<ProjectHRUCharacteristic> projectHRUCharacteristics, int projectHRUCharacteristicID)
        {
            var projectHRUCharacteristic = projectHRUCharacteristics.SingleOrDefault(x => x.ProjectHRUCharacteristicID == projectHRUCharacteristicID);
            Check.RequireNotNullThrowNotFound(projectHRUCharacteristic, "ProjectHRUCharacteristic", projectHRUCharacteristicID);
            return projectHRUCharacteristic;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteProjectHRUCharacteristic(this IQueryable<ProjectHRUCharacteristic> projectHRUCharacteristics, List<int> projectHRUCharacteristicIDList)
        {
            if(projectHRUCharacteristicIDList.Any())
            {
                projectHRUCharacteristics.Where(x => projectHRUCharacteristicIDList.Contains(x.ProjectHRUCharacteristicID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteProjectHRUCharacteristic(this IQueryable<ProjectHRUCharacteristic> projectHRUCharacteristics, ICollection<ProjectHRUCharacteristic> projectHRUCharacteristicsToDelete)
        {
            if(projectHRUCharacteristicsToDelete.Any())
            {
                var projectHRUCharacteristicIDList = projectHRUCharacteristicsToDelete.Select(x => x.ProjectHRUCharacteristicID).ToList();
                projectHRUCharacteristics.Where(x => projectHRUCharacteristicIDList.Contains(x.ProjectHRUCharacteristicID)).Delete();
            }
        }

        public static void DeleteProjectHRUCharacteristic(this IQueryable<ProjectHRUCharacteristic> projectHRUCharacteristics, int projectHRUCharacteristicID)
        {
            DeleteProjectHRUCharacteristic(projectHRUCharacteristics, new List<int> { projectHRUCharacteristicID });
        }

        public static void DeleteProjectHRUCharacteristic(this IQueryable<ProjectHRUCharacteristic> projectHRUCharacteristics, ProjectHRUCharacteristic projectHRUCharacteristicToDelete)
        {
            DeleteProjectHRUCharacteristic(projectHRUCharacteristics, new List<ProjectHRUCharacteristic> { projectHRUCharacteristicToDelete });
        }
    }
}