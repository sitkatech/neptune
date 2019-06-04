//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[BackboneSegment]
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
        public static BackboneSegment GetBackboneSegment(this IQueryable<BackboneSegment> backboneSegments, int backboneSegmentID)
        {
            var backboneSegment = backboneSegments.SingleOrDefault(x => x.BackboneSegmentID == backboneSegmentID);
            Check.RequireNotNullThrowNotFound(backboneSegment, "BackboneSegment", backboneSegmentID);
            return backboneSegment;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteBackboneSegment(this IQueryable<BackboneSegment> backboneSegments, List<int> backboneSegmentIDList)
        {
            if(backboneSegmentIDList.Any())
            {
                backboneSegments.Where(x => backboneSegmentIDList.Contains(x.BackboneSegmentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteBackboneSegment(this IQueryable<BackboneSegment> backboneSegments, ICollection<BackboneSegment> backboneSegmentsToDelete)
        {
            if(backboneSegmentsToDelete.Any())
            {
                var backboneSegmentIDList = backboneSegmentsToDelete.Select(x => x.BackboneSegmentID).ToList();
                backboneSegments.Where(x => backboneSegmentIDList.Contains(x.BackboneSegmentID)).Delete();
            }
        }

        public static void DeleteBackboneSegment(this IQueryable<BackboneSegment> backboneSegments, int backboneSegmentID)
        {
            DeleteBackboneSegment(backboneSegments, new List<int> { backboneSegmentID });
        }

        public static void DeleteBackboneSegment(this IQueryable<BackboneSegment> backboneSegments, BackboneSegment backboneSegmentToDelete)
        {
            DeleteBackboneSegment(backboneSegments, new List<BackboneSegment> { backboneSegmentToDelete });
        }
    }
}