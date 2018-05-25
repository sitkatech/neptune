//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteFieldVisit(this List<int> fieldVisitIDList)
        {
            if(fieldVisitIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFieldVisits.RemoveRange(HttpRequestStorage.DatabaseEntities.FieldVisits.Where(x => fieldVisitIDList.Contains(x.FieldVisitID)));
            }
        }

        public static void DeleteFieldVisit(this ICollection<FieldVisit> fieldVisitsToDelete)
        {
            if(fieldVisitsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFieldVisits.RemoveRange(fieldVisitsToDelete);
            }
        }

        public static void DeleteFieldVisit(this int fieldVisitID)
        {
            DeleteFieldVisit(new List<int> { fieldVisitID });
        }

        public static void DeleteFieldVisit(this FieldVisit fieldVisitToDelete)
        {
            DeleteFieldVisit(new List<FieldVisit> { fieldVisitToDelete });
        }
    }
}