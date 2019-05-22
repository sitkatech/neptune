//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnitAdjustment]
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
        public static TrashGeneratingUnitAdjustment GetTrashGeneratingUnitAdjustment(this IQueryable<TrashGeneratingUnitAdjustment> trashGeneratingUnitAdjustments, int trashGeneratingUnitAdjustmentID)
        {
            var trashGeneratingUnitAdjustment = trashGeneratingUnitAdjustments.SingleOrDefault(x => x.TrashGeneratingUnitAdjustmentID == trashGeneratingUnitAdjustmentID);
            Check.RequireNotNullThrowNotFound(trashGeneratingUnitAdjustment, "TrashGeneratingUnitAdjustment", trashGeneratingUnitAdjustmentID);
            return trashGeneratingUnitAdjustment;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTrashGeneratingUnitAdjustment(this IQueryable<TrashGeneratingUnitAdjustment> trashGeneratingUnitAdjustments, List<int> trashGeneratingUnitAdjustmentIDList)
        {
            if(trashGeneratingUnitAdjustmentIDList.Any())
            {
                trashGeneratingUnitAdjustments.Where(x => trashGeneratingUnitAdjustmentIDList.Contains(x.TrashGeneratingUnitAdjustmentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTrashGeneratingUnitAdjustment(this IQueryable<TrashGeneratingUnitAdjustment> trashGeneratingUnitAdjustments, ICollection<TrashGeneratingUnitAdjustment> trashGeneratingUnitAdjustmentsToDelete)
        {
            if(trashGeneratingUnitAdjustmentsToDelete.Any())
            {
                var trashGeneratingUnitAdjustmentIDList = trashGeneratingUnitAdjustmentsToDelete.Select(x => x.TrashGeneratingUnitAdjustmentID).ToList();
                trashGeneratingUnitAdjustments.Where(x => trashGeneratingUnitAdjustmentIDList.Contains(x.TrashGeneratingUnitAdjustmentID)).Delete();
            }
        }

        public static void DeleteTrashGeneratingUnitAdjustment(this IQueryable<TrashGeneratingUnitAdjustment> trashGeneratingUnitAdjustments, int trashGeneratingUnitAdjustmentID)
        {
            DeleteTrashGeneratingUnitAdjustment(trashGeneratingUnitAdjustments, new List<int> { trashGeneratingUnitAdjustmentID });
        }

        public static void DeleteTrashGeneratingUnitAdjustment(this IQueryable<TrashGeneratingUnitAdjustment> trashGeneratingUnitAdjustments, TrashGeneratingUnitAdjustment trashGeneratingUnitAdjustmentToDelete)
        {
            DeleteTrashGeneratingUnitAdjustment(trashGeneratingUnitAdjustments, new List<TrashGeneratingUnitAdjustment> { trashGeneratingUnitAdjustmentToDelete });
        }
    }
}