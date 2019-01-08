//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]
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
        public static FieldVisit GetFieldVisit(this IQueryable<FieldVisit> fieldVisits, int fieldVisitID)
        {
            var fieldVisit = fieldVisits.SingleOrDefault(x => x.FieldVisitID == fieldVisitID);
            Check.RequireNotNullThrowNotFound(fieldVisit, "FieldVisit", fieldVisitID);
            return fieldVisit;
        }

        public static void DeleteFieldVisit(this IQueryable<FieldVisit> fieldVisits, List<int> fieldVisitIDList)
        {
            if(fieldVisitIDList.Any())
            {
                fieldVisits.Where(x => fieldVisitIDList.Contains(x.FieldVisitID)).Delete();
            }
        }

        public static void DeleteFieldVisit(this IQueryable<FieldVisit> fieldVisits, ICollection<FieldVisit> fieldVisitsToDelete)
        {
            if(fieldVisitsToDelete.Any())
            {
                var fieldVisitIDList = fieldVisitsToDelete.Select(x => x.FieldVisitID).ToList();
                fieldVisits.Where(x => fieldVisitIDList.Contains(x.FieldVisitID)).Delete();
            }
        }

        public static void DeleteFieldVisit(this IQueryable<FieldVisit> fieldVisits, int fieldVisitID)
        {
            DeleteFieldVisit(fieldVisits, new List<int> { fieldVisitID });
        }

        public static void DeleteFieldVisit(this IQueryable<FieldVisit> fieldVisits, FieldVisit fieldVisitToDelete)
        {
            DeleteFieldVisit(fieldVisits, new List<FieldVisit> { fieldVisitToDelete });
        }
    }
}