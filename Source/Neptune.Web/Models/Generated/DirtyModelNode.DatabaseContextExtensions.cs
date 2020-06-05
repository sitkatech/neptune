//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DirtyModelNode]
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
        public static DirtyModelNode GetDirtyModelNode(this IQueryable<DirtyModelNode> dirtyModelNodes, int dirtyModelNodeID)
        {
            var dirtyModelNode = dirtyModelNodes.SingleOrDefault(x => x.DirtyModelNodeID == dirtyModelNodeID);
            Check.RequireNotNullThrowNotFound(dirtyModelNode, "DirtyModelNode", dirtyModelNodeID);
            return dirtyModelNode;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteDirtyModelNode(this IQueryable<DirtyModelNode> dirtyModelNodes, List<int> dirtyModelNodeIDList)
        {
            if(dirtyModelNodeIDList.Any())
            {
                dirtyModelNodes.Where(x => dirtyModelNodeIDList.Contains(x.DirtyModelNodeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteDirtyModelNode(this IQueryable<DirtyModelNode> dirtyModelNodes, ICollection<DirtyModelNode> dirtyModelNodesToDelete)
        {
            if(dirtyModelNodesToDelete.Any())
            {
                var dirtyModelNodeIDList = dirtyModelNodesToDelete.Select(x => x.DirtyModelNodeID).ToList();
                dirtyModelNodes.Where(x => dirtyModelNodeIDList.Contains(x.DirtyModelNodeID)).Delete();
            }
        }

        public static void DeleteDirtyModelNode(this IQueryable<DirtyModelNode> dirtyModelNodes, int dirtyModelNodeID)
        {
            DeleteDirtyModelNode(dirtyModelNodes, new List<int> { dirtyModelNodeID });
        }

        public static void DeleteDirtyModelNode(this IQueryable<DirtyModelNode> dirtyModelNodes, DirtyModelNode dirtyModelNodeToDelete)
        {
            DeleteDirtyModelNode(dirtyModelNodes, new List<DirtyModelNode> { dirtyModelNodeToDelete });
        }
    }
}