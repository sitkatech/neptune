//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitStatus]
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Extensions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static FieldVisitStatus GetFieldVisitStatus(this IQueryable<FieldVisitStatus> fieldVisitStatuses, int fieldVisitStatusID)
        {
            var fieldVisitStatus = fieldVisitStatuses.SingleOrDefault(x => x.FieldVisitStatusID == fieldVisitStatusID);
            Check.RequireNotNullThrowNotFound(fieldVisitStatus, "FieldVisitStatus", fieldVisitStatusID);
            return fieldVisitStatus;
        }

        public static void DeleteFieldVisitStatus(this IQueryable<FieldVisitStatus> fieldVisitStatuses, List<int> fieldVisitStatusIDList)
        {
            if(fieldVisitStatusIDList.Any())
            {
                fieldVisitStatuses.Where(x => fieldVisitStatusIDList.Contains(x.FieldVisitStatusID)).Delete();
            }
        }

        public static void DeleteFieldVisitStatus(this IQueryable<FieldVisitStatus> fieldVisitStatuses, ICollection<FieldVisitStatus> fieldVisitStatusesToDelete)
        {
            if(fieldVisitStatusesToDelete.Any())
            {
                var fieldVisitStatusIDList = fieldVisitStatusesToDelete.Select(x => x.FieldVisitStatusID).ToList();
                fieldVisitStatuses.Where(x => fieldVisitStatusIDList.Contains(x.FieldVisitStatusID)).Delete();
            }
        }

        public static void DeleteFieldVisitStatus(this IQueryable<FieldVisitStatus> fieldVisitStatuses, int fieldVisitStatusID)
        {
            DeleteFieldVisitStatus(fieldVisitStatuses, new List<int> { fieldVisitStatusID });
        }

        public static void DeleteFieldVisitStatus(this IQueryable<FieldVisitStatus> fieldVisitStatuses, FieldVisitStatus fieldVisitStatusToDelete)
        {
            DeleteFieldVisitStatus(fieldVisitStatuses, new List<FieldVisitStatus> { fieldVisitStatusToDelete });
        }
    }
}